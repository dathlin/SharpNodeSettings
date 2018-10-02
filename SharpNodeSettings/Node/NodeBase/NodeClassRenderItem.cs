using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpNodeSettings.Node.NodeBase
{
    /// <summary>
    /// 单个节点的单个数据对象
    /// </summary>
    public class NodeClassRenderItem
    {
        #region Constructor

        /// <summary>
        /// 实例化一个默认的对象
        /// </summary>
        public NodeClassRenderItem( )
        {
            this.ValueName = "Name";
            this.Value = "Value";
        }


        /// <summary>
        /// 实例化一个对象，需要指定当前的键值信息
        /// </summary>
        /// <param name="valueName">名称</param>
        /// <param name="value">值</param>
        public NodeClassRenderItem( string valueName, string value )
        {
            this.ValueName = valueName;
            this.Value = value;
        }



        #endregion



        /// <summary>
        /// 数据名称
        /// </summary>
        public string ValueName { get; set; }

        /// <summary>
        /// 数据值
        /// </summary>
        public string Value { get; set; }


        #region Override 

        /// <summary>
        /// 返回表示当前对象的字符串
        /// </summary>
        /// <returns>字符串信息</returns>
        public override string ToString( )
        {
            return $"Name:[{ValueName}] Value:[{Value}]";
        }

        #endregion


        #region Static Resource

        /// <summary>
        /// 创建一个显示的节点对象
        /// </summary>
        /// <param name="value">节点值</param>
        /// <returns>键值对象</returns>
        public static NodeClassRenderItem CreatNodeeName( string value )
        {
            return new NodeClassRenderItem( "节点名称", value );
        }

        /// <summary>
        /// 创建一个显示描述的键值对象
        /// </summary>
        /// <param name="description">描述信息</param>
        /// <returns>键值对象</returns>
        public static NodeClassRenderItem CreateNodeDescription( string description )
        {
            return new NodeClassRenderItem( "节点描述", description );
        }

        /// <summary>
        /// 创建一个显示的Ip地址的键值对象
        /// </summary>
        /// <param name="ip">Ip地址</param>
        /// <returns>键值对象</returns>
        public static NodeClassRenderItem CreateIpAddress( string ip )
        {
            return new NodeClassRenderItem( "Ip地址", ip );
        }

        /// <summary>
        /// 创建一个显示端口信息的键值对象
        /// </summary>
        /// <param name="port">端口号</param>
        /// <returns>键值对象</returns>
        public static NodeClassRenderItem CreateIpPort( int port )
        {
            return new NodeClassRenderItem( "以太网端口号", port.ToString( ) );
        }


        /// <summary>
        /// 创建一个显示超时时间信息的键值对象
        /// </summary>
        /// <param name="timeout">超时时间，单位 ms</param>
        /// <returns>键值对象</returns>
        public static NodeClassRenderItem CreateConnectTimeOut( int timeout )
        {
            return new NodeClassRenderItem( "连接超时", timeout.ToString( ) );
        }


        /// <summary>
        /// 创建一个显示创建时间信息的键值对象
        /// </summary>
        /// <param name="time">创建时间</param>
        /// <returns>键值对象</returns>
        public static NodeClassRenderItem CreateTime( DateTime time )
        {
            return new NodeClassRenderItem( "创建时间", time.ToString( ) );
        }

        /// <summary>
        /// 创建一个显示站号信息的键值对象
        /// </summary>
        /// <param name="station">站号信息</param>
        /// <returns>键值对象</returns>
        public static NodeClassRenderItem CreateStation( int station )
        {
            return new NodeClassRenderItem( "设备站号", station.ToString( ) );
        }


        /// <summary>
        /// 创建一个显示安装地点的键值对象
        /// </summary>
        /// <param name="place">安装地址信息</param>
        /// <returns>键值对象</returns>
        public static NodeClassRenderItem CreateInstallationPlace(string place )
        {
            return new NodeClassRenderItem( "安装地点", place );
        }


        /// <summary>
        /// 创建一个显示安装日期的键值对象
        /// </summary>
        /// <param name="date">安装日期</param>
        /// <returns>键值对象</returns>
        public static NodeClassRenderItem CreateInstallationDate( DateTime date )
        {
            return new NodeClassRenderItem( "安装日期", date.Date.ToString( ) );
        }


        /// <summary>
        /// 创建一个显示唯一标识的键值对象
        /// </summary>
        /// <param name="uniqueId">唯一信息</param>
        /// <returns>键值对象</returns>
        public static NodeClassRenderItem CreateUniqueId( string uniqueId )
        {
            return new NodeClassRenderItem( "唯一标识", uniqueId );
        }

        /// <summary>
        /// 创建一个显示是否从0开始的键值对象
        /// </summary>
        /// <param name="value">是否值</param>
        /// <returns>键值对象</returns>
        public static NodeClassRenderItem CreateIsAddressStartWithZero(bool value )
        {
            return new NodeClassRenderItem( "是否从0开始", value.ToString() );
        }

        /// <summary>
        /// 创建一个显示是数据格式的键值对象
        /// </summary>
        /// <param name="value">是否值</param>
        /// <returns>键值对象</returns>
        public static NodeClassRenderItem CreateDataFormat( int value )
        {
            return new NodeClassRenderItem( "数据的格式", value.ToString( ) );
        }

        /// <summary>
        /// 创建一个显示是否字符串的键值对象
        /// </summary>
        /// <param name="value">是否值</param>
        /// <returns>键值对象</returns>
        public static NodeClassRenderItem CreateIsStringReverse( bool value )
        {
            return new NodeClassRenderItem( "是否字符串反转", value.ToString( ) );
        }

        /// <summary>
        /// 创建一个显示PLC型号的键值对象
        /// </summary>
        /// <param name="plcMode">PLC的型号</param>
        /// <returns>键值对象</returns>
        public static NodeClassRenderItem CreatePlcMode(string plcMode )
        {
            return new NodeClassRenderItem( "PLC型号", plcMode );
        }

        /// <summary>
        /// 创建一个显示密码的键值对象
        /// </summary>
        /// <param name="password">密码</param>
        /// <returns>键值对象</returns>
        public static NodeClassRenderItem CreatePassword( string password )
        {
            return new NodeClassRenderItem( "密码", password );
        }

        #endregion
    }
}
