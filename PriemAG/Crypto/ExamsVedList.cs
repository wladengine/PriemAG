using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Collections;
using System.Linq;
using System.Transactions;
 
//using BDClassLib;
using EducServLib;
using WordOut;
using BaseFormsLib;
using PriemLib;

using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Priem
{
    public partial class ExamsVedList : PriemLib.ExamsVedList
    {
        public ExamsVedList()
        {

        }

        protected override List<KeyValuePair<string, string>> GetSourceStudyLevelGroup()
        {
            using (PriemEntities context = new PriemEntities())
            {
                var src = MainClass.GetEntry(context)
                    .Select(x => new { x.StudyLevelId, x.StudyLevelName })
                    .Distinct()
                    .ToList()
                    .Select(x => new KeyValuePair<string, string>(x.StudyLevelId.ToString(), x.StudyLevelName))
                    .ToList();

                return src;
            }
        }
        protected override void btnCreate_Click(object sender, EventArgs e)
        {
            if (!StudyLevelGroupId.HasValue)
            {
                WinFormsServ.Error("Не выбран уровень образования!");
                return;
            }

            if (MainClass.RightsFacMain())
            {
                SelectExamCrypto frm = new SelectExamCrypto(this, StudyLevelGroupId.Value, FacultyId, StudyBasisId);
                frm.Show();
            }
        }     
    }
}