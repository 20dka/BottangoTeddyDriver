using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AudioPPM;
using NAudio.CoreAudioApi;

namespace BottangoTeddyDriver
{
    class PPMWrapper
    {
        PpmProfile _profile;
        byte _channelCount = 8;
        private byte[] _channelValues;
        

        PpmGenerator _generator;

        public PPMWrapper()
        {
            _channelValues = new byte[_channelCount];
        }

        public void Start(MMDevice outputDevice, PpmProfile profile, bool wantLoopback)
        {
            if (outputDevice == null)
            {
                throw new ArgumentNullException("No output device specified.");
            }

            _profile = profile;

            _channelCount = 8;
            if (profile.Suffix != null) _channelCount += Convert.ToByte(profile.Suffix.Length);

            _channelValues = new byte[_channelCount];

            if (profile.Suffix != null)
            {
                for (int i = 0; i < profile.Suffix.Length; i++)
                {
                    _channelValues[i + 8] = profile.Suffix[i] ? (byte)255 : (byte)0;
                }
            }

            _generator = new PpmGenerator(_channelCount, profile, outputDevice, wantLoopback);
            _generator.SetValues(_channelValues);
            _generator.Start();
        }

        public void Stop()
        {
            _generator?.Stop();
            _generator?.Dispose();
        }


        byte mapFloatToByte(float f)
        {
            return (byte)Math.Floor(f >= 1.0 ? 255 : f * 256.0);
        }

        public void setValues(float eyes, float nose, float mouth) 
        {
            setValues(eyes, nose, mouth, 0, 0, 0, 0);
        }

        public void setValues(float eyes, float nose, float mouth, float mix, float geyes, float gnose, float gmouth) 
        {
            setValues(mapFloatToByte(eyes), mapFloatToByte(nose), mapFloatToByte(mouth),
                mapFloatToByte(mix), mapFloatToByte(geyes), mapFloatToByte(gnose), mapFloatToByte(gmouth));
        }

        public void setValues(byte eyes, byte nose, byte mouth)
        {
            setValues(eyes, nose, mouth, 0, 0, 0, 0);
        }

        public void setValues(byte eyes, byte nose, byte mouth, byte mix, byte geyes, byte gnose, byte gmouth)
        {
            _channelValues[1] = eyes;
            _channelValues[2] = nose;
            _channelValues[3] = mouth;

            _channelValues[4] = mix;
            _channelValues[5] = geyes;
            _channelValues[6] = gnose;
            _channelValues[7] = gmouth;

            _generator?.SetValues(_channelValues);
        }

        internal void setVolume(float v)
        {
            _generator?.setVolume(v);
        }

        internal void setLoopback(bool enable)
        {
            _generator?.setLoopback(enable);
        }
    }
}
