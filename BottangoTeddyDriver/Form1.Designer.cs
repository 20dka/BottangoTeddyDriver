
namespace BottangoTeddyDriver
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnOutputStart = new System.Windows.Forms.Button();
            this.btnAudioStop = new System.Windows.Forms.Button();
            this.listboxAudioDevices = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.combo_animSourceSelect = new System.Windows.Forms.ComboBox();
            this.track_volume = new System.Windows.Forms.TrackBar();
            this.check_loopback = new System.Windows.Forms.CheckBox();
            this.label_volume = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label_timeSync = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_NetworkStart = new System.Windows.Forms.Button();
            this.listbox_NetworkLog = new System.Windows.Forms.ListBox();
            this.btn_NetworkStop = new System.Windows.Forms.Button();
            this.animTimer = new System.Windows.Forms.Timer(this.components);
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.trackBar3 = new System.Windows.Forms.TrackBar();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.check_keepSamples = new System.Windows.Forms.CheckBox();
            this.check_flipSamples = new System.Windows.Forms.CheckBox();
            this.track_peakThreshold = new System.Windows.Forms.TrackBar();
            this.label_parseProgress = new System.Windows.Forms.Label();
            this.label_readFileCurrentSample = new System.Windows.Forms.Label();
            this.label_readFileFramediff = new System.Windows.Forms.Label();
            this.label_readFileElapsed = new System.Windows.Forms.Label();
            this.label_peakThreshold = new System.Windows.Forms.Label();
            this.list_missedFrames = new System.Windows.Forms.ListBox();
            this.btn_playReadFile = new System.Windows.Forms.Button();
            this.btn_pausePlayReadFile = new System.Windows.Forms.Button();
            this.btn_browseInputWav = new System.Windows.Forms.Button();
            this.btn_parseWavFile = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.deerLineChart2 = new BottangoTeddyDriver.DeerLineChart();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.track_volume)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.track_peakThreshold)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOutputStart
            // 
            this.btnOutputStart.Location = new System.Drawing.Point(6, 19);
            this.btnOutputStart.Name = "btnOutputStart";
            this.btnOutputStart.Size = new System.Drawing.Size(75, 23);
            this.btnOutputStart.TabIndex = 0;
            this.btnOutputStart.Text = "Enable";
            this.toolTip1.SetToolTip(this.btnOutputStart, "Start signal generation (Make sure to select the right audio device!)");
            this.btnOutputStart.UseVisualStyleBackColor = true;
            this.btnOutputStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnAudioStop
            // 
            this.btnAudioStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAudioStop.Location = new System.Drawing.Point(6, 228);
            this.btnAudioStop.Name = "btnAudioStop";
            this.btnAudioStop.Size = new System.Drawing.Size(75, 23);
            this.btnAudioStop.TabIndex = 1;
            this.btnAudioStop.Text = "Disable";
            this.toolTip1.SetToolTip(this.btnAudioStop, "Stop signal generation");
            this.btnAudioStop.UseVisualStyleBackColor = true;
            this.btnAudioStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // listboxAudioDevices
            // 
            this.listboxAudioDevices.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listboxAudioDevices.FormattingEnabled = true;
            this.listboxAudioDevices.Location = new System.Drawing.Point(90, 40);
            this.listboxAudioDevices.Name = "listboxAudioDevices";
            this.listboxAudioDevices.Size = new System.Drawing.Size(280, 160);
            this.listboxAudioDevices.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.combo_animSourceSelect);
            this.groupBox1.Controls.Add(this.trackBar3);
            this.groupBox1.Controls.Add(this.track_volume);
            this.groupBox1.Controls.Add(this.check_loopback);
            this.groupBox1.Controls.Add(this.trackBar1);
            this.groupBox1.Controls.Add(this.trackBar2);
            this.groupBox1.Controls.Add(this.label_volume);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnOutputStart);
            this.groupBox1.Controls.Add(this.listboxAudioDevices);
            this.groupBox1.Controls.Add(this.btnAudioStop);
            this.groupBox1.Location = new System.Drawing.Point(755, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(617, 257);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Output";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(84, 214);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Animation source:";
            // 
            // combo_animSourceSelect
            // 
            this.combo_animSourceSelect.FormattingEnabled = true;
            this.combo_animSourceSelect.Items.AddRange(new object[] {
            "Off",
            "Bottango",
            "File",
            "Microphone",
            "Computer audio"});
            this.combo_animSourceSelect.Location = new System.Drawing.Point(87, 230);
            this.combo_animSourceSelect.Name = "combo_animSourceSelect";
            this.combo_animSourceSelect.Size = new System.Drawing.Size(121, 21);
            this.combo_animSourceSelect.TabIndex = 6;
            this.combo_animSourceSelect.Text = "Off";
            this.combo_animSourceSelect.SelectedIndexChanged += new System.EventHandler(this.combo_animSourceSelect_SelectedIndexChanged);
            // 
            // track_volume
            // 
            this.track_volume.Location = new System.Drawing.Point(376, 19);
            this.track_volume.Maximum = 50;
            this.track_volume.Name = "track_volume";
            this.track_volume.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.track_volume.Size = new System.Drawing.Size(45, 211);
            this.track_volume.TabIndex = 4;
            this.track_volume.TickFrequency = 5;
            this.toolTip1.SetToolTip(this.track_volume, "Volume of signals sent to teddy. 10-20% should work");
            this.track_volume.Value = 10;
            this.track_volume.Scroll += new System.EventHandler(this.track_volume_Scroll);
            // 
            // check_loopback
            // 
            this.check_loopback.Location = new System.Drawing.Point(6, 48);
            this.check_loopback.Name = "check_loopback";
            this.check_loopback.Size = new System.Drawing.Size(74, 17);
            this.check_loopback.TabIndex = 4;
            this.check_loopback.Text = "Loopback";
            this.toolTip1.SetToolTip(this.check_loopback, "If checked, your main audio output (Speakers) will be rerouted to teddy\'s speaker" +
        "");
            this.check_loopback.UseVisualStyleBackColor = true;
            this.check_loopback.CheckedChanged += new System.EventHandler(this.checkLoopback_CheckedChanged);
            // 
            // label_volume
            // 
            this.label_volume.AutoSize = true;
            this.label_volume.Location = new System.Drawing.Point(359, 233);
            this.label_volume.Name = "label_volume";
            this.label_volume.Size = new System.Drawing.Size(68, 13);
            this.label_volume.TabIndex = 3;
            this.label_volume.Text = "Volume: 10%";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(87, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Audio devices:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label_timeSync);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.btn_NetworkStart);
            this.groupBox2.Controls.Add(this.listbox_NetworkLog);
            this.groupBox2.Controls.Add(this.btn_NetworkStop);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(533, 256);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Bottango";
            // 
            // label_timeSync
            // 
            this.label_timeSync.AutoSize = true;
            this.label_timeSync.Location = new System.Drawing.Point(339, 16);
            this.label_timeSync.Name = "label_timeSync";
            this.label_timeSync.Size = new System.Drawing.Size(10, 13);
            this.label_timeSync.TabIndex = 5;
            this.label_timeSync.Text = "-";
            this.label_timeSync.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(87, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Network log:";
            // 
            // btn_NetworkStart
            // 
            this.btn_NetworkStart.Location = new System.Drawing.Point(6, 19);
            this.btn_NetworkStart.Name = "btn_NetworkStart";
            this.btn_NetworkStart.Size = new System.Drawing.Size(75, 23);
            this.btn_NetworkStart.TabIndex = 0;
            this.btn_NetworkStart.Text = "Start";
            this.toolTip1.SetToolTip(this.btn_NetworkStart, "Connect to Bottango\r\nMake sure you enable the network driver inside Bottango firs" +
        "t");
            this.btn_NetworkStart.UseVisualStyleBackColor = true;
            this.btn_NetworkStart.Click += new System.EventHandler(this.btnNetworkStart_Click);
            // 
            // listbox_NetworkLog
            // 
            this.listbox_NetworkLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listbox_NetworkLog.FormattingEnabled = true;
            this.listbox_NetworkLog.Location = new System.Drawing.Point(90, 40);
            this.listbox_NetworkLog.Name = "listbox_NetworkLog";
            this.listbox_NetworkLog.Size = new System.Drawing.Size(432, 186);
            this.listbox_NetworkLog.TabIndex = 2;
            // 
            // btn_NetworkStop
            // 
            this.btn_NetworkStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_NetworkStop.Location = new System.Drawing.Point(6, 215);
            this.btn_NetworkStop.Name = "btn_NetworkStop";
            this.btn_NetworkStop.Size = new System.Drawing.Size(75, 23);
            this.btn_NetworkStop.TabIndex = 1;
            this.btn_NetworkStop.Text = "Stop";
            this.toolTip1.SetToolTip(this.btn_NetworkStop, "Disconnect from Bottango");
            this.btn_NetworkStop.UseVisualStyleBackColor = true;
            this.btn_NetworkStop.Click += new System.EventHandler(this.btnNetworkStop_Click);
            // 
            // animTimer
            // 
            this.animTimer.Interval = 16;
            this.animTimer.Tick += new System.EventHandler(this.animTimer_Tick);
            // 
            // trackBar1
            // 
            this.trackBar1.Enabled = false;
            this.trackBar1.Location = new System.Drawing.Point(464, 12);
            this.trackBar1.Maximum = 255;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar1.Size = new System.Drawing.Size(45, 239);
            this.trackBar1.TabIndex = 4;
            this.trackBar1.TickFrequency = 10;
            // 
            // trackBar2
            // 
            this.trackBar2.Enabled = false;
            this.trackBar2.Location = new System.Drawing.Point(515, 12);
            this.trackBar2.Maximum = 255;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar2.Size = new System.Drawing.Size(45, 239);
            this.trackBar2.TabIndex = 4;
            this.trackBar2.TickFrequency = 10;
            // 
            // trackBar3
            // 
            this.trackBar3.Enabled = false;
            this.trackBar3.Location = new System.Drawing.Point(566, 12);
            this.trackBar3.Maximum = 255;
            this.trackBar3.Name = "trackBar3";
            this.trackBar3.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar3.Size = new System.Drawing.Size(45, 239);
            this.trackBar3.TabIndex = 4;
            this.trackBar3.TickFrequency = 10;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.check_keepSamples);
            this.groupBox3.Controls.Add(this.check_flipSamples);
            this.groupBox3.Controls.Add(this.track_peakThreshold);
            this.groupBox3.Controls.Add(this.label_parseProgress);
            this.groupBox3.Controls.Add(this.label_readFileCurrentSample);
            this.groupBox3.Controls.Add(this.label_readFileFramediff);
            this.groupBox3.Controls.Add(this.label_readFileElapsed);
            this.groupBox3.Controls.Add(this.label_peakThreshold);
            this.groupBox3.Controls.Add(this.list_missedFrames);
            this.groupBox3.Controls.Add(this.btn_playReadFile);
            this.groupBox3.Controls.Add(this.btn_pausePlayReadFile);
            this.groupBox3.Controls.Add(this.btn_browseInputWav);
            this.groupBox3.Controls.Add(this.btn_parseWavFile);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(533, 256);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Read file";
            // 
            // check_keepSamples
            // 
            this.check_keepSamples.AutoSize = true;
            this.check_keepSamples.Location = new System.Drawing.Point(11, 139);
            this.check_keepSamples.Name = "check_keepSamples";
            this.check_keepSamples.Size = new System.Drawing.Size(150, 17);
            this.check_keepSamples.TabIndex = 7;
            this.check_keepSamples.Text = "Keep samples (uses RAM)";
            this.check_keepSamples.UseVisualStyleBackColor = true;
            // 
            // check_flipSamples
            // 
            this.check_flipSamples.AutoSize = true;
            this.check_flipSamples.Location = new System.Drawing.Point(11, 81);
            this.check_flipSamples.Name = "check_flipSamples";
            this.check_flipSamples.Size = new System.Drawing.Size(83, 17);
            this.check_flipSamples.TabIndex = 7;
            this.check_flipSamples.Text = "Flip samples";
            this.check_flipSamples.UseVisualStyleBackColor = true;
            // 
            // track_peakThreshold
            // 
            this.track_peakThreshold.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.track_peakThreshold.Location = new System.Drawing.Point(465, 13);
            this.track_peakThreshold.Maximum = 70;
            this.track_peakThreshold.Minimum = 25;
            this.track_peakThreshold.Name = "track_peakThreshold";
            this.track_peakThreshold.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.track_peakThreshold.Size = new System.Drawing.Size(45, 221);
            this.track_peakThreshold.TabIndex = 6;
            this.track_peakThreshold.Value = 40;
            this.track_peakThreshold.Scroll += new System.EventHandler(this.track_peakTreshold_Scroll);
            // 
            // label_parseProgress
            // 
            this.label_parseProgress.AutoSize = true;
            this.label_parseProgress.Location = new System.Drawing.Point(7, 101);
            this.label_parseProgress.Name = "label_parseProgress";
            this.label_parseProgress.Size = new System.Drawing.Size(85, 13);
            this.label_parseProgress.TabIndex = 2;
            this.label_parseProgress.Text = "Parsing progress";
            // 
            // label_readFileCurrentSample
            // 
            this.label_readFileCurrentSample.AutoSize = true;
            this.label_readFileCurrentSample.Location = new System.Drawing.Point(7, 185);
            this.label_readFileCurrentSample.Name = "label_readFileCurrentSample";
            this.label_readFileCurrentSample.Size = new System.Drawing.Size(24, 13);
            this.label_readFileCurrentSample.TabIndex = 2;
            this.label_readFileCurrentSample.Text = "info";
            // 
            // label_readFileFramediff
            // 
            this.label_readFileFramediff.AutoSize = true;
            this.label_readFileFramediff.Location = new System.Drawing.Point(7, 172);
            this.label_readFileFramediff.Name = "label_readFileFramediff";
            this.label_readFileFramediff.Size = new System.Drawing.Size(32, 13);
            this.label_readFileFramediff.TabIndex = 2;
            this.label_readFileFramediff.Text = "detail";
            // 
            // label_readFileElapsed
            // 
            this.label_readFileElapsed.AutoSize = true;
            this.label_readFileElapsed.Location = new System.Drawing.Point(7, 159);
            this.label_readFileElapsed.Name = "label_readFileElapsed";
            this.label_readFileElapsed.Size = new System.Drawing.Size(51, 13);
            this.label_readFileElapsed.TabIndex = 2;
            this.label_readFileElapsed.Text = "Playback";
            // 
            // label_peakThreshold
            // 
            this.label_peakThreshold.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label_peakThreshold.AutoSize = true;
            this.label_peakThreshold.Location = new System.Drawing.Point(452, 237);
            this.label_peakThreshold.Name = "label_peakThreshold";
            this.label_peakThreshold.Size = new System.Drawing.Size(80, 13);
            this.label_peakThreshold.TabIndex = 3;
            this.label_peakThreshold.Text = "Threshold: 40%";
            // 
            // list_missedFrames
            // 
            this.list_missedFrames.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.list_missedFrames.FormattingEnabled = true;
            this.list_missedFrames.Location = new System.Drawing.Point(168, 13);
            this.list_missedFrames.Name = "list_missedFrames";
            this.list_missedFrames.Size = new System.Drawing.Size(250, 225);
            this.list_missedFrames.TabIndex = 2;
            this.list_missedFrames.SelectedIndexChanged += new System.EventHandler(this.list_missedFrames_SelectedIndexChanged);
            // 
            // btn_playReadFile
            // 
            this.btn_playReadFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_playReadFile.Location = new System.Drawing.Point(6, 201);
            this.btn_playReadFile.Name = "btn_playReadFile";
            this.btn_playReadFile.Size = new System.Drawing.Size(75, 23);
            this.btn_playReadFile.TabIndex = 0;
            this.btn_playReadFile.Text = "Play";
            this.btn_playReadFile.UseVisualStyleBackColor = true;
            this.btn_playReadFile.Click += new System.EventHandler(this.btn_playReadFile_Click);
            // 
            // btn_pausePlayReadFile
            // 
            this.btn_pausePlayReadFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_pausePlayReadFile.Location = new System.Drawing.Point(6, 228);
            this.btn_pausePlayReadFile.Name = "btn_pausePlayReadFile";
            this.btn_pausePlayReadFile.Size = new System.Drawing.Size(75, 23);
            this.btn_pausePlayReadFile.TabIndex = 1;
            this.btn_pausePlayReadFile.Text = "Pause";
            this.btn_pausePlayReadFile.UseVisualStyleBackColor = true;
            this.btn_pausePlayReadFile.Click += new System.EventHandler(this.btn_pausePlayReadFile_Click);
            // 
            // btn_browseInputWav
            // 
            this.btn_browseInputWav.Location = new System.Drawing.Point(6, 19);
            this.btn_browseInputWav.Name = "btn_browseInputWav";
            this.btn_browseInputWav.Size = new System.Drawing.Size(75, 23);
            this.btn_browseInputWav.TabIndex = 0;
            this.btn_browseInputWav.Text = "Browse";
            this.btn_browseInputWav.UseVisualStyleBackColor = true;
            this.btn_browseInputWav.Click += new System.EventHandler(this.btn_browseInputWav_Click);
            // 
            // btn_parseWavFile
            // 
            this.btn_parseWavFile.Location = new System.Drawing.Point(6, 47);
            this.btn_parseWavFile.Name = "btn_parseWavFile";
            this.btn_parseWavFile.Size = new System.Drawing.Size(75, 23);
            this.btn_parseWavFile.TabIndex = 1;
            this.btn_parseWavFile.Text = "Import";
            this.btn_parseWavFile.UseVisualStyleBackColor = true;
            this.btn_parseWavFile.Click += new System.EventHandler(this.btn_parseWavFile_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(106, 113);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "click to see nothing happen";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(547, 288);
            this.tabControl1.TabIndex = 10;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(539, 262);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Bottango";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(539, 262);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "File";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.button1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(539, 262);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Live audio";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.richTextBox1);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(539, 262);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "About";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(3, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(533, 256);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
            this.richTextBox1.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.richTextBox1_LinkClicked);
            // 
            // deerLineChart2
            // 
            this.deerLineChart2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.deerLineChart2.BackColor = System.Drawing.SystemColors.Window;
            this.deerLineChart2.CurrentSample = 0;
            this.deerLineChart2.ForeColor = System.Drawing.Color.Black;
            this.deerLineChart2.FramesVisible = true;
            this.deerLineChart2.Location = new System.Drawing.Point(12, 352);
            this.deerLineChart2.MarkersVisible = true;
            this.deerLineChart2.Name = "deerLineChart2";
            this.deerLineChart2.PeakTreshold = 0F;
            this.deerLineChart2.SampleRate = 0;
            this.deerLineChart2.Size = new System.Drawing.Size(1360, 377);
            this.deerLineChart2.TabIndex = 9;
            this.deerLineChart2.Text = "deerLineChart2";
            this.deerLineChart2.ZoomLevel = 0.05D;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1384, 741);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.deerLineChart2);
            this.Controls.Add(this.groupBox1);
            this.MinimumSize = new System.Drawing.Size(1034, 318);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.track_volume)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.track_peakThreshold)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOutputStart;
        private System.Windows.Forms.Button btnAudioStop;
        private System.Windows.Forms.ListBox listboxAudioDevices;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_NetworkStart;
        private System.Windows.Forms.ListBox listbox_NetworkLog;
        private System.Windows.Forms.Button btn_NetworkStop;
        private System.Windows.Forms.Timer animTimer;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.TrackBar trackBar3;
        private System.Windows.Forms.Label label_timeSync;
        private System.Windows.Forms.CheckBox check_loopback;
        private System.Windows.Forms.TrackBar track_volume;
        private System.Windows.Forms.Label label_volume;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btn_browseInputWav;
        private System.Windows.Forms.Button btn_parseWavFile;
        private System.Windows.Forms.Button btn_playReadFile;
        private System.Windows.Forms.Button btn_pausePlayReadFile;
        private System.Windows.Forms.Label label_readFileFramediff;
        private System.Windows.Forms.Label label_readFileElapsed;
        private System.Windows.Forms.Label label_readFileCurrentSample;
        private System.Windows.Forms.ListBox list_missedFrames;
        private System.Windows.Forms.ComboBox combo_animSourceSelect;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TrackBar track_peakThreshold;
        private System.Windows.Forms.Label label_peakThreshold;
        private System.Windows.Forms.Label label_parseProgress;
        private System.Windows.Forms.CheckBox check_flipSamples;
        private System.Windows.Forms.CheckBox check_keepSamples;
        private System.Windows.Forms.Button button1;
        private DeerLineChart deerLineChart2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}

