using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using SharpNodeSettings.Node.NodeBase;
using SharpNodeSettings.Node.Regular;
using SharpNodeSettings.Node.Request;
using SharpNodeSettings.Device;
using HslCommunication.ModBus;
using SharpNodeSettings.Node.Server;
using HslCommunication.LogNet;
using HslCommunication.Core.Net;
using System.Threading;
using HslCommunication.Enthernet;

namespace SharpNodeSettings.Core
{
    /// <summary>
    /// 节点配置类的服务器对象，包含了自动解析配置文件，自动启动设备，并创建一个数据访问器服务器
    /// </summary>
    public class SharpNodeServer
    {
        /// <summary>
        /// 实例化一个对象，需要传入配置文件的路径，根据配置文件的信息即可创建一个节点服务器
        /// </summary>
        /// <param name="fileName">配置文件的路径</param>
        public SharpNodeServer( string fileName )
        {
            if (!File.Exists( fileName ))
            {
                System.Windows.Forms.MessageBox.Show( "Can't find settings file,  click ok to quit application" );
                System.Windows.Forms.Application.Exit( );
                return;
            }

            this.fileName = fileName;
            token = Guid.Empty;
            deviceCores = new List<DeviceCore>( );
            modbusTcpServers = new List<ModbusTcpServer>( );
            dictDeviceCores = new Dictionary<string, DeviceCore>( );
            settingsLock = new HslCommunication.Core.SimpleHybirdLock( );
            LoadByXmlFile( );
        }


        /// <summary>
        /// 启动服务器的后台存储
        /// </summary>
        /// <param name="port"></param>
        public void ServerStart(int port )
        {
            simplifyServer = new NetSimplifyServer( );
            simplifyServer.Token = this.token;
            simplifyServer.LogNet = logNet;
            simplifyServer.ReceiveStringEvent += SimplifyServer_ReceiveStringEvent;
            simplifyServer.ServerStart( port );

            for (int i = 0; i < deviceCores.Count; i++)
            {
                deviceCores[i].StartRead( );
            }
        }




        private void SimplifyServer_ReceiveStringEvent( AppSession session, HslCommunication.NetHandle handle, string data )
        {
            if (handle == 0)
            {
                // 请求配置文件
                simplifyServer.SendMessage( session, handle, xElementSettings != null ? xElementSettings.ToString( ) : string.Empty );
            }
            else if(handle == 1)
            {
                // 请求设备的所有数据
                string[] nodePath = data.Split( new char[] { '\\', ':', '-', '.', '_', '/' } );
                string response = string.Empty;

                for (int i = 0; i < deviceCores.Count; i++)
                {
                    if (deviceCores[i].IsCurrentDevice( nodePath ))
                    {
                        response = deviceCores[i].GetValueByName( nodePath );
                        break;
                    }
                }

                logNet?.WriteDebug( "请求设备信息：" + data );
                simplifyServer.SendMessage( session, handle, response );
            }
            else
            {
                simplifyServer.SendMessage( session, handle, data );
            }
        }



        #region Public Properties

        /// <summary>
        /// 设备解析完成的值对应的额外的操作方法，传递的参数有节点路径值，变量名，动态值
        /// </summary>
        public Action<string[], string, dynamic> WriteCustomerData { get; set; }
        /// <summary>
        /// 当前系统的令牌
        /// </summary>
        public Guid Token { get => token; set => token = value; }

        /// <summary>
        /// 系统的日志信息
        /// </summary>
        public ILogNet LogNet { get => logNet; set => logNet = value; }

        #endregion

        #region Xml Load

        private void LoadByXmlFile( )
        {
            if (File.Exists( fileName ))
            {
                XElement element = XElement.Load( fileName );
                xElementSettings = element;

                try
                {
                    settingsLock.Enter( );
                    regularkeyValuePairs = Util.ParesRegular( element );
                    ParseNodeItem( element );
                    settingsLock.Leave( );
                }
                catch
                {
                    settingsLock.Leave( );
                    throw;
                }
            }
        }


        private string[] GetXmlPath( XElement element )
        {
            List<string> paths = new List<string>( );
            while (true)
            {
                if(element != null)
                {
                    if(element.Attribute( "Name" ) == null)
                    {
                        break;
                    }
                    paths.Add( element.Attribute("Name").Value );
                    element = element.Parent;
                }
                else
                {
                    break;
                }
            }
            paths.Reverse( );
            return paths.ToArray( );
        }
        


        private void ParseNodeItem( XElement nodeClass )
        {
            foreach (var xmlNode in nodeClass.Elements( ))
            {
                if (xmlNode.Name == "NodeClass")
                {
                    NodeClass nClass = new NodeClass( );
                    nClass.LoadByXmlElement( xmlNode );
                    ParseNodeItem( xmlNode );                   // 继续解析子项的内容
                }
                else if (xmlNode.Name == "DeviceNode")
                {
                    AddDeviceCore( xmlNode );
                }
                else if (xmlNode.Name == "ServerNode")
                {
                    
                }
            }
        }

        private void AddDeviceCore( XElement device )
        {
            if (device.Name == "DeviceNode")
            {
                // 提取名称和描述信息
                string name = device.Attribute( "Name" ).Value;
                string description = device.Attribute( "Description" ).Value;


                DeviceCore deviceReal = Util.CreateFromXElement( device );
                if (deviceReal != null)
                {
                    // 添加所有Request的regular信息
                    foreach (var request in deviceReal.Requests)
                    {
                        if (!string.IsNullOrEmpty( request.PraseRegularCode ))
                        {
                            if (regularkeyValuePairs.ContainsKey( request.PraseRegularCode ))
                            {
                                request.RegularNodes = regularkeyValuePairs[request.PraseRegularCode];
                            }
                        }
                    }

                    deviceReal.WriteCustomerData = this.WriteCustomerData;
                    deviceReal.DeviceNodes = GetXmlPath( device );
                    deviceCores.Add( deviceReal );
                    if (this.dictDeviceCores.ContainsKey( deviceReal.UniqueId ))
                    {
                        this.logNet?.WriteError( "设备唯一码重复，无法添加集合，ID: " + deviceReal.UniqueId );
                    }
                    else
                    {
                        this.dictDeviceCores.Add( deviceReal.UniqueId, deviceReal );
                    }
                }
            }
        }

        private void AddServer( XElement xmlNode )
        {
            int serverType = int.Parse( xmlNode.Attribute( "ServerType" ).Value );
            if (serverType == ServerNode.ModbusServer)
            {
                NodeModbusServer serverNode = new NodeModbusServer( );
                serverNode.LoadByXmlElement( xmlNode );
                ModbusTcpServer server = new ModbusTcpServer( );
                server.LogNet = logNet;
                server.Port = serverNode.Port;
                this.modbusTcpServers.Add( server );
            }
            else if(serverType == ServerNode.AlienServer)
            {
                AlienServerNode alienNode = new AlienServerNode( );
                alienNode.LoadByXmlElement( xmlNode );

                NetworkAlienClient networkAlien = new NetworkAlienClient( );
                networkAlien.LogNet = logNet;
                if (!string.IsNullOrEmpty( alienNode.Password ))
                {
                    networkAlien.SetPassword( Encoding.ASCII.GetBytes( alienNode.Password ) );
                }
                networkAlien.Port = alienNode.Port;
                networkAlien.OnClientConnected += NetworkAlien_OnClientConnected;
                this.networkAliens.Add( networkAlien );
            }
        }

        private void NetworkAlien_OnClientConnected( NetworkAlienClient networkAlien, AlienSession session )
        {
            bool isExist = false;

            if (dictDeviceCores.ContainsKey( session.DTU ))
            {
                dictDeviceCores[session.DTU].SetAlineSession( session );
                isExist = true;
            }

            if (!isExist)
            {
                // 退出
                session.Socket?.Close( );
                networkAlien.AlienSessionLoginOut( session );
            }
        }

        #endregion

        #region Private Member

        private Guid token;
        private ILogNet logNet;
        private string fileName = string.Empty;
        private Dictionary<string, List<RegularItemNode>> regularkeyValuePairs;            // 所有的解析规则列表的对象
        private List<ModbusTcpServer> modbusTcpServers;                                    // 所有的ModbusTcp服务器
        private List<DeviceCore> deviceCores;                                              // 所有的设备对象
        private Dictionary<string, DeviceCore> dictDeviceCores;                            // 所有的设备客户端词典列表，加速设备查找速度
        private List<NetworkAlienClient> networkAliens;                                    // 所有的异形客户端的列表
        private AutoResetEvent autoResetQuit;                                              // 退出系统的时候的同步锁
        private HslCommunication.Core.SimpleHybirdLock settingsLock;                       // 配置文件加载的锁
        private NetSimplifyServer simplifyServer;                                          // 同步数据访问服务器
        private XElement xElementSettings;                                                 // 全部的配置文件信息

        #endregion
    }
}
