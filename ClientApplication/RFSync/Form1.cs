using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RFSync
{
    public partial class Form1 : Form
    {

        RFSync _sync = new RFSync();
        StringBuilder  _console = new StringBuilder();
        const int MAXCONSOLELINES = 22;

        public Form1()
        {
            InitializeComponent();
            
        }



//_sync.Put("start\r\n\r\n");


        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            _sync.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                _sync.Open();
            }
            catch(Exception error)
            {
                cConsole.Text = error.Message;
            }
            
        }



        string CutConsole()
        {
            string s = _console.ToString();
            List<int> crpos = new List<int>();
        
            for(int i=0;i<s.Length;i++)
                if (s[i] == '\n') crpos.Add(i);
            if (crpos.Count<=MAXCONSOLELINES) return s;
            
            _console.Remove(0,crpos[crpos.Count-MAXCONSOLELINES]);
            return _console.ToString();
        }




        string ByteArrayToText(byte[] data)
        {
            StringBuilder str = new StringBuilder();
            foreach (byte b in data)
                str.Append(string.Format("{0:X02}",b));
            return str.ToString();
        }



        private void cTimer_Tick(object sender, EventArgs e)
        {
            if (!_sync.IsOpened) return;

            if (_sync.IsDataDirty)
            {
                cData.Text = ByteArrayToText(_sync.GetData());
            }

            string s;
            if ((s=_sync.Get())==null) return;
            do 
            {   _console.Append(s);
            } while ((s=_sync.Get())!=null);
            cConsole.Text = CutConsole();
        }




        long atoi(string s)
        {
            long result;
            if (long.TryParse(s,out result))  return result;
            return 0;
        
        }





        private void bStart_Click(object sender, EventArgs e)
        {
            _sync.Put("start\r\n");
        }

        private void bStop_Click(object sender, EventArgs e)
        {
            _sync.Put("stop\r\n");
        }



        private void bSet_Click(object sender, EventArgs e)
        {
            long offset = atoi(tOffset.Text);
            long size = atoi(tSize.Text);
            long data = atoi(tData.Text);
                
            if (size==0) 
            {
                _sync.Print("*** error in parameters\r\n");                
                return;
            }

            string sdata = string.Format("{0:X0"+(size*2).ToString()+"}\r\n",data);
            if (sdata.Length>size*2) sdata = sdata.Substring(0,(int)(size*2));

            string cmd = string.Format("{0:X02} {1}\r\n",offset,sdata);
            _sync.Print("*** "+cmd);
            _sync.Put(cmd);
        }


        private void bGet_Click(object sender, EventArgs e)
        {
            _sync.Put("get\r\n");
        }

        private void bRun_Click(object sender, EventArgs e)
        {
            _sync.Print("*** "+tRun.Text + "\r\n");
            _sync.Put(tRun.Text + "\r\n");
        }

        private void cConsole_Click(object sender, EventArgs e)
        {
            _console.Length = 0;
            cConsole.Text = string.Empty;
        }





    }
}
