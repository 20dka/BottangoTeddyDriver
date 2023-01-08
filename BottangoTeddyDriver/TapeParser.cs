using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

//using NAudio.Wave;

namespace BottangoTeddyDriver
{
    class TapeParser
    {
        BackgroundWorker backgroundWorker;
        public event EventHandler ParseDone;
        public event EventHandler ParseProgressChanged;

        private bool keepSamples;
        private bool flipSamples;

        public float peakTreshold { get; private set; }
        public double SamplesPerMs { get; private set; }
        public double SamplesPerSec { get; private set; }
        public float[] Samples { get; private set; }
        private float midPoint;
        private AudioFileReader audioFileReader;

        public WaveFormat WaveFormat { get; private set; }
        private string wavPath;
        public WaveOut WaveOut;
        public WaveStream AudioReader;

        public List<KeyFrame> frames { get; private set; }
        public List<KeyFrame> badFrames { get; private set; }
        public List<int> badSyncs { get; private set; }

        private Stopwatch workerStopwatch;

        public TapeParser()
        {
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;

            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
        }

        int prevPercentage = -1;
        private ISampleProvider sampleProvider;
        private long sampleCount;

        /// <summary>
        /// Starts parsing the provided WAV file (async)
        /// </summary>
        /// <param name="path">Path to the WAV file</param>
        /// <returns>True if the worker succesfully started, False if it's busy.</returns>
        public bool StartParseAsync(string path, float tresh = 0.3f, bool _flipSamples = false, bool _keepSamples = false)
        {
            if (backgroundWorker.IsBusy) return false;

            wavPath = path;
            peakTreshold = tresh;
            flipSamples = _flipSamples;
            keepSamples = _keepSamples;

            frames = new List<KeyFrame>();
            badFrames = new List<KeyFrame>();
            badSyncs = new List<int>();

            workerStopwatch = new Stopwatch();

            workerStopwatch.Start();

            backgroundWorker.RunWorkerAsync();

            return true;
        }


        /// <summary>
        /// Parses the provided WAV file (blocking)
        /// </summary>
        /// <param name="path">Path to the WAV file</param>
        /// <param name="tresh">Peak detecting treshold (0-1f)</param>
        /// <param name="_flipSamples">Flip polarity of each sample</param>
        /// <param name="_keepSamples">Keep samples after parsing (memory intensive)</param>
        public void ParseBlocking(string path, float tresh = 0.3f, bool _flipSamples = false, bool _keepSamples = false)
        {
            wavPath = path;
            peakTreshold = tresh;
            flipSamples = _flipSamples;
            keepSamples = _keepSamples;

            frames = new List<KeyFrame>();
            badFrames = new List<KeyFrame>();
            badSyncs = new List<int>();

            parseActual();
        }


        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            parseActual();
        }

        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage > prevPercentage)
            {
                EventHandler handler = ParseProgressChanged;


                prevPercentage = e.ProgressPercentage;
                Console.WriteLine($"{e.ProgressPercentage}% - {(string)e.UserState}");



                if ((string)e.UserState == "Analyzing samples")
                {
                    var a = new ProgressChangedEventArgs(e.ProgressPercentage, $"{e.ProgressPercentage}% - {(string)e.UserState}");
                    handler?.Invoke(this, a);
                }
                else { handler?.Invoke(this, e); }
            }
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            prevPercentage = -1;
            workerStopwatch.Stop();

            Console.WriteLine("parsing took {0}ms", workerStopwatch.ElapsedMilliseconds);

            EventHandler handler = ParseDone;
            handler?.Invoke(this, e);
        }

        #region helpers
        public static float map(float x, float in_min, float in_max, float out_min, float out_max)
        {
            return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
        }
        public static int map(int x, int in_min, int in_max, int out_min, int out_max)
        {
            return (int)map((float)x, in_min, in_max, out_min, out_max);
        }
        #endregion


        private float[] sampleBuffer;
        private int sampleBufferStart;
        private bool isSampleBufferValid;
        private float minPeakLen;
        private float maxPeakLen;

        private float getSample(int i)
        {
            //Console.WriteLine($"requested sample {i}");
            
            if (keepSamples)
            {
                return Samples[i];
            }
            else
            {
                int sampleBufferEnd = sampleBufferStart + sampleBuffer.Length - 1;

                if(i < sampleBufferStart || i > sampleBufferEnd || !isSampleBufferValid)
                {
                    sampleBufferStart = (i / sampleBuffer.Length) * sampleBuffer.Length;


                    int read;

                    if (WaveFormat.Channels == 1)
                    {
                        audioFileReader.Seek(sampleBufferStart, System.IO.SeekOrigin.Begin);

                        read = audioFileReader.Read(sampleBuffer, 0, sampleBuffer.Length);
                    }
                    else
                    {
                        audioFileReader.Seek(sampleBufferStart*2*(WaveFormat.BitsPerSample/8), System.IO.SeekOrigin.Begin);

                        float[] stereoSampleBuffer = new float[sampleBuffer.Length*2];
                        read = audioFileReader.Read(stereoSampleBuffer, 0, stereoSampleBuffer.Length) / 2;
                        for (int f = 0; f < sampleBuffer.Length; f++)
                        {
                            sampleBuffer[f] = stereoSampleBuffer[f * 2 + 1];
                        }
                    }

                    if (read > 0) isSampleBufferValid = true;
                }

                int offset = i - sampleBufferStart;

                float sample = sampleBuffer[offset];

                return processSample(sample);
            }
        }

        private float processSample(float sample)
        {
            if (midPoint < 0f ^ flipSamples) sample = -sample;

            sample = (float)Math.Cos((1 - sample) * Math.PI / 2);

            return sample;
        }

        private void parseActual()
        {
            bool prepareSuccess = PrepareSamples(out midPoint);

            backgroundWorker.ReportProgress(4, "Processing samples");

            minPeakLen = (float)(SamplesPerMs * 0.65f); //750us
            maxPeakLen = (float)(SamplesPerMs * 2.1f); //1750us

            int i = 0;

            findSync(ref i);
            findSync(ref i);


            while (i < sampleCount)
            {
                int progress = (int)((float)i / sampleCount * 100);
                backgroundWorker.ReportProgress(progress, $"{progress} - Analyzing samples");

                if (!handleFrame(ref i)) break;
            }

            backgroundWorker.ReportProgress(99, "Normalizing frames");

            if (frames.Count > 0) normalizeFrames();

            if (!keepSamples)
            {
                Samples = null;
                //Samples = new float[sampleCount];

                for (int f = 0; f < sampleCount; f++)
                {
                    //Samples[f] = getSample(f);
                }
            }

            backgroundWorker.ReportProgress(100, "Done");
        }

        /// <summary>
        /// find, process, and save a frame
        /// </summary>
        /// <param name="i"></param>
        /// <param name="skipSync"></param>
        /// <returns>False if end of file is reached</returns>
        private bool handleFrame(ref int i, bool skipSync = false)
        {
            if (!skipSync)
            {
                if (!findSync(ref i)) return false; // find next sync
            }

            int syncstart = i;

            if (findPeak(ref i) == -1) return false;
            int firstpeak = i;

            KeyFrame frame = new KeyFrame((decimal)i / WaveFormat.SampleRate);

            int peakIndex = 0;
            int peakLen = 0;

            do
            {
                peakLen = findPeak(ref i);
                if (peakLen == -1) return false;

                if (isSync(peakLen))
                {
                    Console.WriteLine("found sync mid-frame at {0} ({1}s)", i, i / WaveFormat.SampleRate);
                    return handleFrame(ref i, true);
                }

                frame.Values[peakIndex] = map(peakLen, minPeakLen, maxPeakLen, 0, 1);

                frame.positions[peakIndex] = i - peakLen;
                frame.positions[peakIndex + 1] = i;

                if (frame.Values[peakIndex] < 0 || frame.Values[peakIndex] > 1) // invalid value, skip this
                {
                    Console.WriteLine("skipped frame at {0} ({1}s)", i, i / WaveFormat.SampleRate);
                    break;
                }

                peakIndex++;

            } while (peakIndex < KeyFrame.ChannelCount && peakLen < maxPeakLen * 1.2);

            if (peakIndex == KeyFrame.ChannelCount && peakLen < maxPeakLen * 1.2) // valid frame
            {
                //frame.Mouth = frame.Nose; //for goose tapes
                frames.Add(frame);
                return true;
            }
            else
            {
                badFrames.Add(frame);
                badSyncs.Add(syncstart);
                badSyncs.Add(firstpeak);
                Console.WriteLine("invalid frame at {0} ({1}s)", i, i / WaveFormat.SampleRate);
                return true;
            }

        }

        private int findPeak(ref int start)
        {
            int offset = 0;

            while (start + offset < sampleCount - 2)
            {
                float prev = getSample(start + offset);
                float curr = getSample(start + offset + 1);
                float next = getSample(start + offset + 2);
                if (prev > peakTreshold && curr > peakTreshold && next > peakTreshold)
                {
                    if (prev <= curr) //was uphill
                    {
                        if (curr > next) //will be downhill
                        {
                            start = start + offset + 1;
                            return offset + 1;
                        }
                    }
                }

                offset++;
            }
            return -1;
        }

        private bool isSync(int samplesBeforePeak)
        {
            if (samplesBeforePeak == -1) return false;
            float msBeforePeak = (float)samplesBeforePeak / (WaveFormat.SampleRate / 1000);
            return (msBeforePeak > 3); // over 3ms should mean the sync break
        }

        /// <summary>
        /// Finds the next sync pulse and places the cursor just after it
        /// </summary>
        /// <param name="startref"></param>
        /// <returns></returns>
        private bool findSync(ref int startref)
        {
            int start = startref;

            while (start < sampleCount)
            {
                int samplesBeforePeak = findPeak(ref start);
                if (samplesBeforePeak == -1)
                {
                    startref = start;
                    return false;
                }
                if (isSync(samplesBeforePeak))
                {
                    startref = start;
                    return true;
                }
            }
            return false;
        }

        private void normalizeFrames()
        {
            float[] min = { 1, 1, 1, 1, 1, 1, 1 };
            float[] max = new float[KeyFrame.ChannelCount];

            float[][] vals = new float[KeyFrame.ChannelCount][];
            for (int i = 0; i < KeyFrame.ChannelCount; i++) vals[i] = new float[frames.Count];

            for (int i = 0; i < frames.Count; i++) //collecting ranges
            {
                for (int f = 0; f < KeyFrame.ChannelCount; f++)
                {
                    vals[f][i] = frames[i].Values[f];
                }
            }

            int nthsampletouse = 2;
            for (int i = 0; i < vals.Length; i++)
            {
                Array.Sort(vals[i]);
                min[i] = vals[i][nthsampletouse];
                max[i] = vals[i][frames.Count - nthsampletouse -1];
            }



            /*
            float[] min = { 1,1,1,1,1,1,1 };
            float[] max = new float[7];

            for (int i = 0; i < frames.Count; i++)
            {
                for (int f = 0; f < 7; f++)
                {
                    if (frames[i].Values[f] < min[f]) min[f] = frames[i].Values[f];
                    if (frames[i].Values[f] > max[f]) max[f] = frames[i].Values[f];
                }
            }
            */

            for (int i = 0; i < frames.Count; i++)
            {
                for (int f = 0; f < KeyFrame.ChannelCount; f++)
                {
                    frames[i].Values[f] = Math.Min(Math.Max(map(frames[i].Values[f], min[f], max[f], 0, 1), 0f), 1f);
                }
            }
        }

        private bool PrepareSamples(out float midPoint)
        {
            audioFileReader = new AudioFileReader(wavPath);

            backgroundWorker.ReportProgress(0, "Reading WAV file");


            WaveFormat = audioFileReader.WaveFormat;

            sampleCount = audioFileReader.Length / (WaveFormat.BitsPerSample / 8) / WaveFormat.Channels;

            SamplesPerSec = WaveFormat.SampleRate;
            SamplesPerMs = SamplesPerSec / 1000d;


            {
                float max = 0;
                float sum = 0;
                bool rightchannel = false;

                float[] onesecbuffer = new float[audioFileReader.WaveFormat.SampleRate];
                int read;
                do
                {
                    read = audioFileReader.Read(onesecbuffer, 0, onesecbuffer.Length);
                    for (int n = 0; n < read; n++)
                    {
                        if (rightchannel)
                        {
                            var abs = Math.Abs(onesecbuffer[n]);
                            if (abs > max) max = abs;
                            sum += onesecbuffer[n];
                        }
                        rightchannel = !rightchannel;
                    }
                } while (read > 0);
                Console.WriteLine($"Max sample value: {max}");

                midPoint = sum / sampleCount; // avg to get midpoint

                if (max == 0 || max > 1.0f)
                    throw new InvalidOperationException("File cannot be normalized");

                // rewind and amplify
                audioFileReader = new AudioFileReader(wavPath);
                audioFileReader.Position = 0;
                //audioFileReader.Volume = 1.0f / max;
            }

            backgroundWorker.ReportProgress(1, "Reading WAV file again");

            if (keepSamples)
            {
                float[] samples;

                {
                    sampleProvider = audioFileReader.ToSampleProvider(); //isampleprovider deals with byte->float conversion

                    if (audioFileReader.WaveFormat.Channels == 2)
                        sampleProvider = sampleProvider.ToMono(0, 1);


                    samples = new float[sampleCount];

                    int read = sampleProvider.Read(samples, 0, (int)sampleCount);

                    if (audioFileReader.WaveFormat.Channels > 2)
                    {
                        throw new Exception($"Unsupported channel count ({audioFileReader.WaveFormat.Channels})");
                    }
                }

                backgroundWorker.ReportProgress(2, "Preprocessing samples");

                Samples = new float[sampleCount];

                for (int i = 0; i < sampleCount; i++)
                {
                    Samples[i] = processSample(samples[i]);
                }

                audioFileReader.Dispose();
            }
            else
            {
                sampleBuffer = new float[WaveFormat.SampleRate];
                isSampleBufferValid = false;
            }

            return true;
        }

        #region Playback
        public void playWav()
        {
            if (WaveOut != null)
            {
                WaveOut.Resume();
            }
            else
            {
                if (wavPath != "" && wavPath.EndsWith(".wav")) //WaveFileReader works as intended, AudioFileReader doesn't stay in sync
                {
                    WaveOut = new WaveOut();
                    AudioReader = new WaveFileReader(wavPath);

                    WaveOut.DesiredLatency = 100;

                    WaveOut.Init(AudioReader.ToSampleProvider().ToMono(1, 0));
                    WaveOut.Play();
                    //AudioReader.Seek( pos, System.IO.SeekOrigin.Begin);
                }
            }
        }

        public void pauseWav()
        {
            WaveOut.Pause();
        }
        #endregion
    }
}
