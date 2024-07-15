using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System.Diagnostics;

namespace AudioPPM
{
    /// <summary>
    /// Generate PPM controlling signal to selected output device using WASAPI
    /// </summary>
    public class PpmGenerator : IDisposable
    {

        private readonly IWavePlayer _player;
        private readonly PpmProvider _ppmProvider;
        private readonly BufferedWaveProvider loopbackBufferedProvider;
        private readonly WasapiLoopbackCapture loopbackCapture;
        private readonly WaveFormat _waveFormat;
        private readonly bool hasLoopback;
        private VolumeSampleProvider _volumeProvider;

        /// <summary>
        /// Create new PpmGenerator object
        /// </summary>
        /// <param name="channelsCount">Number of channels in signal</param>
        /// <param name="ppmProfile">PPM profile for using transmitter</param>
        /// <param name="device">Selected output device</param>
        public PpmGenerator(byte channelsCount, PpmProfile ppmProfile, MMDevice device, bool wantLoopback)
        {
            int audioChannelCount = device.AudioClient.MixFormat.Channels;

            // set up loopback provider:
            loopbackCapture = new WasapiLoopbackCapture();

            var loopbackWaveformat = new WaveFormat();

            if (wantLoopback)
            {
                loopbackWaveformat = loopbackCapture.WaveFormat;
                hasLoopback = true;
            }

            loopbackBufferedProvider = new BufferedWaveProvider(loopbackWaveformat);
            var monoLoopbackProvider = loopbackBufferedProvider.ToSampleProvider().ToMono(); // stereo -> mono
            loopbackCapture.DataAvailable += _speakerCapture_DataAvailable;
            if (hasLoopback) loopbackCapture.StartRecording();

            // set up right channel:
            _ppmProvider = new PpmProvider(channelsCount, ppmProfile, loopbackWaveformat);
            _waveFormat = new WaveFormatExtensible(loopbackWaveformat.SampleRate, 32, audioChannelCount);

            MultiplexingSampleProvider _multiplexingSampleProvider = new MultiplexingSampleProvider(new ISampleProvider[] { monoLoopbackProvider, _ppmProvider }, _waveFormat);

            _multiplexingSampleProvider.ConnectInputToOutput(0, 0);
            _multiplexingSampleProvider.ConnectInputToOutput(1, 1);
            
            _volumeProvider = new VolumeSampleProvider(_multiplexingSampleProvider);
            _volumeProvider.Volume = 0;

            _player = new  WasapiOut(device, AudioClientShareMode.Shared, false, 30);
            _player.Init(_volumeProvider);
        }

        public void setLoopback(bool enable)
        {
            loopbackBufferedProvider.ClearBuffer();
            if (enable)
            {
                loopbackCapture.StartRecording();
            }
            else
            {
                loopbackCapture.StopRecording();
            }
        }

        public void setVolume(float v)
        {
            _volumeProvider.Volume = v;
        }

        private void _speakerCapture_DataAvailable(object sender, WaveInEventArgs e)
        {
            loopbackBufferedProvider.AddSamples(e.Buffer, 0, e.BytesRecorded);
        }

        /// <summary>
        /// Return list of avaliable to output devices
        /// </summary>
        /// <returns></returns>
        public static MMDeviceCollection GetDevices()
        {
            MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
            return enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);
        }


        /// <summary>
        /// Set new values to the signal.
        /// Number of values must be same as number of channels
        /// </summary>
        /// <param name="values">New value for every channel</param>
        public void SetValues(byte[] values)
        {
            _ppmProvider.SetValues(values);
        }


        /// <summary>
        /// Begin playing signal
        /// </summary>
        public void Start()
        {
            _player.Play();
        }

        /// <summary>
        /// Stop playing signal
        /// </summary>
        public void Stop()
        {
            _player.Stop();
            loopbackCapture?.StopRecording();
        }


        public void Dispose()
        {
            Stop();
        }
    }

}
