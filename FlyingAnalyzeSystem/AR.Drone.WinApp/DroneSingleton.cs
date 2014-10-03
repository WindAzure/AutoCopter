using AR.Drone.Client;
using AR.Drone.Data;
using AR.Drone.Data.Navigation;
using AR.Drone.Video;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AR.Drone.WinApp
{
    public static class DroneSingleton
    {
        public static DroneClient _droneClient = null;
        public static NavigationData _navigationData;
        public static NavigationPacket _navigationPacket;
        public static VideoFrame _frame;
        public static Bitmap _frameBitmap;
        public static uint _frameNumber;
        public static VideoPacketDecoderWorker _videoPacketDecoderWorker;

        public static void InitializeDrone()
        {
            _videoPacketDecoderWorker = new VideoPacketDecoderWorker(PixelFormat.BGR24, true, OnVideoPacketDecoded);
            _videoPacketDecoderWorker.Start();

            _droneClient = new DroneClient("192.168.1.1");
            _droneClient.NavigationPacketAcquired += OnNavigationPacketAcquired;
            _droneClient.VideoPacketAcquired += OnVideoPacketAcquired;
            _droneClient.NavigationDataAcquired += data => _navigationData = data;

            _videoPacketDecoderWorker.UnhandledException += UnhandledException;

            _droneClient.Start();
            _droneClient.ResetEmergency();
            _droneClient.FlatTrim();
        }

        private static void OnVideoPacketDecoded(VideoFrame frame)
        {
            _frame = frame;
        }

        private static void OnNavigationPacketAcquired(NavigationPacket packet)
        {
            _navigationPacket = packet;
        }

        private static void OnVideoPacketAcquired(VideoPacket packet)
        {
            if (_videoPacketDecoderWorker.IsAlive)
                _videoPacketDecoderWorker.EnqueuePacket(packet);
        }

        private static void UnhandledException(object sender, Exception exception)
        {
            MessageBox.Show(exception.ToString(), "Unhandled Exception (Ctrl+C)", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

    }
}
