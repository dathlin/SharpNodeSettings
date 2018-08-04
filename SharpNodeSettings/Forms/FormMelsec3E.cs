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
    public partial class FormMelsec3E : Form
    {
        public FormMelsec3E( NodeMelsecMc melsecMc = null )
        {
            InitializeComponent( );
            Icon = Util.GetWinformICon( );
            
            MelsecMc = melsecMc ?? new NodeMelsecMc( );
        }

        private void FormMelsecBinary_Load( object sender, EventArgs e )
        {

            if (MelsecMc != null)
            {
                textBox1.Text = MelsecMc.Name;
                textBox2.Text = MelsecMc.Description;
                textBox3.Text = MelsecMc.IpAddress;
                textBox4.Text = MelsecMc.Port.ToString( );
                textBox5.Text = MelsecMc.NetworkNumber.ToString( );
                textBox7.Text = MelsecMc.NetworkStationNumber.ToString( );
                textBox6.Text = MelsecMc.ConnectTimeOut.ToString( );
                checkBox1.Checked = MelsecMc.IsBinary;
            }
            
        }




        public NodeMelsecMc MelsecMc { get; set; }

        private void userButton1_Click( object sender, EventArgs e )
        {
            if(!IPAddress.TryParse(textBox3.Text,out IPAddress address))
            {
                MessageBox.Show( "Ip地址填写异常！" );
                return;
            }

            if(!int.TryParse(textBox4.Text,out int port))
            {
                MessageBox.Show( "端口号填写异常！" );
                return;
            }

            if(!byte.TryParse(textBox5.Text,out byte netWorkNumber))
            {
                MessageBox.Show( "网络号填写异常！" );
                return;
            }

            if(!byte.TryParse( textBox7.Text, out byte netWorkStationNumber ))
            {
                MessageBox.Show( "网络站号填写异常！" );
                return;
            }

            if(!int.TryParse(textBox6.Text,out int timeOut))
            {
                MessageBox.Show( "超时时间填写异常！" );
                return;
            }

            MelsecMc = new NodeMelsecMc( )
            {
                Name = textBox1.Text,
                Description = textBox2.Text,
                IpAddress = address.ToString( ),
                Port = port,
                NetworkNumber = netWorkNumber,
                NetworkStationNumber = netWorkStationNumber,
                ConnectTimeOut = timeOut,
                IsBinary = checkBox1.Checked
            };

            DialogResult = DialogResult.OK;

        }
    }
}
