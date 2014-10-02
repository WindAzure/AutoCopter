using AR.Drone.Client;
using AR.Drone.Data;
using AR.Drone.Data.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AR.Drone.WinApp
{
    public class DroneSingleton
    {
        public static DroneClient _droneClient = null;
        public static NavigationData _navigationData;
        public static NavigationPacket _navigationPacket;

        public static void InitializeDrone()
        {
            DroneSingleton._droneClient = new DroneClient("192.168.1.1");
            DroneSingleton._droneClient.NavigationPacketAcquired += OnNavigationPacketAcquired;
            DroneSingleton._droneClient.NavigationDataAcquired += data => _navigationData = data;
            DroneSingleton._droneClient.Start();
            DroneSingleton._droneClient.ResetEmergency();
            DroneSingleton._droneClient.FlatTrim();
        }

        private static void OnNavigationPacketAcquired(NavigationPacket packet)
        {
            _navigationPacket = packet;
        }
    }
}
