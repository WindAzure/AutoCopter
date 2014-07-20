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

namespace UDPTest
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
            Debug.WriteLine(data);
            byte[] package = new byte[3];

            package[0] = 1;
            package[1] = 0;
            package[2] = 2;

            _socket.SendTo(package, package.Length, SocketFlags.None, _endPoint);
        }

        public void SendData(object data)
        {
            Thread sendThread = new Thread(new ParameterizedThreadStart(Send));
            sendThread.Start(data);
        }
    }
}
