using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using SharpNodeSettings.Node.Device;

namespace SharpNodeSettings.Forms
{
    public partial class FormSiemens : Form
    {
        public FormSiemens( NodeSiemens nodeSiemens = null )
        {
            InitializeComponent( );
            Icon = Util.GetWinformICon( );
            NodeSiemens = nodeSiemens ?? new NodeSiemens( );
        }

        private void FormSiemens_Load( object sender, EventArgs e )
        {
            comboBox1.DataSource = new string[]
            {
                NodeSiemens.PLC1200,
                NodeSiemens.PLC1500,
                NodeSiemens.PLC200Smart,
                NodeSiemens.PLC300,
                NodeSiemens.PLCFW
            };


            if (NodeSiemens != null)
            {
                textBox1.Text = NodeSiemens.Name;
                textBox2.Text = NodeSiemens.Description;
                textBox3.Text = NodeSiemens.IpAddress;
                textBox4.Text = NodeSiemens.Port.ToString( );
                textBox6.Text = NodeSiemens.ConnectTimeOut.ToString( );
                comboBox1.SelectedItem = NodeSiemens.PlcType;
            }
        }


        public NodeSiemens NodeSiemens { get; set; }

        private void userButton1_Click( object sender, EventArgs e )
        {
            if (!IPAddress.TryParse( textBox3.Text, out IPAddress address ))
            {
                MessageBox.Show( "IP地址输入失败！" );
                return;
            }

            if(!int.TryParse(textBox4.Text,out int port))
            {
                MessageBox.Show( "端口号输入失败！" );
                return;
            }

            if(!int.TryParse(textBox6.Text,out int connect))
            {
                MessageBox.Show( "超时时间输入失败！" );
                return;
            }

            NodeSiemens = new NodeSiemens( )
            {
                Name = textBox1.Text,
                Description = textBox2.Text,
                IpAddress = address.ToString( ),
                Port = port,
                ConnectTimeOut = connect,
                PlcType = comboBox1.SelectedItem.ToString( ),
            };

            DialogResult = DialogResult.OK;
        }
    }
}
