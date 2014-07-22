using AR.Drone.Video;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AR.Drone.WinApp
{
    public class UdpSender
    {
        private Socket _socket;
        private IPEndPoint _endPoint;

        public UdpSender(String ip, int portNumber)
        {
            _endPoint = new IPEndPoint(IPAddress.Parse(ip), portNumber);
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socket.Connect(_endPoint);
        }

        private void SendData(object data)
        {
            if (data != null)
            {
                VideoFrame frame = data as VideoFrame;
                int width = frame.Width;
                int height = frame.Height;
                byte[] package = new byte[width * 3];
                byte[] frameBytes = (byte[])frame.Data.Clone();

                for (int i = 0; i < height; i++)
                {
                    Buffer.BlockCopy(frameBytes, i * width * 3, package, 0, width * 3);
                    _socket.SendTo(package, package.Length, SocketFlags.None, _endPoint);
                }
                /*byte[] package = new byte[10];
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
