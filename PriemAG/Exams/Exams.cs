using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Data;

using PriemLib;

namespace Priem
{
    public static class Exams
    {
        public static IEnumerable<extExamInEntry> GetExamsWithFilters(PriemEntities context, int? facultyId, int? licenseProgramId, int? obrazProgramId, Guid? profileId, int? stFormId, int? stBasisId, bool? isSecond, bool? isReduced, bool? isParallel)
        {            
            IEnumerable<extExamInEntry> exams = from ex in context.extExamInEntry where ex.StudyLevelGroupId == MainClass.studyLevelGroupId select ex;

            if (facultyId != null)
                exams = exams.Where(c => c.FacultyId == facultyId);
            if (licenseProgramId != null)
                exams = exams.Where(c => c.LicenseProgramId == licenseProgramId);
            if (obrazProgramId != null)
                exams = exams.Where(c => c.ObrazProgramId == obrazProgramId);
            if (profileId != null)
                exams = exams.Where(c => c.ProfileId == profileId);
            if (stFormId != null)
                exams = exams.Where(c => c.StudyFormId == stFormId);
            if (stBasisId != null)
                exams = exams.Where(c => c.StudyBasisId == stBasisId);
            if (isSecond != null)
                exams = exams.Where(c => c.IsSecond == isSecond);
            if (isReduced != null)
                exams = exams.Where(c => c.IsReduced == isReduced);
            if (isParallel != null)
                exams = exams.Where(c => c.IsParallel == isParallel);

            return exams.OrderBy(c=>c.ExamName);           
        }        

        public static string GetFilterForNotAddExam(int? examId, int? facultyId)
        {
            if (MainClass.dbType == PriemType.PriemAspirant)
                return "";

            string flt_privil = string.Empty;
            string flt_ssuz = string.Empty;
            string flt_underPrevYear = string.Empty;
            string flt_foreignEduc = string.Empty;
            string flt_second = string.Empty;
            string flt_hasEge = string.Empty;

            /* Ситуация на 2011 год:
             * Попадают в правый список все, у кого нет оценки по ЕГЭ в этом году
             * Если у чела вне конкурса, то его тоже надо выводить (зачем???)
             * Остальных не надо
             */

            //flt_privil = " (Person.Privileges & 512 > 0 OR Person.Privileges & 32 > 0 OR Person.CountryId > 1) ";
            //flt_ssuz = " OR Person.SchoolTypeId = 2 OR Person.SchoolTypeId = 5 ";
            //flt_underPrevYear = string.Format(" OR (Person.SchoolExitYear < {0} AND qAbiturient.StudyFormId IN (2,3)) ", (DateTime.Now.Year - 1).ToString());
            //flt_foreignEduc = " OR Person.CountryEducId > 1 ";
            //flt_second = " OR qAbiturient.ListenerTypeId > 0 ";
            //flt_hasEge = string.Format(" OR Person.Id NOT IN (SELECT PersonId FROM EgeCertificate LEFT JOIN EgeMark ON egeMArk.EgeCertificateId = EgeCertificate.Id LEFT JOIN EgeToExam ON EgeMArk.EgeExamNameId = EgeToExam.EgeExamNameId LEFT JOIN ExamInProgram ON EgeToExam.ExamNameId = ExamInProgram.ExamNameId WHERE ExamInProgram.Id IN (SELECT Id FROM extExamInProgram WHERE ExamNameId = {0} AND FacultyId = {1}))", examId, facultyId);

            //return " AND (" + flt_privil + flt_ssuz + flt_underPrevYear + flt_foreignEduc + flt_second + flt_hasEge + ")";

            flt_hasEge = string.Format(" ed.extPersonAspirant.Id NOT IN (SELECT PersonId FROM ed.extEgeMark LEFT JOIN ed.EgeToExam ON ed.extEgeMark.EgeExamNameId = ed.EgeToExam.EgeExamNameId LEFT JOIN ed.extExamInEntry ON ed.EgeToExam.ExamId = ed.extExamInEntry.ExamId WHERE ed.extExamInEntry.Id IN (SELECT Id FROM ed.extExamInEntry WHERE ExamId = {0} AND FacultyId = {1}) AND ed.extEgeMark.[Year] = 2012 )", examId, facultyId);
            //flt_hasEge = " 1=1 ";
            //flt_privil = " OR (Person.Privileges & 512 > 0 OR Person.Privileges & 32 > 0) ";
            return " AND (" + flt_hasEge + flt_privil + " ) ";
        }       

        public static string GetExamInEntryId(string examId, string facultyId, string licenseProgramId, string obrazProgramId, string profileId, string stFormId, string stBasisId, bool? isSecond, bool? isParallel, bool? isReduced)
        {
            string fltStLevelGroup = " AND ed.qEntry.StudyLevelGroupId = " + MainClass.studyLevelGroupId.ToString();
            string fltFaculty = facultyId == null || facultyId == string.Empty || facultyId == "0" ? "" : " AND ed.qEntry.FacultyId = " + facultyId;
            string fltProfession = licenseProgramId == null || licenseProgramId == string.Empty || licenseProgramId == "0" ? "" : " AND ed.qEntry.LicenseProgramId = " + licenseProgramId;
            string fltObrazProgram = obrazProgramId == null || obrazProgramId == string.Empty || obrazProgramId == "0" ? "" : " AND ed.qEntry.ObrazProgramId = " + obrazProgramId;
            string fltSpecialization = profileId == null || profileId == string.Empty || profileId == "0" ? " AND ed.qEntry.ProfileId IS NULL " : " AND ed.qEntry.ProfileId = '" + profileId + "'";
            string fltStudyForm = stFormId == null || stFormId == string.Empty || stFormId == "0" ? "" : " AND ed.qEntry.StudyFormId = " + stFormId;
            string fltStudyBasis = stBasisId == null || stBasisId == string.Empty || stBasisId == "0" ? "" : " AND ed.qEntry.StudyBasisId = " + stBasisId;

            string fltIsSecond = isSecond == null ? "" : " AND ed.qEntry.IsSecond = " + (isSecond.Value ? "1" : "0");
            string fltIsParallel = isParallel == null ? "" : " AND ed.qEntry.IsParallel = " + (isParallel.Value ? "1" : "0");
            string fltIsReduced = isReduced == null ? "" : " AND ed.qEntry.IsReduced = " + (isReduced.Value ? "1" : "0");


            string entryId = MainClass.Bdc.GetStringValue(string.Format("SELECT DISTINCT ed.qEntry.Id FROM ed.qEntry WHERE {9} {0} {1} {2} {3} {4} {5} {6} {7} {8}", fltFaculty, fltProfession, fltObrazProgram, fltSpecialization, fltStudyForm, fltStudyBasis, fltIsSecond, fltIsParallel, fltIsReduced, fltStLevelGroup));

            return GetExamInEntryId(examId, entryId);
        }

        public static string GetExamInEntryId(string examId, string entryId)
        {            
            if (string.IsNullOrEmpty(entryId))
                return null;

            return MainClass.Bdc.GetStringValue(string.Format("SELECT DISTINCT ed.extExamInEntry.Id FROM ed.extExamInEntry WHERE ExamId = {0} AND EntryId = '{1}' ", examId, entryId));
        }

        public static List<string> GetExamIdsInEntry(string facultyId, string licenseProgramId, string obrazProgramId, string profileId, string stFormId, string stBasisId, bool isSecond, bool isParallel, bool isReduced)
        {
            string fltStLevelGroup = " AND ed.qEntry.StudyLevelGroupId = " + MainClass.studyLevelGroupId.ToString();
            string fltSpecialization = (profileId == null || profileId == "") ? " AND ed.qEntry.ProfileId IS NULL" : " AND ed.qEntry.ProfileId = '" + profileId + "'";
            string entryId = MainClass.Bdc.GetStringValue(string.Format("SELECT DISTINCT ed.qEntry.Id FROM ed.qEntry WHERE {9} AND FacultyId = {0} AND LicenseProgramId = {1} AND ObrazProgramId = {2} {3} AND StudyFormId = {4} AND StudyBasisId = {5} AND IsSecond = {6} AND IsParallel = {7} AND IsReduced = {8}", facultyId, licenseProgramId, obrazProgramId, fltSpecialization, stFormId, stBasisId, (isSecond ? "1" : "0"), (isParallel ? "1" : "0"), (isReduced ? "1" : "0"), fltStLevelGroup));

            return GetExamIdsInEntry(entryId);
        }

        public static List<string> GetExamIdsInEntry(string entryId)
        {  
            List<string> lst = new List<string>();
            if (string.IsNullOrEmpty(entryId))
                return lst;

            DataSet ds = MainClass.Bdc.GetDataSet(string.Format("SELECT ed.extExamInEntry.ExamId FROM ed.extExamInEntry WHERE  ed.extExamInEntry.EntryId = '{0}' ", entryId));
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                lst.Add(dr["ExamId"].ToString());
            }
            return lst;
        }
    }
}
