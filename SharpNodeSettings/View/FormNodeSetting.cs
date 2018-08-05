using SharpNodeSettings.Forms;
using SharpNodeSettings.Node.Device;
using SharpNodeSettings.Node.NodeBase;
using SharpNodeSettings.Node.Regular;
using SharpNodeSettings.Node.Request;
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
using System.Xml.Linq;


/**********************************************************************************************
 * 
 *    说明：本界面最主要的功能是如果去解析xml文件，以及根据树节点的信息生成xml文件
 * 
 *    备注：树节点包含2个初始化的节点信息
 * 
 ***********************************************************************************************/



namespace SharpNodeSettings.View
{
    public partial class FormNodeSetting : Form
    {
        #region Constructor


        public FormNodeSetting(string fileName )
        {
            InitializeComponent( );
            Icon = Util.GetWinformICon( );
            ImageResourseLoad( );
            this.fileName = fileName;
        }

        #endregion

        #region Images

        private ImageList SharpImageList { get; set; }

        private void ImageResourseLoad( )
        {
            SharpImageList = new ImageList( );
            SharpImageList.Images.Add( "abstr1", Properties.Resources.abstr1 );
            SharpImageList.Images.Add( "action_add_16xLG", Properties.Resources.action_add_16xLG );
            SharpImageList.Images.Add( "action_Cancel_16xLG", Properties.Resources.action_Cancel_16xLG );
            SharpImageList.Images.Add( "ClassIcon", Properties.Resources.ClassIcon );
            SharpImageList.Images.Add( "Class_489", Properties.Resources.Class_489 );
            SharpImageList.Images.Add( "Enum_582", Properties.Resources.Enum_582 );
            SharpImageList.Images.Add( "Event_594", Properties.Resources.Event_594 );
            SharpImageList.Images.Add( "Event_594_exp", Properties.Resources.Event_594_exp );
            SharpImageList.Images.Add( "FieldsHeader_12x", Properties.Resources.FieldsHeader_12x );
            SharpImageList.Images.Add( "FlagRed_16x", Properties.Resources.FlagRed_16x );
            SharpImageList.Images.Add( "FlagSpace_16x", Properties.Resources.FlagSpace_16x );
            SharpImageList.Images.Add( "flag_16xLG", Properties.Resources.flag_16xLG );
            SharpImageList.Images.Add( "GenericVSEditor_9905", Properties.Resources.GenericVSEditor_9905 );
            SharpImageList.Images.Add( "HotSpot_10548", Properties.Resources.HotSpot_10548 );
            SharpImageList.Images.Add( "ExtensionManager_vsix", Properties.Resources.ExtensionManager_vsix );
            SharpImageList.Images.Add( "HotSpot_10548_color", Properties.Resources.HotSpot_10548_color );
            SharpImageList.Images.Add( "library_16xLG", Properties.Resources.library_16xLG );
            SharpImageList.Images.Add( "Method_636", Properties.Resources.Method_636 );
            SharpImageList.Images.Add( "Module_648", Properties.Resources.Module_648 );
            SharpImageList.Images.Add( "Monitor_Screen_16xLG", Properties.Resources.Monitor_Screen_16xLG );
            SharpImageList.Images.Add( "Operator_660", Properties.Resources.Operator_660 );
            SharpImageList.Images.Add( "PencilAngled_16xLG", Properties.Resources.PencilAngled_16xLG );
            SharpImageList.Images.Add( "Property_501", Properties.Resources.Property_501 );
            SharpImageList.Images.Add( "server_Local_16xLG", Properties.Resources.server_Local_16xLG );
            SharpImageList.Images.Add( "star_16xLG", Properties.Resources.star_16xLG );
            SharpImageList.Images.Add( "usbcontroller", Properties.Resources.usbcontroller );
            SharpImageList.Images.Add( "VirtualMachine_16xLG", Properties.Resources.VirtualMachine_16xLG );
            SharpImageList.Images.Add( "WindowsAzure_16xLG", Properties.Resources.WindowsAzure_16xLG );
            SharpImageList.Images.Add( "WindowsAzure_16xLG_Cyan", Properties.Resources.WindowsAzure_16xLG_Cyan );
            SharpImageList.Images.Add( "xbox1Color_16x", Properties.Resources.xbox1Color_16x );
        }

        #endregion

        #region Form Load Show Close

        private void FormNodeSetting_Load( object sender, EventArgs e )
        {
            treeView1.ImageList = SharpImageList;

            treeView1.Nodes[0].ImageKey = "VirtualMachine_16xLG";
            treeView1.Nodes[0].SelectedImageKey = "VirtualMachine_16xLG";
            treeView1.Nodes[0].Tag = new NodeClass( )
            {
                Name = "Devices",
                Description = "所有的设备的集合对象"
            };
            treeView1.Nodes[1].ImageKey = "VirtualMachine_16xLG";
            treeView1.Nodes[1].SelectedImageKey = "VirtualMachine_16xLG";
            treeView1.Nodes[1].Tag = new NodeClass( )
            {
                Name = "Server",
                Description = "所有挂载的服务器"
            };


            treeView1.Nodes[2].ImageKey = "VirtualMachine_16xLG";
            treeView1.Nodes[2].SelectedImageKey = "VirtualMachine_16xLG";
            treeView1.Nodes[2].Tag = new NodeClass( )
            {
                Name = "Regular",
                Description = "所有的解析规则的信息"
            };

            panel2.Dock = DockStyle.Fill;
            panel2.Visible = false;
            checkBox1.CheckedChanged += CheckBox1_CheckedChanged;

            if (!string.IsNullOrEmpty( fileName )) LoadByFile( fileName );
        }

        private void CheckBox1_CheckedChanged( object sender, EventArgs e )
        {
            isShowText = checkBox1.Checked;

            UpdateTreeData( );
        }

        private void FormNodeSetting_FormClosing( object sender, FormClosingEventArgs e )
        {
            if(isNodeSettingsModify)
            {
                if(MessageBox.Show("当前的配置信息已经修改过，但还未保存，是否需要保存？","保存确认",MessageBoxButtons.YesNo,MessageBoxIcon.Warning)==DialogResult.Yes)
                {
                    SaveNodes( fileName );
                }
            }
        }

        #endregion

        #region Device Node Add


        private void 类别classToolStripMenuItem_Click( object sender, EventArgs e )
        {
            // 新增了类别
            TreeNode node = treeView1.SelectedNode;
            if (node.Tag is NodeClass nodeClass)
            {
                if (nodeClass.NodeType == NodeClassInfo.NodeClass)
                {
                    // 允许添加类别
                    using (FormNodeClass formNode = new FormNodeClass( null ))
                    {
                        if (formNode.ShowDialog( ) == DialogResult.OK)
                        {
                            formNode.SelectedNodeClass.Name = GetUniqueName( node, formNode.SelectedNodeClass.Name );

                            TreeNode nodeNew = new TreeNode( formNode.SelectedNodeClass.Name );
                            nodeNew.ImageKey = "Class_489";
                            nodeNew.SelectedImageKey = "Class_489";
                            nodeNew.Tag = formNode.SelectedNodeClass;
                            node.Nodes.Add( nodeNew );
                            node.Expand( );
                            isNodeSettingsModify = true;
                        }
                    }
                }
            }
        }
        
        
        private void modbustcpclientToolStripMenuItem_Click( object sender, EventArgs e )
        {
            // 新增了Modbus-Tcp客户端
            TreeNode node = treeView1.SelectedNode;
            if (node.Tag is NodeClass nodeClass)
            {
                if (nodeClass.NodeType == NodeClassInfo.NodeClass)
                {
                    // 允许添加设备
                    using (FormModbusTcp formNode = new FormModbusTcp( new NodeModbusTcpClient( ) ))
                    {
                        if (formNode.ShowDialog( ) == DialogResult.OK)
                        {
                            formNode.ModbusTcpNode.Name = GetUniqueName( node, formNode.ModbusTcpNode.Name );

                            TreeNode nodeNew = new TreeNode( formNode.ModbusTcpNode.Name );
                            nodeNew.ImageKey = "Module_648";
                            nodeNew.SelectedImageKey = "Module_648";
                            nodeNew.Tag = formNode.ModbusTcpNode;
                            node.Nodes.Add( nodeNew );
                            node.Expand( );
                            isNodeSettingsModify = true;
                        }
                    }
                }
            }
        }


        private void 新增服务器ToolStripMenuItem_Click( object sender, EventArgs e )
        {
            TreeNode node = treeView1.SelectedNode;
            if (node.Tag is NodeClass nodeClass)
            {
                // 允许添加异形服务器
                using (FormAlienNode formNode = new FormAlienNode( ))
                {
                    if (formNode.ShowDialog( ) == DialogResult.OK)
                    {
                        formNode.AlienNode.Name = GetUniqueName( node, formNode.AlienNode.Name );

                        TreeNode nodeNew = new TreeNode( formNode.AlienNode.Name );
                        nodeNew.ImageKey = "server_Local_16xLG";
                        nodeNew.SelectedImageKey = "server_Local_16xLG";
                        nodeNew.Tag = formNode.AlienNode;
                        node.Nodes.Add( nodeNew );
                        node.Expand( );
                        isNodeSettingsModify = true;
                    }
                }
            }
        }

        private void 异形ModbusTcpToolStripMenuItem_Click( object sender, EventArgs e )
        {
            TreeNode node = treeView1.SelectedNode;
            if (node.Tag is NodeClass nodeClass)
            {
                // 允许添加异形客户端
                using (FormModbusTcpAlien formNode = new FormModbusTcpAlien( new NodeModbusTcpAline( ) ))
                {
                    if (formNode.ShowDialog( ) == DialogResult.OK)
                    {
                        // 需要先进行判断DTU是否冲突
                        if (IsDTUExistModbusAlien( formNode.ModbusTcpAline.DTU, node ))
                        {
                            MessageBox.Show( "设备添加失败，DTU码重复！" );
                            return;
                        }


                        formNode.ModbusTcpAline.Name = GetUniqueName( node, formNode.ModbusTcpAline.Name );

                        TreeNode nodeNew = new TreeNode( formNode.ModbusTcpAline.Name );
                        nodeNew.ImageKey = "Module_648";
                        nodeNew.SelectedImageKey = "Module_648";
                        nodeNew.Tag = formNode.ModbusTcpAline;
                        node.Nodes.Add( nodeNew );
                        node.Expand( );
                        isNodeSettingsModify = true;
                    }
                }
            }
        }

        private bool IsDTUExistModbusAlien( string dtu, TreeNode treeNode )
        {
            List<string> dtus = new List<string>( );
            foreach (TreeNode item in treeNode.Nodes)
            {
                if (item.Tag is NodeModbusTcpAline modbusTcp)
                {
                    dtus.Add( modbusTcp.DTU );
                }
            }

            return dtus.Contains( dtu );
        }


        private void 三菱plcmelsecToolStripMenuItem_Click( object sender, EventArgs e )
        {
            // 新增了三菱PLC客户端
            TreeNode node = treeView1.SelectedNode;
            if (node.Tag is NodeClass nodeClass)
            {
                if (nodeClass.NodeType == NodeClassInfo.NodeClass)
                {
                    // 允许添加设备
                    using (FormMelsec3E formNode = new FormMelsec3E( null ))
                    {
                        if (formNode.ShowDialog( ) == DialogResult.OK)
                        {
                            formNode.MelsecMc.Name = GetUniqueName( node, formNode.MelsecMc.Name );

                            TreeNode nodeNew = new TreeNode( formNode.MelsecMc.Name );
                            nodeNew.ImageKey = "Enum_582";
                            nodeNew.SelectedImageKey = "Enum_582";
                            nodeNew.Tag = formNode.MelsecMc;
                            node.Nodes.Add( nodeNew );
                            node.Expand( );
                            isNodeSettingsModify = true;
                        }
                    }
                }
            }
        }

        private void 西门子PlcsiemensToolStripMenuItem_Click( object sender, EventArgs e )
        {
            // 新增了西门子客户端的设备
            TreeNode node = treeView1.SelectedNode;
            if (node.Tag is NodeClass nodeClass)
            {
                if (nodeClass.NodeType == NodeClassInfo.NodeClass)
                {
                    // 允许添加设备
                    using (FormSiemens formNode = new FormSiemens( null ))
                    {
                        if (formNode.ShowDialog( ) == DialogResult.OK)
                        {
                            formNode.NodeSiemens.Name = GetUniqueName( node, formNode.NodeSiemens.Name );

                            TreeNode nodeNew = new TreeNode( formNode.NodeSiemens.Name );
                            nodeNew.ImageKey = "Event_594";
                            nodeNew.SelectedImageKey = "Event_594";
                            nodeNew.Tag = formNode.NodeSiemens;
                            node.Nodes.Add( nodeNew );
                            node.Expand( );
                            isNodeSettingsModify = true;
                        }
                    }
                }
            }
        }

        private void 欧姆龙plcomronToolStripMenuItem_Click( object sender, EventArgs e )
        {
            // 新增了欧姆龙客户端的设备
            TreeNode node = treeView1.SelectedNode;
            if (node.Tag is NodeClass nodeClass)
            {
                if (nodeClass.NodeType == NodeClassInfo.NodeClass)
                {
                    // 允许添加设备
                    using (FormOmron formNode = new FormOmron( null ))
                    {
                        if (formNode.ShowDialog( ) == DialogResult.OK)
                        {
                            formNode.NodeOmron.Name = GetUniqueName( node, formNode.NodeOmron.Name );

                            TreeNode nodeNew = new TreeNode( formNode.NodeOmron.Name );
                            nodeNew.ImageKey = "HotSpot_10548_color";
                            nodeNew.SelectedImageKey = "HotSpot_10548_color";
                            nodeNew.Tag = formNode.NodeOmron;
                            node.Nodes.Add( nodeNew );
                            node.Expand( );
                            isNodeSettingsModify = true;
                        }
                    }
                }
            }
        }

        private void 空设备toolStripMenuItem_Click( object sender, EventArgs e )
        {
            // 新增了空设备的客户端，用作纯节点使用
            TreeNode node = treeView1.SelectedNode;
            if (node.Tag is NodeClass nodeClass)
            {
                if (nodeClass.NodeType == NodeClassInfo.NodeClass)
                {
                    // 允许添加设备
                    using (FormEmpty formNode = new FormEmpty( null ))
                    {
                        if (formNode.ShowDialog( ) == DialogResult.OK)
                        {
                            formNode.NodeEmpty.Name = GetUniqueName( node, formNode.NodeEmpty.Name );

                            TreeNode nodeNew = new TreeNode( formNode.NodeEmpty.Name );
                            nodeNew.ImageKey = "Method_636";
                            nodeNew.SelectedImageKey = "Method_636";
                            nodeNew.Tag = formNode.NodeEmpty;
                            node.Nodes.Add( nodeNew );
                            node.Expand( );
                            isNodeSettingsModify = true;
                        }
                    }
                }
            }

        }
        
        
        private void 新增ModbusTcpServerToolStripMenuItem_Click( object sender, EventArgs e )
        {
            TreeNode node = treeView1.SelectedNode;
            if (node.Tag is NodeClass nodeClass)
            {
                if (nodeClass.NodeType == NodeClassInfo.NodeClass)
                {
                    // 允许添加Modbus-Tcp服务器
                    using (FormModbusServer formNode = new FormModbusServer( ))
                    {
                        if (formNode.ShowDialog( ) == DialogResult.OK)
                        {
                            formNode.ModbusServer.Name = GetUniqueName( node, formNode.ModbusServer.Name );

                            TreeNode nodeNew = new TreeNode( formNode.ModbusServer.Name );
                            nodeNew.ImageKey = "server_Local_16xLG";
                            nodeNew.SelectedImageKey = "server_Local_16xLG";
                            nodeNew.Tag = formNode.ModbusServer;
                            node.Nodes.Add( nodeNew );
                            node.Expand( );
                            isNodeSettingsModify = true;
                        }
                    }
                }
            }
        }


        private void 新增AlienServerToolStripMenuItem_Click( object sender, EventArgs e )
        {
            TreeNode node = treeView1.SelectedNode;
            if (node.Tag is NodeClass nodeClass)
            {
                if (nodeClass.NodeType == NodeClassInfo.NodeClass)
                {
                    // 允许添加Modbus-Tcp服务器
                    using (FormAlienNode formNode = new FormAlienNode( ))
                    {
                        if (formNode.ShowDialog( ) == DialogResult.OK)
                        {
                            formNode.AlienNode.Name = GetUniqueName( node, formNode.AlienNode.Name );

                            TreeNode nodeNew = new TreeNode( formNode.AlienNode.Name );
                            nodeNew.ImageKey = "server_Local_16xLG";
                            nodeNew.SelectedImageKey = "server_Local_16xLG";
                            nodeNew.Tag = formNode.AlienNode;
                            node.Nodes.Add( nodeNew );
                            node.Expand( );
                            isNodeSettingsModify = true;
                        }
                    }
                }
            }
        }


        private void 新增RequestToolStripMenuItem1_Click( object sender, EventArgs e )
        {
            TreeNode node = treeView1.SelectedNode;
            if (node.Tag is NodeClass nodeClass)
            {
                if (nodeClass.NodeType == NodeClassInfo.NodeClass)
                {
                    // 允许添加Modbus-Tcp服务器
                    using (FormRegularNode formNode = new FormRegularNode( ))
                    {
                        if (formNode.ShowDialog( ) == DialogResult.OK)
                        {
                            formNode.RegularNode.Name = GetUniqueName( node, formNode.RegularNode.Name );

                            TreeNode nodeNew = new TreeNode( formNode.RegularNode.Name );
                            nodeNew.ImageKey = "usbcontroller";
                            nodeNew.SelectedImageKey = "usbcontroller";
                            nodeNew.Tag = formNode.RegularNode;
                            node.Nodes.Add( nodeNew );
                            node.Expand( );
                            isNodeSettingsModify = true;
                        }
                    }
                }
            }
        }

        #endregion

        #region DeviceRequestNode Add


        private void 新增RequestToolStripMenuItem_Click( object sender, EventArgs e )
        {
            // 新增了Modbus-Tcp客户端
            TreeNode node = treeView1.SelectedNode;
            if (node.Tag is DeviceNode deviceNode)
            {
                // 允许添加请求
                using (FormRequest formNode = new FormRequest( new DeviceRequest( ) ,null))
                {
                    if (formNode.ShowDialog( ) == DialogResult.OK)
                    {
                        formNode.DeviceRequest.Name = GetUniqueName( node, formNode.DeviceRequest.Name );

                        TreeNode nodeNew = new TreeNode( formNode.DeviceRequest.Name );
                        nodeNew.ImageKey = "usbcontroller";
                        nodeNew.SelectedImageKey = "usbcontroller";
                        nodeNew.Tag = formNode.DeviceRequest;
                        node.Nodes.Add( nodeNew );
                        node.Expand( );
                        isNodeSettingsModify = true;
                    }
                }
            }
        }


        private void toolStripMenuItem2_Click( object sender, EventArgs e )
        {
            // 新增单个的解析规则对象
            TreeNode node = treeView1.SelectedNode;
            using (FormRegularItemNode formNode = new FormRegularItemNode( ))
            {
                if (formNode.ShowDialog( ) == DialogResult.OK)
                {
                    formNode.RegularNode.Name = GetUniqueName( node, formNode.RegularNode.Name );

                    TreeNode nodeNew = new TreeNode( formNode.RegularNode.Name );
                    nodeNew.ImageKey = "Operator_660";
                    nodeNew.SelectedImageKey = "Operator_660";
                    nodeNew.Tag = formNode.RegularNode;
                    node.Nodes.Add( nodeNew );
                    node.Expand( );
                    isNodeSettingsModify = true;
                    UpdateTreeData( );
                }
            }
        }



        #endregion

        #region Node Edit


        private void 编辑类别editClassToolStripMenuItem_Click( object sender, EventArgs e )
        {
            // 节点被选择的时候
            TreeNode node = treeView1.SelectedNode;
            if (node.ImageKey == "VirtualMachine_16xLG")
            {
                MessageBox.Show( "无法编辑系统节点！" );
                return;
            }

            if (node.Tag is NodeClass nodeClass)
            {
                if (nodeClass.NodeType == NodeClassInfo.NodeClass)
                {
                    // 编辑了节点
                    using (FormNodeClass formNode = new FormNodeClass( nodeClass ))
                    {
                        if (formNode.ShowDialog( ) == DialogResult.OK)
                        {
                            node.Text = formNode.SelectedNodeClass.Name;
                            node.Tag = formNode.SelectedNodeClass;
                            isNodeSettingsModify = true;
                        }
                    }
                }
                else if (nodeClass.NodeType == NodeClassInfo.ServerNode)
                {
                    if (node.Tag is AlienServerNode alienNode)
                    {
                        // 编辑了异形服务器节点信息
                        using (FormAlienNode formNode = new FormAlienNode( null ))
                        {
                            if (formNode.ShowDialog( ) == DialogResult.OK)
                            {
                                node.Text = formNode.AlienNode.Name;
                                node.Tag = formNode.AlienNode;
                                isNodeSettingsModify = true;
                            }
                        }
                    }
                    else if(node.Tag is NodeModbusServer nodeModbusServer)
                    {
                        // 编辑了异形服务器节点信息
                        using (FormModbusServer formNode = new FormModbusServer( null ))
                        {
                            if (formNode.ShowDialog( ) == DialogResult.OK)
                            {
                                node.Text = formNode.ModbusServer.Name;
                                node.Tag = formNode.ModbusServer;
                                isNodeSettingsModify = true;
                            }
                        }
                    }
                }
                else if (nodeClass.NodeType == NodeClassInfo.ServerNode)
                {
                    if (node.Tag is NodeModbusServer serverNode)
                    {
                        // 编辑了异形服务器节点信息
                        using (FormModbusServer formNode = new FormModbusServer( serverNode ))
                        {
                            if (formNode.ShowDialog( ) == DialogResult.OK)
                            {
                                node.Text = formNode.ModbusServer.Name;
                                node.Tag = formNode.ModbusServer;
                                isNodeSettingsModify = true;
                            }
                        }
                    }
                }
                else if (nodeClass.NodeType == NodeClassInfo.RegularNode)
                {
                    if (node.Tag is RegularNode regularNode)
                    {
                        // 编辑了规则节点
                        using (FormRegularNode formNode = new FormRegularNode( regularNode ))
                        {
                            if (formNode.ShowDialog( ) == DialogResult.OK)
                            {
                                node.Text = formNode.RegularNode.Name;
                                node.Tag = formNode.RegularNode;
                                isNodeSettingsModify = true;
                            }
                        }
                    }
                }
                else if (nodeClass.NodeType == NodeClassInfo.RegularItemNode)
                {
                    if (node.Tag is RegularItemNode regularItemNode)
                    {
                        // 编辑了规则节点
                        using (FormRegularItemNode formNode = new FormRegularItemNode( regularItemNode ))
                        {
                            if (formNode.ShowDialog( ) == DialogResult.OK)
                            {
                                node.Text = formNode.RegularNode.Name;
                                node.Tag = formNode.RegularNode;
                                isNodeSettingsModify = true;
                            }
                        }
                    }
                }
                else
                {
                    if (node.Tag is NodeModbusTcpClient modbusTcpNode)
                    {
                        // 编辑了Modbus-tcp节点
                        using (FormModbusTcp formNode = new FormModbusTcp( modbusTcpNode ))
                        {
                            if (formNode.ShowDialog( ) == DialogResult.OK)
                            {
                                node.Text = formNode.ModbusTcpNode.Name;
                                node.Tag = formNode.ModbusTcpNode;
                                isNodeSettingsModify = true;
                            }
                        }
                    }
                    else if (node.Tag is NodeModbusTcpAline modbusTcpAline)
                    {
                        // 编辑了Modbus-aline节点
                        using (FormModbusTcpAlien formNode = new FormModbusTcpAlien( modbusTcpAline ))
                        {
                            if (formNode.ShowDialog( ) == DialogResult.OK)
                            {
                                node.Text = formNode.ModbusTcpAline.Name;
                                node.Tag = formNode.ModbusTcpAline;
                                isNodeSettingsModify = true;
                            }
                        }
                    }
                    else if (node.Tag is DeviceRequest deviceRequest)
                    {
                        // 编辑了Request节点
                        using (FormRequest formRequest = new FormRequest( deviceRequest, null ))
                        {
                            if (formRequest.ShowDialog( ) == DialogResult.OK)
                            {
                                node.Text = formRequest.DeviceRequest.Name;
                                node.Tag = formRequest.DeviceRequest;
                                isNodeSettingsModify = true;
                            }
                        }
                    }
                    else if (node.Tag is NodeMelsecMc nodeMelsecMc)
                    {
                        // 编辑了三菱的节点数据
                        using (FormMelsec3E formNode = new FormMelsec3E( nodeMelsecMc ))
                        {
                            if (formNode.ShowDialog( ) == DialogResult.OK)
                            {
                                node.Text = formNode.MelsecMc.Name;
                                node.Tag = formNode.MelsecMc;
                                isNodeSettingsModify = true;
                            }
                        }
                    }
                    else if (node.Tag is NodeOmron nodeOmron)
                    {
                        // 编辑了欧姆龙的节点数据
                        using (FormOmron formNode = new FormOmron( nodeOmron ))
                        {
                            if (formNode.ShowDialog( ) == DialogResult.OK)
                            {
                                node.Text = formNode.NodeOmron.Name;
                                node.Tag = formNode.NodeOmron;
                                isNodeSettingsModify = true;
                            }
                        }
                    }
                    else if (node.Tag is NodeSiemens nodeSiemens)
                    {
                        // 编辑了欧姆龙的节点数据
                        using (FormSiemens formNode = new FormSiemens( nodeSiemens ))
                        {
                            if (formNode.ShowDialog( ) == DialogResult.OK)
                            {
                                node.Text = formNode.NodeSiemens.Name;
                                node.Tag = formNode.NodeSiemens;
                                isNodeSettingsModify = true;
                            }
                        }
                    }
                    else if (node.Tag is NodeEmpty nodeEmpty)
                    {
                        // 编辑了欧姆龙的节点数据
                        using (FormEmpty formNode = new FormEmpty( nodeEmpty ))
                        {
                            if (formNode.ShowDialog( ) == DialogResult.OK)
                            {
                                node.Text = formNode.NodeEmpty.Name;
                                node.Tag = formNode.NodeEmpty;
                                isNodeSettingsModify = true;
                            }
                        }
                    }
                }
            }
        }


        #endregion
        
        #region Node Delete

        private void 删除deleteToolStripMenuItem_Click( object sender, EventArgs e )
        {
            // 删除节点信息
            TreeNode node = treeView1.SelectedNode;
            if (node.ImageKey == "VirtualMachine_16xLG")
            {
                MessageBox.Show( "无法删除系统节点！" );
                return;
            }



            isNodeSettingsModify = true;
            if (node.Nodes.Count == 0)
            {
                node.Parent.Nodes.Remove( node );
            }
            else
            {
                if (MessageBox.Show( "还有子节点数据存在，是否真的删除节点及子节点信息？", "删除确认", MessageBoxButtons.YesNo, MessageBoxIcon.Warning ) == DialogResult.Yes)
                {
                    node.Parent.Nodes.Remove( node );
                }
            }
        }


        #endregion

        #region Node Render


        private void treeView1_AfterSelect( object sender, TreeViewEventArgs e )
        {
            // 节点被选择的时候
            TreeNode node = treeView1.SelectedNode;
            if (node.Tag is NodeClass nodeClass)
            {
                if (nodeClass.GetType( ) == typeof( RegularNode ))
                {
                    treeNodeSelected = node;

                    if (!panel2.Visible) panel2.Visible = true;

                    selectedRegularItemName = string.Empty;
                    UpdateTreeData( );
                }
                else if (nodeClass.GetType( ) == typeof( RegularItemNode ))
                {
                    treeNodeSelected = node.Parent;

                    if (!panel2.Visible) panel2.Visible = true;

                    selectedRegularItemName = nodeClass.Name;
                    UpdateTreeData( );
                }
                else
                {
                    if (panel2.Visible) panel2.Visible = false;

                    // 显示选择的节点信息
                    DataGridViewRenderNodeClass( nodeClass );
                }
            }
        }


        


        private void DataGridSpecifyRowCount( int row )
        {
            if (dataGridView1.RowCount < row)
            {
                // 扩充
                dataGridView1.Rows.Add( row - dataGridView1.RowCount );
            }
            else if (dataGridView1.RowCount > row)
            {
                int deleteRows = dataGridView1.RowCount - row;
                for (int i = 0; i < deleteRows; i++)
                {
                    dataGridView1.Rows.RemoveAt( dataGridView1.Rows.Count - 1 );
                }
            }
            if (row > 0)
            {
                dataGridView1.Rows[0].Cells[0].Selected = false;
            }
        }

        private void DataGridViewRenderNodeClass( NodeClass nodeClass )
        {
            var renders = nodeClass.GetNodeClassRenders( );
            DataGridSpecifyRowCount( renders.Count );
            for (int i = 0; i < renders.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = renders[i].ValueName;
                dataGridView1.Rows[i].Cells[1].Value = renders[i].Value;
            }
        }


        #endregion

        #region Render Bitmap


        private void FormNodeSetting_SizeChanged( object sender, EventArgs e )
        {
            if (panel2.Visible)
            {
                UpdateTreeData( );
            }
        }

        private void UpdateTreeData( )
        {
            if (treeNodeSelected == null) return;

            if (treeNodeSelected.Tag is RegularNode regularNode)
            {
                if (panel1.Width < 10) return;

                List<RegularItemNode> regularNodes = new List<RegularItemNode>( );
                foreach (TreeNode item in treeNodeSelected.Nodes)
                {
                    if (item.Tag is RegularItemNode regular)
                    {
                        regularNodes.Add( regular );
                    }
                }

                pictureBox1.Image?.Dispose( );
                pictureBox1.Image = GetRenderInfo( regularNodes, selectedRegularItemName );
            }
        }

        private int GetNumberByUplimit( int value, int count )
        {
            if (value == 0) return 1;
            if (value % count == 0)
            {
                return value / count;
            }
            else
            {
                return value / count + 1;
            }
        }

        private Point CalculatePointWithByteIndex( int paint_x, int paint_y, int every_line_count, int index )
        {
            return new Point( paint_x + index % every_line_count * 20 + 8, paint_y + 17 + index / every_line_count * 50 + 8 );
        }

        /// <summary>
        /// 绘制小框框的上下的辅助信息
        /// </summary>
        /// <param name="g">绘图资源</param>
        /// <param name="paint_x">当前行绘制的X轴起点</param>
        /// <param name="paint_y">当前行绘制的Y轴起点</param>
        /// <param name="every_line_count">每一行的绘制的字节个数</param>
        /// <param name="index">当前变量信息开始的字节索引</param>
        /// <param name="byteLength">当前变量的所占的总字节长度</param>
        /// <param name="isDowm">选择绘制字节下方的数据信息，还是字节上方的数据信息</param>
        /// <param name="isSelect">当前的变量名是否被选中了</param>
        /// <param name="info">当绘制下方数据时，是变量名信息，当绘制上方时，是类型文本</param>
        /// <param name="font">字体</param>
        /// <param name="stringFormat">绘制文本时候的格式化信息</param>
        /// <param name="regularNode">当前变量的规则类型，用来传递类型的数据长度，字符串长度信息的</param>
        private void PaintLineAuxiliary( Graphics g, int paint_x, int paint_y, int every_line_count, int index, int byteLength, bool isDowm,bool isSelect, string info, Font font, StringFormat stringFormat, RegularItemNode regularNode )
        {
            Point point1 = CalculatePointWithByteIndex( paint_x, paint_y, every_line_count, index );
            Point point2 = CalculatePointWithByteIndex( paint_x, paint_y, every_line_count, index + byteLength - 1 );
            Point point1_right = CalculatePointWithByteIndex( paint_x, paint_y, every_line_count, every_line_count - 1 );
            Point point2_left = CalculatePointWithByteIndex( paint_x, paint_y, every_line_count, 0 );

            if (point1.Y == point2.Y)
            {
                // 同一行的情况
                if (isDowm)
                {
                    // 先绘制选中时的状态效果
                    if (isSelect)
                    {
                        g.FillRectangle( Brushes.LightPink, new Rectangle( point1.X - 9, point1.Y - 28, point2.X - point1.X + 18, 56 ) );
                    }


                    // 绘制从开始到结束的效果
                    point1.Offset( 0, 12 );
                    point2.Offset( 0, 12 );
                    g.DrawLine( Pens.DimGray, point1, new Point( point1.X, point1.Y - 3 ) );
                    g.DrawLine( Pens.DimGray, point2, new Point( point2.X, point2.Y - 3 ) );
                    g.DrawLine( Pens.DimGray, point1, point2 );


                    // 绘制下方的文本
                    Rectangle rectangle = new Rectangle( point1.X - 40, point1.Y, point2.X - point1.X + 80, 20 );
                    g.DrawString( regularNode.TypeLength == 1 ? info : $"{info} * {regularNode.TypeLength}", Font, Brushes.Blue, rectangle, stringFormat );
                }
                else
                {
                    // 绘制从开始到结束的效果
                    point1.Offset( 0, -11 );
                    point2.Offset( 0, -11 );
                    g.DrawLine( Pens.Chocolate, point1, new Point( point1.X, point1.Y + 3 ) );
                    g.DrawLine( Pens.Chocolate, point2, new Point( point2.X, point2.Y + 3 ) );
                    g.DrawLine( Pens.Chocolate, point1, point2 );

                    // 绘制下方的文本
                    Rectangle rectangle = new Rectangle( point1.X - 40, point1.Y - 14, point2.X - point1.X + 80, 15 );
                    if (isShowText) g.DrawString( info, font, Brushes.Green, rectangle, stringFormat );
                }
            }
            else
            {
                if (isDowm)
                {
                    // 跨行的情况就比较麻烦，先绘制第一行，从左到最右边

                    // 先绘制选中时的状态效果
                    if (isSelect)
                    {
                        g.FillRectangle( Brushes.LightPink, new Rectangle( point1.X - 9, point1.Y - 28, point1_right.X, 56 ) );
                        g.FillRectangle( Brushes.LightPink, new Rectangle( point2_left.X - 10, point2.Y - 28, point2.X - point2_left.X + 19, 56 ) );
                    }

                    point1.Offset( 0, 12 );
                    point2.Offset( 0, 12 );

                    point1_right.Y = point1.Y;
                    point1_right.X += 10;
                    
                    g.DrawLine( Pens.DimGray, point1, new Point( point1.X, point1.Y - 3 ) );
                    g.DrawLine( Pens.DimGray, point1, point1_right );


                    point2_left.Y = point2.Y;
                    point2_left.X -= 10;
                    g.DrawLine( Pens.DimGray, point2, new Point( point2.X, point2.Y - 3 ) );
                    g.DrawLine( Pens.DimGray, point2, point2_left );

                    if ((point1_right.X - point1.X) > (point2.X - point2_left.X))
                    {
                        Rectangle rectangle = new Rectangle( point1.X - 40, point1.Y, point1_right.X - point1.X + 80, 20 );
                        if (regularNode.TypeLength == 1)
                        {
                            g.DrawString( info, Font, Brushes.Blue, rectangle, stringFormat );
                        }
                        else
                        {
                            g.DrawString( info + "*" + regularNode.TypeLength, Font, Brushes.Blue, rectangle, stringFormat );
                        }
                    }
                    else
                    {
                        Rectangle rectangle = new Rectangle( point2_left.X - 40, point2.Y, point2.X - point2_left.X + 80, 20 );
                        if (regularNode.TypeLength == 1)
                        {
                            g.DrawString( info, Font, Brushes.Blue, rectangle, stringFormat );
                        }
                        else
                        {
                            g.DrawString( info + "*" + regularNode.TypeLength, Font, Brushes.Blue, rectangle, stringFormat );
                        }
                    }
                }
                else
                {
                    point1.Offset( 0, -11 );
                    point2.Offset( 0, -11 );


                    point1_right.Y = point1.Y;
                    point1_right.X += 10;
                    g.DrawLine( Pens.Chocolate, point1, new Point( point1.X, point1.Y + 3 ) );
                    g.DrawLine( Pens.Chocolate, point1, point1_right );

                    point2_left.Y = point2.Y;
                    point2_left.X -= 10;
                    g.DrawLine( Pens.Chocolate, point2, new Point( point2.X, point2.Y + 3 ) );
                    g.DrawLine( Pens.Chocolate, point2, point2_left );


                    if ((point1_right.X - point1.X) > (point2.X - point2_left.X))
                    {
                        Rectangle rectangle = new Rectangle( point1.X - 40, point1.Y - 14, point1_right.X - point1.X + 80, 15 );
                        if (isShowText) g.DrawString( info, Font, Brushes.Green, rectangle, stringFormat );
                    }
                    else
                    {
                        Rectangle rectangle = new Rectangle( point2_left.X - 40, point2.Y - 14, point2.X - point2_left.X + 80, 15 );
                        if (isShowText) g.DrawString( info, Font, Brushes.Green, rectangle, stringFormat );
                    }

                }
            }
        }

        private readonly int EveryByteWidth = 16;

        private Bitmap GetRenderInfo( List<RegularItemNode> regulars, string selectedRegular )
        {
            regulars.Sort( );
            int max_byte = regulars.Count == 0 ? 0 : regulars.Max( m => m.GetLengthByte( ) );
            int every_byte_occupy = EveryByteWidth + 4;
            int every_line_count = (panel1.Width - 19 - 90) / every_byte_occupy;
            if (every_line_count < 10) every_line_count = 10;
            int line_count = GetNumberByUplimit( max_byte, every_line_count );


            Bitmap bitmap = new Bitmap( panel1.Width - 19, line_count * 50 + 5 );
            if (max_byte == 0) return bitmap;
            Graphics g = Graphics.FromImage( bitmap );
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            StringFormat stringFormat = new StringFormat( );
            stringFormat.LineAlignment = StringAlignment.Center;
            stringFormat.Alignment = StringAlignment.Center;
            Font font_7 = new Font( "宋体", 7f );

            g.Clear( Color.AliceBlue );


            int paint_x = 85;
            int paint_y = 2;
            int count = 0;
            g.DrawLine( Pens.Gray, paint_x - 5, 0, paint_x - 5, bitmap.Height );

            for (int i = 0; i < line_count; i++)
            {
                g.DrawString( $"[{count.ToString( "D3" )} - {(count + Math.Min( max_byte - count - 1, every_line_count - 1 )).ToString( "D3" )}]", Font, Brushes.DimGray, new Point( 2, paint_y + EveryByteWidth ) );

                for (int j = 0; j < every_line_count; j++)
                {
                    Rectangle rec = new Rectangle( paint_x + j * (EveryByteWidth + 4), paint_y + 17, EveryByteWidth, EveryByteWidth );
                    g.DrawRectangle( Pens.Gray, rec );
                    g.DrawString( count.ToString( ), font_7, Brushes.Black, new Rectangle( paint_x + j * every_byte_occupy - every_byte_occupy, paint_y + 17, 56, EveryByteWidth ), stringFormat );
                    count++;

                    if (count >= max_byte)
                        break;
                }

                paint_y += 50;
            }

            paint_y = 2;

            for (int i = 0; i < regulars.Count; i++)
            {
                RegularNodeTypeItem regularNodeTypeItem = RegularNodeTypeItem.GetDataPraseItemByCode( regulars[i].RegularCode );

                bool isSelected = string.IsNullOrEmpty( selectedRegular ) ? false : selectedRegular == regulars[i].Name;

                int start = regulars[i].GetStartedByteIndex( );
                int length = regulars[i].GetLengthByte( ) - regulars[i].GetStartedByteIndex( );

                int rowStart = GetNumberByUplimit( start, every_line_count );
                int rowEnd = GetNumberByUplimit( start + length, every_line_count );


                // 同行的情况
                PaintLineAuxiliary( g, paint_x, paint_y, every_line_count, start, length, true,isSelected, regulars[i].Name, Font, stringFormat, regulars[i] );

                // 绘制上面的数据
                if (regularNodeTypeItem.Length != 0)
                {
                    int tmp = start;
                    for (int j = 0; j < length / regularNodeTypeItem.Length; j++)
                    {
                        PaintLineAuxiliary( g, paint_x, paint_y, every_line_count, tmp, regularNodeTypeItem.Length, false, isSelected, regularNodeTypeItem.Text, Font, stringFormat, regulars[i] );
                        tmp += regularNodeTypeItem.Length;
                    }
                }
                else
                {
                    PaintLineAuxiliary( g, paint_x, paint_y, every_line_count, start, length, false, isSelected, regularNodeTypeItem.Text, Font, stringFormat, regulars[i] );
                }


                for (int j = 0; j < length; j++)
                {
                    int paint_x_tmp = paint_x + (start + j) % every_line_count * every_byte_occupy;
                    int paint_y_tmp = paint_y + 17 + (start + j) / every_line_count * 50;

                    Rectangle rec = new Rectangle( paint_x_tmp, paint_y_tmp, 16, 16 );
                    g.FillRectangle( regularNodeTypeItem.BackColor, rec );
                    g.DrawRectangle( Pens.DimGray, rec );
                    g.DrawString( (regulars[i].GetStartedByteIndex( ) + j).ToString( ), font_7, Brushes.Black, new Rectangle( paint_x_tmp - 20, paint_y_tmp, 56, 16 ), stringFormat );
                }
            }



            stringFormat.Dispose( );
            font_7.Dispose( );
            return bitmap;
        }



        #endregion

        #region ContextMenu Show

        private void treeView1_MouseDown( object sender, MouseEventArgs e )
        {
            if (e.Button == MouseButtons.Right)
            {
                treeView1.SelectedNode = treeView1.GetNodeAt( e.Location );
                // 右键了控件
                TreeNode node = treeView1.SelectedNode;
                if (node == null) return;

                // 右键了Server节点
                if (node.Text == "Server" && node.ImageKey == "VirtualMachine_16xLG")
                {
                    cMS_ModbusServer.Show( treeView1, e.Location );
                    return;
                }


                // 右键了Regular节点
                if (node.Text == "Regular" && node.ImageKey == "VirtualMachine_16xLG")
                {
                    cMS_Regular_Add.Show( treeView1, e.Location );
                    return;
                }



                if (node.Tag.GetType( ) == typeof( NodeClass ))
                {
                    // 显示第一个菜单框
                    cMS_Device.Show( treeView1, e.Location );
                }
                else if(node.Tag.GetType( ) == typeof( AlienServerNode ))
                {
                    // 显示新增异形客户端
                    cMS_AlienClient.Show( treeView1, e.Location );
                }
                else if (node.Tag.GetType( ) == typeof( NodeModbusServer ))
                {
                    // 显示编辑Modbus服务器数据
                    cMs_EditRequest.Show( treeView1, e.Location );
                }
                else if (node.Tag is DeviceNode)
                {
                    // 显示第二个菜单框
                    cMS_Request.Show( treeView1, e.Location );
                }
                else if (node.Tag is DeviceRequest)
                {
                    // 显示第三个菜单框
                    cMs_EditRequest.Show( treeView1, e.Location );
                }
                else if (node.Tag is RegularNode)
                {
                    // 显示第三个菜单框
                    cMS_EditRegular.Show( treeView1, e.Location );
                }
                else if (node.Tag is RegularItemNode)
                {
                    // 显示第三个菜单框
                    cMs_EditRequest.Show( treeView1, e.Location );
                }
            }
        }

        #endregion
        
        #region Node Save

        private XElement AddTreeNode( TreeNode node )
        {
            if (node.Tag is NodeClass nodeClass)
            {
                XElement element = nodeClass.ToXmlElement( );
                foreach (TreeNode item in node.Nodes)
                {
                    element.Add( AddTreeNode( item ) );
                }
                return element;
            }

            return null;
        }


        private void SaveNodes(string fileName)
        {
            try
            {
                XElement element = new XElement( "Settings" );
                foreach (TreeNode item in treeView1.Nodes)
                {
                    element.Add( AddTreeNode( item ) );
                }

                element.Save( fileName );

                MessageBox.Show( "保存成功！" );
            }
            catch (Exception ex)
            {
                MessageBox.Show( "保存失败！原因：" + ex.Message );
            }
        }


        private void 保存文件ToolStripMenuItem_Click( object sender, EventArgs e )
        {
            SaveNodes( fileName );
            isNodeSettingsModify = false;
        }

        private void 另存为ToolStripMenuItem_Click( object sender, EventArgs e )
        {
            using (SaveFileDialog dialog = new SaveFileDialog( ))
            {
                dialog.Filter = "xml文件|*.xml";
                dialog.CheckFileExists = true;
                if (dialog.ShowDialog( ) == DialogResult.OK)
                {
                    SaveNodes( dialog.FileName );
                    isNodeSettingsModify = false;
                }
            }
        }

        #endregion

        #region Node Load


        private void TreeNodeRender( TreeNode treeNode, XElement element )
        {
            foreach (XElement item in element.Elements( ))
            {
                if (item.Name == "NodeClass")
                {
                    TreeNode node = new TreeNode( item.Attribute( "Name" ).Value );
                    node.ImageKey = "Class_489";
                    node.SelectedImageKey = "Class_489";

                    NodeClass nodeClass = new NodeClass( );
                    nodeClass.LoadByXmlElement( item );
                    node.Tag = nodeClass;
                    treeNode.Nodes.Add( node );

                    TreeNodeRender( node, item );
                }
                else if (item.Name == "DeviceNode")
                {
                    int type = int.Parse( item.Attribute( "DeviceType" ).Value );

                    TreeNode deviceNode = new TreeNode( item.Attribute( "Name" ).Value );
                    if (type == DeviceNode.ModbusTcpClient)
                    {
                        deviceNode.ImageKey = "Module_648";
                        deviceNode.SelectedImageKey = "Module_648";

                        NodeModbusTcpClient modbusNode = new NodeModbusTcpClient( );
                        modbusNode.LoadByXmlElement( item );
                        deviceNode.Tag = modbusNode;
                    }
                    else if (type == DeviceNode.ModbusTcpAlien)
                    {
                        deviceNode.ImageKey = "Module_648";
                        deviceNode.SelectedImageKey = "Module_648";

                        NodeModbusTcpAline modbusAlien = new NodeModbusTcpAline( );
                        modbusAlien.LoadByXmlElement( item );
                        deviceNode.Tag = modbusAlien;
                    }
                    else if (type == DeviceNode.MelsecMcQna3E)
                    {
                        deviceNode.ImageKey = "Enum_582";
                        deviceNode.SelectedImageKey = "Enum_582";

                        NodeMelsecMc node = new NodeMelsecMc( );
                        node.LoadByXmlElement( item );
                        deviceNode.Tag = node;
                    }
                    else if (type == DeviceNode.Siemens)
                    {
                        deviceNode.ImageKey = "Event_594";
                        deviceNode.SelectedImageKey = "Event_594";

                        NodeSiemens node = new NodeSiemens( );
                        node.LoadByXmlElement( item );
                        deviceNode.Tag = node;
                    }
                    else if (type == DeviceNode.DeviceNone)
                    {
                        deviceNode.ImageKey = "Method_636";
                        deviceNode.SelectedImageKey = "Method_636";

                        NodeEmpty node = new NodeEmpty( );
                        node.LoadByXmlElement( item );
                        deviceNode.Tag = node;
                    }
                    else if (type == DeviceNode.Omron)
                    {
                        deviceNode.ImageKey = "HotSpot_10548_color";
                        deviceNode.SelectedImageKey = "HotSpot_10548_color";

                        NodeOmron node = new NodeOmron( );
                        node.LoadByXmlElement( item );
                        deviceNode.Tag = node;
                    }
                    else if (type == DeviceNode.SimplifyNet)
                    {
                        deviceNode.ImageKey = "FlagRed_16x";
                        deviceNode.SelectedImageKey = "FlagRed_16x";

                        NodeSimplifyNet node = new NodeSimplifyNet( );
                        node.LoadByXmlElement( item );
                        deviceNode.Tag = node;
                    }


                    treeNode.Nodes.Add( deviceNode );
                    foreach (XElement request in item.Elements( "DeviceRequest" ))
                    {
                        TreeNode nodeRequest = new TreeNode( request.Attribute( "Name" ).Value );
                        nodeRequest.ImageKey = "usbcontroller";
                        nodeRequest.SelectedImageKey = "usbcontroller";

                        DeviceRequest deviceRequest = new DeviceRequest( );
                        deviceRequest.LoadByXmlElement( request );
                        nodeRequest.Tag = deviceRequest;
                        deviceNode.Nodes.Add( nodeRequest );
                    }

                }
                else if (item.Name == "ServerNode")
                {
                    int type = int.Parse( item.Attribute( "ServerType" ).Value );

                    if(type == ServerNode.ModbusServer)
                    {
                        TreeNode node = new TreeNode( item.Attribute( "Name" ).Value );
                        node.ImageKey = "server_Local_16xLG";
                        node.SelectedImageKey = "server_Local_16xLG";

                        NodeModbusServer nodeClass = new NodeModbusServer( );
                        nodeClass.LoadByXmlElement( item );
                        node.Tag = nodeClass;
                        treeNode.Nodes.Add( node );
                    }
                    else if(type == ServerNode.AlienServer)
                    {
                        TreeNode node = new TreeNode( item.Attribute( "Name" ).Value );
                        node.ImageKey = "server_Local_16xLG";
                        node.SelectedImageKey = "server_Local_16xLG";

                        AlienServerNode nodeClass = new AlienServerNode( );
                        nodeClass.LoadByXmlElement( item );
                        node.Tag = nodeClass;
                        treeNode.Nodes.Add( node );

                        TreeNodeRender( node, item );
                    }
                }
                else if(item.Name == "RegularNode")
                {
                    TreeNode node = new TreeNode( item.Attribute( "Name" ).Value );
                    node.ImageKey = "usbcontroller";
                    node.SelectedImageKey = "usbcontroller";

                    RegularNode nodeClass = new RegularNode( );
                    nodeClass.LoadByXmlElement( item );
                    node.Tag = nodeClass;
                    treeNode.Nodes.Add( node );

                    foreach (XElement regularItemXml in item.Elements( "RegularItemNode" ))
                    {
                        TreeNode treeNodeRegular = new TreeNode( regularItemXml.Attribute( "Name" ).Value );
                        treeNodeRegular.ImageKey = "Operator_660";
                        treeNodeRegular.SelectedImageKey = "Operator_660";

                        RegularItemNode regularItemNode = new RegularItemNode( );
                        regularItemNode.LoadByXmlElement( regularItemXml );
                        treeNodeRegular.Tag = regularItemNode;
                        node.Nodes.Add( treeNodeRegular );
                    }
                }
            }
        }

        private void LoadByFile( string fileName )
        {
            if (!System.IO.File.Exists( fileName )) return;
            try
            {
                foreach (TreeNode treeNode in treeView1.Nodes)
                {
                    treeNode.Nodes.Clear( );
                }

                XElement element = XElement.Load( fileName );
                if (element.Name != "Settings") return;

                // 提取Devices节点数据
                TreeNodeRender( treeView1.Nodes[0], element.Elements( ).ToArray( )[0] );

                TreeNodeRender( treeView1.Nodes[1], element.Elements( ).ToArray( )[1] );

                TreeNodeRender( treeView1.Nodes[2], element.Elements( ).ToArray( )[2] );

                treeView1.ExpandAll( );
            }
            catch (Exception ex)
            {
                MessageBox.Show( "加载文件失败，请确认是否系统生成的标准文件！原因：" + ex.Message );
            }
        }


        private void 打开文件ToolStripMenuItem_Click( object sender, EventArgs e )
        {
            using(OpenFileDialog fileDialog = new OpenFileDialog())
            {
                fileDialog.Filter = "Xml File|*.xml";
                fileDialog.Multiselect = false;
                if (fileDialog.ShowDialog( ) == DialogResult.OK)
                {
                    LoadByFile( fileDialog.FileName );
                    isNodeSettingsModify = true;
                }
            }

        }


        #endregion

        #region NodeName Update

        private bool IsNodeSameNodeExist( TreeNode node, string name )
        {
            bool result = false;
            foreach (TreeNode item in node.Nodes)
            {
                if (item.Text == name)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        private string GetUniqueName( TreeNode node, string name )
        {
            if (!IsNodeSameNodeExist( node, name )) return name;

            int index = 1;
            while (IsNodeSameNodeExist( node, name + index ))
            {
                index++;
            }
            return name + index;
        }

        #endregion
        
        #region Private Member

        private bool isNodeSettingsModify = false;             // 指示系统的节点是否已经被编辑过
        private string fileName = string.Empty;                // 文件加载和解析的路径
        private bool isShowText = true;                        // 是否显示规则名信息
        private TreeNode treeNodeSelected = null;              // 当前选择的树节点
        private string selectedRegularItemName = string.Empty; // 选择的规则节点变量的名称

        #endregion




    }
}
