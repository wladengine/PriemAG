using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace Priem
{
	public static partial class MainClass
	{
        public static bool IsRoleMember(string roleName)
        {
            using (PriemEntities context = new PriemEntities())
            {
                ObjectParameter entId = new ObjectParameter("result", typeof(bool));
                context.RoleMember(roleName, entId);

                return Convert.ToBoolean(entId.Value);
            }
        }

        public static bool IsFilologFac()
        {
            return (IsRoleMember("filolog_fac") || IsRoleMember("iskus_fac"));
        }

        public static bool IsReadOnly()
        {
            return IsRoleMember("readOnly");
        }       

        public static bool IsFaculty()
        {
            return IsRoleMember("faculty");
        }

        public static bool IsFacMain()
        {
            return IsRoleMember("faculty_main");
        }

        public static bool IsSovetnik()
        {
            return IsRoleMember("sovetnik");
        }

        public static bool IsSovetnikMain()
        {
            return IsRoleMember("sovetnik_main");
        }

        public static bool IsCrypto()
        {
            return IsRoleMember("crypto");
        }

        public static bool IsCryptoMain()
        {
            return IsRoleMember("crypto_main");
        }

        public static bool IsPrintOrder()
        {
            return IsRoleMember("printOrder");
        }

        public static bool IsPasha()
        {
            return (IsRoleMember("pasha") || IsOwner());
        }

        public static bool IsRectorat()
        {
            if (System.Environment.UserName == "e.babeluk" || IsRoleMember("rectorat"))
                return true;
            return false;
        }

        public static bool IsEntryChanger()
        {
            return (IsRoleMember("entryChanger") || IsOwner() || IsPasha());
        }

        public static bool IsOwner()
        {
            if (System.Environment.UserName == "o.belenog" || System.Environment.UserName == "v.chikhira" || System.Environment.UserName == "st021085")
                return true;
            else
                return false;
        }

        // права собранные из нескольких
        public static bool RightsToEditCards()
        {
            if (IsPasha() || IsFaculty() || IsFacMain() || IsOwner())
                return true;
            
            return false;
        }

        public static bool RightsFacMain()
        {
            if (IsPasha() || IsFacMain())
                return true;
            else

                return false;
        }
       
        public static bool RightsFaculty()
        {
            if (IsFaculty() && !IsFacMain() && !IsSovetnik())
                return true;
            else
                return false;
        }

        public static bool RightsSov_SovMain_FacMain()
        {
            if (IsFacMain() || IsSovetnik() || IsSovetnikMain() || IsOwner())
                return true;
            else
                return false;
        }

        public static bool RightsSov_SovMain()
        {
            if (IsSovetnik() || IsSovetnikMain() || IsOwner())
                return true;
            else
                return false;
        }

        public static bool RightsJustView()
        {
            if (!IsPasha() && !IsFaculty() && !IsOwner())
                return true;
            else
                return false;
        }

        public static bool RightViewAll()
        {
            if (!IsFaculty() && !IsSovetnik())
                return true;
            else
                return false;
        }

        public static bool RightComboVseFaculty()
        {
            if (!IsFaculty())
                return true;
            else
                return false;
        }

        public static string GetFacultyId()
        {
            using (PriemEntities context = new PriemEntities())
            {
                ObjectParameter entId = new ObjectParameter("result", typeof(string));
                context.GetFacultyId(entId);

                return entId.Value.ToString();
            }
        }

        public static int GetFacultyIdInt()
        {
            using (PriemEntities context = new PriemEntities())
            {
                ObjectParameter entId = new ObjectParameter("result", typeof(string));
                context.GetFacultyId(entId);

                return int.Parse(entId.Value.ToString());
            }
        }

        public static bool HasRightsForFaculty(int? facId)
        {
            if (facId == null)
                return true;

            using (PriemEntities context = new PriemEntities())
            {
                ObjectParameter entId = new ObjectParameter("result", typeof(bool));
                context.HasRightsForFaculty(facId.ToString(), entId);

                return Convert.ToBoolean(entId.Value);
            }
        }

        public static bool HasAddRightsForPriem(int iFacultyId, int? iProfessionId, int? iObrazProgramId, Guid? iSpecializationId, int iStudyFormId, int iStudyBasisId)
        {
            if (IsOwner())
                return true;
            // Медколледж открываем
//            if (facultyId == "11")
//                return true;

//            string flt = GetFilter("hlpStudyPlan", facultyId, professionId, obrazProgramId, specializationId, studyFormId, studyBasisId);
//            int kc = (int)MainClass.Bdc.GetValue(string.Format(@"SELECT Sum(Value) FROM hlpStudyPlan WHERE 1=1 {0} ", flt));

//            flt = GetFilter("extAbitAspirant", facultyId, professionId, obrazProgramId, specializationId, studyFormId, studyBasisId);
//            int entry = (int)MainClass.Bdc.GetValue(string.Format(@"SELECT Count(extAbitAspirant.Id) FROM extAbitAspirant 
//                         INNER JOIN extEntryView ON extAbitAspirant.Id=extEntryView.AbiturientId 
//                         WHERE Excluded=0 {0} ", flt));

//            int kcRest = kc - entry;

//            if (kcRest > 0)
//                return true;

            return false;
        }

        //public static string GetAbitFilter(string tableName)
        //{
        //    if (IsFaculty())
        //        return string.Format(" AND {0}.FacultyId IN ({1})", tableName, GetFacultyId());

        //    else
        //        return "";
        //}

        //        public static bool HasAddRightsForPriem(string facultyId, string professionId, string obrazProgramId, string specializationId, string studyFormId, string studyBasisId)
//        {
//            string flt = GetFilter("hlpStudyPlan", facultyId, professionId, obrazProgramId, specializationId, studyFormId, studyBasisId);
//            int kc = (int)MainClass.Bdc.GetValue(string.Format(@"SELECT Sum(Value) FROM hlpStudyPlan WHERE 1=1 {0} ", flt));

//            flt = GetFilter("extAbitAspirant", facultyId, professionId, obrazProgramId, specializationId, studyFormId, studyBasisId);
//            int entry = (int)MainClass.Bdc.GetValue(string.Format(@"SELECT Count(extAbitAspirant.Id) FROM extAbitAspirant 
//                         INNER JOIN extEntryView ON extAbitAspirant.Id=extEntryView.AbiturientId 
//                         WHERE Excluded=0 {0} ", flt));

//            int kcRest = kc - entry;

//            if (kcRest > 0)
//                return true;

//            return false;
//        }

        //        private static string GetFilter(string tablename, string facultyId, string professionId, string obrazProgramId, string specializationId, string studyFormId, string studyBasisId)
//        {
//            string filter = string.Empty;

//            if (!string.IsNullOrEmpty(facultyId))
//                filter += string.Format(" AND {0}.FacultyId={1}", tablename, facultyId);
//            if (!string.IsNullOrEmpty(professionId))
//                filter += string.Format(" AND {0}.ProfessionId={1}", tablename, professionId);
//            if (!string.IsNullOrEmpty(obrazProgramId))
//                filter += string.Format(" AND {0}.ObrazProgramId={1}", tablename, obrazProgramId);
//            if (!string.IsNullOrEmpty(specializationId))
//                filter += string.Format(" AND {0}.SpecializationId={1}", tablename, specializationId);
//            if (!string.IsNullOrEmpty(studyFormId))
//                filter += string.Format(" AND {0}.StudyFormId={1}", tablename, studyFormId);
//            if (!string.IsNullOrEmpty(studyBasisId))
//                filter += string.Format(" AND {0}.StudyBasisId={1}", tablename, studyBasisId);

//            return filter;
//        }
	}
}
