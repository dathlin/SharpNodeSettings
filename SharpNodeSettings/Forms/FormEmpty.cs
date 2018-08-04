using SharpNodeSettings.Node.Device;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpNodeSettings.Forms
{
    public partial class FormEmpty : Form
    {
        public FormEmpty( NodeEmpty nodeEmpty = null)
        {
            InitializeComponent( );
            NodeEmpty = nodeEmpty ?? new NodeEmpty( );
            Icon = Util.GetWinformICon( );
        }

        private void FormEmpty_Load( object sender, EventArgs e )
        {
            if(NodeEmpty != null)
            {
                textBox1.Text = NodeEmpty.Name;
                textBox2.Text = NodeEmpty.Description;
            }
        }
        

        public NodeEmpty NodeEmpty { get; set; }
        

        private void userButton1_Click( object sender, EventArgs e )
        {
            NodeEmpty = new NodeEmpty( )
            {
                Name = textBox1.Text,
                Description = textBox2.Text,
            };
            DialogResult = DialogResult.OK;
        }
    }
}
