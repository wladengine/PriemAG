using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using System.Data.Entity.Core.Objects;
using System.Transactions;

using EducServLib;
//using BDClassLib;
using BaseFormsLib;
using PriemLib;

namespace Priem
{
    public partial class ExamsVedCard : PriemLib.ExamsVedCard
    {
        public ExamsVedCard(ExamsVedList owner, int StudyLevelGroupId, int facId, int examId, DateTime date, int? basisId, bool isAdd)
            : base( owner, StudyLevelGroupId, facId, examId, date,  basisId,  isAdd)
        {
        }

        protected override List<KeyValuePair<string, string>> GetSourceObrazProgram()
        {
            if (iStudyLevelId.HasValue)
            {
                using (PriemEntities context = new PriemEntities())
                {
                    List<KeyValuePair<string, string>> lst =
                        ((from ent in MainClass.GetEntry(context)
                          where ent.FacultyId == facultyId
                          && (StudyBasisId != null ? ent.StudyBasisId == StudyBasisId : true == true)
                          && (StudyFormId != null ? ent.StudyBasisId == StudyFormId : true == true)
                          && ent.StudyLevelId == iStudyLevelId
                          select new
                          {
                              Id = ent.ObrazProgramId,
                              Name = ent.ObrazProgramName,
                              Crypt = ent.ObrazProgramCrypt
                          }).Distinct()).ToList().Select(u => new KeyValuePair<string, string>(u.Id.ToString(), u.Name + ' ' + u.Crypt)).ToList();
                    return lst;
                }
            }
            else
            {
                using (PriemEntities context = new PriemEntities())
                {
                    List<KeyValuePair<string, string>> lst =
                        ((from ent in MainClass.GetEntry(context)
                          where ent.FacultyId == facultyId
                          && (StudyBasisId != null ? ent.StudyBasisId == StudyBasisId : true == true)
                          && (StudyFormId != null ? ent.StudyBasisId == StudyFormId : true == true)
                          && ent.StudyLevelGroupId == iStudyLevelGroupId
                          select new
                          {
                              Id = ent.ObrazProgramId,
                              Name = ent.ObrazProgramName,
                              Crypt = ent.ObrazProgramCrypt
                          }).Distinct()).ToList().Select(u => new KeyValuePair<string, string>(u.Id.ToString(), u.Name + ' ' + u.Crypt)).ToList();
                    return lst;
                }
            }
        }
        protected override List<KeyValuePair<string, string>> GetSourceStudyBasis()
        {
            if (studyBasisId == null)
                return HelpClass.GetComboListByTable("ed.StudyBasis", "ORDER BY Name");
            else
                return HelpClass.GetComboListByQuery(string.Format("SELECT CONVERT(varchar(100), Id) AS Id, Name FROM ed.StudyBasis WHERE Id = {0} ORDER BY Name", studyBasisId));
        }

        protected override List<KeyValuePair<string, string>> GetSourceStudyForm()
        {
            if (iStudyLevelId.HasValue)
                return HelpClass.GetComboListByQuery(string.Format(@"SELECT DISTINCT CONVERT(varchar(100), StudyFormId) AS Id, StudyFormName AS Name 
    FROM ed.qEntry WHERE StudyLevelId = {0} AND FacultyId = {1} ORDER BY Name", iStudyLevelId, facultyId));
            else
                return HelpClass.GetComboListByQuery(string.Format(@"SELECT DISTINCT CONVERT(varchar(100), StudyFormId) AS Id, StudyFormName AS Name 
    FROM ed.qEntry WHERE StudyLevelGroupId = {0} AND FacultyId = {1} ORDER BY Name", iStudyLevelGroupId, facultyId));
        }

        protected override void GetStudyLevelGroupIdFromStudyLevelId()
        {
            using (PriemEntities context = new PriemEntities())
            {
                var StudyLevel = (from lv in context.StudyLevel
                                  where lv.Id == iStudyLevelGroupId
                                  select lv.LevelGroupId).ToList();
                if (StudyLevel.Count>0)
                {
                    iStudyLevelId = iStudyLevelGroupId;
                    iStudyLevelGroupId = StudyLevel.First();
                }
            }

            if (iStudyLevelId.HasValue)
                sQueryWhere = @" WHERE qAbiturient.FacultyId = {0} AND qAbiturient.StudyLevelId = {1} ";
        }
    }
}