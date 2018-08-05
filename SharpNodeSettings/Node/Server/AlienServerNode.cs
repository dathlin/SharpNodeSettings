using SharpNodeSettings.Node.NodeBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SharpNodeSettings.Node.Server
{
    public class AlienServerNode : ServerNode
    {
        public AlienServerNode( )
        {
            ServerType = ServerNode.AlienServer;
            
            Name = "异形服务器";
            Description = "这是一个异形服务器";
        }

        /// <summary>
        /// 密码，6位数，为空的话默认都是0x00
        /// </summary>
        public string Password { get; set; }


        #region Override Method

        /// <summary>
        /// 获取用于在数据表信息中显示的键值数据对信息
        /// </summary>
        /// <returns>键值数据对列表</returns>
        public override List<NodeClassRenderItem> GetNodeClassRenders( )
        {
            var list = base.GetNodeClassRenders( );
            list.Add( NodeClassRenderItem.CreateIpPort( Port ) );
            list.Add( NodeClassRenderItem.CreatePassword( Password ) );
            return list;
        }

        /// <summary>
        /// 对象解析为Xml元素，方便的存储
        /// </summary>
        /// <returns>包含节点信息的Xml元素</returns>
        public override XElement ToXmlElement( )
        {
            XElement element = base.ToXmlElement( );
            element.SetAttributeValue( "Password", Password ); ;
            element.SetAttributeValue( "Port", Port );
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
            Password = element.Attribute( "Password" ).Value;
        }

        #endregion

    }
}
