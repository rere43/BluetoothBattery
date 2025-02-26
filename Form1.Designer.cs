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
            this.label_HowToUse = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
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
            this.notifyIcon1.MouseDown+=NotifyIcon1_MouseDown;
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
            this.label_DeviceName.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label_DeviceName.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label_DeviceName.Location = new System.Drawing.Point(200, 9);
            this.label_DeviceName.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label_DeviceName.Name = "label_DeviceName";
            this.label_DeviceName.Size = new System.Drawing.Size(107, 39);
            this.label_DeviceName.TabIndex = 2;
            this.label_DeviceName.Text = "设备名";
            this.label_DeviceName.Click += new System.EventHandler(this.label1_Click);
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox2.Location = new System.Drawing.Point(200, 174);
            this.textBox2.Margin = new System.Windows.Forms.Padding(1);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(277, 55);
            this.textBox2.TabIndex = 5;
            this.textBox2.Validating += new System.ComponentModel.CancelEventHandler(this.textBox2_Validating);
            this.textBox2.Validated += new System.EventHandler(this.textBox2_Validated);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label2.Location = new System.Drawing.Point(200, 134);
            this.label2.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(456, 39);
            this.label2.TabIndex = 4;
            this.label2.Text = "刷新间隔(毫秒),小于1千会×1000";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label3.Location = new System.Drawing.Point(200, 263);
            this.label3.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(164, 39);
            this.label3.TabIndex = 6;
            this.label3.Text = "字体family";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // comboBox_DeviceName
            // 
            this.comboBox_DeviceName.BackColor = System.Drawing.SystemColors.Window;
            this.comboBox_DeviceName.Font = new System.Drawing.Font("Microsoft YaHei UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBox_DeviceName.FormattingEnabled = true;
            this.comboBox_DeviceName.Location = new System.Drawing.Point(200, 49);
            this.comboBox_DeviceName.Margin = new System.Windows.Forms.Padding(1);
            this.comboBox_DeviceName.Name = "comboBox_DeviceName";
            this.comboBox_DeviceName.Size = new System.Drawing.Size(419, 60);
            this.comboBox_DeviceName.TabIndex = 8;
            // 
            // comboBox_FontFamily
            // 
            this.comboBox_FontFamily.BackColor = System.Drawing.SystemColors.Window;
            this.comboBox_FontFamily.Font = new System.Drawing.Font("Microsoft YaHei UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBox_FontFamily.FormattingEnabled = true;
            this.comboBox_FontFamily.Location = new System.Drawing.Point(200, 303);
            this.comboBox_FontFamily.Margin = new System.Windows.Forms.Padding(1);
            this.comboBox_FontFamily.Name = "comboBox_FontFamily";
            this.comboBox_FontFamily.Size = new System.Drawing.Size(531, 60);
            this.comboBox_FontFamily.TabIndex = 9;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Info;
            this.button1.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(200, 464);
            this.button1.Margin = new System.Windows.Forms.Padding(1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(231, 65);
            this.button1.TabIndex = 10;
            this.button1.Text = "保存并刷新";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.ConfirmBtn_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.Info;
            this.button2.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button2.ForeColor = System.Drawing.Color.Black;
            this.button2.Location = new System.Drawing.Point(500, 464);
            this.button2.Margin = new System.Windows.Forms.Padding(1);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(231, 65);
            this.button2.TabIndex = 11;
            this.button2.Text = "恢复默认";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.SetToDefaultBtn_Click);
            // 
            // refresh_Btn
            // 
            this.refresh_Btn.BackColor = System.Drawing.SystemColors.Info;
            this.refresh_Btn.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.refresh_Btn.ForeColor = System.Drawing.Color.Black;
            this.refresh_Btn.Location = new System.Drawing.Point(621, 49);
            this.refresh_Btn.Margin = new System.Windows.Forms.Padding(1);
            this.refresh_Btn.Name = "refresh_Btn";
            this.refresh_Btn.Size = new System.Drawing.Size(110, 60);
            this.refresh_Btn.TabIndex = 12;
            this.refresh_Btn.Text = "刷新";
            this.refresh_Btn.UseVisualStyleBackColor = false;
            this.refresh_Btn.Click += new System.EventHandler(this.refresh_Btn_Click);
            // 
            // checkBox_CloseBtnMinimize
            // 
            this.checkBox_CloseBtnMinimize.AutoSize = true;
            this.checkBox_CloseBtnMinimize.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.checkBox_CloseBtnMinimize.Location = new System.Drawing.Point(200, 379);
            this.checkBox_CloseBtnMinimize.Name = "checkBox_CloseBtnMinimize";
            this.checkBox_CloseBtnMinimize.Size = new System.Drawing.Size(548, 54);
            this.checkBox_CloseBtnMinimize.TabIndex = 13;
            this.checkBox_CloseBtnMinimize.Text = "点击关闭按钮时最小化到托盘";
            this.checkBox_CloseBtnMinimize.UseVisualStyleBackColor = true;
            this.checkBox_CloseBtnMinimize.CheckedChanged += new System.EventHandler(this.checkBox_CloseBtnMinimize_CheckedChanged);
            // 
            // label_HowToUse
            // 
            this.label_HowToUse.AutoSize = true;
            this.label_HowToUse.Font = new System.Drawing.Font("Microsoft YaHei UI", 34F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label_HowToUse.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label_HowToUse.Location = new System.Drawing.Point(893, 0);
            this.label_HowToUse.Name = "label_HowToUse";
            this.label_HowToUse.Size = new System.Drawing.Size(94, 118);
            this.label_HowToUse.TabIndex = 14;
            this.label_HowToUse.Text = "?";
            this.label_HowToUse.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.WindowText;
            this.ClientSize = new System.Drawing.Size(974, 558);
            this.Controls.Add(this.label_HowToUse);
            this.Controls.Add(this.checkBox_CloseBtnMinimize);
            this.Controls.Add(this.refresh_Btn);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox_FontFamily);
            this.Controls.Add(this.comboBox_DeviceName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label_DeviceName);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }



        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel=true;
            form1.Hide();
        }

        #endregion

        private NotifyIcon notifyIcon1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem exitToolStripMenuItem;
        private Label label_DeviceName;
        private TextBox textBox2;
        private Label label2;
        private Label label3;
        private ComboBox comboBox_DeviceName;
        private ComboBox comboBox_FontFamily;
        private Button button1;
        private Button button2;
        private Button refresh_Btn;
        private CheckBox checkBox_CloseBtnMinimize;
        private ToolStripMenuItem 显示或隐藏主窗口ToolStripMenuItem;
        private Label label_HowToUse;
    }
}