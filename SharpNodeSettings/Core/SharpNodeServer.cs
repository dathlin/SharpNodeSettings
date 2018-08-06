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

namespace SharpNodeSettings.Core
{
    /// <summary>
    /// 节点配置类的服务器对象，包含了自动解析配置文件，自动启动设备，并创建一个数据访问器服务器
    /// </summary>
    public class SharpNodeServer
    {

        public SharpNodeServer( string fileName )
        {
            this.fileName = fileName;
            deviceCores = new List<DeviceCore>( );
            settingsLock = new HslCommunication.Core.SimpleHybirdLock( );
            LoadByXmlFile( );
        }



        public void ServerStart(int port )
        {

        }


        private void LoadByXmlFile( )
        {
            if (File.Exists( fileName ))
            {
                XElement element = XElement.Load( fileName );
                try
                {
                    settingsLock.Enter( );
                    ParesRegular( element );
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
                    paths.Add( element.Name.ToString() );
                    element = element.Parent;
                }
                else
                {
                    break;
                }
            }
            return paths.ToArray( );
        }


        private Dictionary<string, List<RegularItemNode>> regularkeyValuePairs;

        private void ParesRegular( XElement nodeClass )
        {
            regularkeyValuePairs = new Dictionary<string, List<RegularItemNode>>( );
            foreach (var xmlNode in nodeClass.Elements( ))
            {
                if (xmlNode.Attribute( "Name" ).Value == "Regular")
                {
                    foreach(XElement element in nodeClass.Elements( "RegularNode" ))
                    {
                        List<RegularItemNode> itemNodes = new List<RegularItemNode>( );
                        foreach(XElement xmlItemNode in element.Elements( "RegularItemNode" ))
                        {
                            RegularItemNode regularItemNode = new RegularItemNode( );
                            regularItemNode.LoadByXmlElement( xmlItemNode );
                            itemNodes.Add( regularItemNode );
                        }

                        if (regularkeyValuePairs.ContainsKey( element.Attribute( "Name" ).Value ))
                        {
                            regularkeyValuePairs[element.Attribute( "Name" ).Value] = itemNodes;
                        }
                        else
                        {
                            regularkeyValuePairs.Add( element.Attribute( "Name" ).Value, itemNodes );
                        }
                    }
                }
            }
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


                DeviceCore deviceReal = DeviceCore.CreateFromXElement( device );
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

                    deviceReal.DeviceNodes = GetXmlPath( device );
                    deviceCores.Add( deviceReal );
                }
            }
        }

        #region Private Member

        private string fileName = string.Empty;
        private List<DeviceCore> deviceCores;
        private HslCommunication.Core.SimpleHybirdLock settingsLock; 

        #endregion
    }
}
