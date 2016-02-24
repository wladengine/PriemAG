using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using BaseFormsLib;
using PriemLib;
namespace Priem
{
    public partial class ExamList : BookList
    {
        public ExamList()
        {
            InitializeComponent();
            Dgv = dgvExams;
            _tableName = "ed.[Exam]";            
            _title = "Список экзаменов СПбГУ";
            InitControls();
        }

        protected override void GetSource()
        {
            using (PriemEntities context = new PriemEntities())
            {
                var tt = from f in context.Exam
                         join en in context.ExamName
                         on f.ExamNameId equals en.Id
                         select new
                         {
                             Id = f.Id,
                             Name = en.Name,
                             IsAdd = f.IsAdditional ? "да" : "нет"
                         };

                Dgv.DataSource = tt;

                SetVisibleColumnsAndNameColumns();
            }
        }

        protected override void SetVisibleColumnsAndNameColumns()
        {
            SetVisibleColumnsAndNameColumns("Name", "Название");
            SetVisibleColumnsAndNameColumns("IsAdd", "Дополнительный");
        }

        protected override void OpenCard(string itemId)
        {
            CardExam crd = new CardExam(itemId);
            crd.ToUpdateList += new UpdateListHandler(UpdateDataGrid);
            crd.Show();            
        }

        protected override void DeleteSelectedRows(string sId)
        {
            using (PriemEntities context = new PriemEntities())
            {
                if (!MainClass.IsEntryChanger())
                    return;

                int entId = int.Parse(sId);
                context.Exam_Delete(entId);
            }
        }
    }
}
