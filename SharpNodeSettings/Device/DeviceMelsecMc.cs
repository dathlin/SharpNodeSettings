using HslCommunication.Profinet.Melsec;
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
    /// 三菱的设备信息，包含了核心的连接对象
    /// </summary>
    public class DeviceMelsecMc : DeviceCore
    {
        /// <summary>
        /// 实例化一个三菱的设备对象，从配置信息创建
        /// </summary>
        /// <param name="element">配置信息</param>
        public DeviceMelsecMc(XElement element )
        {
            NodeMelsecMc nodeMelsec = new NodeMelsecMc( );
            nodeMelsec.LoadByXmlElement( element );
            LoadRequest( element );

            if (nodeMelsec.IsBinary)
            {
                protocol = 1;
                melsecMcNet = new MelsecMcNet( nodeMelsec.IpAddress, nodeMelsec.Port );
                melsecMcNet.NetworkNumber = nodeMelsec.NetworkNumber;
                melsecMcNet.NetworkStationNumber = nodeMelsec.NetworkStationNumber;
                melsecMcNet.ConnectTimeOut = nodeMelsec.ConnectTimeOut;

                ByteTransform = melsecMcNet.ByteTransform;
                ReadWriteDevice = melsecMcNet;
                UniqueId = melsecMcNet.ConnectionId;
            }
            else
            {
                melsecMcAscii = new MelsecMcAsciiNet( nodeMelsec.IpAddress, nodeMelsec.Port );
                melsecMcAscii.NetworkNumber = nodeMelsec.NetworkNumber;
                melsecMcAscii.NetworkStationNumber = nodeMelsec.NetworkStationNumber;
                melsecMcAscii.ConnectTimeOut = nodeMelsec.ConnectTimeOut;

                ByteTransform = melsecMcAscii.ByteTransform;
                ReadWriteDevice = melsecMcAscii;
                UniqueId = melsecMcAscii.ConnectionId;

            }

            TypeName = "三菱设备";
        }





        #region Protect Override

        /// <summary>
        /// 在启动之前进行的操作信息
        /// </summary>
        protected override void BeforStart( )
        {
            if (protocol == 0)
            {
                melsecMcAscii.SetPersistentConnection( );
            }
            else if (protocol == 1)
            {
                melsecMcNet.SetPersistentConnection( );
            }
        }

        /// <summary>
        /// 在关闭的时候需要进行的操作
        /// </summary>
        protected override void AfterClose( )
        {
            if (protocol == 0)
            {
                melsecMcAscii.ConnectClose( );
            }
            else if (protocol == 1)
            {
                melsecMcNet.ConnectClose( );
            }
        }

        #endregion

        #region Private


        private MelsecMcAsciiNet melsecMcAscii;               // ASCII格式的交互对象
        private MelsecMcNet melsecMcNet;                      // 二进制格式的交互对象
        private int protocol = 0;                             // 当前选择的协议，0 是ascii 1是二进制

        #endregion
    }
}
