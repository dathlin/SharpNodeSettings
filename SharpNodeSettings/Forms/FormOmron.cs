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
    public partial class FormOmron : Form
    {
        public FormOmron( NodeOmron nodeOmron = null )
        {
            InitializeComponent( );
            Icon = Util.GetWinformICon( );
            NodeOmron = nodeOmron ?? new NodeOmron( );
        }

        private void FormOmron_Load( object sender, EventArgs e )
        {
            if (NodeOmron != null)
            {
                textBox1.Text = NodeOmron.Name;
                textBox2.Text = NodeOmron.Description;
                textBox3.Text = NodeOmron.IpAddress;
                textBox4.Text = NodeOmron.Port.ToString( );
                textBox5.Text = NodeOmron.DA2.ToString( );
                textBox7.Text = NodeOmron.DA1.ToString( );
                textBox8.Text = NodeOmron.SA1.ToString( );
                textBox6.Text = NodeOmron.ConnectTimeOut.ToString( );
            }
        }

        public NodeOmron NodeOmron { get; set; }

        private void userButton1_Click( object sender, EventArgs e )
        {
            if(string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show( "名称不能为空！" );
                return;
            }

            if(!IPAddress.TryParse(textBox3.Text,out IPAddress address))
            {
                MessageBox.Show( "Ip地址输入失败！" );
                return;
            }

            if(!int.TryParse(textBox4.Text,out int port))
            {
                MessageBox.Show( "Port端口号输入失败！" );
                return;
            }

            if(!byte.TryParse(textBox5.Text,out byte da2))
            {
                MessageBox.Show( "PLC的单元号输入失败！" );
                return;
            }

            if(!byte.TryParse(textBox7.Text,out byte da1))
            {
                MessageBox.Show( "PLC的节点地址输入失败！" );
                return;
            }

            if(!byte.TryParse(textBox8.Text,out byte sa1))
            {
                MessageBox.Show( "上位机的节点地址输入失败！" );
                return;
            }

            if(!int.TryParse(textBox6.Text,out int connect))
            {
                MessageBox.Show( "超时时间输入失败！" );
                return;
            }

            NodeOmron = new NodeOmron( )
            {
                Name = textBox1.Text,
                Description = textBox2.Text,
                IpAddress = address.ToString( ),
                Port = port,
                DA1 = da1,
                DA2 = da2,
                SA1 = sa1,
                ConnectTimeOut = connect,
            };

            DialogResult = DialogResult.OK;
        }
    }
}
