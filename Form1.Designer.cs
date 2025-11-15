namespace BluetoothBattery2
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.显示或隐藏主窗口ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label_DeviceName = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox_DeviceName = new System.Windows.Forms.ComboBox();
            this.comboBox_FontFamily = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.refresh_Btn = new System.Windows.Forms.Button();
            this.checkBox_CloseBtnMinimize = new System.Windows.Forms.CheckBox();
            this.checkBox_RunOnStartup = new System.Windows.Forms.CheckBox();
            this.label_HowToUse = new System.Windows.Forms.Label();
            this.label_OffsetX = new System.Windows.Forms.Label();
            this.label_OffsetY = new System.Windows.Forms.Label();
            this.label_FontSize = new System.Windows.Forms.Label();
            this.numericUpDown_OffsetX = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_OffsetY = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_FontSize = new System.Windows.Forms.NumericUpDown();
            this.button_ApplyIconLayout = new System.Windows.Forms.Button();
            this.label_Language = new System.Windows.Forms.Label();
            this.comboBox_Language = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel_Main = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel_Header = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel_LanguageRow = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel_DeviceRow = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel_IntervalRow = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel_FontRow = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox_IconLayout = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel_Icon = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel_ActionRow = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel_CheckRow = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel_StartupRow = new System.Windows.Forms.FlowLayoutPanel();
            this.panel_LanguageSeparator = new System.Windows.Forms.Panel();
            this.panel_DeviceSeparator = new System.Windows.Forms.Panel();
            this.panel_IntervalSeparator = new System.Windows.Forms.Panel();
            this.panel_FontSeparator = new System.Windows.Forms.Panel();
            this.panel_CheckSeparator = new System.Windows.Forms.Panel();
            this.panel_StartupSeparator = new System.Windows.Forms.Panel();
            this.panel_ActionSeparator = new System.Windows.Forms.Panel();
            this.contextMenuStrip1.SuspendLayout();
            this.tableLayoutPanel_Main.SuspendLayout();
            this.flowLayoutPanel_Header.SuspendLayout();
            this.flowLayoutPanel_LanguageRow.SuspendLayout();
            this.flowLayoutPanel_DeviceRow.SuspendLayout();
            this.flowLayoutPanel_IntervalRow.SuspendLayout();
            this.flowLayoutPanel_FontRow.SuspendLayout();
            this.groupBox_IconLayout.SuspendLayout();
            this.tableLayoutPanel_Icon.SuspendLayout();
            this.flowLayoutPanel_ActionRow.SuspendLayout();
            this.flowLayoutPanel_CheckRow.SuspendLayout();
            this.flowLayoutPanel_StartupRow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_OffsetX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_OffsetY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_FontSize)).BeginInit();
            this.SuspendLayout();

            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipTitle = "32";
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            this.notifyIcon1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_RefreshTooltips);
            this.notifyIcon1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon1_MouseDown);

            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.BackColor = System.Drawing.SystemColors.MenuBar;
            this.contextMenuStrip1.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.显示或隐藏主窗口ToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(409, 116);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);

            // 
            // 显示或隐藏主窗口ToolStripMenuItem
            // 
            this.显示或隐藏主窗口ToolStripMenuItem.Name = "显示或隐藏主窗口ToolStripMenuItem";
            this.显示或隐藏主窗口ToolStripMenuItem.Size = new System.Drawing.Size(408, 56);
            this.显示或隐藏主窗口ToolStripMenuItem.Text = "显示或隐藏主窗口";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(408, 56);
            this.exitToolStripMenuItem.Text = "退出";
            // 
            // label_DeviceName
            // 
            this.label_DeviceName.AutoSize = true;
            this.label_DeviceName.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label_DeviceName.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label_DeviceName.Location = new System.Drawing.Point(3, 0);
            this.label_DeviceName.Margin = new System.Windows.Forms.Padding(0, 8, 16, 0);
            this.label_DeviceName.Name = "label_DeviceName";
            this.label_DeviceName.Size = new System.Drawing.Size(126, 39);
            this.label_DeviceName.TabIndex = 2;
            this.label_DeviceName.Text = "设备名称";
            this.label_DeviceName.Click += new System.EventHandler(this.label1_Click);

            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox2.Location = new System.Drawing.Point(0, 0);
            this.textBox2.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.textBox2.MinimumSize = new System.Drawing.Size(160, 0);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(200, 55);
            this.textBox2.TabIndex = 5;
            this.textBox2.BackColor = System.Drawing.Color.FromArgb(32, 32, 32);
            this.textBox2.ForeColor = System.Drawing.Color.Gainsboro;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox2.Validating += new System.ComponentModel.CancelEventHandler(this.textBox2_Validating);
            this.textBox2.Validated += new System.EventHandler(this.textBox2_Validated);

            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(0, 8, 12, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(544, 39);
            this.label2.TabIndex = 4;
            this.label2.Text = "刷新间隔（毫秒，小于 1000 将自动 ×1000）";
            this.label2.Click += new System.EventHandler(this.label2_Click);

            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(0, 8, 12, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(213, 39);
            this.label3.TabIndex = 6;
            this.label3.Text = "字体（Font family）";
            this.label3.Click += new System.EventHandler(this.label3_Click);

            // 
            // comboBox_DeviceName
            // 
            this.comboBox_DeviceName.BackColor = System.Drawing.SystemColors.Window;
            this.comboBox_DeviceName.Font = new System.Drawing.Font("Microsoft YaHei UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBox_DeviceName.FormattingEnabled = true;
            this.comboBox_DeviceName.Location = new System.Drawing.Point(0, 0);
            this.comboBox_DeviceName.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.comboBox_DeviceName.MinimumSize = new System.Drawing.Size(320, 0);
            this.comboBox_DeviceName.Name = "comboBox_DeviceName";
            this.comboBox_DeviceName.Size = new System.Drawing.Size(360, 60);
            this.comboBox_DeviceName.TabIndex = 8;
            this.comboBox_DeviceName.BackColor = System.Drawing.Color.FromArgb(32, 32, 32);
            this.comboBox_DeviceName.ForeColor = System.Drawing.Color.Gainsboro;
            this.comboBox_DeviceName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            // 
            // comboBox_FontFamily
            // 
            this.comboBox_FontFamily.BackColor = System.Drawing.SystemColors.Window;
            this.comboBox_FontFamily.Font = new System.Drawing.Font("Microsoft YaHei UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBox_FontFamily.FormattingEnabled = true;
            this.comboBox_FontFamily.Location = new System.Drawing.Point(0, 0);
            this.comboBox_FontFamily.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.comboBox_FontFamily.MinimumSize = new System.Drawing.Size(320, 0);
            this.comboBox_FontFamily.Name = "comboBox_FontFamily";
            this.comboBox_FontFamily.Size = new System.Drawing.Size(420, 60);
            this.comboBox_FontFamily.TabIndex = 9;
            this.comboBox_FontFamily.BackColor = System.Drawing.Color.FromArgb(32, 32, 32);
            this.comboBox_FontFamily.ForeColor = System.Drawing.Color.Gainsboro;
            this.comboBox_FontFamily.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button1.BackColor = System.Drawing.SystemColors.Info;
            this.button1.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.button1.Name = "button1";
            this.button1.Padding = new System.Windows.Forms.Padding(18, 6, 18, 6);
            this.button1.Size = new System.Drawing.Size(231, 65);
            this.button1.MinimumSize = new System.Drawing.Size(260, 60);
            this.button1.TabIndex = 10;
            this.button1.Text = "保存并刷新";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.ConfirmBtn_Click);

            // 
            // button2
            // 
            this.button2.AutoSize = true;
            this.button2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button2.BackColor = System.Drawing.SystemColors.Info;
            this.button2.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button2.ForeColor = System.Drawing.Color.Black;
            this.button2.Location = new System.Drawing.Point(0, 0);
            this.button2.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.button2.Name = "button2";
            this.button2.Padding = new System.Windows.Forms.Padding(18, 6, 18, 6);
            this.button2.Size = new System.Drawing.Size(231, 65);
            this.button2.MinimumSize = new System.Drawing.Size(260, 60);
            this.button2.TabIndex = 11;
            this.button2.Text = "恢复默认";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.SetToDefaultBtn_Click);

            // 
            // refresh_Btn
            // 
            this.refresh_Btn.AutoSize = true;
            this.refresh_Btn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.refresh_Btn.BackColor = System.Drawing.SystemColors.Info;
            this.refresh_Btn.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.refresh_Btn.ForeColor = System.Drawing.Color.Black;
            this.refresh_Btn.Location = new System.Drawing.Point(0, 0);
            this.refresh_Btn.Margin = new System.Windows.Forms.Padding(0);
            this.refresh_Btn.Name = "refresh_Btn";
            this.refresh_Btn.MinimumSize = new System.Drawing.Size(150, 60);
            this.refresh_Btn.Size = new System.Drawing.Size(150, 60);
            this.refresh_Btn.TabIndex = 12;
            this.refresh_Btn.Text = "刷新";
            this.refresh_Btn.UseVisualStyleBackColor = false;
            this.refresh_Btn.Click += new System.EventHandler(this.refresh_Btn_Click);

            // 
            // checkBox_CloseBtnMinimize
            // 
            this.checkBox_CloseBtnMinimize.AutoSize = true;
            this.checkBox_CloseBtnMinimize.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.checkBox_CloseBtnMinimize.Location = new System.Drawing.Point(3, 0);
            this.checkBox_CloseBtnMinimize.Margin = new System.Windows.Forms.Padding(0, 0, 24, 0);
            this.checkBox_CloseBtnMinimize.Name = "checkBox_CloseBtnMinimize";
            this.checkBox_CloseBtnMinimize.Size = new System.Drawing.Size(548, 54);
            this.checkBox_CloseBtnMinimize.TabIndex = 13;
            this.checkBox_CloseBtnMinimize.Text = "点击关闭按钮时最小化到托盘";
            this.checkBox_CloseBtnMinimize.UseVisualStyleBackColor = true;
            this.checkBox_CloseBtnMinimize.CheckedChanged += new System.EventHandler(this.checkBox_CloseBtnMinimize_CheckedChanged);

            // 
            // checkBox_RunOnStartup
            // 
            this.checkBox_RunOnStartup.AutoSize = true;
            this.checkBox_RunOnStartup.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.checkBox_RunOnStartup.Location = new System.Drawing.Point(3, 0);
            this.checkBox_RunOnStartup.Margin = new System.Windows.Forms.Padding(0);
            this.checkBox_RunOnStartup.Name = "checkBox_RunOnStartup";
            this.checkBox_RunOnStartup.Size = new System.Drawing.Size(188, 54);
            this.checkBox_RunOnStartup.TabIndex = 105;
            this.checkBox_RunOnStartup.Text = "开机启动";
            this.checkBox_RunOnStartup.UseVisualStyleBackColor = true;
            this.checkBox_RunOnStartup.CheckedChanged += new System.EventHandler(this.checkBox_RunOnStartup_CheckedChanged);

            // 
            // label_HowToUse
            // 
            this.label_HowToUse.AutoSize = true;
            this.label_HowToUse.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label_HowToUse.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label_HowToUse.Location = new System.Drawing.Point(3, 0);
            this.label_HowToUse.Margin = new System.Windows.Forms.Padding(0);
            this.label_HowToUse.Name = "label_HowToUse";
            this.label_HowToUse.Size = new System.Drawing.Size(168, 69);
            this.label_HowToUse.TabIndex = 14;
            this.label_HowToUse.Text = "使用说明 ?";
            this.label_HowToUse.Click += new System.EventHandler(this.label1_Click_1);

            // 
            // label_OffsetX
            // 
            this.label_OffsetX.AutoSize = true;
            this.label_OffsetX.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label_OffsetX.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label_OffsetX.Location = new System.Drawing.Point(3, 0);
            this.label_OffsetX.Margin = new System.Windows.Forms.Padding(0, 8, 12, 0);
            this.label_OffsetX.Name = "label_OffsetX";
            this.label_OffsetX.Size = new System.Drawing.Size(169, 39);
            this.label_OffsetX.TabIndex = 100;
            this.label_OffsetX.Text = "水平偏移 (X)";

            // 
            // label_OffsetY
            // 
            this.label_OffsetY.AutoSize = true;
            this.label_OffsetY.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label_OffsetY.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label_OffsetY.Location = new System.Drawing.Point(3, 0);
            this.label_OffsetY.Margin = new System.Windows.Forms.Padding(0, 8, 12, 0);
            this.label_OffsetY.Name = "label_OffsetY";
            this.label_OffsetY.Size = new System.Drawing.Size(169, 39);
            this.label_OffsetY.TabIndex = 101;
            this.label_OffsetY.Text = "垂直偏移 (Y)";

            // 
            // label_FontSize
            // 
            this.label_FontSize.AutoSize = true;
            this.label_FontSize.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label_FontSize.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label_FontSize.Location = new System.Drawing.Point(3, 0);
            this.label_FontSize.Margin = new System.Windows.Forms.Padding(0, 8, 12, 0);
            this.label_FontSize.Name = "label_FontSize";
            this.label_FontSize.Size = new System.Drawing.Size(153, 39);
            this.label_FontSize.TabIndex = 102;
            this.label_FontSize.Text = "字号调整";

            // 
            // label_Language
            // 
            this.label_Language.AutoSize = true;
            this.label_Language.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label_Language.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label_Language.Location = new System.Drawing.Point(3, 0);
            this.label_Language.Margin = new System.Windows.Forms.Padding(0, 8, 12, 0);
            this.label_Language.Name = "label_Language";
            this.label_Language.Size = new System.Drawing.Size(153, 39);
            this.label_Language.TabIndex = 103;
            this.label_Language.Text = "界面语言";

            // 
            // comboBox_Language
            // 
            this.comboBox_Language.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Language.Font = new System.Drawing.Font("Microsoft YaHei UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBox_Language.FormattingEnabled = true;
            this.comboBox_Language.Location = new System.Drawing.Point(0, 0);
            this.comboBox_Language.Margin = new System.Windows.Forms.Padding(0);
            this.comboBox_Language.MinimumSize = new System.Drawing.Size(220, 0);
            this.comboBox_Language.Name = "comboBox_Language";
            this.comboBox_Language.Size = new System.Drawing.Size(260, 60);
            this.comboBox_Language.TabIndex = 104;
            this.comboBox_Language.BackColor = System.Drawing.Color.FromArgb(32, 32, 32);
            this.comboBox_Language.ForeColor = System.Drawing.Color.Gainsboro;
            this.comboBox_Language.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox_Language.SelectedIndexChanged += new System.EventHandler(this.comboBox_Language_SelectedIndexChanged);

            // 
            // numericUpDown_OffsetX
            // 
            this.numericUpDown_OffsetX.Location = new System.Drawing.Point(3, 3);
            this.numericUpDown_OffsetX.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericUpDown_OffsetX.Name = "numericUpDown_OffsetX";
            this.numericUpDown_OffsetX.Size = new System.Drawing.Size(140, 33);
            this.numericUpDown_OffsetX.TabIndex = 100;
            this.numericUpDown_OffsetX.BackColor = System.Drawing.Color.FromArgb(32, 32, 32);
            this.numericUpDown_OffsetX.ForeColor = System.Drawing.Color.Gainsboro;

            // 
            // numericUpDown_OffsetY
            // 
            this.numericUpDown_OffsetY.Location = new System.Drawing.Point(3, 3);
            this.numericUpDown_OffsetY.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericUpDown_OffsetY.Name = "numericUpDown_OffsetY";
            this.numericUpDown_OffsetY.Size = new System.Drawing.Size(140, 33);
            this.numericUpDown_OffsetY.TabIndex = 101;
            this.numericUpDown_OffsetY.BackColor = System.Drawing.Color.FromArgb(32, 32, 32);
            this.numericUpDown_OffsetY.ForeColor = System.Drawing.Color.Gainsboro;

            // 
            // numericUpDown_FontSize
            // 
            this.numericUpDown_FontSize.Location = new System.Drawing.Point(3, 3);
            this.numericUpDown_FontSize.Minimum = new decimal(new int[] {
            -50,
            0,
            0,
            -2147483648});
            this.numericUpDown_FontSize.Name = "numericUpDown_FontSize";
            this.numericUpDown_FontSize.Size = new System.Drawing.Size(140, 33);
            this.numericUpDown_FontSize.TabIndex = 102;
            this.numericUpDown_FontSize.BackColor = System.Drawing.Color.FromArgb(32, 32, 32);
            this.numericUpDown_FontSize.ForeColor = System.Drawing.Color.Gainsboro;

            // 
            // button_ApplyIconLayout
            // 
            this.button_ApplyIconLayout.AutoSize = true;
            this.button_ApplyIconLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button_ApplyIconLayout.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button_ApplyIconLayout.ForeColor = System.Drawing.Color.Black;
            this.button_ApplyIconLayout.Location = new System.Drawing.Point(3, 3);
            this.button_ApplyIconLayout.Name = "button_ApplyIconLayout";
            this.button_ApplyIconLayout.Padding = new System.Windows.Forms.Padding(16, 6, 16, 6);
            this.button_ApplyIconLayout.Size = new System.Drawing.Size(120, 53);
            this.button_ApplyIconLayout.MinimumSize = new System.Drawing.Size(180, 0);
            this.button_ApplyIconLayout.TabIndex = 103;
            this.button_ApplyIconLayout.Text = "应用";
            this.button_ApplyIconLayout.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.button_ApplyIconLayout.UseVisualStyleBackColor = true;
            this.numericUpDown_OffsetX.ValueChanged += new System.EventHandler(this.numericUpDown_OffsetX_ValueChanged);
            this.numericUpDown_OffsetY.ValueChanged += new System.EventHandler(this.numericUpDown_OffsetY_ValueChanged);
            this.numericUpDown_FontSize.ValueChanged += new System.EventHandler(this.numericUpDown_FontSize_ValueChanged);
            this.button_ApplyIconLayout.Click += new System.EventHandler(this.button_ApplyIconLayout_Click);

            // 
            // tableLayoutPanel_Main
            // 
            this.tableLayoutPanel_Main.AutoSize = true;
            this.tableLayoutPanel_Main.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel_Main.ColumnCount = 1;
            this.tableLayoutPanel_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_Main.Controls.Add(this.flowLayoutPanel_Header, 0, 0);
            this.tableLayoutPanel_Main.Controls.Add(this.flowLayoutPanel_LanguageRow, 0, 1);
            this.tableLayoutPanel_Main.Controls.Add(this.panel_LanguageSeparator, 0, 2);
            this.tableLayoutPanel_Main.Controls.Add(this.flowLayoutPanel_DeviceRow, 0, 3);
            this.tableLayoutPanel_Main.Controls.Add(this.panel_DeviceSeparator, 0, 4);
            this.tableLayoutPanel_Main.Controls.Add(this.flowLayoutPanel_IntervalRow, 0, 5);
            this.tableLayoutPanel_Main.Controls.Add(this.panel_IntervalSeparator, 0, 6);
            this.tableLayoutPanel_Main.Controls.Add(this.flowLayoutPanel_FontRow, 0, 7);
            this.tableLayoutPanel_Main.Controls.Add(this.panel_FontSeparator, 0, 8);
            this.tableLayoutPanel_Main.Controls.Add(this.groupBox_IconLayout, 0, 9);
            this.tableLayoutPanel_Main.Controls.Add(this.panel_CheckSeparator, 0, 10);
            this.tableLayoutPanel_Main.Controls.Add(this.flowLayoutPanel_CheckRow, 0, 11);
            this.tableLayoutPanel_Main.Controls.Add(this.panel_StartupSeparator, 0, 12);
            this.tableLayoutPanel_Main.Controls.Add(this.flowLayoutPanel_StartupRow, 0, 13);
            this.tableLayoutPanel_Main.Controls.Add(this.panel_ActionSeparator, 0, 14);
            this.tableLayoutPanel_Main.Controls.Add(this.flowLayoutPanel_ActionRow, 0, 15);
            this.tableLayoutPanel_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_Main.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel_Main.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel_Main.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel_Main.Name = "tableLayoutPanel_Main";
            this.tableLayoutPanel_Main.Padding = new System.Windows.Forms.Padding(24, 24, 24, 16);
            this.tableLayoutPanel_Main.RowCount = 16;
            this.tableLayoutPanel_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_Main.Size = new System.Drawing.Size(974, 558);
            this.tableLayoutPanel_Main.TabIndex = 106;

            // 
            // flowLayoutPanel_Header
            // 
            this.flowLayoutPanel_Header.AutoSize = true;
            this.flowLayoutPanel_Header.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel_Header.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel_Header.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel_Header.Location = new System.Drawing.Point(24, 24);
            this.flowLayoutPanel_Header.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.flowLayoutPanel_Header.Name = "flowLayoutPanel_Header";
            this.flowLayoutPanel_Header.Size = new System.Drawing.Size(926, 69);
            this.flowLayoutPanel_Header.TabIndex = 0;
            this.flowLayoutPanel_Header.WrapContents = false;
            this.flowLayoutPanel_Header.Controls.Add(this.label_HowToUse);

            // 
            // flowLayoutPanel_LanguageRow
            // 
            this.flowLayoutPanel_LanguageRow.AutoSize = true;
            this.flowLayoutPanel_LanguageRow.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel_LanguageRow.Controls.Add(this.label_Language);
            this.flowLayoutPanel_LanguageRow.Controls.Add(this.comboBox_Language);
            this.flowLayoutPanel_LanguageRow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel_LanguageRow.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel_LanguageRow.Location = new System.Drawing.Point(24, 105);
            this.flowLayoutPanel_LanguageRow.Margin = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.flowLayoutPanel_LanguageRow.Name = "flowLayoutPanel_LanguageRow";
            this.flowLayoutPanel_LanguageRow.Size = new System.Drawing.Size(926, 99);
            this.flowLayoutPanel_LanguageRow.TabIndex = 7;
            this.flowLayoutPanel_LanguageRow.WrapContents = false;

            // 
            // flowLayoutPanel_DeviceRow
            // 
            this.flowLayoutPanel_DeviceRow.AutoSize = true;
            this.flowLayoutPanel_DeviceRow.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel_DeviceRow.Controls.Add(this.label_DeviceName);
            this.flowLayoutPanel_DeviceRow.Controls.Add(this.comboBox_DeviceName);
            this.flowLayoutPanel_DeviceRow.Controls.Add(this.refresh_Btn);
            this.flowLayoutPanel_DeviceRow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel_DeviceRow.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel_DeviceRow.Location = new System.Drawing.Point(24, 226);
            this.flowLayoutPanel_DeviceRow.Margin = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.flowLayoutPanel_DeviceRow.Name = "flowLayoutPanel_DeviceRow";
            this.flowLayoutPanel_DeviceRow.Size = new System.Drawing.Size(926, 121);
            this.flowLayoutPanel_DeviceRow.TabIndex = 1;
            this.flowLayoutPanel_DeviceRow.WrapContents = false;

            // 
            // flowLayoutPanel_IntervalRow
            // 
            this.flowLayoutPanel_IntervalRow.AutoSize = true;
            this.flowLayoutPanel_IntervalRow.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel_IntervalRow.Controls.Add(this.label2);
            this.flowLayoutPanel_IntervalRow.Controls.Add(this.textBox2);
            this.flowLayoutPanel_IntervalRow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel_IntervalRow.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel_IntervalRow.Location = new System.Drawing.Point(24, 358);
            this.flowLayoutPanel_IntervalRow.Margin = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.flowLayoutPanel_IntervalRow.Name = "flowLayoutPanel_IntervalRow";
            this.flowLayoutPanel_IntervalRow.Size = new System.Drawing.Size(926, 94);
            this.flowLayoutPanel_IntervalRow.TabIndex = 2;
            this.flowLayoutPanel_IntervalRow.WrapContents = false;

            // 
            // flowLayoutPanel_FontRow
            // 
            this.flowLayoutPanel_FontRow.AutoSize = true;
            this.flowLayoutPanel_FontRow.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel_FontRow.Controls.Add(this.label3);
            this.flowLayoutPanel_FontRow.Controls.Add(this.comboBox_FontFamily);
            this.flowLayoutPanel_FontRow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel_FontRow.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel_FontRow.Location = new System.Drawing.Point(24, 500);
            this.flowLayoutPanel_FontRow.Margin = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.flowLayoutPanel_FontRow.Name = "flowLayoutPanel_FontRow";
            this.flowLayoutPanel_FontRow.Size = new System.Drawing.Size(926, 107);
            this.flowLayoutPanel_FontRow.TabIndex = 3;
            this.flowLayoutPanel_FontRow.WrapContents = false;

            // 
            // groupBox_IconLayout
            // 
            this.groupBox_IconLayout.AutoSize = true;
            this.groupBox_IconLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox_IconLayout.Controls.Add(this.tableLayoutPanel_Icon);
            this.groupBox_IconLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_IconLayout.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox_IconLayout.ForeColor = System.Drawing.Color.Gainsboro;
            this.groupBox_IconLayout.Location = new System.Drawing.Point(27, 640);
            this.groupBox_IconLayout.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.groupBox_IconLayout.Name = "groupBox_IconLayout";
            this.groupBox_IconLayout.Padding = new System.Windows.Forms.Padding(16);
            this.groupBox_IconLayout.Size = new System.Drawing.Size(920, 137);
            this.groupBox_IconLayout.TabIndex = 8;
            this.groupBox_IconLayout.TabStop = false;
            this.groupBox_IconLayout.Text = "图标布局";

            // 
            // tableLayoutPanel_Icon
            // 
            this.tableLayoutPanel_Icon.AutoSize = true;
            this.tableLayoutPanel_Icon.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel_Icon.ColumnCount = 2;
            this.tableLayoutPanel_Icon.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel_Icon.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_Icon.Controls.Add(this.label_OffsetX, 0, 0);
            this.tableLayoutPanel_Icon.Controls.Add(this.numericUpDown_OffsetX, 1, 0);
            this.tableLayoutPanel_Icon.Controls.Add(this.label_OffsetY, 0, 1);
            this.tableLayoutPanel_Icon.Controls.Add(this.numericUpDown_OffsetY, 1, 1);
            this.tableLayoutPanel_Icon.Controls.Add(this.label_FontSize, 0, 2);
            this.tableLayoutPanel_Icon.Controls.Add(this.numericUpDown_FontSize, 1, 2);
            this.tableLayoutPanel_Icon.Controls.Add(this.button_ApplyIconLayout, 0, 3);
            this.tableLayoutPanel_Icon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_Icon.Location = new System.Drawing.Point(16, 45);
            this.tableLayoutPanel_Icon.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel_Icon.Name = "tableLayoutPanel_Icon";
            this.tableLayoutPanel_Icon.RowCount = 4;
            this.tableLayoutPanel_Icon.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel_Icon.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel_Icon.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel_Icon.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel_Icon.Size = new System.Drawing.Size(888, 76);
            this.tableLayoutPanel_Icon.TabIndex = 0;
            this.tableLayoutPanel_Icon.SetColumnSpan(this.button_ApplyIconLayout, 2);

            // 
            // flowLayoutPanel_ActionRow
            // 
            this.flowLayoutPanel_ActionRow.AutoSize = true;
            this.flowLayoutPanel_ActionRow.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel_ActionRow.Controls.Add(this.button1);
            this.flowLayoutPanel_ActionRow.Controls.Add(this.button2);
            this.flowLayoutPanel_ActionRow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel_ActionRow.Location = new System.Drawing.Point(24, 934);
            this.flowLayoutPanel_ActionRow.Margin = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.flowLayoutPanel_ActionRow.Name = "flowLayoutPanel_ActionRow";
            this.flowLayoutPanel_ActionRow.Size = new System.Drawing.Size(926, 65);
            this.flowLayoutPanel_ActionRow.TabIndex = 4;
            this.flowLayoutPanel_ActionRow.WrapContents = false;

            // 
            // flowLayoutPanel_CheckRow
            // 
            this.flowLayoutPanel_CheckRow.AutoSize = true;
            this.flowLayoutPanel_CheckRow.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel_CheckRow.Controls.Add(this.checkBox_CloseBtnMinimize);
            this.flowLayoutPanel_CheckRow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel_CheckRow.Location = new System.Drawing.Point(24, 780);
            this.flowLayoutPanel_CheckRow.Margin = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.flowLayoutPanel_CheckRow.Name = "flowLayoutPanel_CheckRow";
            this.flowLayoutPanel_CheckRow.Size = new System.Drawing.Size(926, 54);
            this.flowLayoutPanel_CheckRow.TabIndex = 5;
            this.flowLayoutPanel_CheckRow.WrapContents = false;

            // 
            // flowLayoutPanel_StartupRow
            // 
            this.flowLayoutPanel_StartupRow.AutoSize = true;
            this.flowLayoutPanel_StartupRow.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel_StartupRow.Controls.Add(this.checkBox_RunOnStartup);
            this.flowLayoutPanel_StartupRow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel_StartupRow.Location = new System.Drawing.Point(24, 861);
            this.flowLayoutPanel_StartupRow.Margin = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.flowLayoutPanel_StartupRow.Name = "flowLayoutPanel_StartupRow";
            this.flowLayoutPanel_StartupRow.Size = new System.Drawing.Size(926, 54);
            this.flowLayoutPanel_StartupRow.TabIndex = 6;
            this.flowLayoutPanel_StartupRow.WrapContents = false;

            // 
            // panel_LanguageSeparator
            // 
            this.panel_LanguageSeparator.AutoSize = false;
            this.panel_LanguageSeparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.panel_LanguageSeparator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_LanguageSeparator.Location = new System.Drawing.Point(24, 206);
            this.panel_LanguageSeparator.Margin = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.panel_LanguageSeparator.MaximumSize = new System.Drawing.Size(2147483647, 2);
            this.panel_LanguageSeparator.MinimumSize = new System.Drawing.Size(0, 2);
            this.panel_LanguageSeparator.Name = "panel_LanguageSeparator";
            this.panel_LanguageSeparator.Size = new System.Drawing.Size(926, 2);
            this.panel_LanguageSeparator.TabIndex = 14;

            // 
            // panel_DeviceSeparator
            // 
            this.panel_DeviceSeparator.AutoSize = false;
            this.panel_DeviceSeparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.panel_DeviceSeparator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_DeviceSeparator.Location = new System.Drawing.Point(24, 345);
            this.panel_DeviceSeparator.Margin = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.panel_DeviceSeparator.MaximumSize = new System.Drawing.Size(2147483647, 2);
            this.panel_DeviceSeparator.MinimumSize = new System.Drawing.Size(0, 2);
            this.panel_DeviceSeparator.Name = "panel_DeviceSeparator";
            this.panel_DeviceSeparator.Size = new System.Drawing.Size(926, 2);
            this.panel_DeviceSeparator.TabIndex = 8;

            // 
            // panel_IntervalSeparator
            // 
            this.panel_IntervalSeparator.AutoSize = false;
            this.panel_IntervalSeparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.panel_IntervalSeparator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_IntervalSeparator.Location = new System.Drawing.Point(24, 487);
            this.panel_IntervalSeparator.Margin = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.panel_IntervalSeparator.MaximumSize = new System.Drawing.Size(2147483647, 2);
            this.panel_IntervalSeparator.MinimumSize = new System.Drawing.Size(0, 2);
            this.panel_IntervalSeparator.Name = "panel_IntervalSeparator";
            this.panel_IntervalSeparator.Size = new System.Drawing.Size(926, 2);
            this.panel_IntervalSeparator.TabIndex = 9;

            // 
            // panel_FontSeparator
            // 
            this.panel_FontSeparator.AutoSize = false;
            this.panel_FontSeparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.panel_FontSeparator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_FontSeparator.Location = new System.Drawing.Point(24, 629);
            this.panel_FontSeparator.Margin = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.panel_FontSeparator.MaximumSize = new System.Drawing.Size(2147483647, 2);
            this.panel_FontSeparator.MinimumSize = new System.Drawing.Size(0, 2);
            this.panel_FontSeparator.Name = "panel_FontSeparator";
            this.panel_FontSeparator.Size = new System.Drawing.Size(926, 2);
            this.panel_FontSeparator.TabIndex = 10;

            // 
            // panel_CheckSeparator
            // 
            this.panel_CheckSeparator.AutoSize = false;
            this.panel_CheckSeparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.panel_CheckSeparator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_CheckSeparator.Location = new System.Drawing.Point(24, 783);
            this.panel_CheckSeparator.Margin = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.panel_CheckSeparator.MaximumSize = new System.Drawing.Size(2147483647, 2);
            this.panel_CheckSeparator.MinimumSize = new System.Drawing.Size(0, 2);
            this.panel_CheckSeparator.Name = "panel_CheckSeparator";
            this.panel_CheckSeparator.Size = new System.Drawing.Size(926, 2);
            this.panel_CheckSeparator.TabIndex = 12;

            // 
            // panel_StartupSeparator
            // 
            this.panel_StartupSeparator.AutoSize = false;
            this.panel_StartupSeparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.panel_StartupSeparator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_StartupSeparator.Location = new System.Drawing.Point(24, 864);
            this.panel_StartupSeparator.Margin = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.panel_StartupSeparator.MaximumSize = new System.Drawing.Size(2147483647, 2);
            this.panel_StartupSeparator.MinimumSize = new System.Drawing.Size(0, 2);
            this.panel_StartupSeparator.Name = "panel_StartupSeparator";
            this.panel_StartupSeparator.Size = new System.Drawing.Size(926, 2);
            this.panel_StartupSeparator.TabIndex = 13;

            // 
            // panel_ActionSeparator
            // 
            this.panel_ActionSeparator.AutoSize = false;
            this.panel_ActionSeparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.panel_ActionSeparator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_ActionSeparator.Location = new System.Drawing.Point(24, 904);
            this.panel_ActionSeparator.Margin = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.panel_ActionSeparator.MaximumSize = new System.Drawing.Size(2147483647, 2);
            this.panel_ActionSeparator.MinimumSize = new System.Drawing.Size(0, 2);
            this.panel_ActionSeparator.Name = "panel_ActionSeparator";
            this.panel_ActionSeparator.Size = new System.Drawing.Size(926, 2);
            this.panel_ActionSeparator.TabIndex = 11;

            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.WindowText;
            this.ClientSize = new System.Drawing.Size(974, 558);
            this.Controls.Add(this.tableLayoutPanel_Main);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(820, 600);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.tableLayoutPanel_Main.ResumeLayout(false);
            this.tableLayoutPanel_Main.PerformLayout();
            this.flowLayoutPanel_Header.ResumeLayout(false);
            this.flowLayoutPanel_Header.PerformLayout();
            this.flowLayoutPanel_DeviceRow.ResumeLayout(false);
            this.flowLayoutPanel_DeviceRow.PerformLayout();
            this.flowLayoutPanel_IntervalRow.ResumeLayout(false);
            this.flowLayoutPanel_IntervalRow.PerformLayout();
            this.flowLayoutPanel_FontRow.ResumeLayout(false);
            this.flowLayoutPanel_FontRow.PerformLayout();
            this.flowLayoutPanel_ActionRow.ResumeLayout(false);
            this.flowLayoutPanel_ActionRow.PerformLayout();
            this.flowLayoutPanel_CheckRow.ResumeLayout(false);
            this.flowLayoutPanel_CheckRow.PerformLayout();
            this.flowLayoutPanel_StartupRow.ResumeLayout(false);
            this.flowLayoutPanel_StartupRow.PerformLayout();
            this.flowLayoutPanel_LanguageRow.ResumeLayout(false);
            this.flowLayoutPanel_LanguageRow.PerformLayout();
            this.groupBox_IconLayout.ResumeLayout(false);
            this.groupBox_IconLayout.PerformLayout();
            this.tableLayoutPanel_Icon.ResumeLayout(false);
            this.tableLayoutPanel_Icon.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_OffsetX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_OffsetY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_FontSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 显示或隐藏主窗口ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label label_DeviceName;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox_DeviceName;
        private System.Windows.Forms.ComboBox comboBox_FontFamily;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button refresh_Btn;
        private System.Windows.Forms.CheckBox checkBox_CloseBtnMinimize;
        private System.Windows.Forms.CheckBox checkBox_RunOnStartup;
        private System.Windows.Forms.Label label_HowToUse;
        private System.Windows.Forms.Label label_OffsetX;
        private System.Windows.Forms.Label label_OffsetY;
        private System.Windows.Forms.Label label_FontSize;
        private System.Windows.Forms.Label label_Language;
        private System.Windows.Forms.ComboBox comboBox_Language;
        private System.Windows.Forms.NumericUpDown numericUpDown_OffsetX;
        private System.Windows.Forms.NumericUpDown numericUpDown_OffsetY;
        private System.Windows.Forms.NumericUpDown numericUpDown_FontSize;
        private System.Windows.Forms.Button button_ApplyIconLayout;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_Main;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_Header;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_DeviceRow;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_IntervalRow;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_FontRow;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_ActionRow;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_CheckRow;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_StartupRow;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_LanguageRow;
        private System.Windows.Forms.GroupBox groupBox_IconLayout;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_Icon;
        private System.Windows.Forms.Panel panel_DeviceSeparator;
        private System.Windows.Forms.Panel panel_IntervalSeparator;
        private System.Windows.Forms.Panel panel_FontSeparator;
        private System.Windows.Forms.Panel panel_ActionSeparator;
        private System.Windows.Forms.Panel panel_CheckSeparator;
        private System.Windows.Forms.Panel panel_StartupSeparator;
        private System.Windows.Forms.Panel panel_LanguageSeparator;
    }
}