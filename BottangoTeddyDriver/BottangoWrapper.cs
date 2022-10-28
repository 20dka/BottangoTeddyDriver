using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace BottangoTeddyDriver
{


    class Globals
    {
        public static Form1 form;
    }




    class BottangoWrapper
    {
        internal static readonly object apiVersion = "0.5.0b";
        TcpClient client;

        Timer networkTimer;
        //Timer effectorTimer;

        BottangoCommandParser parser;

        public EffectorPool effectorPool = new EffectorPool();

        string serverIP;
        Int32 serverPort;

        int animationRate = 1000 / 60;

        public BottangoWrapper(string serverIP = "127.0.0.1", Int32 port = 59225, int animationRate = 1000 / 60)
        {
            parser = new BottangoCommandParser(this);

            this.serverIP = serverIP;
            this.serverPort = port;
            this.animationRate = animationRate;

            networkTimer = new Timer(10);
            networkTimer.Elapsed += networkTimer_Elapsed;
            //effectorTimer = new Timer(animationRate);
            //effectorTimer.Elapsed += animationTimer_Elapsed;

        }

        private void networkTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                if (client.Available > 0)
                {
                    using (StreamReader reader = new StreamReader(client.GetStream(), Encoding.UTF8))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            Globals.form.LogLine(line);
                            parser.Parse(line);
                        }
                    }
                }
            }
            catch
            {
                networkTimer.Stop();
            }
        }

        public void Reply(string message)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(message + "\n");
            client.GetStream().Write(bytes, 0, bytes.Length);
        }

        internal bool Connect()
        {
            client?.Close();

            try
            {
                client = new TcpClient(serverIP, serverPort);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Failed to connect to Bottango", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                return false;
            }

            networkTimer.Start();
            return true;
        }

        public void Disconnect()
        {
            client?.Close();
        }

        public byte[] getCurrentByte()
        {
            effectorPool.updateAll();

            byte[] positions = new byte[8];

            for (int i = 0; i < 8; i++)
            {
                PinServoEffector effector = (PinServoEffector)effectorPool.getEffector(i.ToString());
                if (effector != null)
                {
                    int index = effector.pin;
                    positions[index] = (byte)Math.Min((int)(effector.getSignalNormalized()*255), 255);
                }
            }

            return positions;
        }
    }

    public class Time
    {
        public static Stopwatch sw = Stopwatch.StartNew();

        public static long lastSyncTime = 0;
        public static long lastSyncDT = 0;

        public static uint getTimeOnServer()
        {
            if (lastSyncDT == 0) return 0;
            return (uint)(lastSyncTime + (sw.ElapsedMilliseconds) - (lastSyncDT));
        }

        public static uint getElapsedMS()
        {
            return (uint)sw.ElapsedMilliseconds;
        }

        public static uint getElapsedUS()
        {
            return (uint)sw.ElapsedMilliseconds * 1000;
        }
    }


    public class Curve
    {
        //public Curve(uint startTimeInMs, uint duration, int startY, int startControlX, int startControlY, int endY, int endControlX, int endControlY)
        public Curve(uint startTimeInMs, uint duration, float startY, int startControlX, float startControlY, float endY, int endControlX, float endControlY)
        {

            this.startTime = startTimeInMs;
            this.duration = duration;

            this.startY = startY;
            this.startControlX = startControlX;
            this.startControlY = startControlY;

            this.endY = endY;
            this.endControlX = endControlX;
            this.endControlY = endControlY;
            this.endTime = startTime + duration;
        }

        public bool Expired(uint time)
        {
            return time > this.endTime;
        }

        public bool isInProgress(uint currentTimeMs)
        {
            return currentTimeMs >= this.startTime && currentTimeMs <= this.endTime;
        }


        private float lerp(float start, float end, float u)
        {
            return ((end - start) * u) + start;
        }

        private void EvaluateForU(float u, ref float outx, ref float outy)
        {
            float p11x = lerp((float)startTime, (float)(startTime + startControlX), u);
            float p11y = lerp((float)startY, (float)(startY + startControlY), u);

            float p12x = lerp((float)startTime + startControlX, (float)(endTime + endControlX), u);
            float p12y = lerp((float)(startY + startControlY), (float)(endY + endControlY), u);

            float p13x = lerp((float)(endTime + endControlX), (float)endTime, u);
            float p13y = lerp((float)(endY + endControlY), (float)endY, u);

            float p21x = lerp(p11x, p12x, u);
            float p21y = lerp(p11y, p12y, u);

            float p22x = lerp(p12x, p13x, u);
            float p22y = lerp(p12y, p13y, u);

            outx = lerp(p21x, p22x, u);
            outy = lerp(p21y, p22y, u);
        }

        public float Evaluate(uint x)
        {
            float uLower = 0f;
            float uUpper = 1f;
            float u = 0.5f;

            int count = 0;
            float evaluatedY = 0F;

            while (++count < 50)
            {
                float evaluatedX = 0F;
                EvaluateForU(u, ref evaluatedX, ref evaluatedY);

                float diff = evaluatedX - x;

                if (Math.Abs(evaluatedX - x) <= 2)
                {
                    return evaluatedY;
                }
                else if (evaluatedX > x)
                {
                    uUpper = u;
                }
                else if (evaluatedX < x)
                {
                    uLower = u;
                }

                u = (uUpper - uLower) / 2f + uLower;
            }
            return evaluatedY;
        }


        public uint startTime = 0;
        public uint duration = 0;
        public uint endTime = 0;

        public float startY = 0;
        public int startControlX = 0;
        public float startControlY = 0;

        public float endY = 0;
        public int endControlX = 0;
        public float endControlY = 0;
    }


    public abstract class AbstractEffector : System.IDisposable
    {

        public AbstractEffector(string _effectorType, string _identifier, int _minSignal, int _maxSignal, int _maxSignalChangePerSecond, int _startingSignal)
        {
            this.effectorType = _effectorType;
            this.identifier = _identifier;
            this.minSignal = _minSignal;
            this.maxSignal = _maxSignal;
            this.maxSignalChangePerSecond = _maxSignalChangePerSecond;

            this.setSignal(_startingSignal);

            this.curves = new List<Curve>();
        }

        public string getIdentifier()
        {
            return this.identifier;
        }

        public void setSignal(int signal)
        {
            this.currentSignal = signal;
            this.currentFloat = map(signal, this.minSignal, this.maxSignal, 0, 1);
            this.lastSignalSetTime = Time.getElapsedMS();
        }
        public void setSignalFloat(float signal)
        {
            this.currentSignal = (int)lerp(this.minSignal, this.maxSignal, signal);
            this.currentFloat = signal;
            this.lastSignalSetTime = Time.getElapsedMS();
        }

        public int getSignal()
        {
            return this.currentSignal;
        }

        public float getSignalNormalized()
        {
            return this.currentFloat;
        }

        public virtual void deregister()
        {
            clearCurves();
        }

        public virtual void addCurve(Curve curve)
        {
            curves.Add(curve);
        }

        public virtual void clearCurves()
        {
            curves.Clear();
        }


        public virtual void destroy()
        {
            clearCurves();
        }

        public virtual void Dispose()
        {
        }

        float map(float s, float a1, float a2, float b1, float b2)
        {
            return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
        }

        float lerp(float start, float end, float u)
        {
            return ((end - start) * u) + start;
        }


        public void update()
        {
            var timeOnServer = Time.getTimeOnServer();

            List<Curve> expiredCurves = new List<Curve>();
            List<Curve> inRangeCurves = new List<Curve>();

            foreach (var curve in curves.ToList())
            {
                if (curve.Expired(timeOnServer))
                {
                    expiredCurves.Add(curve);
                }
                else if(curve.isInProgress(timeOnServer)){
                    inRangeCurves.Add(curve);
                }
            }
            Curve curveToExcecute = null;


            if (inRangeCurves.Count > 0)
            {
                foreach (var curve in expiredCurves)
                {
                    this.curves.Remove(curve);
                }

                foreach (var curve in inRangeCurves)
                {
                    if (curveToExcecute == null)
                    {
                        curveToExcecute = curve;
                    }
                    else if (curve.startTime < curveToExcecute.startTime)
                    {
                        curveToExcecute = curve;
                    }
                }
            }
            else if (expiredCurves.Count > 0)
            {
                foreach (var curve in expiredCurves)
                {
                    if (curveToExcecute == null)
                        curveToExcecute = curve;
                    else if (curve.endTime > curveToExcecute.endTime)
                    {
                        if (this.curves.Contains(curveToExcecute))
                            this.curves.Remove(curveToExcecute);
                        curveToExcecute = curve;
                    }
                }
            }

            if (curveToExcecute != null)
            {
                float movement;
                if (curveToExcecute.Expired(timeOnServer))
                    movement = curveToExcecute.endY;
                else
                    movement = curveToExcecute.Evaluate(timeOnServer);


                this.setSignalFloat(movement);
                //this.setSignal(lerp(this.minSignal, this.maxSignal, movement));

            }
        }



        /*


            # curve in range to play
            if len(inRangeCurves) > 0:
                # remove all in expired
                for curve in expiredCurves:
                    self.curves.remove(curve)
                # play earliest start in range curve
                for curve in inRangeCurves:
                    if curveToExcecute is None:
                        curveToExcecute = curve
                    elif curve.startTime<curveToExcecute.startTime:

                        curveToExcecute = curve
            # play latest finish expired curve
            elif len(expiredCurves) > 0:
                for curve in expiredCurves:
                    if curveToExcecute is None:
                        curveToExcecute = curve
                    elif curve.endTime > curveToExcecute.endTime:
                        # remove previous
                        if curveToExcecute in self.curves:
                            self.curves.remove(curveToExcecute)
                        curveToExcecute = curve

            if curveToExcecute is not None:

                if isinstance(curveToExcecute, Curve) :

                    if curveToExcecute.expired(timeOnServer):
                        # go to end if expired
                        movement = curveToExcecute.endY
                    else:
                        # evaluate if in range
                        movement = curveToExcecute.evaluate(timeOnServer)

                    signal = lerp(self.minSignal, self.maxSignal, movement)

                    if src.CallbacksAndConfiguration.roundSignalToInt:
                        signal = round(signal)

                    # signal = self.speedLimitSignal(lerp(self.minSignal, self.maxSignal, movement))

                    if not signal == self.currentSignal:
                        self.setSignal(signal)

                elif isinstance(curveToExcecute, OnOffCurve) :


                    on = curveToExcecute.evaluate(timeOnServer)
                    if not on == self.currentSignal:
                        self.setOnOffSignal(on)

                elif isinstance(curveToExcecute, TriggerCurve) :

                    self.setTrigger()
                    self.curves.remove(curveToExcecute) # trigger curves should only fire once

        */








        protected int minSignal = 0;
        protected int maxSignal = 0;

        protected int maxSignalChangePerSecond;
        protected int currentSignal;  // int between minSignal and maxSignal
        protected float currentFloat; // float between 0 and 1
        protected uint lastSignalSetTime;

        protected string effectorType;
        protected string identifier;

        List<Curve> curves = new List<Curve>();
    }

    public class EffectorPool
    {
        private Dictionary<string, AbstractEffector> effectors = new Dictionary<string, AbstractEffector>();

        public EffectorPool()
        {
        }

        public AbstractEffector getEffector(string identifier)
        {
            AbstractEffector result;
            if (effectors.TryGetValue(identifier, out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }


        public void addEffector(string identifier, AbstractEffector inEffector)
        {
            if (effectors.ContainsKey(identifier))
            {
                throw new Exception("ServoCollision");
            }

            effectors[identifier] = inEffector;
        }

        public void removeEffector(string identifier)
        {
            if (!effectors.ContainsKey(identifier))
            {
                throw new Exception("Can't remove nonexistent effector");
            }

            effectors.Remove(identifier);
        }

        public void addCurveToEffector(string identifier, Curve curve)
        {
            AbstractEffector effector = getEffector(identifier);
            if (effector == null)
            {
                throw new NullReferenceException();
                //return;
            }
            effector.addCurve(curve);
        }

        public void clearCurvesForEffector(string identifier)
        {
            AbstractEffector effector = getEffector(identifier);
            if (effector == null)
            {
                throw new NullReferenceException();
                //return;
            }
            effector.clearCurves();
        }

        public void updateAll()
        {
            foreach (var effector in effectors)
            {
                effector.Value.update();
            }
        }

        public void deregisterAll()
        {
            effectors.Clear();
        }

        public void clearAllCurves()
        {
            foreach (var effector in effectors)
            {
                effector.Value.clearCurves();
            }
        }
    }

    public class PinServoEffector : AbstractEffector
    {
        public int pin;

        public PinServoEffector(int _pin, int _minSignal, int _maxSignal, int _maxSignalChangePerSecond, int _startingSignal) : base("pinServo", _pin.ToString(), _minSignal, _maxSignal, _maxSignalChangePerSecond, _startingSignal)
        {
            this.pin = _pin;
        }


    }

    class BottangoCommandParser
    {
        BottangoWrapper _wrapper;

        public BottangoCommandParser(BottangoWrapper wrapper)
        {
            _wrapper = wrapper;
        }

        string sendHandshakeResponse(string code)
        {
            string response = string.Format("btngoHSK,{0},{1},1", BottangoWrapper.apiVersion, code);
        	return response;
        }

        void handleTimeSync(string timecode)
        {
            Time.lastSyncTime = long.Parse(timecode);
            Time.lastSyncDT = Time.sw.ElapsedMilliseconds;
        }

        void registerPinServo(string[] args)
        {
            int pinId = Convert.ToInt32(args[1]);
            short minPWM = Convert.ToInt16(args[2]);
            short maxPWM = Convert.ToInt16(args[3]);
            int maxPWMSec = Convert.ToInt32(args[4]);
            short startPWM = Convert.ToInt16(args[5]);

            PinServoEffector newEffector = new PinServoEffector(pinId, minPWM, maxPWM, maxPWMSec, startPWM);
            string identifier = newEffector.getIdentifier();
            _wrapper.effectorPool.addEffector(identifier, newEffector);
        }

        public void Parse(string cmd)
        {
            string[] args = cmd.Split(',');
            switch (args[0])
            {
                case "hRQ":
                    _wrapper.Reply(sendHandshakeResponse(args[1]));
                    break;
                case "tSYN":
                    Console.WriteLine("sync time: " + args[1]);
                    handleTimeSync(args[1]);
                    break;
                case "xE":
                    Console.WriteLine("deregister all effectors");
                    _wrapper.effectorPool.deregisterAll();
                    break;

                case "xC":
                    Console.WriteLine("clear all curves");
                    _wrapper.effectorPool.clearAllCurves();
                    break;

                case "xUE":
                    Console.WriteLine("remove effector " + args[1]);
                    _wrapper.effectorPool.removeEffector(args[1]);
                    break;

                case "xUC":
                    Console.WriteLine("clear curves for effector " + args[1]);
                    _wrapper.effectorPool.clearCurvesForEffector(args[1]);
                    break;

                case "rSVPin":
                    Console.WriteLine("register effector " + args[1]);
                    registerPinServo(args);
                    break;

                case "sC":
                    {
                        // start is time in MS since last time sync
                        //if (args[2][0] == '-') AddLine(read);
                        uint startTime = (uint)(int.Parse(args[2]) + Time.lastSyncTime);

                        // duration of curve
                        uint duration = uint.Parse(args[3]);

                        // start Y is int 0-1000
                        float startMovement = int.Parse(args[4]) / 1000f;

                        int startControlX = int.Parse(args[5]);

                        float startControlY = int.Parse(args[6]) / 1000f;

                        float endMovement = int.Parse(args[7]) / 1000f;

                        int endControlX = int.Parse(args[8]);

                        float endControlY = int.Parse(args[9]) / 1000f;

                        _wrapper.effectorPool.addCurveToEffector(args[1], new Curve(startTime, duration, startMovement, startControlX, startControlY, endMovement, endControlX, endControlY));
                        break;
                    }
                case "sCI":
                    {
                        float endMovement = int.Parse(args[2]) / 1000f;
                        _wrapper.effectorPool.addCurveToEffector(args[1], new Curve(Time.getTimeOnServer(), 0, endMovement, 0, 0, endMovement, 0, 0));
                        break;
                    }
                default:
                    Console.WriteLine("Unimplemented packet: " + args[0]);
                    Globals.form.LogLine("Unimplemented packet: " + cmd);
                    return;
            }
            _wrapper.Reply("OK");
        }
    }

}
