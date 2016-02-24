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

namespace Priem
{
    public partial class CardExamName : BookCardInt
    {
        public CardExamName()
        {
            InitializeComponent();
        }

        public CardExamName(string id)
        {
            InitializeComponent();
            _Id = id;
        }

        protected string ExamName
        {
            get { return tbName.Text.Trim(); }
            set { tbName.Text = value; }
        }

        public string NamePad
        {
            get { return tbNamePad.Text.Trim(); }
            set { tbNamePad.Text = value; }
        }

        public string Acronym
        {
            get { return tbAcronym.Text.Trim(); }
            set { tbAcronym.Text = value; }
        }

        protected override void ExtraInit()
        {
            base.ExtraInit();
            _tableName = "ed.ExamName";
        }

        protected override bool IsForReadOnly()
        {
            return !MainClass.RightsPashaOlia();
        }

        protected override void FillCard()
        {           
            if (_Id == null)
                return;

            try
            {
                using (PriemEntities context = new PriemEntities())
                {
                    ExamName exn = (from pr in context.ExamName
                                     where pr.Id == IntId
                                     select pr).FirstOrDefault();

                    ExamName = exn.Name;
                    NamePad = exn.NamePad;
                    Acronym = exn.Acronym;
                }
            }
            catch (Exception ex)
            {
                WinFormsServ.Error("Ошибка при заполнении формы " + ex.Message);
            }
        }

        protected override bool CheckFields()
        {
            using (PriemEntities context = new PriemEntities())
            {
                int cnt = (from exn in context.ExamName
                           where exn.Name == ExamName
                           select exn).Count();

                if(cnt > 0)
                {
                    epError.SetError(tbName, "Такой экзамен уже существует");                    
                    return false;
                }
                else
                    epError.Clear();

                return true;                     
            }
        }

        protected override void InsertRec(PriemEntities context, ObjectParameter idParam)
        {
           
        }

        protected override void UpdateRec(PriemEntities context, Guid id)
        {
           
        }             
    }
}
