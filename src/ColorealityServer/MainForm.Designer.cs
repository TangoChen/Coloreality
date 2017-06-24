namespace ColorealityServer
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.IpTextBox = new System.Windows.Forms.TextBox();
            this.LeapStatusLabel = new System.Windows.Forms.Label();
            this.LogTextbox = new System.Windows.Forms.TextBox();
            this.PortInput = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.StartServerButton = new System.Windows.Forms.Button();
            this.ConnectionLabel = new System.Windows.Forms.Label();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.AutoStartServerToggle = new System.Windows.Forms.CheckBox();
            this.LeapOffsetXInput = new System.Windows.Forms.TextBox();
            this.OffsetXLabel = new System.Windows.Forms.Label();
            this.OffsetTitle = new System.Windows.Forms.Label();
            this.LeapOffsetXBar = new System.Windows.Forms.TrackBar();
            this.LeapOffsetYBar = new System.Windows.Forms.TrackBar();
            this.LeapOffsetYInput = new System.Windows.Forms.TextBox();
            this.OffsetYLabel = new System.Windows.Forms.Label();
            this.LeapOffsetZBar = new System.Windows.Forms.TrackBar();
            this.LeapOffsetZInput = new System.Windows.Forms.TextBox();
            this.OffsetZLabel = new System.Windows.Forms.Label();
            this.LeapScaleBar = new System.Windows.Forms.TrackBar();
            this.LeapScaleInput = new System.Windows.Forms.TextBox();
            this.ScaleLabel = new System.Windows.Forms.Label();
            this.PanelLeapConfig = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.SendIntervalInput = new System.Windows.Forms.TextBox();
            this.SendIntervalBar = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.LeapOffsetXBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LeapOffsetYBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LeapOffsetZBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LeapScaleBar)).BeginInit();
            this.PanelLeapConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SendIntervalBar)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 54);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Leap Status:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 78);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "Connection:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 111);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "IP:";
            // 
            // IpTextBox
            // 
            this.IpTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.IpTextBox.Location = new System.Drawing.Point(70, 108);
            this.IpTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.IpTextBox.Name = "IpTextBox";
            this.IpTextBox.ReadOnly = true;
            this.IpTextBox.Size = new System.Drawing.Size(169, 21);
            this.IpTextBox.TabIndex = 2;
            this.IpTextBox.Text = "192.168.1.101";
            this.IpTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LeapStatusLabel
            // 
            this.LeapStatusLabel.Location = new System.Drawing.Point(108, 54);
            this.LeapStatusLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LeapStatusLabel.Name = "LeapStatusLabel";
            this.LeapStatusLabel.Size = new System.Drawing.Size(129, 12);
            this.LeapStatusLabel.TabIndex = 4;
            this.LeapStatusLabel.Text = "None";
            this.LeapStatusLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // LogTextbox
            // 
            this.LogTextbox.BackColor = System.Drawing.SystemColors.Window;
            this.LogTextbox.Location = new System.Drawing.Point(256, 10);
            this.LogTextbox.Multiline = true;
            this.LogTextbox.Name = "LogTextbox";
            this.LogTextbox.ReadOnly = true;
            this.LogTextbox.Size = new System.Drawing.Size(191, 275);
            this.LogTextbox.TabIndex = 3;
            this.LogTextbox.Text = "Logs\r\n";
            // 
            // PortInput
            // 
            this.PortInput.BackColor = System.Drawing.SystemColors.Window;
            this.PortInput.Location = new System.Drawing.Point(70, 130);
            this.PortInput.Margin = new System.Windows.Forms.Padding(2);
            this.PortInput.Name = "PortInput";
            this.PortInput.Size = new System.Drawing.Size(169, 21);
            this.PortInput.TabIndex = 1;
            this.PortInput.Text = "0";
            this.PortInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.PortInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PortInput_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 134);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "Port:";
            // 
            // StartServerButton
            // 
            this.StartServerButton.Location = new System.Drawing.Point(13, 10);
            this.StartServerButton.Name = "StartServerButton";
            this.StartServerButton.Size = new System.Drawing.Size(226, 33);
            this.StartServerButton.TabIndex = 0;
            this.StartServerButton.Text = "&Start Server";
            this.StartServerButton.UseVisualStyleBackColor = true;
            this.StartServerButton.Click += new System.EventHandler(this.StartServerButton_Click);
            // 
            // ConnectionLabel
            // 
            this.ConnectionLabel.Location = new System.Drawing.Point(110, 78);
            this.ConnectionLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ConnectionLabel.Name = "ConnectionLabel";
            this.ConnectionLabel.Size = new System.Drawing.Size(127, 12);
            this.ConnectionLabel.TabIndex = 5;
            this.ConnectionLabel.Text = "None";
            this.ConnectionLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // notifyIcon
            // 
            this.notifyIcon.Text = "Coloreality Server";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon_MouseDoubleClick);
            // 
            // AutoStartServerToggle
            // 
            this.AutoStartServerToggle.AutoSize = true;
            this.AutoStartServerToggle.Location = new System.Drawing.Point(256, 291);
            this.AutoStartServerToggle.Name = "AutoStartServerToggle";
            this.AutoStartServerToggle.Size = new System.Drawing.Size(180, 16);
            this.AutoStartServerToggle.TabIndex = 9;
            this.AutoStartServerToggle.Text = "Auto start server at start";
            this.AutoStartServerToggle.UseVisualStyleBackColor = true;
            this.AutoStartServerToggle.CheckedChanged += new System.EventHandler(this.AutoStartServerToggle_CheckedChanged);
            // 
            // LeapOffsetXInput
            // 
            this.LeapOffsetXInput.Location = new System.Drawing.Point(190, 27);
            this.LeapOffsetXInput.Name = "LeapOffsetXInput";
            this.LeapOffsetXInput.Size = new System.Drawing.Size(44, 21);
            this.LeapOffsetXInput.TabIndex = 17;
            this.LeapOffsetXInput.Tag = "0";
            this.LeapOffsetXInput.Text = "0";
            this.LeapOffsetXInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LeapConfigInput_KeyDown);
            // 
            // OffsetXLabel
            // 
            this.OffsetXLabel.AutoSize = true;
            this.OffsetXLabel.Location = new System.Drawing.Point(29, 29);
            this.OffsetXLabel.Name = "OffsetXLabel";
            this.OffsetXLabel.Size = new System.Drawing.Size(11, 12);
            this.OffsetXLabel.TabIndex = 16;
            this.OffsetXLabel.Text = "X";
            // 
            // OffsetTitle
            // 
            this.OffsetTitle.AutoSize = true;
            this.OffsetTitle.Location = new System.Drawing.Point(7, 6);
            this.OffsetTitle.Name = "OffsetTitle";
            this.OffsetTitle.Size = new System.Drawing.Size(41, 12);
            this.OffsetTitle.TabIndex = 18;
            this.OffsetTitle.Text = "Offset";
            // 
            // LeapOffsetXBar
            // 
            this.LeapOffsetXBar.AutoSize = false;
            this.LeapOffsetXBar.Location = new System.Drawing.Point(46, 27);
            this.LeapOffsetXBar.Margin = new System.Windows.Forms.Padding(2);
            this.LeapOffsetXBar.Maximum = 3500;
            this.LeapOffsetXBar.Minimum = -3500;
            this.LeapOffsetXBar.Name = "LeapOffsetXBar";
            this.LeapOffsetXBar.Size = new System.Drawing.Size(142, 18);
            this.LeapOffsetXBar.TabIndex = 19;
            this.LeapOffsetXBar.Tag = "0";
            this.LeapOffsetXBar.TickFrequency = 0;
            this.LeapOffsetXBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.LeapOffsetXBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.LeapConfigBar_MouseUp);
            // 
            // LeapOffsetYBar
            // 
            this.LeapOffsetYBar.AutoSize = false;
            this.LeapOffsetYBar.Location = new System.Drawing.Point(46, 47);
            this.LeapOffsetYBar.Margin = new System.Windows.Forms.Padding(2);
            this.LeapOffsetYBar.Maximum = 3500;
            this.LeapOffsetYBar.Minimum = -3500;
            this.LeapOffsetYBar.Name = "LeapOffsetYBar";
            this.LeapOffsetYBar.Size = new System.Drawing.Size(142, 18);
            this.LeapOffsetYBar.TabIndex = 22;
            this.LeapOffsetYBar.Tag = "1";
            this.LeapOffsetYBar.TickFrequency = 0;
            this.LeapOffsetYBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.LeapOffsetYBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.LeapConfigBar_MouseUp);
            // 
            // LeapOffsetYInput
            // 
            this.LeapOffsetYInput.Location = new System.Drawing.Point(190, 47);
            this.LeapOffsetYInput.Name = "LeapOffsetYInput";
            this.LeapOffsetYInput.Size = new System.Drawing.Size(44, 21);
            this.LeapOffsetYInput.TabIndex = 21;
            this.LeapOffsetYInput.Tag = "1";
            this.LeapOffsetYInput.Text = "0";
            this.LeapOffsetYInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LeapConfigInput_KeyDown);
            // 
            // OffsetYLabel
            // 
            this.OffsetYLabel.AutoSize = true;
            this.OffsetYLabel.Location = new System.Drawing.Point(29, 49);
            this.OffsetYLabel.Name = "OffsetYLabel";
            this.OffsetYLabel.Size = new System.Drawing.Size(11, 12);
            this.OffsetYLabel.TabIndex = 20;
            this.OffsetYLabel.Text = "Y";
            // 
            // LeapOffsetZBar
            // 
            this.LeapOffsetZBar.AutoSize = false;
            this.LeapOffsetZBar.Location = new System.Drawing.Point(46, 68);
            this.LeapOffsetZBar.Margin = new System.Windows.Forms.Padding(2);
            this.LeapOffsetZBar.Maximum = 3500;
            this.LeapOffsetZBar.Minimum = -3500;
            this.LeapOffsetZBar.Name = "LeapOffsetZBar";
            this.LeapOffsetZBar.Size = new System.Drawing.Size(142, 18);
            this.LeapOffsetZBar.TabIndex = 25;
            this.LeapOffsetZBar.Tag = "2";
            this.LeapOffsetZBar.TickFrequency = 0;
            this.LeapOffsetZBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.LeapOffsetZBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.LeapConfigBar_MouseUp);
            // 
            // LeapOffsetZInput
            // 
            this.LeapOffsetZInput.Location = new System.Drawing.Point(190, 68);
            this.LeapOffsetZInput.Name = "LeapOffsetZInput";
            this.LeapOffsetZInput.Size = new System.Drawing.Size(44, 21);
            this.LeapOffsetZInput.TabIndex = 24;
            this.LeapOffsetZInput.Tag = "2";
            this.LeapOffsetZInput.Text = "0";
            this.LeapOffsetZInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LeapConfigInput_KeyDown);
            // 
            // OffsetZLabel
            // 
            this.OffsetZLabel.AutoSize = true;
            this.OffsetZLabel.Location = new System.Drawing.Point(29, 69);
            this.OffsetZLabel.Name = "OffsetZLabel";
            this.OffsetZLabel.Size = new System.Drawing.Size(11, 12);
            this.OffsetZLabel.TabIndex = 23;
            this.OffsetZLabel.Text = "Z";
            // 
            // LeapScaleBar
            // 
            this.LeapScaleBar.AutoSize = false;
            this.LeapScaleBar.Location = new System.Drawing.Point(46, 98);
            this.LeapScaleBar.Margin = new System.Windows.Forms.Padding(2);
            this.LeapScaleBar.Maximum = 20000;
            this.LeapScaleBar.Minimum = 100;
            this.LeapScaleBar.Name = "LeapScaleBar";
            this.LeapScaleBar.Size = new System.Drawing.Size(142, 18);
            this.LeapScaleBar.TabIndex = 28;
            this.LeapScaleBar.Tag = "3";
            this.LeapScaleBar.TickFrequency = 0;
            this.LeapScaleBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.LeapScaleBar.Value = 10000;
            this.LeapScaleBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.LeapConfigBar_MouseUp);
            // 
            // LeapScaleInput
            // 
            this.LeapScaleInput.Location = new System.Drawing.Point(190, 98);
            this.LeapScaleInput.Name = "LeapScaleInput";
            this.LeapScaleInput.Size = new System.Drawing.Size(44, 21);
            this.LeapScaleInput.TabIndex = 27;
            this.LeapScaleInput.Tag = "3";
            this.LeapScaleInput.Text = "1";
            this.LeapScaleInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LeapConfigInput_KeyDown);
            // 
            // ScaleLabel
            // 
            this.ScaleLabel.AutoSize = true;
            this.ScaleLabel.Location = new System.Drawing.Point(7, 98);
            this.ScaleLabel.Name = "ScaleLabel";
            this.ScaleLabel.Size = new System.Drawing.Size(35, 12);
            this.ScaleLabel.TabIndex = 26;
            this.ScaleLabel.Text = "Scale";
            // 
            // PanelLeapConfig
            // 
            this.PanelLeapConfig.Controls.Add(this.OffsetTitle);
            this.PanelLeapConfig.Controls.Add(this.LeapScaleBar);
            this.PanelLeapConfig.Controls.Add(this.OffsetXLabel);
            this.PanelLeapConfig.Controls.Add(this.LeapScaleInput);
            this.PanelLeapConfig.Controls.Add(this.LeapOffsetXInput);
            this.PanelLeapConfig.Controls.Add(this.ScaleLabel);
            this.PanelLeapConfig.Controls.Add(this.LeapOffsetXBar);
            this.PanelLeapConfig.Controls.Add(this.LeapOffsetZBar);
            this.PanelLeapConfig.Controls.Add(this.OffsetYLabel);
            this.PanelLeapConfig.Controls.Add(this.LeapOffsetZInput);
            this.PanelLeapConfig.Controls.Add(this.LeapOffsetYInput);
            this.PanelLeapConfig.Controls.Add(this.OffsetZLabel);
            this.PanelLeapConfig.Controls.Add(this.LeapOffsetYBar);
            this.PanelLeapConfig.Enabled = false;
            this.PanelLeapConfig.Location = new System.Drawing.Point(6, 183);
            this.PanelLeapConfig.Name = "PanelLeapConfig";
            this.PanelLeapConfig.Size = new System.Drawing.Size(244, 128);
            this.PanelLeapConfig.TabIndex = 29;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 159);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 30;
            this.label5.Text = "Interval:";
            // 
            // SendIntervalInput
            // 
            this.SendIntervalInput.Location = new System.Drawing.Point(195, 156);
            this.SendIntervalInput.Name = "SendIntervalInput";
            this.SendIntervalInput.Size = new System.Drawing.Size(44, 21);
            this.SendIntervalInput.TabIndex = 31;
            this.SendIntervalInput.Tag = "0";
            this.SendIntervalInput.Text = "30";
            this.SendIntervalInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SendIntervalInput_KeyDown);
            // 
            // SendIntervalBar
            // 
            this.SendIntervalBar.AutoSize = false;
            this.SendIntervalBar.Location = new System.Drawing.Point(70, 158);
            this.SendIntervalBar.Margin = new System.Windows.Forms.Padding(2);
            this.SendIntervalBar.Maximum = 500;
            this.SendIntervalBar.Name = "SendIntervalBar";
            this.SendIntervalBar.Size = new System.Drawing.Size(120, 18);
            this.SendIntervalBar.TabIndex = 32;
            this.SendIntervalBar.Tag = "0";
            this.SendIntervalBar.TickFrequency = 0;
            this.SendIntervalBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.SendIntervalBar.Value = 40;
            this.SendIntervalBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SendIntervalBar_MouseUp);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 319);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.SendIntervalInput);
            this.Controls.Add(this.SendIntervalBar);
            this.Controls.Add(this.PanelLeapConfig);
            this.Controls.Add(this.AutoStartServerToggle);
            this.Controls.Add(this.StartServerButton);
            this.Controls.Add(this.PortInput);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.LogTextbox);
            this.Controls.Add(this.ConnectionLabel);
            this.Controls.Add(this.LeapStatusLabel);
            this.Controls.Add(this.IpTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.ShowInTaskbar = false;
            this.Text = "Coloreality.io";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.LeapOffsetXBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LeapOffsetYBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LeapOffsetZBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LeapScaleBar)).EndInit();
            this.PanelLeapConfig.ResumeLayout(false);
            this.PanelLeapConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SendIntervalBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox IpTextBox;
        private System.Windows.Forms.Label LeapStatusLabel;
        private System.Windows.Forms.TextBox LogTextbox;
        private System.Windows.Forms.TextBox PortInput;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button StartServerButton;
        private System.Windows.Forms.Label ConnectionLabel;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.CheckBox AutoStartServerToggle;
        private System.Windows.Forms.TextBox LeapOffsetXInput;
        private System.Windows.Forms.Label OffsetXLabel;
        private System.Windows.Forms.Label OffsetTitle;
        private System.Windows.Forms.TrackBar LeapOffsetXBar;
        private System.Windows.Forms.TrackBar LeapOffsetYBar;
        private System.Windows.Forms.TextBox LeapOffsetYInput;
        private System.Windows.Forms.Label OffsetYLabel;
        private System.Windows.Forms.TrackBar LeapOffsetZBar;
        private System.Windows.Forms.TextBox LeapOffsetZInput;
        private System.Windows.Forms.Label OffsetZLabel;
        private System.Windows.Forms.TrackBar LeapScaleBar;
        private System.Windows.Forms.TextBox LeapScaleInput;
        private System.Windows.Forms.Label ScaleLabel;
        private System.Windows.Forms.Panel PanelLeapConfig;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox SendIntervalInput;
        private System.Windows.Forms.TrackBar SendIntervalBar;
    }
}

