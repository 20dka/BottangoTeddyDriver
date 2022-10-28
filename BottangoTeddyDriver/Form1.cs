using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using AudioPPM;
using NAudio.CoreAudioApi;

namespace BottangoTeddyDriver
{
    public partial class Form1 : Form
    {

        private void Form1_Load(object sender, EventArgs e)
        {
            Globals.form = this;

            parser = new TapeParser();
            parser.ParseDone += parser_ParseDone;
        }

        #region Audio - Output
        private bool _isPlaying;
        IList<MMDevice> _audioDevices;

        PPMWrapper _generator;

        public Form1() {
            InitializeComponent();

            UpdateDevices();

            _generator = new PPMWrapper();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (listboxAudioDevices.SelectedIndex == -1)
            {
                MessageBox.Show("Please select the output device", "PPM Control", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                _generator.Start(_audioDevices[listboxAudioDevices.SelectedIndex], check_loopback.Checked);
                _generator.setVolume(track_volume.Value / 100f);
                SetPlaying(true);
                animTimer.Start();

                check_loopback.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error starting PPM:\n" + ex.Message, "PPM Control", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }


        // Stop PPM Generator
        private void btnStop_Click(object sender, EventArgs e)
        {
            _generator?.Stop();
            SetPlaying(false);
            animTimer.Stop();

            check_loopback.Enabled = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateDevices();
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _generator?.Stop();
        }


        // Update list of output devices
        private void UpdateDevices()
        {
            listboxAudioDevices.Items.Clear();
            _audioDevices = PpmGenerator.GetDevices().OrderBy(x => x.FriendlyName).ToList();

            foreach (var device in _audioDevices)
                listboxAudioDevices.Items.Add($"{device.FriendlyName} - {device.State}");

            listboxAudioDevices.SelectedIndex = 0;
        }


        // Set playing state and GUI
        private void SetPlaying(bool isPlaying)
        {
            _isPlaying = isPlaying;

            if (isPlaying)
            {
                btnOutputStart.Enabled = false;
                btnAudioStop.Enabled = true;
            }
            else
            {
                btnOutputStart.Enabled = true;
                btnAudioStop.Enabled = false;
            }
        }

        private void checkLoopback_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void track_volume_Scroll(object sender, EventArgs e)
        {
            _generator.setVolume(track_volume.Value / 100f);
            label_volume.Text = "Volume: " + track_volume.Value.ToString() + "%";
        }


        #endregion


        #region Network - Bottango

        BottangoWrapper _bottango;

        private void btnNetworkStart_Click(object sender, EventArgs e)
        {
            _bottango = new BottangoWrapper();

            _bottango.Connect();
        }

        private void btnNetworkStop_Click(object sender, EventArgs e)
        {
            _bottango?.Disconnect();
        }


        delegate void LogLineCallback(string text);
        internal void LogLine(string line)
        {
            if (this.listbox_NetworkLog.InvokeRequired)
            {
                LogLineCallback d = new LogLineCallback(LogLine);
                this.Invoke(d, new object[] { line });
            }
            else
            {
                while (listbox_NetworkLog.Items.Count > 100) listbox_NetworkLog.Items.RemoveAt(listbox_NetworkLog.Items.Count-1);
                listbox_NetworkLog.Items.Insert(0,line);
            }
        }

        #endregion

        byte mapFloatToByte(float f)
        {
            return (byte)Math.Floor(f >= 1.0 ? 255 : f * 256.0);
        }

        private void animTimer_Tick(object sender, EventArgs e)
        {
            if (combo_animSourceSelect.DroppedDown) return;
            switch ((string)combo_animSourceSelect.SelectedItem)
            {
                case "Bottango":
                    {
                        if (_bottango == null) return;
                        byte[] vals = _bottango.getCurrentByte();

                        _generator.setValues(vals[1], vals[2], vals[3]);

                        trackBar1.Value = vals[1];
                        trackBar2.Value = vals[2];
                        trackBar3.Value = vals[3];

                        label_timeSync.Text = "elapsed ms:" + Time.getElapsedMS().ToString();

                        break;
                    }
                case "File":
                    {
                        if (parser == null) return;

                        if (parser.frames == null || parser.frames.Count < 2) return;

                        float[] vals = getParsedAnimData();

                        _generator.setValues(vals[0], vals[1], vals[2], vals[3], vals[4], vals[5], vals[6]);

                        trackBar1.Value = mapFloatToByte(vals[0]);
                        trackBar2.Value = mapFloatToByte(vals[1]);
                        trackBar3.Value = mapFloatToByte(vals[2]);

                        break;
                    }
                case "Microphone":
                case "Computer audio":
                    {
                        if (microphone == null) return;

                        float[] vals = getMicrophoneValues();

                        _generator.setValues(vals[0], vals[1], vals[2], vals[3], vals[4], vals[5], vals[6]);

                        trackBar1.Value = mapFloatToByte(vals[0]);
                        trackBar2.Value = mapFloatToByte(vals[1]);
                        trackBar3.Value = mapFloatToByte(vals[2]);

                        break;
                    }
                case "Off":
                default:
                    return;
            }
        }


        #region Read Wav

        TapeParser parser;
        string inputFilePath = @"C:\Users\tventy\Desktop\the_airship_55.wav";

        private void btn_browseInputWav_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            inputFilePath = dialog.FileName;
        }

        private void btn_parseWavFile_Click(object sender, EventArgs e)
        {
            if (inputFilePath != "")
            {
                //clear previous data
                lastFrameIndex = 0;

                parser.ParseProgressChanged += Parser_ParseProgressChanged;

                parser.StartParseAsync(inputFilePath, track_peakThreshold.Value/100f, check_flipSamples.Checked, check_keepSamples.Checked);

                combo_animSourceSelect.SelectedItem = "File";
            }
        }

        private void Parser_ParseProgressChanged(object sender, EventArgs _e)
        {
            var e = (ProgressChangedEventArgs)_e;

            label_parseProgress.Text = (string)e.UserState;
        }

        private void parser_ParseDone(object sender, EventArgs e)
        {
            deerLineChart2.Clear();

            deerLineChart2.PeakTreshold = track_peakThreshold.Value / 100f;
            deerLineChart2.SampleRate = (int)parser.SamplesPerSec;

            label_parseProgress.Text = $"good:{parser.frames.Count}, bad:{parser.badFrames.Count}";

            if (parser.Samples != null)
            {
                deerLineChart2.SetSamples(parser.Samples);
            }

            deerLineChart2.SetFrames(parser.frames);
            //deerLineChart2.AddFrames(parser.badFrames);
            deerLineChart2.AddMarkers(parser.badSyncs);

            deerLineChart2.FramesVisible = false;
            //deerLineChart2.MarkersVisible = false;

            deerLineChart2.Update();

            if (parser.frames.Count > 1)
            {
                string[] text = new string[parser.badSyncs.Count];
                int i = 0;

                foreach (var syncSample in parser.badSyncs)
                {

                    text[i++] = $"- {syncSample} - {syncSample/parser.SamplesPerSec:F}s";
                }


                list_missedFrames.Items.Clear();
                list_missedFrames.Items.AddRange(text);


                /*
                for (int i = 0; i < parser.frames.Count - 1; i++)
                {
                    KeyFrame a = parser.frames[i];

                    KeyFrame b = parser.frames[i + 1];

                    decimal frameDiffMs = ((b.Time - a.Time) * 1000);

                    if (frameDiffMs > 50)
                    {
                        list_missedFrames.Items.Add($"{frameDiffMs:F}ms - {a.positions[0]} - {a.Time:F}s");
                    }
                }*/
            }
        }

        int lastFrameIndex = 0;
        //private Stopwatch sw_animFile;

        private float lerp(float start, float end, float u)
        {
            return ((end - start) * u) + start;
        }

        private float[] getParsedAnimData()
        {
            decimal now = getFrameTime(); //(decimal)sw_animFile.Elapsed.TotalSeconds;


            int pastIndex = getPastFrameIndex();
            int nextIndex = pastIndex+1;

            if (parser.frames[pastIndex].Time > now) // before first frame
            {
                nextIndex = pastIndex;
            }

            //if (_parser.frames[nextIndex].Time > now) // TODO: after last frame

            KeyFrame a = parser.frames[pastIndex];

            KeyFrame b = parser.frames[nextIndex];

            float p = 0;

            if (nextIndex != pastIndex)
            {
                decimal dTime = now - a.Time;
                decimal tMax = b.Time - a.Time;
                p = Math.Min((float)(dTime / tMax), 1);
            }


            label_readFileElapsed.Text = $"elapsed: {now:F}";
            decimal frameDiffMs = (b.Time - a.Time) * 1000;
            label_readFileFramediff.Text = $"frame diff: {frameDiffMs:F}ms";
            label_readFileCurrentSample.Text = $"frame's first sample: {a.positions[0]}";

            lastFrameIndex = pastIndex;

            deerLineChart2.CurrentSample = a.positions[0];

            return new float[] { lerp(a.Eye, b.Eye, p), lerp(a.Nose, b.Nose, p), lerp(a.Mouth, b.Mouth, p),
                lerp(a.Mix, b.Mix, p), lerp(a.GEye, b.GEye, p), lerp(a.GNose, b.GNose, p), lerp(a.GMouth, b.GMouth, p) };
        }

        int getPastFrameIndex()
        {
            decimal now = getFrameTime(); //(decimal)sw_animFile.Elapsed.TotalSeconds;
            int i = lastFrameIndex;

            KeyFrame output = parser.frames[i];

            if (now < output.Time)
            {
                return i;
            }

            while (i+1 < parser.frames.Count && parser.frames[i].Time < now) i++;

            return i-1;
        }

        private decimal getFrameTime()
        {
            if (parser == null || parser.WaveOut == null) return 0;

            double ms = (double)parser.WaveOut.GetPosition() / parser.AudioReader.WaveFormat.BitsPerSample / parser.AudioReader.WaveFormat.Channels * 8 / parser.AudioReader.WaveFormat.SampleRate;

            //ms = parser.AudioReader.CurrentTime.TotalMilliseconds;

            //var x = _parser._reader.Position / _parser._reader.WaveFormat.SampleRate / _parser._reader.WaveFormat.Channels;
            return (decimal)ms + (decimal)parser.WaveOut.DesiredLatency/1000;
        }

        private void btn_playReadFile_Click(object sender, EventArgs e)
        {
            animTimer.Start();
            //sw_animFile = new Stopwatch();
            //sw_animFile.Start();
            parser.playWav();
        }

        private void btn_pausePlayReadFile_Click(object sender, EventArgs e)
        {
            //animTimer.Stop();
            parser.pauseWav();
        }

        private void list_missedFrames_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = (string)list_missedFrames.Items[list_missedFrames.SelectedIndex];

            string samplestr = selected.Split(new char[] { '-' })[1];

            int sample = int.Parse(samplestr);

            deerLineChart2.ScrollToSample(sample);
        }

        private void combo_animSourceSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = ((string)combo_animSourceSelect.SelectedItem).ToLower();
            switch (selected)
            {
                case "computer audio":
                case "microphone":
                    if (microphone != null)
                        microphone.Start(selected);

                    break;
                default:
                    break;
            }
        }

        private void track_peakTreshold_Scroll(object sender, EventArgs e)
        {
            label_peakThreshold.Text = $"Threshold: {track_peakThreshold.Value}%";
        }

        #endregion

        #region Microphone

        private MicrophoneWrapper microphone;

        float[] getMicrophoneValues()
        {
            return microphone.GetValues();
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            microphone = new MicrophoneWrapper();
            microphone.Start(combo_animSourceSelect.Text);
        }
    }
}
