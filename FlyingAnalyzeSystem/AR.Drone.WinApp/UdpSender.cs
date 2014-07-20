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
            _socket = new Socket(AddressFamily.InterNetwork,SocketType.Dgram,ProtocolType.Udp);
        }

        private void Send(object data)
        {
            if (data != null)
            {
                byte[] package = (byte[])data;
                _socket.SendTo(package, package.Length, SocketFlags.None, _endPoint);
            }
        }

        public void SendData(object data)
        {
            Thread sendThread = new Thread(new ParameterizedThreadStart(Send));
            sendThread.Start(data);
        }
    }
}
