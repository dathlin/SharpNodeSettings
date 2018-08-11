#define File
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;


namespace SharpNodeSettings.Tools
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main( )
        {
            Application.EnableVisualStyles( );
            Application.SetCompatibleTextRenderingDefault( false );

#if File
            // 从文件启动
            Application.Run( new SharpNodeSettings.View.FormNodeSetting( "settings.xml" ) );

#else
            // 下面的代码
            using(SharpNodeSettings.View.FormNodeSetting form = new SharpNodeSettings.View.FormNodeSetting( XElement.Load( "settings.xml" ) ))
            {
                if (form.ShowDialog( ) == DialogResult.OK)
                {
                    // 配置好的数据信息，在这种方式下可以实现远程配置的操作。
                    XElement xElement = form.XmlSettings;
                    MessageBox.Show( "success" );
                }
                else
                {
                    MessageBox.Show( "failed" );
                }
            }
            Application.Exit( );
#endif
            //SharpNodeSettings.View.FormSelectDevice selectDevice = new View.FormSelectDevice( );
            //if (selectDevice.ShowDialog( ) == DialogResult.OK)
            //{
            //    XElement xmlDevice = selectDevice.DeviceXml;
            //    // 设备的配置对象可用于存储，网络传输等等操作


            //    // 如果想要通过xml信息创建设备
            //    SharpNodeSettings.Device.DeviceCore deviceCore = SharpNodeSettings.Util.CreateFromXElement( xmlDevice );
            //    // 演示读取数据，此处有个问题在于如果是相同种类的PLC，应用还是很方便的，如果是不同种类的，地址模型就比较麻烦。
            //    HslCommunication.OperateResult<short> read = deviceCore.ReadWriteDevice.ReadInt16( "D100" );
            //}

        }
    }
}
