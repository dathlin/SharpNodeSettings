using SharpNodeSettings.Node.NodeBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SharpNodeSettings.Node.Request
{
    /// <summary>
    /// 通用的客户端模型，指示了一般的客户端模式下的，一次数据请求，一个客户端可以进行多次的数据请求
    /// </summary>
    public class DeviceRequest : NodeClass, IXmlConvert
    {
        #region Constructor

        /// <summary>
        /// 实例化一个对象
        /// </summary>
        public DeviceRequest( )
        {
            Name = "数据请求";
            Description = "一次完整的数据请求";
            Address = "123";
            Length = 10;
            CaptureInterval = 1000;
            PraseRegularCode = "ABCDEFG";
            NodeType = NodeClassInfo.DeviceRequest;
            NodeHead = "DeviceRequest";
            LastActiveTime = DateTime.Now.AddDays( -1 );           // 自动设置为一天以前
        }

        #endregion

        #region Node Properties


        /// <summary>
        /// 变量的地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 读取的数据长度
        /// </summary>
        public ushort Length { get; set; }

        /// <summary>
        /// 本次请求的时间间隔，单位为毫秒
        /// </summary>
        public int CaptureInterval { get; set; }

        /// <summary>
        /// 本次请求解析字节数据的规则
        /// </summary>
        public string PraseRegularCode { get; set; }

        #endregion

        #region Runtime Properties
        
        /// <summary>
        /// 上一次读取数据的时间节点
        /// </summary>
        public DateTime LastActiveTime { get; set; }


        /// <summary>
        /// 本次请求所有关联的节点信息
        /// </summary>
        public List<Regular.RegularItemNode> RegularNodes { get; set; }

        /// <summary>
        /// 本次所请求的所有的字节数据在缓存中的位置
        /// </summary>
        public int WriteByteIndex { get; set; }

        #endregion

        #region Xml Support

        /// <summary>
        /// 对象从xml元素解析，初始化指定的数据
        /// </summary>
        /// <param name="element">包含节点信息的Xml元素</param>
        public override void LoadByXmlElement( XElement element )
        {
            base.LoadByXmlElement( element );
            Address = element.Attribute( "Address" ).Value;
            Length = ushort.Parse( element.Attribute( "Length" ).Value );
            CaptureInterval = int.Parse( element.Attribute( "CaptureInterval" ).Value );
            PraseRegularCode = element.Attribute( "PraseRegularCode" ).Value;
        }



        /// <summary>
        /// 对象解析为Xml元素，方便的存储
        /// </summary>
        /// <returns>包含节点信息的Xml元素</returns>
        public override XElement ToXmlElement( )
        {
            XElement element = base.ToXmlElement( );
            element.SetAttributeValue( "Address", Address );
            element.SetAttributeValue( "Length", Length );
            element.SetAttributeValue( "CaptureInterval", CaptureInterval );
            element.SetAttributeValue( "PraseRegularCode", PraseRegularCode );
            
            return element;
        }


        #endregion

        #region Render Support
        
        /// <summary>
        /// 获取用于在数据表信息中显示的键值数据对信息
        /// </summary>
        /// <returns>键值数据对列表</returns>
        public override List<NodeClassRenderItem> GetNodeClassRenders( )
        {
            var result = base.GetNodeClassRenders( );
            result.Add( new NodeClassRenderItem( )
            {
                ValueName = "地址",
                Value = Address,
            } );
            result.Add( new NodeClassRenderItem( )
            {
                ValueName = "读取长度",
                Value = Length.ToString( ),
            } );
            result.Add( new NodeClassRenderItem( )
            {
                ValueName = "读取间隔",
                Value = CaptureInterval.ToString( ),
            } );
            result.Add( new NodeClassRenderItem( )
            {
                ValueName = "解析代号",
                Value = PraseRegularCode,
            } );

            return result;
        }

        #endregion
    }
}
