using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using NAudio.Wave;

namespace BottangoTeddyDriver
{
    class MicrophoneWrapper
    {
        private WaveIn microphone;

        private WasapiLoopbackCapture speakerCapture;

        private Stopwatch blinkStopwatch;
        private Random blinkRandom;
        public Timer blinkTimer;

        private bool mouthWasOpen;
        private Stopwatch mouthClosedStopwatch;
        public int noseLowerDelay = 800;

        private float currentSample;
        private float highestSample;

        public int selectedSourceIndex { get; private set; }
        public float Gain { get; set; } = 1.5f;

        private IWaveIn[] captures;

        public MicrophoneWrapper()
        {
            blinkStopwatch = new Stopwatch();
            blinkRandom = new Random();
            blinkTimer = new Timer();

            blinkTimer.AutoReset = false;
            blinkTimer.Interval = blinkRandom.Next(10000, 30000);
            blinkTimer.Elapsed += BlinkTimer_Elapsed;

            mouthClosedStopwatch = new Stopwatch();


            microphone = new WaveIn();
            speakerCapture = new WasapiLoopbackCapture();

            captures = new IWaveIn[] { microphone, speakerCapture };

            microphone.BufferMilliseconds = 10;
            microphone.DataAvailable += Microphone_DataAvailable;

            speakerCapture.DataAvailable += speakerCapture_DataAvailable;
            //speakerCapture.StartRecording();
        }

        private void BlinkTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            blinkStopwatch.Restart();
        }

        private void speakerCapture_DataAvailable(object sender, WaveInEventArgs e)
        {

            int bytesPerSample = speakerCapture.WaveFormat.BitsPerSample / 8;
            int samplesRecorded = e.BytesRecorded / bytesPerSample;

            float[] samples = new float[samplesRecorded];

            currentSample = 0;


            for (int index = 0; index < samplesRecorded; index++)
            {
                float samplef = BitConverter.ToSingle(e.Buffer, index * bytesPerSample);

                samples[index] = samplef;
            }

            for (int index = 0; index < samplesRecorded; index++)
            {
                float sample = Math.Abs(samples[index]);

                if (sample > currentSample) currentSample = sample;
            }


            if (currentSample > highestSample) highestSample = currentSample;
        }


        private void Microphone_DataAvailable(object sender, WaveInEventArgs e)
        {
            currentSample = 0;

            int bytesPerSample = microphone.WaveFormat.BitsPerSample / 8;
            int samplesRecorded = e.BytesRecorded / bytesPerSample;

            byte[] buffer = e.Buffer;

            for (int index = 0; index < e.BytesRecorded; index += 2)
            {
                short sample = Math.Abs((short)((buffer[index + 1] << 8) |
                                        buffer[index]));
                float sample32 = sample / 32768f;

                if (sample > currentSample) currentSample = sample32;
            }


            if (currentSample > highestSample) highestSample = currentSample;
        }

        public void Start(string sourceName)
        {
            Start();

            SelectSource(sourceName);
        }

        private void Start()
        {
            blinkStopwatch.Reset();
            mouthClosedStopwatch.Reset();

            blinkTimer.Stop();
            blinkTimer.Start();

            currentSample = 0;
            highestSample = 0;
        }


        public float[] GetValues()
        {
            if (blinkStopwatch.ElapsedMilliseconds > 400f)
            {
                blinkStopwatch.Reset();
                blinkTimer.Interval = blinkRandom.Next(10000, 30000);
                blinkTimer.Start();
            }


            float micSample = Math.Min((float)currentSample / highestSample * Gain, 1f);

            float eyes = 0f;
            float nose = 0f;
            float mouth = 0f;

            if (blinkStopwatch.IsRunning) eyes = 1f;
            if (micSample > 0.1f)
            {
                nose = micSample;
                mouth = micSample;

                mouthWasOpen = true;
            }
            else
            {
                //Console.WriteLine(mouthClosedStopwatch.ElapsedMilliseconds);
                if (mouthWasOpen)
                {
                    mouthClosedStopwatch.Restart();
                    mouthWasOpen = false;

                    nose = 0.6f;
                }
                else if (mouthClosedStopwatch.ElapsedMilliseconds > 0 && mouthClosedStopwatch.ElapsedMilliseconds < noseLowerDelay)
                {
                    nose = 0.6f * ((noseLowerDelay - mouthClosedStopwatch.ElapsedMilliseconds) / (float)noseLowerDelay);
                }
                else
                {
                    mouthClosedStopwatch.Reset();
                }
            }


            return new float[] { eyes, mouth, mouth, 0.5f, eyes, nose, mouth };
        }

        internal void SelectSource(string sourceName)
        {
            switch (sourceName)
            {
                case "computer audio":
                    selectedSourceIndex = 1;
                    captures[0].StopRecording();
                    captures[1].StartRecording();
                    highestSample = 0;
                    break;
                case "microphone":
                    selectedSourceIndex = 0;
                    captures[1].StopRecording();
                    captures[0].StartRecording();
                    break;
                default:
                    break;
            }
        }
    }
}
