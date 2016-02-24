using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EducServLib;
using PriemLib;
namespace Priem
{
    public partial class CardAbit
    { 
        public Guid? EntryId
        {
            get 
            {
                try
                {
                    using (PriemEntities context = new PriemEntities())
                    {                        
                        Guid? entId = (from ent in context.qEntry
                                                 where ent.IsSecond == IsSecond && ent.IsParallel == IsParallel && ent.IsReduced == IsReduced
                                                 && ent.LicenseProgramId == LicenseProgramId
                                                 && ent.ObrazProgramId == ObrazProgramId
                                                 && (ProfileId == null ? ent.ProfileId == null : ent.ProfileId == ProfileId)   
                                                 && ent.StudyFormId == StudyFormId
                                                 && ent.StudyBasisId == StudyBasisId
                                                 select ent.Id).FirstOrDefault();
                        return entId;
                    }
                }
                catch
                {
                    return null;
                }
            }            
        }
        
        public int? FacultyId
        {
            get { return ComboServ.GetComboIdInt(cbFaculty); }
            set { ComboServ.SetComboId(cbFaculty, value); }
        }

        public int? LicenseProgramId
        {
            get { return ComboServ.GetComboIdInt(cbLicenseProgram); }
            set { ComboServ.SetComboId(cbLicenseProgram, value); }
        }

        public int? ObrazProgramId
        {
            get { return ComboServ.GetComboIdInt(cbObrazProgram); }
            set { ComboServ.SetComboId(cbObrazProgram, value); }
        }

        public Guid? ProfileId
        {
            get 
            {
                string prId = ComboServ.GetComboId(cbProfile);
                if(string.IsNullOrEmpty(prId))
                    return null;
                else
                    return new Guid(prId);               
            }
            set 
            { 
                if(value == null)
                    ComboServ.SetComboId(cbProfile, (string)null); 
                else
                    ComboServ.SetComboId(cbProfile, value.ToString()); 
            }
        }

        public int? StudyFormId
        {
            get { return ComboServ.GetComboIdInt(cbStudyForm);  }
            set { ComboServ.SetComboId(cbStudyForm, value); }
        }

        public int? StudyBasisId
        {
            get { return ComboServ.GetComboIdInt(cbStudyBasis); }
            set { ComboServ.SetComboId(cbStudyBasis, value); }
        }

        public int? CompetitionId
        {
            get { return ComboServ.GetComboIdInt(cbCompetition); }
            set { ComboServ.SetComboId(cbCompetition, value); }
        }

        public bool IsSecond
        {
            get { return chbIsSecond.Checked; }
            set { chbIsSecond.Checked = value; }
        }

        public bool IsReduced
        {
            get { return chbIsReduced.Checked; }
            set { chbIsReduced.Checked = value; }
        }

        public bool IsParallel
        {
            get { return chbIsParallel.Checked; }
            set { chbIsParallel.Checked = value; }
        }

        public bool IsListener
        {
            get { return chbIsListener.Checked; }
            set { chbIsListener.Checked = value; }
        }

        public bool IsPaid
        {
            get { return chbIsPaid.Checked; }
            set { chbIsPaid.Checked = value; }
        }

        public bool BackDoc
        {
            get { return chbBackDoc.Checked; }
            set 
            { 
                chbBackDoc.Checked = value;
                if (BackDoc)
                    chbBackDoc.ForeColor = System.Drawing.Color.Red;                
            }
        }

        public DateTime? BackDocDate
        {
            get 
            {
                if (BackDoc)
                    return dtBackDocDate.Value.Date;
                else
                    return null;
            }
            set 
            {
                if (BackDoc)
                {
                    if (value.HasValue)
                        dtBackDocDate.Value = value.Value;
                }               
            }
        }

        public DateTime? DocDate
        {
            get { return dtDocDate.Value.Date; }
            set 
            {
                if (value.HasValue)
                    dtDocDate.Value = value.Value; 
            }          
        }

        public DateTime? DocInsertDate
        {
            get { return dtDocInsertDate.Value.Date; }
            set 
            {
                if (value.HasValue)
                    dtDocInsertDate.Value = value.Value; 
            }
        }

        public bool Checked
        {
            get { return chbChecked.Checked; }
            set { chbChecked.Checked = value; }
        }

        public bool NotEnabled
        {
            get { return chbNotEnabled.Checked; }
            set { chbNotEnabled.Checked = value; }
        }

        public string Sum
        {
            get { return tbSum.Text.Trim(); }
            set { tbSum.Text = value; }
        }        

        public double? Coefficient
        {
            get
            {
                double j;
                if (double.TryParse(tbCoefficient.Text.Trim(), out j))
                    return j;
                else
                    return null;
            }
            set { tbCoefficient.Text = Util.ToStr(value); }
        }

        public int? OtherCompetitionId
        {
            get 
            {
                if (CompetitionId == 6 && cbOtherCompetition.SelectedIndex != 0)
                    return ComboServ.GetComboIdInt(cbOtherCompetition);
                else
                    return null;                
            }
            set 
            {
                if (CompetitionId == 6)
                    if (value != null)
                        ComboServ.SetComboId(cbOtherCompetition, value);                 
            }
        }

        public int? CelCompetitionId
        {
            get 
            {
                if (CompetitionId == 6 && cbCelCompetition.SelectedIndex != 0)
                    return ComboServ.GetComboIdInt(cbCelCompetition);
                else
                    return null;                
            }
            set 
            {
                if (CompetitionId == 6)
                    if (value != null)
                        ComboServ.SetComboId(cbCelCompetition, value);                     
            } 
        }

        public string CelCompetitionText
        {
            get
            {
                if (CompetitionId == 6)
                    return tbCelCompetitionText.Text;
                else
                    return string.Empty;
            }
            set
            {
                if (CompetitionId == 6)
                    tbCelCompetitionText.Text = value;                    
            }
        }

        public bool CompFromOlymp
        {
            set { lblCompFromOlymp.Visible = value; }           
        }

        public int? LanguageId
        {
            get { return ComboServ.GetComboIdInt(cbLanguage); }
            set { ComboServ.SetComboId(cbLanguage, value); }
        }

        public bool HasOriginals
        {
            get { return chbHasOriginals.Checked; }
            set { chbHasOriginals.Checked = value; }
        }

        public bool IsCommonRussianCompetition
        {
            get { return chbIsCommonRussianCompetition.Checked; }
            set { chbIsCommonRussianCompetition.Checked = value; }
        }

        public double? Priority
        {
            get
            {
                double j;
                if (double.TryParse(tbPriority.Text.Trim(), out j))
                    return j;
                else
                    return null;
            }
            set { tbPriority.Text = Util.ToStr(value); }
        }        
    }
}
