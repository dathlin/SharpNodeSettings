using HslCommunication.Profinet.Siemens;
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
    /// 西门子的设备信息，包含了核心的连接对象
    /// </summary>
    public class DeviceSiemens : DeviceCore
    {
        #region Constructor

        /// <summary>
        /// 实例化一个西门子的设备对象，从配置信息创建
        /// </summary>
        /// <param name="element">配置信息</param>
        public DeviceSiemens( XElement element )
        {
            NodeSiemens nodeSiemens = new NodeSiemens( );
            nodeSiemens.LoadByXmlElement( element );
            LoadRequest( element );

            if (nodeSiemens.PlcType == NodeSiemens.PLCFW)
            {
                isS7Net = false;
                siemensFetchWrite = new SiemensFetchWriteNet( nodeSiemens.IpAddress, nodeSiemens.Port );
                siemensFetchWrite.ConnectTimeOut = nodeSiemens.ConnectTimeOut;
                ReadWriteDevice = siemensFetchWrite;
                ByteTransform = siemensFetchWrite.ByteTransform;
                UniqueId = siemensFetchWrite.ConnectionId;
            }
            else
            {
                isS7Net = true;
                if (nodeSiemens.PlcType == NodeSiemens.PLC300)
                {
                    siemensS7Net = new SiemensS7Net( SiemensPLCS.S300 );
                }
                else if (nodeSiemens.PlcType == NodeSiemens.PLC1200)
                {
                    siemensS7Net = new SiemensS7Net( SiemensPLCS.S1200 );
                }
                else if (nodeSiemens.PlcType == NodeSiemens.PLC1500)
                {
                    siemensS7Net = new SiemensS7Net( SiemensPLCS.S1500 );
                }
                else if (nodeSiemens.PlcType == NodeSiemens.PLC200Smart)
                {
                    siemensS7Net = new SiemensS7Net( SiemensPLCS.S200Smart );
                }
                else
                {
                    siemensS7Net = new SiemensS7Net( SiemensPLCS.S1200 );
                }

                siemensS7Net.IpAddress = nodeSiemens.IpAddress;
                siemensS7Net.ConnectTimeOut = nodeSiemens.ConnectTimeOut;
                ByteTransform = siemensS7Net.ByteTransform;
                ReadWriteDevice = siemensFetchWrite;
                UniqueId = siemensS7Net.ConnectionId;
            }


            TypeName = "西门子设备";
        }


        #endregion
        

        #region Protect Override

        /// <summary>
        /// 在启动之前进行的操作信息
        /// </summary>
        protected override void BeforStart( )
        {
            if (isS7Net)
            {
                siemensS7Net.ConnectServer( );
            }
            else
            {
                siemensFetchWrite.ConnectServer( );
            }
        }

        /// <summary>
        /// 在关闭的时候需要进行的操作
        /// </summary>
        protected override void AfterClose( )
        {
            if (isS7Net)
            {
                siemensS7Net.ConnectClose( );
            }
            else
            {
                siemensFetchWrite.ConnectClose( );
            }
        }

        #endregion

        #region Private


        private SiemensS7Net siemensS7Net;               // S7协议交互对象
        private SiemensFetchWriteNet siemensFetchWrite;  // FW协议交互对象
        private bool isS7Net = true;                     // 是否是S7协议

        #endregion
    }
}
