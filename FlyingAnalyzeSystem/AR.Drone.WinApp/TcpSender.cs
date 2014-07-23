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
                        NetworkStream stream = client.GetStream();
                        if (stream.CanWrite)
                        {
                            byte[] package = new byte[10];
                            package[0] = 1;
                            package[1] = 2;
                            stream.Write(package, 0, package.Length);
                        }
                        Debug.WriteLine("Done");
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Data.ToString());
                }
                /*VideoFrame frame = data as VideoFrame;
                int width = frame.Width;
                int height = frame.Height;
                byte[] package = new byte[width * 3];
                byte[] frameBytes = (byte[])frame.Data.Clone();

                for (int i = 0; i < height; i++)
                {
                    Buffer.BlockCopy(frameBytes, i * width * 3, package, 0, width * 3);
                    _socket.SendTo(package, package.Length, SocketFlags.None, _endPoint);
                }
                byte[] package = new byte[10];
                package[0] = 1;
                package[1] = 2;
                _socket.SendTo(package, package.Length, SocketFlags.None, _endPoint);
                Debug.WriteLine("Done");*/
            }
        }

        public void SendVideoData(object data)
        {
            Thread sendThread = new Thread(new ParameterizedThreadStart(SendData));
            sendThread.Start(data);
        }
    }
}
