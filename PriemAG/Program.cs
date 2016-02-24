using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Configuration;

namespace Priem
{
    static class Program
    {
        public static MainForm mf;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //try
            //{
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                mf = new MainForm();
                Application.Run(mf);
            //}
            //catch (Exception exc)
            //{
            //    MessageBox.Show("Ошибка в приложении:\n", exc);
            //}
        }

        static void EncryptCS()
        {
            try
            {
                // Open the configuration file and retrieve 
                // the connectionStrings section.
                Configuration config = ConfigurationManager.
                    OpenExeConfiguration(ConfigurationUserLevel.None);

                ConnectionStringsSection section =
                    config.GetSection("connectionStrings")
                    as ConnectionStringsSection;

                if (!section.SectionInformation.IsProtected)
                {                    
                    // Encrypt the section.
                    section.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
                    // Save the current configuration.
                    config.Save();
                }               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
