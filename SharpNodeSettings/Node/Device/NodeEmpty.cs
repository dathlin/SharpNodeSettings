using SharpNodeSettings.Node.NodeBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpNodeSettings.Node.Device
{
    /// <summary>
    /// 空设备，没有连接，不负责数据采集，只负责节点显示
    /// </summary>
    public class NodeEmpty : DeviceNode, IXmlConvert
    {

        #region Constructor
        
        /// <summary>
        /// 实例化一个空的设备对象
        /// </summary>
        public NodeEmpty( )
        {
            Name = "空设备";
            Description = "此设备安装在角落，编号0001";
            DeviceType = DeviceNode.DeviceNone;
        }

        #endregion

        #region Object Override

        /// <summary>
        /// 返回表示当前对象的字符串
        /// </summary>
        /// <returns>字符串信息</returns>
        public override string ToString( )
        {
            return "[空设备] " + Name;
        }

        #endregion

    }
}
