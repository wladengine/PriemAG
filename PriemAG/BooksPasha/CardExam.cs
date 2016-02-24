using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Objects;

using EducServLib;
using PriemLib;
namespace Priem
{
    public partial class CardExam : BookCard
    {      
        public CardExam()
        {
            InitializeComponent();
            InitControls();
        }

        public CardExam(string exId)
        {
            InitializeComponent();
            _Id = exId;                  

            InitControls();
        }

        protected override void SetReadOnlyFieldsAfterFill()
        {
            base.SetReadOnlyFieldsAfterFill();

            if (!MainClass.IsEntryChanger())
                btnSaveChange.Enabled = false;
        }     

        protected int? IntId
        {
            get
            {
                if (_Id == null)
                    return null;
                else
                    return int.Parse(_Id);
            }
        }

        protected override void ExtraInit()
        {
            base.ExtraInit();
            _title = "Экзамен";
            _tableName = "ed.[Exam]";

            try
            {
                using (PriemEntities context = new PriemEntities())
                {                   
                    List<KeyValuePair<string, string>> lst = ((from f in context.ExamName                                                             
                                                              select new 
                                                              {
                                                                  Id = f.Id,
                                                                  Name = f.Name                                                                 
                                                              }).Distinct()).ToList().Select(u => new KeyValuePair<string, string>(u.Id.ToString(), u.Name)).ToList();                                                                 
                                                                   
                    ComboServ.FillCombo(cbExamName, lst, false, false);                   
                }
            }
            catch (Exception exc)
            {
                WinFormsServ.Error("Ошибка при инициализации формы " + exc.Message);
            }
        }

        protected int? ExamNameId
        {
            get { return ComboServ.GetComboIdInt(cbExamName); }
            set { ComboServ.SetComboId(cbExamName, value); }
        }

        protected bool IsAdditional
        {
            get { return chbIsAdd.Checked; }
            set { chbIsAdd.Checked = value; }
        }

        protected override void FillCard()
        {
            if (_Id == null)                
                return;
            
            try
            {
                using (PriemEntities context = new PriemEntities())
                {
                    Exam ent = (from ex in context.Exam
                                       where ex.Id == IntId
                                       select ex).FirstOrDefault();


                    ExamNameId = ent.ExamNameId;
                    IsAdditional = ent.IsAdditional;                                 
                }
            }
            catch (Exception exc)
            {
                WinFormsServ.Error("Ошибка при заполнении формы " + exc.Message);
            }
        }

        protected override string Save()
        {
            try
            {
                using (PriemEntities context = new PriemEntities())
                {
                    if (_Id == null)
                    {
                        ObjectParameter exId;

                        try
                        {
                            exId = new ObjectParameter("id", typeof(Int32));
                            context.Exam_Insert(ExamNameId, IsAdditional, exId);                           
                        }
                        catch (Exception exc)
                        {
                            throw new Exception("Ошибка при сохранении данных: " + exc.Message);
                        }

                        return exId.Value.ToString();
                    }
                    else
                    {
                        context.Exam_Update(ExamNameId, IsAdditional, IntId);
                        return _Id;
                    }
                }
            }
            catch (Exception exc)
            {
                throw new Exception("Ошибка при сохранении данных: " + exc.Message);
            }
        }

        protected override void CloseCardAfterSave()
        {
            this.Close();
        }
    }
}
