using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using System.Data.Objects;
using System.Transactions;

//using BDClassLib;
using EducServLib;
using BaseFormsLib;
using WordOut;
using PriemLib;
namespace Priem
{
    public partial class ProtocolCard : BaseFormEx
    {
        protected ProtocolList _Owner;
        protected int _facultyId;
        protected int _studyFormId;
        protected int _studyBasisId;
        protected int? _licenseProgramId;
        protected Guid? _id;
        protected ProtocolTypes _type;
                
        protected bool? _isSecond;
        protected bool? _isReduced;
        protected bool? _isParallel;
        protected bool? _isCel;

        protected bool? _isListener;

        protected DataRefreshHandler _drh;
        protected bool isNew;
        protected bool isConfirmed; //проставили галочку, что уверены

        protected string sQuery = "";
        protected string sOrderby = " ORDER BY ФИО ";

        /// <summary>
        /// Номер протокола
        /// </summary>
        protected string ProtocolName
        {
            get
            {
                return tbNum.Text.Trim();
            }
            set
            {
                tbNum.Text = value;
            }
        }

        /// <summary>
        /// конструктор по умолчанию для дизайнера
        /// </summary>
        public ProtocolCard() : base() 
        { 
            InitializeComponent(); 
        }

        //конструктор
        public ProtocolCard(ProtocolList owner, int facultyId, int studyBasisId, int studyFormId)
            : this(owner, facultyId, studyBasisId, studyFormId, null, null)
        {
        }

        //конструктор 
        public ProtocolCard(ProtocolList owner, int facultyId, int studyBasisId, int studyFormId, Guid? id) :
            this(owner, facultyId, studyBasisId, studyFormId, null, id)
        {
        }

        //конструктор 
        public ProtocolCard(ProtocolList owner, int facultyId, int studyBasisId, int studyFormId, int? licenseProgramId, Guid? id) :
            this(owner, facultyId, studyBasisId, studyFormId, licenseProgramId, null, null, null, null, id)
        {
        }

        public ProtocolCard(ProtocolList owner, int facultyId, int studyBasisId, int studyFormId, int? licenseProgramId, bool? isSecond, bool? isReduced, bool? isParallel, bool? isListener, Guid? id)
            : this(owner, facultyId, studyBasisId, studyFormId, licenseProgramId, isSecond, isReduced, isParallel, isListener, null, id)
        { }

        //конструктор 
        public ProtocolCard(ProtocolList owner, int facultyId, int studyBasisId, int studyFormId, int? licenseProgramId, bool? isSecond, bool? isReduced, bool? isParallel, bool? isListener, bool? isCel, Guid? id)
        {
            InitializeComponent();

            _facultyId = facultyId;
            _licenseProgramId = licenseProgramId;
            _studyBasisId = studyBasisId;
            _studyFormId = studyFormId;           
            _id = id;
            _Owner = owner;

            _isSecond = isSecond;
            _isReduced = isReduced;
            _isParallel = isParallel;
            _isListener = isListener;
            _isCel = isCel;

            //флаг, если новый
            isNew = id.HasValue ? false : true;
            chbInostr.Visible = false;

            InitControls();
        }       

        //дополнительная инициализация
        protected virtual void InitControls()
        {
            this.MdiParent = MainClass.mainform;
            this.CenterToParent();
            InitFocusHandlers();

            _drh = new DataRefreshHandler(UpdateRightGrid);           
            MainClass.AddHandler(_drh);

            btnDelete.Visible = false;
            this.Text = "Протокол";

            Dgv = this.dgvRight;

            //заполнение контролов для нового или существующего протокола
            if (isNew)
            {
                //строим номер протокола
                string sProtNum = MainClass.studyLevelGroupId.ToString();

                sProtNum += (_facultyId > 9 ? "" : "0") + _facultyId.ToString();//номер факультета
                sProtNum += DateTime.Now.Year.ToString().Substring(2);//год

                try
                {
                    string sNum = MainClass.Bdc.GetStringValue(string.Format("SELECT ProtocolNum FROM ed.ProtocolNumbers WHERE FacultyId = {0} AND StudyLevelGroupId = {1}", 
                        _facultyId, MainClass.studyLevelGroupId));
                    sNum = "0000" + sNum;
                    sProtNum += sNum.Substring(sNum.Length - 4);
                }
                catch (Exception ex)
                {
                    WinFormsServ.Error("Ошибка при присвоении номера протокола: " + ex.Message);
                }

                ProtocolName = sProtNum;
            }
            else
            {
                try
                {
                    using (PriemEntities context = new PriemEntities())
                    {
                        var protocolInfo = context.qProtocol.Where(x => x.Id == _id.Value).Select(x => new { x.Number, x.Date }).FirstOrDefault();
                        ProtocolName = protocolInfo.Number;
                        dtpDate.Value = protocolInfo.Date.Value;
                    }                    
                }
                catch (Exception ex)
                {
                    WinFormsServ.Error("Ошибка взятия данных протокола: " + ex.Message);
                }
            }

            //
            //заполнение гридов
            //
            InitAndFillGrids();
        }

        protected virtual void InitAndFillGrids()
        {
        }

        //подготовка нужного грида
        protected virtual void InitGrid(DataGridView dgv)
        {
            dgv.Columns.Clear();

            DataGridViewCheckBoxColumn column = new DataGridViewCheckBoxColumn();
            column.Width = 20;
            column.ReadOnly = false;
            column.Resizable = DataGridViewTriState.False;
            dgv.Columns.Add(column);
            dgv.Columns.Add("Id", "Id");
            dgv.Columns.Add("Red", "Red");
            dgv.Columns.Add("Black", "Black");
            dgv.Columns.Add("Backdoc", "Backdoc");
            dgv.Columns.Add("Num", "Рег_номер");
            dgv.Columns.Add("FIO", "ФИО");
            dgv.Columns.Add("Sum", "Сумма баллов");
            dgv.Columns.Add("Attestat", "Документ_об_образовании");
            dgv.Columns.Add("Spec", "Направление/Профиль");
            dgv.Columns.Add("Konkurs", "Конкурс");
            dgv.Columns.Add("Pasport", "Паспорт");

            dgv.Columns["Id"].Visible = false;
            dgv.Columns["Red"].Visible = false;
            dgv.Columns["Black"].Visible = false;
            dgv.Columns["Backdoc"].Visible = false;

            dgv.Update();
        }

        //функция заполнения грида
        protected virtual void FillGrid(DataGridView dgv, string sQuery, string sWhere, string sOrderby)
        {
            DataSet ds = MainClass.Bdc.GetDataSet(sQuery + sWhere + sOrderby);
            FillGrid(dgv, ds.Tables[0]);
        }

        //функция заполнения грида
        protected virtual void FillGrid(DataGridView dgv, DataTable dt)
        {
            //подготовили грид
            InitGrid(dgv);

            //заполняем строки
            foreach (DataRow dr in dt.Rows)
            {
                DataGridViewRow r = new DataGridViewRow();
                r.CreateCells(dgv, false, dr["Id"].ToString(), dr["Red"].ToString(), false, dr["backdoc"].ToString(), dr["Рег_номер"].ToString(), dr["ФИО"].ToString(), dr["Sum"].ToString(), dr["Документ_об_образовании"].ToString(), dr["Направление"].ToString(), dr["Конкурс"].ToString(), dr["Паспорт"].ToString());
                if (bool.Parse(dr["Red"].ToString()))
                {
                    r.Cells[5].Style.ForeColor = Color.Red;
                    r.Cells[6].Style.ForeColor = Color.Red;
                }

                dgv.Rows.Add(r);
            }

            //блокируем редактирование
            for (int i = 1; i < dgv.ColumnCount; i++)
                dgv.Columns[i].ReadOnly = true;

            dgv.Sort(dgv.Columns["Red"], ListSortDirection.Ascending);
            dgv.Update();

            lblTotalLeft.Text = "Всего: " + dgvLeft.Rows.Count.ToString();
            lblTotalRight.Text = "Всего: " + dgvRight.Rows.Count.ToString();
            //}
            //catch (Exception ex)
            //{
            //    WinFormsServ.Error("Ошибка при заполнении грида данными протокола: " + ex.Message);
            //}
        }

        //для обработчика
        private void UpdateGrid(DataGridView dgv)
        {
            if (dgv.RowCount <= 0)
                return;

            string whereString = string.Empty;
            List<string> idList = new List<string>();
            foreach (DataGridViewRow row in dgv.Rows)
                idList.Add(string.Format("'{0}'", row.Cells["Id"].Value.ToString()));

            whereString = string.Format(" WHERE ed.extAbitAspirant.StudyLevelGroupId = {1} AND ed.extAbitAspirant.Id IN ({0}) ", Util.BuildStringWithCollection(idList), MainClass.studyLevelGroupId);
            dgv.Rows.Clear();
            FillGrid(dgvRight, sQuery, whereString, sOrderby);
        }

        private void UpdateRightGrid()
        {
            UpdateGrid(dgvRight);
        }

        //возвращает строку фильтров
        protected virtual string GetWhereClause(string sTable)
        {
            string rez = string.Format(" WHERE {3}.StudyLevelGroupId = {4} AND {3}.FacultyId = {0} AND {3}.StudyFormId = {1} AND {3}.StudyBasisId = {2}  ", 
                _facultyId.ToString(), _studyFormId.ToString(), _studyBasisId.ToString(), sTable, MainClass.studyLevelGroupId);

            return rez;
        }

        //отмена
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //ресайз
        private void Protocol_Resize(object sender, EventArgs e)
        {
            int width = (this.Width - 90 - 11 * 2) / 2;
            dgvLeft.Width = dgvRight.Width = width;
            dgvLeft.Location = new Point(12, 56);
            dgvRight.Location = new Point(90 + width, 56);

            btnLeft.Left = 11 + width + 11;
            btnLeftAll.Left = 11 + width + 11;

            btnRight.Left = 90 + width - btnRight.Width - 11;
            btnRightAll.Left = 90 + width - btnRightAll.Width - 11;
        }

        //перенос строк
        private void MoveRows(DataGridView from, DataGridView to, bool isAll)
        {
            for (int i = from.Rows.Count - 1; i >= 0; i--)// 
            {
                DataGridViewRow dr = from.Rows[i];

                if (bool.Parse(dr.Cells["Black"].Value.ToString()) || bool.Parse(dr.Cells["Red"].Value.ToString()))
                    continue;

                if (isAll || (bool)dr.Cells[0].Value)
                {
                    dr.Cells[0].Value = false;
                    from.Rows.Remove(dr);
                    to.Rows.Add(dr);
                }
            }

            OnMoved();
        }

        protected virtual void OnMoved()
        {
            lblTotalLeft.Text = "Всего: " + dgvLeft.Rows.Count.ToString();
            lblTotalRight.Text = "Всего: " + dgvRight.Rows.Count.ToString();
        }
        //убрать влево
        private void btnLeft_Click(object sender, EventArgs e)
        {
            MoveRows(dgvRight, dgvLeft, false);
        }

        //убрать вправо
        private void btnRight_Click(object sender, EventArgs e)
        {
            MoveRows(dgvLeft, dgvRight, false);
        }

        //все влево
        private void btnLeftAll_Click(object sender, EventArgs e)
        {
            MoveRows(dgvRight, dgvLeft, true);
        }

        //все вправо
        private void btnRightAll_Click(object sender, EventArgs e)
        {
            MoveRows(dgvLeft, dgvRight, true);
        }

        //проверка данных
        protected virtual bool CheckData()
        {
            int iProtocolTypeId = ProtocolList.TypeToInt(_type);
            string sNum = ProtocolName;
            //проверяем, есть ли в базе такое же значение
            string s = string.Format("SELECT id FROM ed.qProtocol {0}  AND ProtocolTypeId = {1} AND Number = '{2}'", GetWhereClause("ed.qProtocol"), iProtocolTypeId, sNum);
            if (!isNew)
                s += string.Format(" AND IsOld=0 AND Id<>'{0}'", _id);

            string sCheck = MainClass.Bdc.GetStringValue(s);

            if (sNum.Length == 0)
            {
                epError.SetError(tbNum, "Пустое или повторяющееся значение.");
                return false;
            }
            else if (sCheck.Length > 0)
            {
                epError.SetError(tbNum, "Протокол с данным номером уже существует");
                return false;
            }
            else
            {
                epError.Clear();
            }

            return true;
        }

        //все хорошо
        private void btnOk_Click(object sender, EventArgs e)
        {
            OnOK();
        }

        protected virtual void OnOK()
        {
            if (Save())
                this.Close();
        }

        //сохранение
        protected virtual bool Save()
        {
            //проверка данных
            if (!CheckData())
                return false;

            if (dgvLeft.Rows.Count == 0)
            {
                MessageBox.Show("Нельзя создать пустой протокол!", "Внимание");
                return false;
            }
          
            try
            {
                Guid ProtocolId;
                using (PriemEntities context = new PriemEntities())
                {
                    using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.RequiresNew))
                    {
                        try
                        {
                            if (!isNew)                                                           
                                context.Protocol_UpdateIsOld(true, _id);
                            
                            ObjectParameter paramId = new ObjectParameter("id", typeof(Guid));
                            int iProtocolTypeId = ProtocolList.TypeToInt(_type);

                            context.Protocol_InsertAll(MainClass.studyLevelGroupId,
                                _facultyId, _licenseProgramId, _studyFormId, _studyBasisId, tbNum.Text, dtpDate.Value, iProtocolTypeId,
                                string.Empty, !isNew, null, _isSecond, _isReduced, _isParallel, _isListener, paramId);

                            ProtocolId = (Guid)paramId.Value;
                            //сохраняем людей в протоколе
                            foreach (DataGridViewRow rw in dgvLeft.Rows)
                            {
                                Guid abId = new Guid(rw.Cells["Id"].Value.ToString());
                                context.ProtocolHistory_Insert(abId, ProtocolId, false, null, null, paramId);
                            }

                            transaction.Complete();
                        }
                        catch (Exception exc)
                        {
                            throw new Exception("Ошибка при сохранении данных: " + exc.Message);
                        }
                    }
                }                
            }
            catch (Exception ex)
            {
                WinFormsServ.Error("Ошибка при сохранении протокола: " + ex.Message);
                return false;
            }

            return true;
        }

        //обновили родительский грид
        private void Protocol_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainClass.ProtocolRefresh();
        }

        //по кнопке просмотр
        private void btnPreview_Click(object sender, EventArgs e)
        {
            Preview();
        }

        //предварительный просмотр
        protected virtual void Preview()
        {
            if (dgvLeft.Rows.Count == 0)
            {
                MessageBox.Show("Протокол пуст", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                WordDoc wd = new WordDoc(string.Format(@"{0}\EmptyTemplate.dot", MainClass.dirTemplates));
                wd.AddNewTable(dgvLeft.Rows.Count + 1, 7);
                TableDoc td = wd.Tables[0];
                //заполняем таблицу в шаблоне

                int r = 0;

                td[0, r] = "№ п/п";
                td[1, r] = "Рег.номер";
                td[2, r] = "ФАМИЛИЯ, ИМЯ, ОТЧЕСТВО";
                td[3, r] = "Номер документа об образовании";
                td[4, r] = "Направление(профиль)";
                td[5, r] = "Вид конкурса";
                td[6, r] = "Номер и серия паспорта";

                foreach (DataGridViewRow row in dgvLeft.Rows)
                {
                    ++r;
                    td[0, r] = r.ToString();
                    td[1, r] = row.Cells["Num"].Value.ToString();
                    td[2, r] = row.Cells["FIO"].Value.ToString();
                    td[3, r] = row.Cells["Attestat"].Value.ToString();
                    td[4, r] = row.Cells["Spec"].Value.ToString();
                    td[5, r] = row.Cells["Konkurs"].Value.ToString();
                    td[6, r] = row.Cells["Pasport"].Value.ToString();
                }

            }
            catch (Exception ex)
            {
                WinFormsServ.Error("Ошибка при выводе в Word протокола о допуске: " + ex.Message);
            }
        }

        private void dgvRight_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                OpenCard(dgvRight, false);
        }

        //открытие карточки
        private void OpenCard(DataGridView dgv, bool edit)
        {
            string abId = dgv.Rows[dgv.CurrentCell.RowIndex].Cells["Id"].Value.ToString();
            if (abId != "")
            {
                MainClass.OpenCardAbit(abId, this, dgv.CurrentCell.RowIndex);
            }
        }

        private void chbEnable_CheckedChanged(object sender, EventArgs e)
        {
            btnOk.Enabled = chbEnable.Checked;
        }

        private void dgvLeft_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (isConfirmed && e.RowIndex >= 0)
                OpenCard(dgvLeft, true);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                MainClass.Bdc.ExecuteQuery(string.Format("UPDATE ed.qProtocol SET IsOld = 1 WHERE Id = '{0}'", _id));
            }
            catch (Exception ex)
            {
                WinFormsServ.Error("Ошибка при удалении протокола: " + ex.Message);
            }
            this.Close();
        }

        private void chbFilter_CheckedChanged(object sender, EventArgs e)
        {
            InitAndFillGrids();
        }
    }
}
