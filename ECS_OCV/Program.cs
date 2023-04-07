using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnifiedAutomation.UaBase;

namespace ECS_OCV
{
    internal static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new ECS_OCV());

            try
            {
                ApplicationLicenseManager.AddProcessLicenses(System.Reflection.Assembly.GetExecutingAssembly(), "License.lic");
                // Create the certificate if it does not exist yet
                ApplicationInstance.Default.AutoCreateCertificate = true;

                // start the application.
                ApplicationInstance.Default.Start(Program.Run, ApplicationInstance.Default);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        static void Run(object userState)
        {
            ApplicationInstance applicationInstance = userState as ApplicationInstance;
            System.Windows.Forms.Application.Run(new ECS_OCV(applicationInstance));
        }
    }
}
