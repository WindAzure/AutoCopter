using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace AR.Drone.WinApp
{
    public class ReceiveUdpModel
    {
        int[] _ultrasonicDistance = new int[5];
        bool _pirSensor;
        List<string> _beaconMac = new List<String>();
        BackgroundWorker BackWork_UDP = new BackgroundWorker();
        public int intPort = 2323;

        public ReceiveUdpModel()
        {
            initUdpBackgroundWork();
        }

        private void initUdpBackgroundWork()
        {
            BackWork_UDP.ProgressChanged += BackWork_UDP_ProgressChanged;
            BackWork_UDP.DoWork += BackWork_UDP_DoWork;
            BackWork_UDP.WorkerSupportsCancellation = true;
            BackWork_UDP.WorkerReportsProgress = true;
            BackWork_UDP.RunWorkerCompleted += BackWork_UDP_RunWorkerCompleted;

            if (!BackWork_UDP.IsBusy) BackWork_UDP.RunWorkerAsync();  //啟動UDP接收
        }

        private void BackWork_UDP_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                IPEndPoint ipep = new IPEndPoint(IPAddress.Any, intPort);
                Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                newsock.Bind(ipep);
                IPEndPoint sendert = new IPEndPoint(IPAddress.Any, 0);
                EndPoint Remote = (EndPoint)(sendert);
                while (true)
                {
                    if (BackWork_UDP.CancellationPending)
                    {
                        e.Cancel = true;
                        newsock.Close();
                        break;
                    }
                    else
                    {
                        byte[] data = new byte[1024];
                        int recv = newsock.ReceiveFrom(data, ref Remote);
                        BackWork_UDP.ReportProgress(0, Encoding.UTF8.GetString(data, 0, recv));
                        System.Threading.Thread.Sleep(10);
                    }
                }

            }
            catch (Exception ex)
            {
                BackWork_UDP.ReportProgress(0, ex.Message);
            }
        }
        private void BackWork_UDP_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            String UdpData = e.UserState.ToString();
            if (UdpData.Contains("ultrasonic"))
            {
                _ultrasonicDistance = ReadUltrasonicData(e.UserState.ToString());
            }
            if (UdpData.Contains("CMacFound:"))
            {
                _beaconMac.AddRange(ReadBeaconMac(e.UserState.ToString()));
            }
            else if (UdpData.Contains("MacFound:"))
            {
                _beaconMac.Clear();
                _beaconMac.AddRange(ReadBeaconMac(e.UserState.ToString()));
            }
            if (UdpData.ToString().Contains("PIR"))
            {
                _pirSensor = ReadPirState(e.UserState.ToString());
            }
            //TB_RUDP.AppendText(e.UserState.ToString() + "\r\n");
        }
        private void BackWork_UDP_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                //TB_RUDP.AppendText(e.Error.Message + "\r\n");
            }
            else if (e.Cancelled)
            {
                //TB_RUDP.AppendText("Stop \r\n");
            }
            else
            {
                //TB_RUDP.AppendText("Work done \r\n");
            }
            //BT_RUDP.Enabled = true;
            //BT_RSUDP.Enabled = false;
        }

        private List<String> ReadBeaconMac(String Data)
        {
            int index = Data.IndexOf("MacFound:") + 11;
            int DataIndex = (Data.Length - index) / 12;
            string mac = null;
            List<String> beaconMac = new List<string>();
            for (int i = 0; i < DataIndex; i++)
            {
                mac = null;
                for (int j = 0; j < 12; j++)
                {
                    mac += Data[index++];
                }
                beaconMac.Add(mac);
            }
            return beaconMac;
        }

        private bool ReadPirState(string data)
        {
            int index = data.IndexOf("PIR:") + 4;
            int pirState = Convert.ToInt16(data[index]);
            if (pirState == 1)
                return true;
            else
                return false;
        }

        private int[] ReadUltrasonicData(string UdpData)
        {
            int[] UltrasonicData = new int[5];
            string keyWord = "FBRLUN";
            string data = null;
            int[] dataLenth = new int[5];
            int[] index = new int[6];
            for (int i = 0; i < 6; i++)
                index[i] = UdpData.IndexOf(keyWord[i]) + 2;
            for (int i = 0; i < 5; i++)
            {
                UltrasonicData[i] = 0;
                data = null;
                dataLenth[i] = index[i + 1] - index[i] - 3;
                for (int j = 0; j < dataLenth[i]; j++)
                    data += UdpData[index[i] + j];
                UltrasonicData[i] = Convert.ToInt16(data);
            }
            return UltrasonicData;
        }

        public bool PirState
        {
            get
            {
                return _pirSensor;
            }
        }

        public List<String> BeaconMac
        {
            get
            {
                return _beaconMac;
            }
        }

        public int[] UltraSonicDistance
        {
            get
            {
                return _ultrasonicDistance;
            }
        }
    }
}
