using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Opc.Ua;
using Opc.Ua.Configuration;
using Opc.Ua.Server.Controls;

namespace SharpNodeSettings.OpcUaServer
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main( )
        {
            // Initialize the user interface.
            Application.EnableVisualStyles( );
            Application.SetCompatibleTextRenderingDefault( false );

            ApplicationInstance.MessageDlg = new ApplicationMessageDlg( );
            ApplicationInstance application = new ApplicationInstance( );
            application.ApplicationType = ApplicationType.Server;
            application.ConfigSectionName = "SharpNodeSettingsServer";

            try
            {

                // load the application configuration.
                application.LoadApplicationConfiguration( false ).Wait( );

                // check the application certificate.
                bool certOk = application.CheckApplicationInstanceCertificate( false, 0 ).Result;
                if (!certOk)
                {
                    throw new Exception( "Application instance certificate invalid!" );
                }

                // start the server.
                application.Start( new SharpNodeSettingsServer( ) ).Wait( );

                // run the application interactively.
                Application.Run( new ServerForm( application ) );
            }
            catch (Exception e)
            {
                ExceptionDlg.Show( application.ApplicationName, e );
            }
        }
    }
}
