using SharpNodeSettings.Node.NodeBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SharpNodeSettings.Node.Server
{
    /// <summary>
    /// 服务器节点的基类，包含了端口号信息，服务器类型，服务器创建时间等基本的要素
    /// </summary>
    public class ServerNode : NodeClass
    {

        #region Constructor

        /// <summary>
        /// 实例化一个默认的对象
        /// </summary>
        public ServerNode( )
        {
            Port = 502;
            CreateTime = DateTime.Now;
            NodeType = NodeClassInfo.ServerNode;
            Description = "这是一个Alien服务器";
            NodeHead = "ServerNode";
        }


        #endregion

        /// <summary>
        /// 当前主站的端口号信息
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 服务器的创建日期
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 服务器的类别
        /// </summary>
        public int ServerType { get; set; }


        #region Override Method



        /// <summary>
        /// 对象解析为Xml元素，方便的存储
        /// </summary>
        /// <returns>包含节点信息的Xml元素</returns>
        public override XElement ToXmlElement( )
        {
            XElement element = base.ToXmlElement( );
            element.SetAttributeValue( "CreateTime", CreateTime.ToString( ) ); ;
            element.SetAttributeValue( "Port", Port );
            element.SetAttributeValue( "ServerType", ServerType );
            return element;
        }

        /// <summary>
        /// 对象从xml元素解析，初始化指定的数据
        /// </summary>
        /// <param name="element">包含节点信息的Xml元素</param>
        public override void LoadByXmlElement( XElement element )
        {
            base.LoadByXmlElement( element );
            Port = Convert.ToInt32( element.Attribute( "Port" ).Value );
            CreateTime = DateTime.Parse( element.Attribute( "CreateTime" ).Value );
            ServerType = int.Parse( element.Attribute( "ServerType" ).Value );
        }


        /// <summary>
        /// 获取用于在数据表信息中显示的键值数据对信息
        /// </summary>
        /// <returns>键值数据对列表</returns>
        public override List<NodeClassRenderItem> GetNodeClassRenders( )
        {
            var list = base.GetNodeClassRenders( );
            list.Add( NodeClassRenderItem.CreateIpPort( Port ) );
            list.Add( NodeClassRenderItem.CreateTime( CreateTime ) );
            list.Add( new NodeClassRenderItem( "服务器类型", ServerType.ToString( ) ) );
            return list;
        }


        #endregion
        


        #region Const Define

        /// <summary>
        /// Modbus服务器
        /// </summary>
        public const int ModbusServer = 1;

        /// <summary>
        /// 三菱的Qna兼容3E帧协议的客户端
        /// </summary>
        public const int AlienServer = 2;
        

        #endregion
        
    }
}
