using SharpNodeSettings.Node.Device;
using SharpNodeSettings.Node.NodeBase;
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



namespace SharpNodeSettings.Forms
{
    public partial class FormNodeSetting : Form
    {
        #region Constructor


        public FormNodeSetting()
        {
            InitializeComponent( );
            Icon = Util.GetWinformICon( );
        }

        #endregion

        #region Images

        private ImageList SharpImageList { get; set; }


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
                Name = "ModbusServer",
                Description = "所有的Modbus服务器的集合对象"
            };


            treeView1.Nodes[2].ImageKey = "VirtualMachine_16xLG";
            treeView1.Nodes[2].SelectedImageKey = "VirtualMachine_16xLG";
            treeView1.Nodes[2].Tag = new NodeClass( )
            {
                Name = "ModbusAlien",
                Description = "所有的异形ModbusTcp客户端的集合对象"
            };


            
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

        #region ClassNode Add


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

        #endregion
        
        #region Modbus-Tcp Node Add
        
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
                    if (node.Tag is ServerNode alienNode)
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

        #region Node Render


        private void treeView1_AfterSelect( object sender, TreeViewEventArgs e )
        {
            // 节点被选择的时候
            TreeNode node = treeView1.SelectedNode;
            if (node.Tag is NodeClass nodeClass)
            {
                // 显示选择的节点信息
                DataGridViewRenderNodeClass( nodeClass );
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

        #region ContextMenu Show

        private void treeView1_MouseDown( object sender, MouseEventArgs e )
        {
            if (e.Button == MouseButtons.Right)
            {
                treeView1.SelectedNode = treeView1.GetNodeAt( e.Location );
                // 右键了控件
                TreeNode node = treeView1.SelectedNode;
                if (node == null) return;

                if (node.Text == "ModbusAlien" && node.ImageKey == "VirtualMachine_16xLG")
                {
                    cMS_AlienNode.Show( treeView1, e.Location );
                    return;
                }


                if (node.Text == "ModbusServer" && node.ImageKey == "VirtualMachine_16xLG")
                {
                    cMS_ModbusServer.Show( treeView1, e.Location );
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
                else if (item.Name == "AlienNode")
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
                else if(item.Name == "ModbusServer")
                {
                    TreeNode node = new TreeNode( item.Attribute( "Name" ).Value );
                    node.ImageKey = "server_Local_16xLG";
                    node.SelectedImageKey = "server_Local_16xLG";

                    NodeModbusServer nodeClass = new NodeModbusServer( );
                    nodeClass.LoadByXmlElement( item );
                    node.Tag = nodeClass;
                    treeNode.Nodes.Add( node );

                    TreeNodeRender( node, item );
                }
            }
        }

        private void LoadByFile( string fileName )
        {
            if (!System.IO.File.Exists( fileName )) return;
            try
            {
                treeView1.Nodes[0].Nodes.Clear( );
                treeView1.Nodes[1].Nodes.Clear( );

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

        #region Node Add

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


        private void simplifyNetToolStripMenuItem_Click( object sender, EventArgs e )
        {
            TreeNode node = treeView1.SelectedNode;
            if (node.Tag is NodeClass nodeClass)
            {
                if (nodeClass.NodeType == NodeClassInfo.NodeClass)
                {
                    // 允许添加SimplifyNet设备
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
        

        #endregion
        
        #region Private Member

        private bool isNodeSettingsModify = false;            // 指示系统的节点是否已经被编辑过
        private string fileName = string.Empty;               // 文件加载和解析的路径

        #endregion


        

    }
}
