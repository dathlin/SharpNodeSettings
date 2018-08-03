using HslCommunication.Profinet.Omron;
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
    /// 欧姆龙的设备信息，包含了核心的连接对象
    /// </summary>
    public class DeviceOmron : DeviceCore
    {

        #region Constructor
        
        /// <summary>
        /// 实例化一个欧姆龙的设备对象，从配置信息创建
        /// </summary>
        /// <param name="element">配置信息</param>
        public DeviceOmron( XElement element )
        {
            NodeOmron nodeOmron = new NodeOmron( );
            nodeOmron.LoadByXmlElement( element );
            LoadRequest( element );

            omronFinsNet = new OmronFinsNet( nodeOmron.IpAddress, nodeOmron.Port );
            omronFinsNet.DA1 = nodeOmron.DA1;
            omronFinsNet.DA2 = nodeOmron.DA2;
            omronFinsNet.SA1 = nodeOmron.SA1;
            omronFinsNet.ConnectTimeOut = nodeOmron.ConnectTimeOut;

            ByteTransform = omronFinsNet.ByteTransform;
            ReadWriteDevice = omronFinsNet;
            UniqueId = omronFinsNet.ConnectionId;

            TypeName = "欧姆龙设备";
        }


        #endregion

        #region Protect Override

        /// <summary>
        /// 在启动之前进行的操作信息
        /// </summary>
        protected override void BeforStart( )
        {
            omronFinsNet.SetPersistentConnection( );
        }


        /// <summary>
        /// 在关闭的时候需要进行的操作
        /// </summary>
        protected override void AfterClose( )
        {
            omronFinsNet.ConnectClose( );
        }

        #endregion

        #region Private


        private OmronFinsNet omronFinsNet;               // 核心交互对象


        #endregion
    }
}
