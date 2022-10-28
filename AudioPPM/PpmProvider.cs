using System;
using NAudio.Wave;

namespace AudioPPM
{
    /// <inheritdoc />
    /// <summary>
    /// IProvider implementation that generates PPM controlling singal and change
    /// controlling values
    /// </summary>
    public class PpmProvider : ISampleProvider
    {
        private WaveFormat _waveFormat;
        public WaveFormat WaveFormat => _waveFormat;

        private PpmProfile _ppmProfile;

        // Data buffers
        private readonly byte[] _playingBuffer;         // Currently playing values
        private readonly byte[] _writingBuffer;         // User write new values there
        private object _lock;

        // PPM timing params
        private int _currentChannel;
        private int _currentChannelSample;
        private int _pauseSamples;
        private int _periodSamples;
        private int _totalSamples;

        private const float DIVIDER = 1000000;          // microseconds to seconds

        /// <summary>
        /// PPM timings and polarity settings. Read only.
        /// </summary>
        public PpmProfile PpmProfile => _ppmProfile;

        /// <summary>
        /// Number of channels
        /// </summary>
        public byte ChannelsCount { get; private set; }



        public PpmProvider(byte channels, PpmProfile ppmProfile) : this(channels, ppmProfile, 44100)
        {
        }

        public PpmProvider(byte channels, PpmProfile ppmProfile, WaveFormat waveFormat) : this(channels, ppmProfile, waveFormat.SampleRate)
        {
        }


        public PpmProvider(byte channels, PpmProfile ppmProfile, int sampleRate)
        {
            ChannelsCount = channels;
            _ppmProfile = ppmProfile;
            _waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, 1);

            _playingBuffer = new byte[channels];
            _writingBuffer = new byte[channels];

            _lock = new object();

            _currentChannel = 0;
            _currentChannelSample = 0;
            _totalSamples = 0;

            _pauseSamples = (int)(_ppmProfile.PauseDuration / 1000000.0 * _waveFormat.SampleRate);

            TakeNextChannel();
        }


        /// <summary>
        /// Set new values. They will be applied from new cycle. 
        /// NOTE: Number of values should be equal to number of channels.
        /// Otherwise exception will be thrown.
        /// </summary>
        /// <param name="values"></param>
        public void SetValues(byte[] values)
        {
            // 1 - _currentBuffer is buffer swapping
            lock (_lock)
            {
                Array.Copy(values, _writingBuffer, ChannelsCount);
            }
        }


        public int Read(float[] buffer, int offset, int sampleCount)
        {
            for (int i = 0; i < sampleCount; i++)
            {
                if (_currentChannelSample <= _pauseSamples)
                    buffer[offset + i] = GetValue(false);
                else
                    buffer[offset + i] = GetValue(true);

                _currentChannelSample++;
                _totalSamples--;

                if (_currentChannelSample > _periodSamples)
                    TakeNextChannel();
            }

            return sampleCount;
        }


        /// <summary>
        /// Get value from next channel and calculate timings
        /// </summary>
        private void TakeNextChannel()
        {
            _currentChannel++;
            _currentChannelSample = 0;

            if (_currentChannel == ChannelsCount)
            {
                //Pause
                _periodSamples = _totalSamples;

                _totalSamples = 0;
                _currentChannel = -1;

                lock (_lock)
                {
                    Array.Copy(_writingBuffer, _playingBuffer, ChannelsCount);
                }
            }
            else
            {
                float period = _playingBuffer[_currentChannel] / 255.0f *
                            (_ppmProfile.MaxChannelDuration -
                            _ppmProfile.MinChannelDuration) +
                            _ppmProfile.MinChannelDuration;

                _periodSamples = (int)(period / DIVIDER * _waveFormat.SampleRate);

                // New packages started - reset total samples counter
                if (_currentChannel == 0)
                    _totalSamples = (int)(_ppmProfile.Period / DIVIDER * _waveFormat.SampleRate);
            }
        }

        /// <summary>
        /// Get sample value by selected PPM polarity and needed value
        /// </summary>
        /// <param name="isSignal">Is value signal (variable length) or pause (constant length)</param>
        /// <returns></returns>
        private float GetValue(bool isSignal)
        {
            return isSignal ^ _ppmProfile.Polarity == PpmPolarity.HIGH ? -1 : 0; // XOR
        }
    }
}