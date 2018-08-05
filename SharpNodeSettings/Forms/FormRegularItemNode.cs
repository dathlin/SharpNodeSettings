using SharpNodeSettings.Node.Regular;
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
    public partial class FormRegularItemNode : Form
    {
        public FormRegularItemNode( RegularItemNode regularNode = null )
        {
            InitializeComponent( );

            RegularNode = regularNode;
            Icon = Util.GetWinformICon( );
        }

        private void FormRegularNode_Load( object sender, EventArgs e )
        {
            comboBox1.DataSource = new RegularNodeTypeItem[]
            {
                RegularNodeTypeItem.Bool,
                RegularNodeTypeItem.Byte,
                RegularNodeTypeItem.Int16,
                RegularNodeTypeItem.UInt16,
                RegularNodeTypeItem.Int32,
                RegularNodeTypeItem.UInt32,
                RegularNodeTypeItem.Int64,
                RegularNodeTypeItem.UInt64,
                RegularNodeTypeItem.Float,
                RegularNodeTypeItem.Double,
                RegularNodeTypeItem.StringAscii,
                RegularNodeTypeItem.StringUnicode,
                RegularNodeTypeItem.StringUtf8,
            };

            if (RegularNode != null)
            {
                textBox1.Text = RegularNode.Name;
                textBox2.Text = RegularNode.Description;
                textBox3.Text = RegularNode.Index.ToString( );
                comboBox1.SelectedItem = RegularNodeTypeItem.GetDataPraseItemByCode(RegularNode.RegularCode);
                textBox5.Text = RegularNode.TypeLength.ToString( );
            }
            else
            {
                comboBox1.SelectedItem = RegularNodeTypeItem.GetDataPraseItemByCode( RegularNodeTypeItem.Int16.Code );
            }

        }



        public RegularItemNode RegularNode { get; set; }    // 结果信息

        private void userButton1_Click( object sender, EventArgs e )
        {
            // 检查数据输入
            if (string.IsNullOrEmpty( textBox1.Text ))
            {
                MessageBox.Show( "名称不能为空！" );
                textBox1.Focus( );
                return;
            }
            
            if (!int.TryParse( textBox3.Text, out int index ))
            {
                MessageBox.Show( "索引号输入的格式有误，请重新输入。" );
                textBox3.Focus( );
                return;
            }
            
            if (index < 0)
            {
                MessageBox.Show( "索引号不能小于0，请重新输入。" );
                textBox3.Focus( );
                return;
            }
            
            if(!int.TryParse(textBox5.Text,out int typeLength))
            {
                MessageBox.Show( "数据长度输入错误。" );
                textBox5.Focus( );
                return;
            }

            RegularNode = new RegularItemNode( )
            {
                Name = textBox1.Text,
                Description = textBox2.Text,
                Index = index,
                RegularCode = ((RegularNodeTypeItem)comboBox1.SelectedItem).Code,
                TypeLength = typeLength,
            };


            DialogResult = DialogResult.OK;
        }
    }
}
