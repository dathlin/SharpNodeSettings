
namespace SharpNodeSettings.View
{
    partial class FormNodeSetting
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if (disposing && (components != null))
            {
                components.Dispose( );
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent( )
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Devices");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Server");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Regular");
            this.cMS_Device = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.新增类别ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.类别classToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.空设备toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.西门子PlcsiemensToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.三菱plcmelsecToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.欧姆龙plcomronToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modbustcpclientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑类别editClassToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.HslSharpValueName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HslSharpValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cMS_Request = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.新增RequestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑RequestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除RequestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.打开文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.另存为ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cMs_EditRequest = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.编辑节点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除节点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cMS_ModbusServer = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.新增ModbusTcpServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新增AlienServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cMS_AlienClient = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.异形ModbusTcpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑节点ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.删除节点ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new SharpNodeSettings.Controls.TreeViewEx();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.cMS_Regular_Add = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.新增RequestToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.cMS_EditRegular = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.cMS_Device.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.cMS_Request.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.cMs_EditRequest.SuspendLayout();
            this.cMS_ModbusServer.SuspendLayout();
            this.cMS_AlienClient.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.cMS_Regular_Add.SuspendLayout();
            this.cMS_EditRegular.SuspendLayout();
            this.SuspendLayout();
            // 
            // cMS_Device
            // 
            this.cMS_Device.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新增类别ToolStripMenuItem,
            this.编辑类别editClassToolStripMenuItem,
            this.删除deleteToolStripMenuItem});
            this.cMS_Device.Name = "contextMenuStrip1";
            this.cMS_Device.Size = new System.Drawing.Size(125, 70);
            // 
            // 新增类别ToolStripMenuItem
            // 
            this.新增类别ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.类别classToolStripMenuItem,
            this.空设备toolStripMenuItem,
            this.西门子PlcsiemensToolStripMenuItem,
            this.三菱plcmelsecToolStripMenuItem,
            this.欧姆龙plcomronToolStripMenuItem,
            this.modbustcpclientToolStripMenuItem});
            this.新增类别ToolStripMenuItem.Image = global::SharpNodeSettings.Properties.Resources.action_add_16xLG;
            this.新增类别ToolStripMenuItem.Name = "新增类别ToolStripMenuItem";
            this.新增类别ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.新增类别ToolStripMenuItem.Text = "新增类别";
            // 
            // 类别classToolStripMenuItem
            // 
            this.类别classToolStripMenuItem.Image = global::SharpNodeSettings.Properties.Resources.Class_489;
            this.类别classToolStripMenuItem.Name = "类别classToolStripMenuItem";
            this.类别classToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.类别classToolStripMenuItem.Text = "类别(class)";
            this.类别classToolStripMenuItem.Click += new System.EventHandler(this.类别classToolStripMenuItem_Click);
            // 
            // 空设备toolStripMenuItem
            // 
            this.空设备toolStripMenuItem.Image = global::SharpNodeSettings.Properties.Resources.Method_636;
            this.空设备toolStripMenuItem.Name = "空设备toolStripMenuItem";
            this.空设备toolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.空设备toolStripMenuItem.Text = "空设备(empty)";
            this.空设备toolStripMenuItem.Click += new System.EventHandler(this.空设备toolStripMenuItem_Click);
            // 
            // 西门子PlcsiemensToolStripMenuItem
            // 
            this.西门子PlcsiemensToolStripMenuItem.Image = global::SharpNodeSettings.Properties.Resources.Event_594;
            this.西门子PlcsiemensToolStripMenuItem.Name = "西门子PlcsiemensToolStripMenuItem";
            this.西门子PlcsiemensToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.西门子PlcsiemensToolStripMenuItem.Text = "西门子Plc(siemens)";
            this.西门子PlcsiemensToolStripMenuItem.Click += new System.EventHandler(this.西门子PlcsiemensToolStripMenuItem_Click);
            // 
            // 三菱plcmelsecToolStripMenuItem
            // 
            this.三菱plcmelsecToolStripMenuItem.Image = global::SharpNodeSettings.Properties.Resources.Enum_582;
            this.三菱plcmelsecToolStripMenuItem.Name = "三菱plcmelsecToolStripMenuItem";
            this.三菱plcmelsecToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.三菱plcmelsecToolStripMenuItem.Text = "三菱plc(melsec)";
            this.三菱plcmelsecToolStripMenuItem.Click += new System.EventHandler(this.三菱plcmelsecToolStripMenuItem_Click);
            // 
            // 欧姆龙plcomronToolStripMenuItem
            // 
            this.欧姆龙plcomronToolStripMenuItem.Image = global::SharpNodeSettings.Properties.Resources.HotSpot_10548_color;
            this.欧姆龙plcomronToolStripMenuItem.Name = "欧姆龙plcomronToolStripMenuItem";
            this.欧姆龙plcomronToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.欧姆龙plcomronToolStripMenuItem.Text = "欧姆龙plc(omron)";
            this.欧姆龙plcomronToolStripMenuItem.Click += new System.EventHandler(this.欧姆龙plcomronToolStripMenuItem_Click);
            // 
            // modbustcpclientToolStripMenuItem
            // 
            this.modbustcpclientToolStripMenuItem.Image = global::SharpNodeSettings.Properties.Resources.Module_648;
            this.modbustcpclientToolStripMenuItem.Name = "modbustcpclientToolStripMenuItem";
            this.modbustcpclientToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.modbustcpclientToolStripMenuItem.Text = "Modbus-tcp-client";
            this.modbustcpclientToolStripMenuItem.Click += new System.EventHandler(this.modbustcpclientToolStripMenuItem_Click);
            // 
            // 编辑类别editClassToolStripMenuItem
            // 
            this.编辑类别editClassToolStripMenuItem.Image = global::SharpNodeSettings.Properties.Resources.PencilAngled_16xLG_color;
            this.编辑类别editClassToolStripMenuItem.Name = "编辑类别editClassToolStripMenuItem";
            this.编辑类别editClassToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.编辑类别editClassToolStripMenuItem.Text = "编辑节点";
            this.编辑类别editClassToolStripMenuItem.Click += new System.EventHandler(this.编辑类别editClassToolStripMenuItem_Click);
            // 
            // 删除deleteToolStripMenuItem
            // 
            this.删除deleteToolStripMenuItem.Image = global::SharpNodeSettings.Properties.Resources.action_Cancel_16xLG;
            this.删除deleteToolStripMenuItem.Name = "删除deleteToolStripMenuItem";
            this.删除deleteToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.删除deleteToolStripMenuItem.Text = "删除节点";
            this.删除deleteToolStripMenuItem.Click += new System.EventHandler(this.删除deleteToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(212, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "系统的树节点信息：（右键进行操作）";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "数据列表";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.HslSharpValueName,
            this.HslSharpValue});
            this.dataGridView1.Location = new System.Drawing.Point(6, 24);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(686, 583);
            this.dataGridView1.TabIndex = 3;
            // 
            // HslSharpValueName
            // 
            this.HslSharpValueName.HeaderText = "数据名称(Value Name)";
            this.HslSharpValueName.Name = "HslSharpValueName";
            this.HslSharpValueName.ReadOnly = true;
            this.HslSharpValueName.Width = 200;
            // 
            // HslSharpValue
            // 
            this.HslSharpValue.HeaderText = "数据值(Value)";
            this.HslSharpValue.Name = "HslSharpValue";
            this.HslSharpValue.ReadOnly = true;
            this.HslSharpValue.Width = 280;
            // 
            // cMS_Request
            // 
            this.cMS_Request.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新增RequestToolStripMenuItem,
            this.编辑RequestToolStripMenuItem,
            this.删除RequestToolStripMenuItem});
            this.cMS_Request.Name = "contextMenuStrip2";
            this.cMS_Request.Size = new System.Drawing.Size(148, 70);
            // 
            // 新增RequestToolStripMenuItem
            // 
            this.新增RequestToolStripMenuItem.Image = global::SharpNodeSettings.Properties.Resources.action_add_16xLG;
            this.新增RequestToolStripMenuItem.Name = "新增RequestToolStripMenuItem";
            this.新增RequestToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.新增RequestToolStripMenuItem.Text = "新增Request";
            this.新增RequestToolStripMenuItem.Click += new System.EventHandler(this.新增RequestToolStripMenuItem_Click);
            // 
            // 编辑RequestToolStripMenuItem
            // 
            this.编辑RequestToolStripMenuItem.Image = global::SharpNodeSettings.Properties.Resources.PencilAngled_16xLG_color;
            this.编辑RequestToolStripMenuItem.Name = "编辑RequestToolStripMenuItem";
            this.编辑RequestToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.编辑RequestToolStripMenuItem.Text = "编辑节点";
            this.编辑RequestToolStripMenuItem.Click += new System.EventHandler(this.编辑类别editClassToolStripMenuItem_Click);
            // 
            // 删除RequestToolStripMenuItem
            // 
            this.删除RequestToolStripMenuItem.Image = global::SharpNodeSettings.Properties.Resources.action_Cancel_16xLG;
            this.删除RequestToolStripMenuItem.Name = "删除RequestToolStripMenuItem";
            this.删除RequestToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.删除RequestToolStripMenuItem.Text = "删除节点";
            this.删除RequestToolStripMenuItem.Click += new System.EventHandler(this.删除deleteToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开文件ToolStripMenuItem,
            this.保存文件ToolStripMenuItem,
            this.另存为ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1020, 25);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 打开文件ToolStripMenuItem
            // 
            this.打开文件ToolStripMenuItem.Image = global::SharpNodeSettings.Properties.Resources.arrow_open_16xLG;
            this.打开文件ToolStripMenuItem.Name = "打开文件ToolStripMenuItem";
            this.打开文件ToolStripMenuItem.Size = new System.Drawing.Size(84, 21);
            this.打开文件ToolStripMenuItem.Text = "打开文件";
            this.打开文件ToolStripMenuItem.Click += new System.EventHandler(this.打开文件ToolStripMenuItem_Click);
            // 
            // 保存文件ToolStripMenuItem
            // 
            this.保存文件ToolStripMenuItem.Image = global::SharpNodeSettings.Properties.Resources.save_16xLG;
            this.保存文件ToolStripMenuItem.Name = "保存文件ToolStripMenuItem";
            this.保存文件ToolStripMenuItem.Size = new System.Drawing.Size(84, 21);
            this.保存文件ToolStripMenuItem.Text = "保存文件";
            this.保存文件ToolStripMenuItem.Click += new System.EventHandler(this.保存文件ToolStripMenuItem_Click);
            // 
            // 另存为ToolStripMenuItem
            // 
            this.另存为ToolStripMenuItem.Image = global::SharpNodeSettings.Properties.Resources.save_16xLG;
            this.另存为ToolStripMenuItem.Name = "另存为ToolStripMenuItem";
            this.另存为ToolStripMenuItem.Size = new System.Drawing.Size(72, 21);
            this.另存为ToolStripMenuItem.Text = "另存为";
            this.另存为ToolStripMenuItem.Click += new System.EventHandler(this.另存为ToolStripMenuItem_Click);
            // 
            // cMs_EditRequest
            // 
            this.cMs_EditRequest.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.编辑节点ToolStripMenuItem,
            this.删除节点ToolStripMenuItem});
            this.cMs_EditRequest.Name = "contextMenuStrip3";
            this.cMs_EditRequest.Size = new System.Drawing.Size(125, 48);
            // 
            // 编辑节点ToolStripMenuItem
            // 
            this.编辑节点ToolStripMenuItem.Image = global::SharpNodeSettings.Properties.Resources.PencilAngled_16xLG_color;
            this.编辑节点ToolStripMenuItem.Name = "编辑节点ToolStripMenuItem";
            this.编辑节点ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.编辑节点ToolStripMenuItem.Text = "编辑节点";
            this.编辑节点ToolStripMenuItem.Click += new System.EventHandler(this.编辑类别editClassToolStripMenuItem_Click);
            // 
            // 删除节点ToolStripMenuItem
            // 
            this.删除节点ToolStripMenuItem.Image = global::SharpNodeSettings.Properties.Resources.action_Cancel_16xLG;
            this.删除节点ToolStripMenuItem.Name = "删除节点ToolStripMenuItem";
            this.删除节点ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.删除节点ToolStripMenuItem.Text = "删除节点";
            this.删除节点ToolStripMenuItem.Click += new System.EventHandler(this.删除deleteToolStripMenuItem_Click);
            // 
            // cMS_ModbusServer
            // 
            this.cMS_ModbusServer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新增ModbusTcpServerToolStripMenuItem,
            this.新增AlienServerToolStripMenuItem});
            this.cMS_ModbusServer.Name = "cMS_ModbusServer";
            this.cMS_ModbusServer.Size = new System.Drawing.Size(208, 48);
            // 
            // 新增ModbusTcpServerToolStripMenuItem
            // 
            this.新增ModbusTcpServerToolStripMenuItem.Image = global::SharpNodeSettings.Properties.Resources.action_add_16xLG;
            this.新增ModbusTcpServerToolStripMenuItem.Name = "新增ModbusTcpServerToolStripMenuItem";
            this.新增ModbusTcpServerToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.新增ModbusTcpServerToolStripMenuItem.Text = "新增ModbusTcpServer";
            this.新增ModbusTcpServerToolStripMenuItem.Click += new System.EventHandler(this.新增ModbusTcpServerToolStripMenuItem_Click);
            // 
            // 新增AlienServerToolStripMenuItem
            // 
            this.新增AlienServerToolStripMenuItem.Image = global::SharpNodeSettings.Properties.Resources.action_add_16xLG;
            this.新增AlienServerToolStripMenuItem.Name = "新增AlienServerToolStripMenuItem";
            this.新增AlienServerToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.新增AlienServerToolStripMenuItem.Text = "新增AlienServer";
            this.新增AlienServerToolStripMenuItem.Click += new System.EventHandler(this.新增AlienServerToolStripMenuItem_Click);
            // 
            // cMS_AlienClient
            // 
            this.cMS_AlienClient.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.编辑节点ToolStripMenuItem1,
            this.删除节点ToolStripMenuItem1});
            this.cMS_AlienClient.Name = "cMS_AlienClient";
            this.cMS_AlienClient.Size = new System.Drawing.Size(125, 70);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.异形ModbusTcpToolStripMenuItem});
            this.toolStripMenuItem1.Image = global::SharpNodeSettings.Properties.Resources.action_add_16xLG;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.toolStripMenuItem1.Text = "新增设备";
            // 
            // 异形ModbusTcpToolStripMenuItem
            // 
            this.异形ModbusTcpToolStripMenuItem.Image = global::SharpNodeSettings.Properties.Resources.Module_648;
            this.异形ModbusTcpToolStripMenuItem.Name = "异形ModbusTcpToolStripMenuItem";
            this.异形ModbusTcpToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.异形ModbusTcpToolStripMenuItem.Text = "异形Modbus-Tcp";
            this.异形ModbusTcpToolStripMenuItem.Click += new System.EventHandler(this.异形ModbusTcpToolStripMenuItem_Click);
            // 
            // 编辑节点ToolStripMenuItem1
            // 
            this.编辑节点ToolStripMenuItem1.Image = global::SharpNodeSettings.Properties.Resources.PencilAngled_16xLG_color;
            this.编辑节点ToolStripMenuItem1.Name = "编辑节点ToolStripMenuItem1";
            this.编辑节点ToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.编辑节点ToolStripMenuItem1.Text = "编辑节点";
            this.编辑节点ToolStripMenuItem1.Click += new System.EventHandler(this.编辑类别editClassToolStripMenuItem_Click);
            // 
            // 删除节点ToolStripMenuItem1
            // 
            this.删除节点ToolStripMenuItem1.Image = global::SharpNodeSettings.Properties.Resources.action_Cancel_16xLG;
            this.删除节点ToolStripMenuItem1.Name = "删除节点ToolStripMenuItem1";
            this.删除节点ToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.删除节点ToolStripMenuItem1.Text = "删除节点";
            this.删除节点ToolStripMenuItem1.Click += new System.EventHandler(this.删除deleteToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(1020, 610);
            this.splitContainer1.SplitterDistance = 321;
            this.splitContainer1.TabIndex = 6;
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.Location = new System.Drawing.Point(6, 24);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "node_devices";
            treeNode1.Text = "Devices";
            treeNode2.Name = "node_modbusServer";
            treeNode2.Text = "Server";
            treeNode3.Name = "node_modbusAlien";
            treeNode3.Text = "Regular";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            this.treeView1.Size = new System.Drawing.Size(312, 583);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseDown);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(695, 610);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.checkBox1);
            this.panel2.Location = new System.Drawing.Point(132, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(490, 483);
            this.panel2.TabIndex = 4;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.AutoScroll = true;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.pictureBox1);
            this.panel3.Location = new System.Drawing.Point(4, 26);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(483, 454);
            this.panel3.TabIndex = 12;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(453, 418);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(4, 4);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(147, 21);
            this.checkBox1.TabIndex = 11;
            this.checkBox1.Text = "是否显示数据类型名称";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // cMS_Regular_Add
            // 
            this.cMS_Regular_Add.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新增RequestToolStripMenuItem1});
            this.cMS_Regular_Add.Name = "contextMenuStrip3";
            this.cMS_Regular_Add.Size = new System.Drawing.Size(146, 26);
            // 
            // 新增RequestToolStripMenuItem1
            // 
            this.新增RequestToolStripMenuItem1.Image = global::SharpNodeSettings.Properties.Resources.action_add_16xLG;
            this.新增RequestToolStripMenuItem1.Name = "新增RequestToolStripMenuItem1";
            this.新增RequestToolStripMenuItem1.Size = new System.Drawing.Size(145, 22);
            this.新增RequestToolStripMenuItem1.Text = "新增Regular";
            this.新增RequestToolStripMenuItem1.Click += new System.EventHandler(this.新增RequestToolStripMenuItem1_Click);
            // 
            // cMS_EditRegular
            // 
            this.cMS_EditRegular.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4});
            this.cMS_EditRegular.Name = "contextMenuStrip1";
            this.cMS_EditRegular.Size = new System.Drawing.Size(172, 70);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Image = global::SharpNodeSettings.Properties.Resources.action_add_16xLG;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(171, 22);
            this.toolStripMenuItem2.Text = "新增RegularItem";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Image = global::SharpNodeSettings.Properties.Resources.PencilAngled_16xLG_color;
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(171, 22);
            this.toolStripMenuItem3.Text = "编辑Regular";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.编辑类别editClassToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Image = global::SharpNodeSettings.Properties.Resources.action_Cancel_16xLG;
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(171, 22);
            this.toolStripMenuItem4.Text = "删除Regular";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.删除deleteToolStripMenuItem_Click);
            // 
            // FormNodeSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(1020, 635);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormNodeSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "节点配置器";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormNodeSetting_FormClosing);
            this.Load += new System.EventHandler(this.FormNodeSetting_Load);
            this.SizeChanged += new System.EventHandler(this.FormNodeSetting_SizeChanged);
            this.cMS_Device.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.cMS_Request.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.cMs_EditRequest.ResumeLayout(false);
            this.cMS_ModbusServer.ResumeLayout(false);
            this.cMS_AlienClient.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.cMS_Regular_Add.ResumeLayout(false);
            this.cMS_EditRegular.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.TreeViewEx treeView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ContextMenuStrip cMS_Device;
        private System.Windows.Forms.ToolStripMenuItem 新增类别ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 类别classToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 西门子PlcsiemensToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 三菱plcmelsecToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 欧姆龙plcomronToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modbustcpclientToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编辑类别editClassToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除deleteToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip cMS_Request;
        private System.Windows.Forms.ToolStripMenuItem 新增RequestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编辑RequestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除RequestToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 打开文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存文件ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip cMs_EditRequest;
        private System.Windows.Forms.ToolStripMenuItem 编辑节点ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除节点ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 另存为ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip cMS_ModbusServer;
        private System.Windows.Forms.ToolStripMenuItem 新增ModbusTcpServerToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn HslSharpValueName;
        private System.Windows.Forms.DataGridViewTextBoxColumn HslSharpValue;
        private System.Windows.Forms.ContextMenuStrip cMS_AlienClient;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 编辑节点ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 删除节点ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 异形ModbusTcpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 空设备toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新增AlienServerToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ContextMenuStrip cMS_Regular_Add;
        private System.Windows.Forms.ToolStripMenuItem 新增RequestToolStripMenuItem1;
        private System.Windows.Forms.ContextMenuStrip cMS_EditRegular;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
    }
}