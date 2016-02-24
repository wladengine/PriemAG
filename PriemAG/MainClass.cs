using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.ComponentModel;
using System.Linq;
using System.DirectoryServices.AccountManagement;

using BaseFormsLib;
using EducServLib;

namespace Priem
{    
    public static partial class MainClass
    {
        //test
        public static Form mainform;
        private static DBPriem _bdc = null;

        public static PriemType dbType;
        public static string connString;
        public static string connStringOnline;        
        
        public static string directory;
        public static string dirTemplates;
        public static string saveTempFolder;
        public static string userName;
       
        public static int studyLevelGroupId;
        public static int countryRussiaId;
        public static int educSchoolId;
        public static int pasptypeRFId;
        public static int olympSpbguId;

        public static bool IsTestDB { get; private set; }

        //GLOBALS
        //-----------------------------------------------------
        public static string sPriemYear;
        public static int iPriemYear;

        public static bool b1kCheckProtocolsEnabled;
        public static bool bMagCheckProtocolsEnabled;
        //-----------------------------------------------------

        public const string PriemYear = "2014";

        public static QueryBuilder qBuilder;

        //пользовательские настройки
        public static ConfigFile _config;
        
        private static DataRefreshHandler _drHandler;
        private static ProtocolRefreshHandler _prHandler;
        
        public static DBPriem Bdc
        {
            get { return _bdc; }          
        }

        /// <summary>
        /// opens DataBase
        /// </summary>
        /// <param name="connectionString"></param>
        public static void Init(Form mf)
        {
            try
            {
                _bdc = new DBPriem();
                _bdc.OpenDatabase(connString);

                mainform = mf;            
                userName = System.Environment.UserName;
                
                // database constant id
                using (PriemEntities context = new PriemEntities())
                {
                    //постоянный id страны Россия
                    countryRussiaId = 1;
                    //постоянный id типа уч.заведения Школа
                    educSchoolId = 1;
                    //постоянный id типа паспорта Паспорт РФ
                    pasptypeRFId = 1;
                    //постоянный id олимпиады СПбГУ
                    olympSpbguId = 3;

                    var dicSettings = context.C_AppSettings.Select(x => new { x.ParamKey, x.ParamValue }).ToList().ToDictionary(x => x.ParamKey, y => y.ParamValue);
                    sPriemYear = dicSettings.ContainsKey("PriemYear") ? dicSettings["PriemYear"] : DateTime.Now.Year.ToString();
                    iPriemYear = int.Parse(sPriemYear);

                    string tmp = dicSettings.ContainsKey("b1kCheckProtocolsEnabled") ? dicSettings["b1kCheckProtocolsEnabled"] : "False";
                    b1kCheckProtocolsEnabled = bool.Parse(tmp);

                    tmp = dicSettings.ContainsKey("bMagCheckProtocolsEnabled") ? dicSettings["bMagCheckProtocolsEnabled"] : "False";
                    bMagCheckProtocolsEnabled = bool.Parse(tmp);
                }

                //4 для аспирантуры
                studyLevelGroupId = 4;
              
                directory = string.Format(@"{0}\Priem", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));               
                saveTempFolder = string.Format(@"{0}\DocTempFiles\",Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                try
                {
                    // уточнить у Дениса, кто создавал эту папку!!!!! может будет ошибка
                    if (!Directory.Exists(directory))
                        Directory.CreateDirectory(directory);

                    // Determine whether the directory exists.
                    if (!Directory.Exists(saveTempFolder))
                        Directory.CreateDirectory(saveTempFolder);
                }
                catch (Exception e)
                {
                    WinFormsServ.Error("Ошибка при загрузке файла конфигурации: " + e.Message + (e.InnerException == null ? "" : "\nВнутреннее исключение: " + e.InnerException.Message));
                }      
                                
                //взяли конфиг
                _config = GetConfig();
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public static string GetUserName()
        {
            return GetADUserName(System.Environment.UserName);
        }
        public static string GetADUserName(string userName)
        {
            try
            {
                var ADPrincipal = new PrincipalContext(ContextType.Domain);
                UserPrincipal user = UserPrincipal.FindByIdentity(ADPrincipal, userName);

                if (user != null)
                    return user.DisplayName + " (" + userName + ")";
            }
            catch { }

            return userName;
        }

        //прочитали конфиг
        private static ConfigFile GetConfig()
        {
            string configFile = "config.xml";
            return new ConfigFile(directory, configFile);
        }

        public static void DeleteTempFiles()
        {
            try
            {
                foreach (string file in Directory.GetFiles(saveTempFolder))
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch
                    {
                    }
                }

                foreach (var file in new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)).GetFiles())
                {
                    if (file.Name.Contains("DocTempFiles"))
                        File.Delete(file.FullName);
                }
            }
            catch
            {
            }
        }

        public static string GetAbitNum(string abNum, string perNum)
        {
            return perNum + @"\"+ abNum;           
        }       

        public static string GetStringAbitNumber(string abitView)
        {
            return string.Format(" substring('000' + Convert(nvarchar(2), {0}.FacultyId), len('000' + Convert(nvarchar(2), {0}.FacultyId))-1, 2) + substring('000000' + Convert(nvarchar(5), {0}.RegNum), len('000000' + Convert(nvarchar(5), {0}.RegNum))-4, 5)", abitView);
        }

        public static bool CheckPersonBarcode(int? barcode)
        {
            if (barcode == null)
                return true;

            using (PriemEntities context = new PriemEntities())
            {
                int cnt = (from pers in context.Person
                           where pers.Barcode == barcode
                           select pers).Count();

                if (cnt > 0)
                    return false;
                else
                    return true;
            }
        }

        public static bool CheckAbitBarcode(int? barcode)
        {
            if (barcode == null)
                return true;

            using (PriemEntities context = new PriemEntities())
            {
                int cnt = (from abit in context.extAbitAspirant
                           where abit.Barcode == barcode
                           select abit).Count();

                if (cnt > 0)
                    return false;
                else
                    return true;
            }
        }

        public static IEnumerable<qEntry> GetEntry(PriemEntities context)
        {
            try
            {
                IEnumerable<qEntry> entry = context.qEntry;
                return entry.Where(c => c.StudyLevelGroupId == studyLevelGroupId);
            }
            catch (Exception exc)
            {
                WinFormsServ.Error("Ошибка qEntry " + exc.Message);
                return null;
            }      
        }

        public static string GetStLevelFilter(string tableName)
        {
            return string.Format(" AND {1}.StudyLevelGroupId = {0} ", studyLevelGroupId, tableName);          
        }
    }
}
