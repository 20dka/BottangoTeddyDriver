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
        private readonly MixingSampleProvider _mixingSampleProvider;
        private readonly SavingWaveProvider _savingWaveProvider;
        private readonly IWavePlayer _player;
        private readonly PpmProvider _ppmProvider;
        private readonly BufferedWaveProvider _outputBufferedProvider;
        private readonly BufferedWaveProvider _speakerBufferedProvider;
        private readonly WasapiLoopbackCapture _speakerCapture;
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
            _speakerCapture = new WasapiLoopbackCapture();

            //TODO: Create capture of output device for a lack of a better solution for saving to file :)))

            _waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(44100, 2);

            if (wantLoopback && _speakerCapture.WaveFormat.Channels > 1) // we can't use mono as the mixing source
            {
                _waveFormat = _speakerCapture.WaveFormat;
                hasLoopback = true;
            }

            // set up left channel:
            _speakerBufferedProvider = new BufferedWaveProvider(_waveFormat);
            var monoLoopbackProvider = _speakerBufferedProvider.ToSampleProvider().ToMono(); // stereo -> mono -> stereo (mixed left)
            //_mixingSampleProvider.AddMixerInput(bufferedLeftOnly);
            _speakerCapture.DataAvailable += _speakerCapture_DataAvailable;
            if (hasLoopback) _speakerCapture.StartRecording();

            // set up right channel:
            _ppmProvider = new PpmProvider(channelsCount, ppmProfile, _waveFormat);
            //_mixingSampleProvider.AddMixerInput(_ppmProvider); // right channel


            MultiplexingSampleProvider _multiplexingSampleProvider = new MultiplexingSampleProvider(new ISampleProvider[] { monoLoopbackProvider, _ppmProvider }, 2);

            _multiplexingSampleProvider.ConnectInputToOutput(0, 0);
            _multiplexingSampleProvider.ConnectInputToOutput(1, 1);

            _volumeProvider = new VolumeSampleProvider(_multiplexingSampleProvider);
            _volumeProvider.Volume = 0;

            //_savingWaveProvider = new SavingWaveProvider(_volumeProvider.ToWaveProvider(), "test.wav");


            //_outputBufferedProvider = new BufferedWaveProvider(_waveFormat);

            _player = new  WasapiOut(device, AudioClientShareMode.Shared, false, 30);
            _player.Init(_volumeProvider);
        }

        public void setVolume(float v)
        {
            _volumeProvider.Volume = v;
        }

        private void _speakerCapture_DataAvailable(object sender, WaveInEventArgs e)
        {
            _speakerBufferedProvider.AddSamples(e.Buffer, 0, e.BytesRecorded);
        }

        /// <summary>
        /// Return list of avaliable to output devises
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
            _savingWaveProvider?.Dispose();
            _speakerCapture?.StopRecording();
        }


        public void Dispose()
        {
            Stop();
        }
    }

}
