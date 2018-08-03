using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SharpNodeSettings.Node.NodeBase
{
    /// <summary>
    /// 一个接口，表示该对象就有和Xml元素相互转换的能力
    /// </summary>
    interface IXmlConvert
    {

        /// <summary>
        /// 对象解析为Xml元素，方便的存储
        /// </summary>
        /// <returns>包含节点信息的Xml元素</returns>
        XElement ToXmlElement( );

        /// <summary>
        /// 对象从xml元素解析，初始化指定的数据
        /// </summary>
        /// <param name="element">包含节点信息的Xml元素</param>
        void LoadByXmlElement( XElement element );

    }
}
