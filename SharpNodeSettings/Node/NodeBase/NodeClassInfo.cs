using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpNodeSettings.Node.NodeBase
{
    /// <summary>
    /// 节点类相关的常用资源
    /// </summary>
    public class NodeClassInfo
    {
        #region 一级节点
        
        /// <summary>
        /// 系统的根节点信息
        /// </summary>
        public const int NodeRoot = 0;

        /// <summary>
        /// 普通的分类节点，允许再跟分类节点
        /// </summary>
        public const int NodeClass = 1;

        /// <summary>
        /// 设备节点信息，下面只能跟请求节点信息
        /// </summary>
        public const int DeviceNode = 2;

        /// <summary>
        /// 服务器类型Server的节点
        /// </summary>
        public const int ServerNode = 3;

        /// <summary>
        /// 解析规则的节点
        /// </summary>
        public const int RegularNode = 4;

        #endregion

        #region 二级节点
        
        /// <summary>
        /// 设备的请求信息
        /// </summary>
        public const int DeviceRequest = 100;

        /// <summary>
        /// 设备的解析规则的子节点
        /// </summary>
        public const int RegularItemNode = 200;

        #endregion

    }
}
