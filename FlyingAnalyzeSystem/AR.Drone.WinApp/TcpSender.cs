﻿using AR.Drone.Video;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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
                    /*TcpClient client = _tcpLister.AcceptTcpClient();
                    client.NoDelay = true;
                    if (client.Connected)
                    {
                        int width = 640;
                        int height = 480;
                        NetworkStream stream = client.GetStream();
                        Image bitmap = Image.FromFile(@"C:\Users\Public\Pictures\Sample Pictures\Penguins.bmp");
                        ImageConverter converter = new ImageConverter();
                        byte[] frameBytes = (byte[])converter.ConvertTo(bitmap, typeof(byte[]));
                        if(stream.CanWrite)
                            stream.Write(frameBytes, 0, width*height* 3);
                        stream.Flush();
                        for (int i = 0; i < 30; i+=3)
                            Debug.WriteLine(frameBytes[i + 1].ToString() + " " + frameBytes[i + 2].ToString() + " " + frameBytes[i + 3].ToString());
                        int T = 0;
                        while (T < height)
                        {
                            if (stream.CanWrite)
                            {
                                stream.Write(frameBytes, T * width * 3, width * 3);
                                T++;
                            }
                            stream.Flush();
                        }
                    }*/
                    /* NetworkStream stream = client.GetStream();
                     if (stream.CanWrite)
                     {
                         byte[] package = new byte[10];
                         package[0] = 1;
                         package[1] = 2;
                         stream.Write(package, 0, package.Length);
                     }*/
                      TcpClient client = _tcpLister.AcceptTcpClient();
                      client.NoDelay = true;
                      if (client.Connected)
                      {
                          VideoFrame frame = data as VideoFrame;
                          Debug.WriteLine(frame.Width.ToString() + " " + frame.Height.ToString());
                          int width = frame.Width;
                          int height = frame.Height;
                          byte[] frameBytes = (byte[])frame.Data.Clone();
                          NetworkStream stream = client.GetStream();

                          stream.Write(frameBytes, 0, width * height * 3);
                          stream.Flush();
                          /*int T = 0;
                          while (T < height)
                          {
                              if (stream.CanWrite)
                              {
                                      stream.Write(frameBytes, T * width * 3, width * 3);
                                      T++;
                              }
                              //Debug.WriteLine("Done");
                              stream.Flush();
                          }*/
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
            //SendData(data);
        }
    }
}
