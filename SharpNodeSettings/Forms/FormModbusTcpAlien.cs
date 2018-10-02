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
    public partial class FormModbusTcpAlien : Form
    {
        public FormModbusTcpAlien( NodeModbusTcpAline modbusTcpAline = null)
        {
            InitializeComponent( );

            ModbusTcpAline = modbusTcpAline ?? new NodeModbusTcpAline();
            Icon = Util.GetWinformICon( );
        }

        private void FormModbusTcpAlien_Load( object sender, EventArgs e )
        {
            comboBox1.DataSource = HslCommunication.BasicFramework.SoftBasic.GetEnumValues<HslCommunication.Core.DataFormat>( );

            if (ModbusTcpAline != null)
            {
                textBox1.Text = ModbusTcpAline.Name;
                textBox2.Text = ModbusTcpAline.Description;
                textBox3.Text = ModbusTcpAline.DTU;
                textBox5.Text = ModbusTcpAline.Station.ToString( );
                checkBox1.Checked = !ModbusTcpAline.IsAddressStartWithZero;
                //checkBox2.Checked = ModbusTcpAline.IsWordReverse;
                comboBox1.SelectedIndex = ModbusTcpAline.DataFormat;
                checkBox3.Checked = ModbusTcpAline.IsStringReverse;
            }
        }





        public NodeModbusTcpAline ModbusTcpAline { get; set; }

        private void userButton1_Click( object sender, EventArgs e )
        {
            if(!byte.TryParse(textBox5.Text,out byte station))
            {
                MessageBox.Show( "站号的输入失败！" );
                return;
            }

            ModbusTcpAline = new NodeModbusTcpAline( )
            {
                Name = textBox1.Text,
                Description = textBox2.Text,
                DTU = textBox3.Text,
                Station = station,
                IsAddressStartWithZero = !checkBox1.Checked,
                //IsWordReverse = checkBox2.Checked,
                DataFormat = comboBox1.SelectedIndex,
                IsStringReverse = checkBox3.Checked
            };

            DialogResult = DialogResult.OK;

        }




    }
}
