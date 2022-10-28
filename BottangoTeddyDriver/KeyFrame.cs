using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BottangoTeddyDriver
{
    [Serializable]
    public class KeyFrame
    {
        public static readonly int ChannelCount = 7;

        public float[] Values = new float[ChannelCount];

        public int[] positions = new int[ChannelCount+1];

        public decimal Time { get; set; }
        public float Eye { get { return Values[0];  } set { Values[0] = value; } }
        public float Nose { get { return Values[1]; } set { Values[1] = value; } }
        public float Mouth { get { return Values[2]; } set { Values[2] = value; } }
        public float Mix { get { return Values[3]; } set { Values[3] = value; } }
        public float GEye { get { return Values[4]; } set { Values[4] = value; } }
        public float GNose { get { return Values[5]; } set { Values[5] = value; } }
        public float GMouth { get { return Values[6]; } set { Values[6] = value; } }

        public KeyFrame() : this(0,0,0,0)
        { }

        public KeyFrame(decimal t) : this(t,0,0,0)
        { }

        /// <summary>
        /// Creates a new KeyFrame objects, only defining teddy's positions
        /// </summary>
        /// <param name="t">Time, decimal</param>
        /// <param name="e">Teddy Eye</param>
        /// <param name="n">Teddy Nose</param>
        /// <param name="m">Teddy Mouth</param>
        /// <param name="flip">flip teddy eye?</param>
        public KeyFrame(decimal t, float e, float n, float m, bool flip = false) : this(t,e,n,m,0,0,0,0,flip)
        {
        }

        /// <summary>
        /// Creates a new KeyFrame objects, defining all positions
        /// </summary>
        /// <param name="t">Time, decimal</param>
        /// <param name="e">Teddy Eye</param>
        /// <param name="n">Teddy Nose</param>
        /// <param name="m">Teddy Mouth</param>
        /// <param name="mM">Audio mix</param>
        /// <param name="gE">Grubby Eye</param>
        /// <param name="gN">Grubby Nose</param>
        /// <param name="gM">Grubby Mouth</param>
        /// <param name="flip">flip teddy eye?</param>
        public KeyFrame(decimal t, float e, float n, float m, float mM, float gE, float gN, float gM, bool flip = false)
        {
            Time = t;
            Eye = flip ? 1 - e : e;
            Nose = n;
            Mouth = m;

            Mix = mM;
            GEye = flip ? 1 - gE : gE;
            GNose = gN;
            GMouth = gM;
        }

        public KeyFrame(string str)
        {
            string[] a = str.Split(',');

            Time = decimal.Parse(a[0]);
            Eye = float.Parse(a[1]);
            Nose = float.Parse(a[2]);
            Mouth = float.Parse(a[3]);

            Mix = float.Parse(a[4]);
            GEye = float.Parse(a[5]);
            GNose = float.Parse(a[6]);
            GMouth = float.Parse(a[7]);
        }

        public override string ToString()
        {
            return Time.ToString() + "," + string.Join(",", Values);
            //return string.Format("{0},{1},{2},{3},{4},{5},{6},{7}", Time, eye, nose, mouth, mix, gEye, gNose, gMouth);
        }
    }
}
