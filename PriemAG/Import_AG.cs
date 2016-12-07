
using EducServLib;
using PriemLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Priem
{
    public static class Import_AG
    {
        public static void ImportRusLangOGE()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel 2007|*.xlsx";
            var dr = ofd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                DataTable tbl = EducServLib.PrintClass.GetDataTableFromExcel2007_New(ofd.FileName);

                using (PriemEntities context = new PriemEntities())
                {

                    foreach (DataRow rw in tbl.Rows)
                    {
                        string FIO = rw.Field<string>(0);
                        string OP = rw.Field<string>(1);
                        string sMark = rw.Field<string>(2);

                        int iMark = 0;

                        if (!int.TryParse(sMark, out iMark))
                            continue;

                        var Abiturient = context.extAbit.Where(x => x.FIO == FIO && x.ObrazProgramName == OP).Select(x => new { x.Id, x.EntryId }).FirstOrDefault();
                        if (Abiturient == null)
                        {
                            WinFormsServ.Error("Не найден в базе абитуриент " + FIO);
                            continue;
                        }

                        Guid ExamInEntryBlockUnitId = context.extExamInEntry
                            .Where(x => x.EntryId == Abiturient.EntryId && x.ExamName == "Русский язык")
                            .Select(x => x.Id).DefaultIfEmpty(Guid.Empty).First();
                        if (ExamInEntryBlockUnitId == Guid.Empty)
                        {
                            WinFormsServ.Error("Не найден в базе предмет РусЯз для программы " + OP);
                            continue;
                        }

                        int cnt = context.Mark.Where(x => x.AbiturientId == Abiturient.Id && x.ExamInEntryBlockUnitId == ExamInEntryBlockUnitId).Count();
                        if (cnt == 0)
                            context.Mark_Insert(Abiturient.Id, ExamInEntryBlockUnitId, iMark, DateTime.Now, true, false, false, null, null, null);
                    }
                }

                MessageBox.Show("OK");
            }
        }
    }
}
