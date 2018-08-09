using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HslCommunication.LogNet;
using SharpNodeSettings.Core;


namespace SharpNodeSettings.SampleServer
{
    class Program
    {
        static void Main( string[] args )
        {
            // 创建日志
            ILogNet logNet = new LogNetSingle( "log.txt" );
            logNet.BeforeSaveToFile += LogNet_BeforeSaveToFile;

            SharpNodeServer sharpNodeServer = new SharpNodeServer( );
            sharpNodeServer.LogNet = logNet;
            sharpNodeServer.LoadByXmlFile( "settings.xml" );
            sharpNodeServer.ServerStart( 12345 );


            Console.ReadLine( );
        }

        private static void LogNet_BeforeSaveToFile( object sender, HslEventArgs e )
        {
            Console.WriteLine( e.HslMessage.ToString( ) );
        }
    }
}
