using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpNodeSettings.Controls
{
    public class TreeViewEx : TreeView
    {
        public TreeViewEx()
        {
            
        }

        protected override void WndProc( ref Message m )
        {
            if (m.Msg == 0x0014) // 禁掉清除背景消息
                return;
            base.WndProc( ref m );
        }

    }
}
