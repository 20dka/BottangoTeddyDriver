
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            btnOutputStart = new System.Windows.Forms.Button();
            btnAudioStop = new System.Windows.Forms.Button();
            listboxAudioDevices = new System.Windows.Forms.ListBox();
            groupBox1 = new System.Windows.Forms.GroupBox();
            combo_talkerSelect = new System.Windows.Forms.ComboBox();
            label6 = new System.Windows.Forms.Label();
            combo_animSourceSelect = new System.Windows.Forms.ComboBox();
            trackBar3 = new System.Windows.Forms.TrackBar();
            track_volume = new System.Windows.Forms.TrackBar();
            check_loopback = new System.Windows.Forms.CheckBox();
            trackBar1 = new System.Windows.Forms.TrackBar();
            trackBar2 = new System.Windows.Forms.TrackBar();
            label_volume = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            groupBox2 = new System.Windows.Forms.GroupBox();
            label_timeSync = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            btn_NetworkStart = new System.Windows.Forms.Button();
            listbox_NetworkLog = new System.Windows.Forms.ListBox();
            btn_NetworkStop = new System.Windows.Forms.Button();
            animTimer = new System.Windows.Forms.Timer(components);
            toolTip1 = new System.Windows.Forms.ToolTip(components);
            groupBox3 = new System.Windows.Forms.GroupBox();
            check_keepSamples = new System.Windows.Forms.CheckBox();
            check_flipSamples = new System.Windows.Forms.CheckBox();
            track_peakThreshold = new System.Windows.Forms.TrackBar();
            label_parseProgress = new System.Windows.Forms.Label();
            label_readFileCurrentSample = new System.Windows.Forms.Label();
            label_readFileFramediff = new System.Windows.Forms.Label();
            label_readFileElapsed = new System.Windows.Forms.Label();
            label_peakThreshold = new System.Windows.Forms.Label();
            list_missedFrames = new System.Windows.Forms.ListBox();
            btn_playReadFile = new System.Windows.Forms.Button();
            btn_pausePlayReadFile = new System.Windows.Forms.Button();
            btn_browseInputWav = new System.Windows.Forms.Button();
            btn_parseWavFile = new System.Windows.Forms.Button();
            button1 = new System.Windows.Forms.Button();
            tabControl1 = new System.Windows.Forms.TabControl();
            tabPage1 = new System.Windows.Forms.TabPage();
            tabPage2 = new System.Windows.Forms.TabPage();
            tabPage3 = new System.Windows.Forms.TabPage();
            tabPage4 = new System.Windows.Forms.TabPage();
            richTextBox1 = new System.Windows.Forms.RichTextBox();
            deerLineChart2 = new DeerLineChart();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)track_volume).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar2).BeginInit();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)track_peakThreshold).BeginInit();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            tabPage3.SuspendLayout();
            tabPage4.SuspendLayout();
            SuspendLayout();
            // 
            // btnOutputStart
            // 
            btnOutputStart.Location = new System.Drawing.Point(7, 22);
            btnOutputStart.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnOutputStart.Name = "btnOutputStart";
            btnOutputStart.Size = new System.Drawing.Size(88, 27);
            btnOutputStart.TabIndex = 0;
            btnOutputStart.Text = "Enable";
            toolTip1.SetToolTip(btnOutputStart, "Start signal generation (Make sure to select the right audio device!)");
            btnOutputStart.UseVisualStyleBackColor = true;
            btnOutputStart.Click += btnStart_Click;
            // 
            // btnAudioStop
            // 
            btnAudioStop.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            btnAudioStop.Location = new System.Drawing.Point(7, 263);
            btnAudioStop.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnAudioStop.Name = "btnAudioStop";
            btnAudioStop.Size = new System.Drawing.Size(88, 27);
            btnAudioStop.TabIndex = 1;
            btnAudioStop.Text = "Disable";
            toolTip1.SetToolTip(btnAudioStop, "Stop signal generation");
            btnAudioStop.UseVisualStyleBackColor = true;
            btnAudioStop.Click += btnStop_Click;
            // 
            // listboxAudioDevices
            // 
            listboxAudioDevices.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            listboxAudioDevices.FormattingEnabled = true;
            listboxAudioDevices.ItemHeight = 15;
            listboxAudioDevices.Location = new System.Drawing.Point(105, 46);
            listboxAudioDevices.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            listboxAudioDevices.Name = "listboxAudioDevices";
            listboxAudioDevices.Size = new System.Drawing.Size(326, 184);
            listboxAudioDevices.TabIndex = 2;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            groupBox1.Controls.Add(combo_talkerSelect);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(combo_animSourceSelect);
            groupBox1.Controls.Add(trackBar3);
            groupBox1.Controls.Add(track_volume);
            groupBox1.Controls.Add(check_loopback);
            groupBox1.Controls.Add(trackBar1);
            groupBox1.Controls.Add(trackBar2);
            groupBox1.Controls.Add(label_volume);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(btnOutputStart);
            groupBox1.Controls.Add(listboxAudioDevices);
            groupBox1.Controls.Add(btnAudioStop);
            groupBox1.Location = new System.Drawing.Point(974, 14);
            groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox1.Size = new System.Drawing.Size(720, 297);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "Output";
            // 
            // combo_talkerSelect
            // 
            combo_talkerSelect.FormattingEnabled = true;
            combo_talkerSelect.Items.AddRange(new object[] { "Teddy", "Goose", "Mickey" });
            combo_talkerSelect.Location = new System.Drawing.Point(7, 81);
            combo_talkerSelect.Name = "combo_talkerSelect";
            combo_talkerSelect.Size = new System.Drawing.Size(88, 23);
            combo_talkerSelect.TabIndex = 11;
            combo_talkerSelect.Text = "Teddy";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(98, 247);
            label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(104, 15);
            label6.TabIndex = 7;
            label6.Text = "Animation source:";
            // 
            // combo_animSourceSelect
            // 
            combo_animSourceSelect.FormattingEnabled = true;
            combo_animSourceSelect.Items.AddRange(new object[] { "Off", "Bottango", "File", "Microphone", "Computer audio" });
            combo_animSourceSelect.Location = new System.Drawing.Point(102, 265);
            combo_animSourceSelect.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            combo_animSourceSelect.Name = "combo_animSourceSelect";
            combo_animSourceSelect.Size = new System.Drawing.Size(140, 23);
            combo_animSourceSelect.TabIndex = 6;
            combo_animSourceSelect.Text = "Off";
            combo_animSourceSelect.SelectedIndexChanged += combo_animSourceSelect_SelectedIndexChanged;
            // 
            // trackBar3
            // 
            trackBar3.Enabled = false;
            trackBar3.Location = new System.Drawing.Point(660, 14);
            trackBar3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            trackBar3.Maximum = 255;
            trackBar3.Name = "trackBar3";
            trackBar3.Orientation = System.Windows.Forms.Orientation.Vertical;
            trackBar3.Size = new System.Drawing.Size(45, 276);
            trackBar3.TabIndex = 4;
            trackBar3.TickFrequency = 10;
            // 
            // track_volume
            // 
            track_volume.Location = new System.Drawing.Point(439, 22);
            track_volume.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            track_volume.Maximum = 50;
            track_volume.Name = "track_volume";
            track_volume.Orientation = System.Windows.Forms.Orientation.Vertical;
            track_volume.Size = new System.Drawing.Size(45, 243);
            track_volume.TabIndex = 4;
            track_volume.TickFrequency = 5;
            toolTip1.SetToolTip(track_volume, "Volume of signals sent to teddy. 10-20% should work");
            track_volume.Value = 10;
            track_volume.Scroll += track_volume_Scroll;
            // 
            // check_loopback
            // 
            check_loopback.Location = new System.Drawing.Point(7, 55);
            check_loopback.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            check_loopback.Name = "check_loopback";
            check_loopback.Size = new System.Drawing.Size(86, 20);
            check_loopback.TabIndex = 4;
            check_loopback.Text = "Loopback";
            toolTip1.SetToolTip(check_loopback, "If checked, your main audio output (Speakers) will be rerouted to teddy's speaker");
            check_loopback.UseVisualStyleBackColor = true;
            check_loopback.CheckedChanged += checkLoopback_CheckedChanged;
            // 
            // trackBar1
            // 
            trackBar1.Enabled = false;
            trackBar1.Location = new System.Drawing.Point(541, 14);
            trackBar1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            trackBar1.Maximum = 255;
            trackBar1.Name = "trackBar1";
            trackBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
            trackBar1.Size = new System.Drawing.Size(45, 276);
            trackBar1.TabIndex = 4;
            trackBar1.TickFrequency = 10;
            // 
            // trackBar2
            // 
            trackBar2.Enabled = false;
            trackBar2.Location = new System.Drawing.Point(601, 14);
            trackBar2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            trackBar2.Maximum = 255;
            trackBar2.Name = "trackBar2";
            trackBar2.Orientation = System.Windows.Forms.Orientation.Vertical;
            trackBar2.Size = new System.Drawing.Size(45, 276);
            trackBar2.TabIndex = 4;
            trackBar2.TickFrequency = 10;
            // 
            // label_volume
            // 
            label_volume.AutoSize = true;
            label_volume.Location = new System.Drawing.Point(419, 269);
            label_volume.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label_volume.Name = "label_volume";
            label_volume.Size = new System.Drawing.Size(75, 15);
            label_volume.TabIndex = 3;
            label_volume.Text = "Volume: 10%";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(102, 22);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(84, 15);
            label1.TabIndex = 3;
            label1.Text = "Audio devices:";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label_timeSync);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(btn_NetworkStart);
            groupBox2.Controls.Add(listbox_NetworkLog);
            groupBox2.Controls.Add(btn_NetworkStop);
            groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            groupBox2.Location = new System.Drawing.Point(4, 3);
            groupBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox2.Size = new System.Drawing.Size(622, 298);
            groupBox2.TabIndex = 3;
            groupBox2.TabStop = false;
            groupBox2.Text = "Bottango";
            // 
            // label_timeSync
            // 
            label_timeSync.AutoSize = true;
            label_timeSync.Location = new System.Drawing.Point(396, 18);
            label_timeSync.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label_timeSync.Name = "label_timeSync";
            label_timeSync.Size = new System.Drawing.Size(12, 15);
            label_timeSync.TabIndex = 5;
            label_timeSync.Text = "-";
            label_timeSync.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(102, 22);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(75, 15);
            label2.TabIndex = 3;
            label2.Text = "Network log:";
            // 
            // btn_NetworkStart
            // 
            btn_NetworkStart.Location = new System.Drawing.Point(7, 22);
            btn_NetworkStart.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btn_NetworkStart.Name = "btn_NetworkStart";
            btn_NetworkStart.Size = new System.Drawing.Size(88, 27);
            btn_NetworkStart.TabIndex = 0;
            btn_NetworkStart.Text = "Start";
            toolTip1.SetToolTip(btn_NetworkStart, "Connect to Bottango\r\nMake sure you enable the network driver inside Bottango first");
            btn_NetworkStart.UseVisualStyleBackColor = true;
            btn_NetworkStart.Click += btnNetworkStart_Click;
            // 
            // listbox_NetworkLog
            // 
            listbox_NetworkLog.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            listbox_NetworkLog.FormattingEnabled = true;
            listbox_NetworkLog.ItemHeight = 15;
            listbox_NetworkLog.Location = new System.Drawing.Point(105, 46);
            listbox_NetworkLog.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            listbox_NetworkLog.Name = "listbox_NetworkLog";
            listbox_NetworkLog.Size = new System.Drawing.Size(503, 214);
            listbox_NetworkLog.TabIndex = 2;
            // 
            // btn_NetworkStop
            // 
            btn_NetworkStop.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            btn_NetworkStop.Location = new System.Drawing.Point(7, 251);
            btn_NetworkStop.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btn_NetworkStop.Name = "btn_NetworkStop";
            btn_NetworkStop.Size = new System.Drawing.Size(88, 27);
            btn_NetworkStop.TabIndex = 1;
            btn_NetworkStop.Text = "Stop";
            toolTip1.SetToolTip(btn_NetworkStop, "Disconnect from Bottango");
            btn_NetworkStop.UseVisualStyleBackColor = true;
            btn_NetworkStop.Click += btnNetworkStop_Click;
            // 
            // animTimer
            // 
            animTimer.Interval = 16;
            animTimer.Tick += animTimer_Tick;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(check_keepSamples);
            groupBox3.Controls.Add(check_flipSamples);
            groupBox3.Controls.Add(track_peakThreshold);
            groupBox3.Controls.Add(label_parseProgress);
            groupBox3.Controls.Add(label_readFileCurrentSample);
            groupBox3.Controls.Add(label_readFileFramediff);
            groupBox3.Controls.Add(label_readFileElapsed);
            groupBox3.Controls.Add(label_peakThreshold);
            groupBox3.Controls.Add(list_missedFrames);
            groupBox3.Controls.Add(btn_playReadFile);
            groupBox3.Controls.Add(btn_pausePlayReadFile);
            groupBox3.Controls.Add(btn_browseInputWav);
            groupBox3.Controls.Add(btn_parseWavFile);
            groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            groupBox3.Location = new System.Drawing.Point(4, 3);
            groupBox3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox3.Size = new System.Drawing.Size(622, 298);
            groupBox3.TabIndex = 3;
            groupBox3.TabStop = false;
            groupBox3.Text = "Read file";
            // 
            // check_keepSamples
            // 
            check_keepSamples.AutoSize = true;
            check_keepSamples.Location = new System.Drawing.Point(13, 160);
            check_keepSamples.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            check_keepSamples.Name = "check_keepSamples";
            check_keepSamples.Size = new System.Drawing.Size(98, 19);
            check_keepSamples.TabIndex = 7;
            check_keepSamples.Text = "Keep samples";
            check_keepSamples.UseVisualStyleBackColor = true;
            // 
            // check_flipSamples
            // 
            check_flipSamples.AutoSize = true;
            check_flipSamples.Location = new System.Drawing.Point(13, 93);
            check_flipSamples.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            check_flipSamples.Name = "check_flipSamples";
            check_flipSamples.Size = new System.Drawing.Size(91, 19);
            check_flipSamples.TabIndex = 7;
            check_flipSamples.Text = "Flip samples";
            check_flipSamples.UseVisualStyleBackColor = true;
            // 
            // track_peakThreshold
            // 
            track_peakThreshold.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            track_peakThreshold.Location = new System.Drawing.Point(542, 15);
            track_peakThreshold.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            track_peakThreshold.Maximum = 70;
            track_peakThreshold.Minimum = 25;
            track_peakThreshold.Name = "track_peakThreshold";
            track_peakThreshold.Orientation = System.Windows.Forms.Orientation.Vertical;
            track_peakThreshold.Size = new System.Drawing.Size(45, 258);
            track_peakThreshold.TabIndex = 6;
            track_peakThreshold.Value = 40;
            track_peakThreshold.Scroll += track_peakTreshold_Scroll;
            // 
            // label_parseProgress
            // 
            label_parseProgress.AutoSize = true;
            label_parseProgress.Location = new System.Drawing.Point(8, 117);
            label_parseProgress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label_parseProgress.Name = "label_parseProgress";
            label_parseProgress.Size = new System.Drawing.Size(94, 15);
            label_parseProgress.TabIndex = 2;
            label_parseProgress.Text = "Parsing progress";
            // 
            // label_readFileCurrentSample
            // 
            label_readFileCurrentSample.AutoSize = true;
            label_readFileCurrentSample.Location = new System.Drawing.Point(8, 213);
            label_readFileCurrentSample.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label_readFileCurrentSample.Name = "label_readFileCurrentSample";
            label_readFileCurrentSample.Size = new System.Drawing.Size(28, 15);
            label_readFileCurrentSample.TabIndex = 2;
            label_readFileCurrentSample.Text = "info";
            // 
            // label_readFileFramediff
            // 
            label_readFileFramediff.AutoSize = true;
            label_readFileFramediff.Location = new System.Drawing.Point(8, 198);
            label_readFileFramediff.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label_readFileFramediff.Name = "label_readFileFramediff";
            label_readFileFramediff.Size = new System.Drawing.Size(36, 15);
            label_readFileFramediff.TabIndex = 2;
            label_readFileFramediff.Text = "detail";
            // 
            // label_readFileElapsed
            // 
            label_readFileElapsed.AutoSize = true;
            label_readFileElapsed.Location = new System.Drawing.Point(8, 183);
            label_readFileElapsed.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label_readFileElapsed.Name = "label_readFileElapsed";
            label_readFileElapsed.Size = new System.Drawing.Size(54, 15);
            label_readFileElapsed.TabIndex = 2;
            label_readFileElapsed.Text = "Playback";
            // 
            // label_peakThreshold
            // 
            label_peakThreshold.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            label_peakThreshold.AutoSize = true;
            label_peakThreshold.Location = new System.Drawing.Point(527, 276);
            label_peakThreshold.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label_peakThreshold.Name = "label_peakThreshold";
            label_peakThreshold.Size = new System.Drawing.Size(87, 15);
            label_peakThreshold.TabIndex = 3;
            label_peakThreshold.Text = "Threshold: 40%";
            // 
            // list_missedFrames
            // 
            list_missedFrames.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            list_missedFrames.FormattingEnabled = true;
            list_missedFrames.ItemHeight = 15;
            list_missedFrames.Location = new System.Drawing.Point(196, 15);
            list_missedFrames.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            list_missedFrames.Name = "list_missedFrames";
            list_missedFrames.Size = new System.Drawing.Size(291, 259);
            list_missedFrames.TabIndex = 2;
            list_missedFrames.SelectedIndexChanged += list_missedFrames_SelectedIndexChanged;
            // 
            // btn_playReadFile
            // 
            btn_playReadFile.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            btn_playReadFile.Location = new System.Drawing.Point(7, 235);
            btn_playReadFile.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btn_playReadFile.Name = "btn_playReadFile";
            btn_playReadFile.Size = new System.Drawing.Size(88, 27);
            btn_playReadFile.TabIndex = 0;
            btn_playReadFile.Text = "Play";
            btn_playReadFile.UseVisualStyleBackColor = true;
            btn_playReadFile.Click += btn_playReadFile_Click;
            // 
            // btn_pausePlayReadFile
            // 
            btn_pausePlayReadFile.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            btn_pausePlayReadFile.Location = new System.Drawing.Point(7, 266);
            btn_pausePlayReadFile.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btn_pausePlayReadFile.Name = "btn_pausePlayReadFile";
            btn_pausePlayReadFile.Size = new System.Drawing.Size(88, 27);
            btn_pausePlayReadFile.TabIndex = 1;
            btn_pausePlayReadFile.Text = "Pause";
            btn_pausePlayReadFile.UseVisualStyleBackColor = true;
            btn_pausePlayReadFile.Click += btn_pausePlayReadFile_Click;
            // 
            // btn_browseInputWav
            // 
            btn_browseInputWav.Location = new System.Drawing.Point(7, 22);
            btn_browseInputWav.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btn_browseInputWav.Name = "btn_browseInputWav";
            btn_browseInputWav.Size = new System.Drawing.Size(88, 27);
            btn_browseInputWav.TabIndex = 0;
            btn_browseInputWav.Text = "Browse";
            btn_browseInputWav.UseVisualStyleBackColor = true;
            btn_browseInputWav.Click += btn_browseInputWav_Click;
            // 
            // btn_parseWavFile
            // 
            btn_parseWavFile.Location = new System.Drawing.Point(7, 54);
            btn_parseWavFile.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btn_parseWavFile.Name = "btn_parseWavFile";
            btn_parseWavFile.Size = new System.Drawing.Size(88, 27);
            btn_parseWavFile.TabIndex = 1;
            btn_parseWavFile.Text = "Import";
            btn_parseWavFile.UseVisualStyleBackColor = true;
            btn_parseWavFile.Click += btn_parseWavFile_Click;
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(124, 130);
            button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(175, 27);
            button1.TabIndex = 8;
            button1.Text = "click to see nothing happen";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Location = new System.Drawing.Point(14, 14);
            tabControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new System.Drawing.Size(638, 332);
            tabControl1.TabIndex = 10;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(groupBox2);
            tabPage1.Location = new System.Drawing.Point(4, 24);
            tabPage1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabPage1.Size = new System.Drawing.Size(630, 304);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Bottango";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(groupBox3);
            tabPage2.Location = new System.Drawing.Point(4, 24);
            tabPage2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabPage2.Size = new System.Drawing.Size(630, 304);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "File";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(button1);
            tabPage3.Location = new System.Drawing.Point(4, 24);
            tabPage3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new System.Drawing.Size(630, 304);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Live audio";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            tabPage4.Controls.Add(richTextBox1);
            tabPage4.Location = new System.Drawing.Point(4, 24);
            tabPage4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabPage4.Size = new System.Drawing.Size(630, 304);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "About";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            richTextBox1.Location = new System.Drawing.Point(4, 3);
            richTextBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new System.Drawing.Size(622, 298);
            richTextBox1.TabIndex = 0;
            richTextBox1.Text = resources.GetString("richTextBox1.Text");
            richTextBox1.LinkClicked += richTextBox1_LinkClicked;
            // 
            // deerLineChart2
            // 
            deerLineChart2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            deerLineChart2.BackColor = System.Drawing.SystemColors.Window;
            deerLineChart2.CurrentSample = 0;
            deerLineChart2.ForeColor = System.Drawing.Color.Black;
            deerLineChart2.FramesVisible = true;
            deerLineChart2.Location = new System.Drawing.Point(14, 406);
            deerLineChart2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            deerLineChart2.MarkersVisible = true;
            deerLineChart2.Name = "deerLineChart2";
            deerLineChart2.PeakTreshold = 0F;
            deerLineChart2.SampleRate = 0;
            deerLineChart2.Size = new System.Drawing.Size(1680, 435);
            deerLineChart2.TabIndex = 9;
            deerLineChart2.Text = "deerLineChart2";
            deerLineChart2.ZoomLevel = 0.05D;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1708, 855);
            Controls.Add(tabControl1);
            Controls.Add(deerLineChart2);
            Controls.Add(groupBox1);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MinimumSize = new System.Drawing.Size(1204, 361);
            Name = "Form1";
            Text = "Form1";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar3).EndInit();
            ((System.ComponentModel.ISupportInitialize)track_volume).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar2).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)track_peakThreshold).EndInit();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            tabPage3.ResumeLayout(false);
            tabPage4.ResumeLayout(false);
            ResumeLayout(false);
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
        private System.Windows.Forms.ComboBox combo_talkerSelect;
    }
}

