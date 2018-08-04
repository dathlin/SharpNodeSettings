using SharpNodeSettings.Node.NodeBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SharpNodeSettings.Node.Device
{
    public class NodeSiemens : DeviceNode, IXmlConvert
    {
        
        #region Constructor

        /// <summary>
        /// 使用默认的参数实例化一个设备信息
        /// </summary>
        public NodeSiemens( )
        {
            Name = "西门子设备";
            Description = "此设备安装在角落，编号0001";
            DeviceType = DeviceNode.Siemens;

            IpAddress = "192.168.0.3";
            Port = 102;

            PlcType = PLC1200;
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
        /// 选择的是什么类型的PLC
        /// </summary>
        public string PlcType { get; set; }


        #endregion

        #region Xml Interface


        /// <summary>
        /// 对象从xml元素解析，初始化指定的数据
        /// </summary>
        /// <param name="element">包含节点信息的Xml元素</param>
        public override void LoadByXmlElement( XElement element )
        {
            base.LoadByXmlElement( element );
            IpAddress     = element.Attribute( "IpAddress" ).Value;
            Port          = int.Parse( element.Attribute( "Port" ).Value );
            PlcType       = element.Attribute( "PlcType" ).Value;
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
            element.SetAttributeValue( "PlcType", PlcType );
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
            list.Add( NodeClassRenderItem.CreatePlcMode( PlcType ) );

            return list;
        }

        #endregion

        #region Const Plc Types


        public const string PLC300 = "S7-300";

        public const string PLC1200 = "S7-1200";

        public const string PLC1500 = "S7-1500";

        public const string PLC200Smart = "S7-200Smart";

        public const string PLCFW = "Fetch/Write";

        #endregion

        #region Object Override

        /// <summary>
        /// 返回表示当前对象的字符串
        /// </summary>
        /// <returns>字符串信息</returns>
        public override string ToString( )
        {
            return "[西门子设备] " + Name;
        }

        #endregion
    }
}
