using SharpNodeSettings.Node;
using SharpNodeSettings.Node.NodeBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SharpNodeSettings.Node.Device
{
    /// <summary>
    /// 设备对象类，指明一个设备的所有信息
    /// </summary>
    public class DeviceNode : NodeClass
    {
        #region Constructor

        /// <summary>
        /// 实例化一个构造对象
        /// </summary>
        public DeviceNode( )
        {
            NodeType = NodeClassInfo.DeviceNode;
            NodeHead = "DeviceNode";
            CreateTime = DateTime.Now;
            ConnectTimeOut = 2000;
            InstallationDate = DateTime.Now;
        }

        #endregion

        #region Public Properties
        
        /// <summary>
        /// 设备的类别
        /// </summary>
        public int DeviceType { get; set; }
        
        /// <summary>
        /// 安装的时间
        /// </summary>
        public DateTime InstallationDate { get; set; }

        /// <summary>
        /// 连接超时的时间，单位毫秒
        /// </summary>
        public int ConnectTimeOut { get; set; }

        /// <summary>
        /// 服务器的创建日期
        /// </summary>
        public DateTime CreateTime { get; set; }

        #endregion

        #region Override Method


        /// <summary>
        /// 获取用于在数据表信息中显示的键值数据对信息
        /// </summary>
        /// <returns>键值数据对列表</returns>
        public override List<NodeClassRenderItem> GetNodeClassRenders( )
        {
            var list = base.GetNodeClassRenders( );
            list.Add( NodeClassRenderItem.CreateConnectTimeOut( ConnectTimeOut ) );
            list.Add( NodeClassRenderItem.CreateTime( CreateTime ) );
            list.Add( NodeClassRenderItem.CreateInstallationDate( InstallationDate ) );
            return list;
        }

        /// <summary>
        /// 对象解析为Xml元素，方便的存储
        /// </summary>
        /// <returns>包含节点信息的Xml元素</returns>
        public override XElement ToXmlElement( )
        {
            XElement element = base.ToXmlElement( );
            element.SetAttributeValue( "DeviceType", DeviceType );
            element.SetAttributeValue( "ConnectTimeOut", ConnectTimeOut );
            element.SetAttributeValue( "CreateTime", CreateTime.ToString( ) );
            element.SetAttributeValue( "InstallationDate", InstallationDate.ToString() );
            return element;
        }


        /// <summary>
        /// 对象从xml元素解析，初始化指定的数据
        /// </summary>
        /// <param name="element">包含节点信息的Xml元素</param>
        public override void LoadByXmlElement( XElement element )
        {
            base.LoadByXmlElement( element );
            DeviceType = int.Parse( element.Attribute( "DeviceType" ).Value );
            ConnectTimeOut = int.Parse( element.Attribute( "ConnectTimeOut" ).Value );
            CreateTime = DateTime.Parse( element.Attribute( "CreateTime" ).Value );
            InstallationDate = DateTime.Parse( element.Attribute( "InstallationDate" ).Value );
        }

        #endregion

        #region Const Define

        /// <summary>
        /// 空设备的节点
        /// </summary>
        public const int DeviceNone = 0;

        /// <summary>
        /// 三菱的Qna兼容3E帧协议的客户端
        /// </summary>
        public const int MelsecMcQna3E = 1;

        /// <summary>
        /// 常规的Modbus-Tcp客户端
        /// </summary>
        public const int ModbusTcpClient = 10;
        
        /// <summary>
        /// 异形的Modbus-Tcp客户端
        /// </summary>
        public const int ModbusTcpAlien = 20;

        /// <summary>
        /// 西门子的PLC设备
        /// </summary>
        public const int Siemens = 30;

        /// <summary>
        /// 欧姆龙的PLC设备
        /// </summary>
        public const int Omron = 40;

        /// <summary>
        /// 其他电脑的SimplifyNet服务器
        /// </summary>
        public const int SimplifyNet = 50;


        
        #endregion
        
    }
}
