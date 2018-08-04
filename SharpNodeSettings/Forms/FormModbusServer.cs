using SharpNodeSettings.Node.Server;
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
    public partial class FormModbusServer : Form
    {
        public FormModbusServer( NodeModbusServer modbusServer = null )
        {
            InitializeComponent( );
            Icon = Util.GetWinformICon( );
            ModbusServer = modbusServer ?? new NodeModbusServer( );
        }

        private void FormModbusServer_Load( object sender, EventArgs e )
        {
            if(ModbusServer!=null)
            {
                textBox1.Text = ModbusServer.Name;
                textBox2.Text = ModbusServer.Description;
                textBox4.Text = ModbusServer.Port.ToString( );
            }
        }
        

        public NodeModbusServer ModbusServer { get; set; }

        private void userButton1_Click( object sender, EventArgs e )
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show( "节点名称不能为空" );
                return;
            }

            if (!int.TryParse( textBox4.Text, out int port ))
            {
                MessageBox.Show( "端口号输入失败！" );
                return;
            }

            ModbusServer = new NodeModbusServer( )
            {
                Name = textBox1.Text,
                Description = textBox2.Text,
                Port = port
            };

            DialogResult = DialogResult.OK;
        }
    }
}
