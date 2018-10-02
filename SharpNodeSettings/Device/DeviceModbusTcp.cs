using HslCommunication.ModBus;
using SharpNodeSettings.Node.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SharpNodeSettings.Device
{
    /// <summary>
    /// Modbus-Tcp的设备信息，包含了核心的连接对象
    /// </summary>
    public class DeviceModbusTcp : DeviceCore
    {
        #region Constructor

        /// <summary>
        /// 实例化一个Modbus-Tcp的设备对象，从配置信息创建
        /// </summary>
        /// <param name="element">配置信息</param>
        public DeviceModbusTcp( XElement element )
        {
            NodeModbusTcpClient nodeModbus = new NodeModbusTcpClient( );
            LoadRequest( element );

            modbusTcp = new ModbusTcpNet( nodeModbus.IpAddress, nodeModbus.Port, nodeModbus.Station );
            modbusTcp.AddressStartWithZero = nodeModbus.IsAddressStartWithZero;
            modbusTcp.ConnectTimeOut = nodeModbus.ConnectTimeOut;
            modbusTcp.DataFormat = (HslCommunication.Core.DataFormat)nodeModbus.DataFormat;
            modbusTcp.IsStringReverse = nodeModbus.IsStringReverse;

            ByteTransform = modbusTcp.ByteTransform;
            UniqueId = modbusTcp.ConnectionId;
            ReadWriteDevice = modbusTcp;

            TypeName = "Modbus-Tcp设备";
        }


        #endregion
        
        #region Protect Override

        /// <summary>
        /// 在启动之前进行的操作信息
        /// </summary>
        protected override void BeforStart( )
        {
            modbusTcp.SetPersistentConnection( );
        }


        /// <summary>
        /// 在关闭的时候需要进行的操作
        /// </summary>
        protected override void AfterClose( )
        {
            modbusTcp.ConnectClose( );
        }

        #endregion

        #region Private

        private ModbusTcpNet modbusTcp;               // 核心交互对象
        
        #endregion
    }
}
