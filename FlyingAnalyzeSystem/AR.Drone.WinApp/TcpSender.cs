using AR.Drone.Video;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace AR.Drone.WinApp
{
    public class TcpSender
    {
        private IPEndPoint _ipEndPoint;
        private TcpListener _tcpLister;

        public TcpSender(String ip, int portNumber)
        {
            _ipEndPoint = new IPEndPoint(IPAddress.Parse(ip), portNumber);
            _tcpLister = new TcpListener(_ipEndPoint);
            _tcpLister.Start();
        }

        private void SendData(object data)
        {
            if (data != null)
            {
                try
                {
                    TcpClient client = _tcpLister.AcceptTcpClient();
                    if (client.Connected)
                    {
                        VideoFrame frame = data as VideoFrame;
                        int width = frame.Width;
                        int height = frame.Height;
                        byte[] frameBytes = (byte[])frame.Data.Clone();
                        NetworkStream stream = client.GetStream();

                        if (stream.CanWrite)
                        {
                            for (int i = 0; i < height; i++)
                                stream.Write(frameBytes, i * width * 3, width*3);
                        }

                      /*  if (stream.CanWrite)
                        {
                            byte[] package = new byte[10];
                            package[0] = 1;
                            package[1] = 2;
                            stream.Write(package, 0, package.Length);
                        }
                        Debug.WriteLine("Done");*/
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Data.ToString());
                }
            }
        }

        public void SendVideoData(object data)
        {
            Thread sendThread = new Thread(new ParameterizedThreadStart(SendData));
            sendThread.Start(data);
        }
    }
}
