using SharpNodeSettings.Node.Regular;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SharpNodeSettings
{
    /// <summary>
    /// 节点配置类的工具辅助类
    /// </summary>
    public class Util
    {












        #region Static Method

        /// <summary>
        /// 子窗口的图标显示信息
        /// </summary>
        /// <returns></returns>
        public static Icon GetWinformICon( )
        {
            return Icon.ExtractAssociatedIcon( Application.ExecutablePath );
        }



        /// <summary>
        /// 解析一个配置文件中的所有的规则解析，并返回一个词典信息
        /// </summary>
        /// <param name="nodeClass">配置文件的根信息</param>
        /// <returns>词典</returns>
        public static Dictionary<string, List<RegularItemNode>> ParesRegular( XElement nodeClass )
        {
            Dictionary<string, List<RegularItemNode>>  regularkeyValuePairs = new Dictionary<string, List<RegularItemNode>>( );
            foreach (var xmlNode in nodeClass.Elements( ))
            {
                if (xmlNode.Attribute( "Name" ).Value == "Regular")
                {
                    foreach (XElement element in nodeClass.Elements( "RegularNode" ))
                    {
                        List<RegularItemNode> itemNodes = new List<RegularItemNode>( );
                        foreach (XElement xmlItemNode in element.Elements( "RegularItemNode" ))
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
            return regularkeyValuePairs;
        }

        #endregion
    }
}
