using HslCommunication.LogNet;
using SharpNodeSettings.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;


/*****************************************************************************************************************
 * 
 *    说明：本示例的作用是根据配置文件信息，生成设备数据，所有的设备数据将会被写入到Redis中
 * 
 *    前提：您需要在您的电脑上安装Redis，Redis服务器的下载地址为：https://github.com/MicrosoftArchive/redis/releases
 *           如果没有可使用的redis服务器，请换个项目运行，运行 SampleServer 项目或是OPC UA项目
 * 
 *****************************************************************************************************************/




namespace SharpNodeSettings.RedisServer
{
    class Program
    {
        static void Main( string[] args )
        {
            // 创建Redis
            ConnectionMultiplexer redis = null;
            try
            {
                redis = ConnectionMultiplexer.Connect( "127.0.0.1:6379" );
            }
            catch
            {
                Console.WriteLine( "本次数据创建失败，请稍候重试，具体原因。" );
                Console.ReadLine( );
                return;
            }

            // 创建日志
            ILogNet logNet = new LogNetSingle( "log.txt" );
            logNet.BeforeSaveToFile += LogNet_BeforeSaveToFile;

            SharpNodeServer sharpNodeServer = new SharpNodeServer( );
            sharpNodeServer.LogNet = logNet;
            sharpNodeServer.WriteCustomerData = ( Device.DeviceCore deviceCore, string name ) =>
            {
                try
                {
                    IDatabase redisDb = redis.GetDatabase( );
                    redisDb.StringSet( string.Join( ":", deviceCore.DeviceNodes ) + ":" + name, deviceCore.GetStringValueByName( name ) );
                }
                catch
                {
                    logNet.WriteError( "写入Redis失败" );
                }
            };
            // 加载配置文件之前设置redis写入方法
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
