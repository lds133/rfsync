using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Text.RegularExpressions;

namespace RFSync
{
    class RFSync
    {
        const int PAYLOADSIZE = 30;
 

        const int INPUTLINESMAX = 100;
        const uint READBUFFERSIZE = 10000;
        const int READTIMEOUT = 200;
        const int THREADTIMEOUT = 10000;
        const string DEVICE_VID  = "2156";
        const string DEVICE_PID  = "C002";
        const UInt32 NOTSET = 0xFFFFFFFF;

        UInt32 _handle;
        UInt16 _device;
        byte[] _data = new byte[PAYLOADSIZE];
        byte[] _mask = new byte[PAYLOADSIZE];
        bool _isdatadirty = true;
        bool _isopened = false;

        Thread _thread;
        ManualResetEvent _started = new ManualResetEvent(false);
        ManualResetEvent _stop = new ManualResetEvent(false);
        ManualResetEvent _stopped = new ManualResetEvent(false);

        Queue<string> _input = new Queue<string>();


        public RFSync()
        {
            Clean();

            _thread = null;


        }

        public byte[] GetData()
        {
            _isdatadirty = false;
            return _data;
        }

        public bool IsDataDirty
        {   get
            {
                return _isdatadirty;
            }
        }

        public bool IsOpened
        {
            get{    return _isopened;}
        }

        UInt16 FindDevice()
        {
            UInt32 n=0;
            byte[] vid = new byte[5];
            byte[] pid = new byte[5];

            if (USB.GetNumDevices(ref n) != USB.SUCCESS) return USB.DEVICE_NOT_FOUND;

            for(UInt16 i=0;i<n;i++)
            {
                if (USB.GetProductString(0,ref vid[0],USB.RETURN_VID) != USB.SUCCESS) continue;
                if (System.Text.Encoding.Default.GetString(vid, 0, 4).ToUpper() != DEVICE_VID) continue;
                if (USB.GetProductString(0,ref pid[0],USB.RETURN_PID) != USB.SUCCESS) continue;
                if (System.Text.Encoding.Default.GetString(pid, 0, 4).ToUpper() != DEVICE_PID) continue;

                return i;
            }

            return USB.DEVICE_NOT_FOUND;
        }


        void Clean()
        {
            _handle = NOTSET;
            _device = USB.DEVICE_NOT_FOUND;  
            Array.Clear(_data,0,PAYLOADSIZE);
            Array.Clear(_mask,0,PAYLOADSIZE);
        }


        public void Close()
        {
            if (_thread==null) return;

            _stop.Set();
            _stopped.WaitOne(THREADTIMEOUT);


        }

        public void Print(string text)
        {
            if (_input.Count>INPUTLINESMAX) return;
            lock(_input)
            {
                _input.Enqueue(text);
            }
        
        }

        void ProcessInput(string text)
        {
            
            //todo: improve parsing, string can come as two parts
            Parse(text);
         
            Print(text);
        }



        long FromHEX(string s)
        {
            long result;
            if (long.TryParse(s,System.Globalization.NumberStyles.HexNumber,null,out result))  return result;
            return 0;
        
        }

        byte[] StringToByteArray(string s)
        {
            int pos=0;
            List<byte> b = new List<byte>();
            while(pos+2<s.Length)
            {
                b.Add( (byte)FromHEX(s.Substring(pos,2)));
                pos+=2;
            }
            return b.ToArray();
        }


        Regex _rx = new Regex(@"([0-9A-F]+) ([0-9A-F]+)\r\n");
        void Parse(string cmd)
        {
            MatchCollection matches = _rx.Matches(cmd);
            if (matches.Count==0) return;

            byte[] data;
            int offset;
            foreach(Match m in matches)
            {
                offset = (int)FromHEX( m.Groups[1].Value );
                data = StringToByteArray(m.Groups[2].Value);

                _isdatadirty = true;
                if (offset+data.Length<PAYLOADSIZE)
                    Array.Copy(data,0,_data,offset,data.Length);
            
            }

        }


        void Process()
        {
            UInt32 bytestoread=0;
            UInt32 readstatus=0;
            UInt32 bytesread=0;
            byte[] readbuffer = new byte[READBUFFERSIZE];


            _started.Set();

            while(true)
            {
                if (USB.CheckRXQueue(_handle, ref bytestoread,ref readstatus)!= USB.SUCCESS) break; 
                if (bytestoread!=0)
                {   
                    USB.SetTimeouts(500, 500);
                    if (USB.Read(_handle, ref readbuffer[0], READBUFFERSIZE , ref bytesread, 0) != USB.SUCCESS) break; 
                    ProcessInput(System.Text.Encoding.Default.GetString(readbuffer, 0,(int)bytesread));
                    bytestoread-=bytesread;
                    if (bytestoread!=0) continue;
                }

                if (_stop.WaitOne(READTIMEOUT)) break;
            }

            USB.Close(ref _handle);
            Clean();

            _stopped.Set();
        }


        public bool Open()
        {

            if (_thread!=null) throw new Exception("Device alredy opened");

            _device = FindDevice();
            if (_device == USB.DEVICE_NOT_FOUND) throw new Exception("Device not found");

            if (USB.Open(_device,ref _handle) != USB.SUCCESS)
            {   Clean();
                throw new Exception("Device open error");
            }

            _input.Clear();

            _started.Reset();
            _stop.Reset();
            _stopped.Reset();
            _thread = new Thread(Process);
            _thread.Name = "RF-2400U";
            _thread.Start();

            if (!_started.WaitOne(THREADTIMEOUT))
            {
                _stop.Set();
                throw new Exception("Problems with startting device thread");
            }

            _isopened = true;
            
            return true;
        }

        public string Get()
        {
            if (_input.Count==0) return null;
            string str;
            lock(_input)
            {
                str = _input.Dequeue();
            }
            return str;
        }

        bool Put(byte[] text)
        {
            if (_handle == NOTSET) return false;
            UInt32 written=0;
            UInt32 towrite=(UInt32)text.Length;
            USB.SetTimeouts(500, 500);
            if (USB.Write(_handle, ref text[0], towrite , ref written, 0)!=USB.SUCCESS) return false;
            return (written == text.Length);
        
        }


        public bool Put(string text)
        {
            if (!_isopened) return false;
            Parse(text);
            return Put(System.Text.Encoding.Default.GetBytes(text));
        }








    }
}
