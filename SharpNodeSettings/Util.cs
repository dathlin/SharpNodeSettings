using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        

        #endregion
    }
}
