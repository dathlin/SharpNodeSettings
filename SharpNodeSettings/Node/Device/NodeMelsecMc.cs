using SharpNodeSettings.Node.NodeBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SharpNodeSettings.Node.Device
{
    /// <summary>
    /// 实例化一个三菱MC协议的节点对象的设备
    /// </summary>
    public class NodeMelsecMc : DeviceNode, IXmlConvert
    {
        #region Constructor

        /// <summary>
        /// 使用默认的参数实例化一个设备信息
        /// </summary>
        public NodeMelsecMc( )
        {
            Name = "三菱设备";
            Description = "此设备安装在角度，编号0001";
            DeviceType = DeviceNode.MelsecMcQna3E;

            IpAddress = "192.168.0.3";
            Port = 6000;

            IsBinary = true;
        }


        #endregion

        #region Public Properties


        /// <summary>
        /// 设备的Ip地址
        /// </summary>
        public string IpAddress { get; set; }


        /// <summary>
        /// 设备的端口号
        /// </summary>
        public int Port { get; set; }


        /// <summary>
        /// 网络号
        /// </summary>
        public byte NetworkNumber { get; set; } = 0x00;

        /// <summary>
        /// 网络站号
        /// </summary>
        public byte NetworkStationNumber { get; set; } = 0x00;

        /// <summary>
        /// 是否是二进制通讯
        /// </summary>
        public bool IsBinary { get; set; }


        #endregion

        #region Xml Interface


        /// <summary>
        /// 对象从xml元素解析，初始化指定的数据
        /// </summary>
        /// <param name="element">包含节点信息的Xml元素</param>
        public override void LoadByXmlElement( XElement element )
        {
            base.LoadByXmlElement( element );
            IpAddress = element.Attribute( "IpAddress" ).Value;
            Port = int.Parse( element.Attribute( "Port" ).Value );
            NetworkNumber = byte.Parse( element.Attribute( "NetworkNumber" ).Value );
            NetworkStationNumber = byte.Parse( element.Attribute( "NetworkStationNumber" ).Value );
            IsBinary = bool.Parse( element.Attribute( "IsBinary" ).Value );
        }

        /// <summary>
        /// 对象解析为Xml元素，方便的存储
        /// </summary>
        /// <returns>包含节点信息的Xml元素</returns>
        public override XElement ToXmlElement( )
        {
            XElement element = base.ToXmlElement( );
            element.SetAttributeValue( "IpAddress", IpAddress );
            element.SetAttributeValue( "Port", Port );
            element.SetAttributeValue( "NetworkNumber", NetworkNumber );
            element.SetAttributeValue( "NetworkStationNumber", NetworkStationNumber );
            element.SetAttributeValue( "IsBinary", IsBinary.ToString( ) );
            return element;
        }

        #endregion

        #region Overide Method

        /// <summary>
        /// 获取用于在数据表信息中显示的键值数据对信息
        /// </summary>
        /// <returns>键值数据对列表</returns>
        public override List<NodeClassRenderItem> GetNodeClassRenders( )
        {
            var list = base.GetNodeClassRenders( );
            list.Add( NodeClassRenderItem.CreateIpAddress( IpAddress ) );
            list.Add( NodeClassRenderItem.CreateIpPort( Port ) );
            list.Add( new NodeClassRenderItem( "网络号", NetworkNumber.ToString( ) ) );
            list.Add( new NodeClassRenderItem( "网络站号", NetworkStationNumber.ToString( ) ) );
            list.Add( new NodeClassRenderItem( "是否二进制", IsBinary.ToString( ) ) );

            return list;
        }


        #endregion

        #region Object Override

        /// <summary>
        /// 返回表示当前对象的字符串
        /// </summary>
        /// <returns>字符串信息</returns>
        public override string ToString( )
        {
            return "[三菱设备] " + Name;
        }

        #endregion
    }

}
