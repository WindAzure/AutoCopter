﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AR.Drone.Client;
using AR.Drone.Client.Command;
using AR.Drone.Client.Configuration;
using AR.Drone.Data;
using AR.Drone.Data.Navigation;
using AR.Drone.Data.Navigation.Native;
using AR.Drone.Media;
using AR.Drone.Video;
using AR.Drone.Avionics;
using AR.Drone.Avionics.Objectives;
using AR.Drone.Avionics.Objectives.IntentObtainers;

namespace AR.Drone.WinApp
{
    public partial class MainForm : Form
    {
        private const string ARDroneTrackFileExt = ".ardrone";
        private const string ARDroneTrackFilesFilter = "AR.Drone track files (*.ardrone)|*.ardrone";

        private readonly DroneClient _droneClient;
        private readonly List<PlayerForm> _playerForms;
        private readonly VideoPacketDecoderWorker _videoPacketDecoderWorker;
        private Settings _settings;
        private VideoFrame _frame;
        private Bitmap _frameBitmap;
        private uint _frameNumber;
        private NavigationData _navigationData;
        private NavigationPacket _navigationPacket;
        private PacketRecorder _packetRecorderWorker;
        private FileStream _recorderStream;

        private UdpSender _udpSender = new UdpSender("192.168.1.3", 11111);
        //private TcpSender _tcpSender = new TcpSender("192.168.1.101", 11111);
        //private TcpSender _tcpSender = new TcpSender("192.168.1.2", 11111);

        public MainForm()
        {
            InitializeComponent();

            _videoPacketDecoderWorker = new VideoPacketDecoderWorker(PixelFormat.BGR24, true, OnVideoPacketDecoded);
            _videoPacketDecoderWorker.Start();

            _droneClient = new DroneClient("192.168.1.1");
            _droneClient.NavigationPacketAcquired += OnNavigationPacketAcquired;
            _droneClient.VideoPacketAcquired += OnVideoPacketAcquired;
            _droneClient.NavigationDataAcquired += data => _navigationData = data;

            tmrStateUpdate.Enabled = true;
            tmrVideoUpdate.Enabled = true;

            _playerForms = new List<PlayerForm>();

            _videoPacketDecoderWorker.UnhandledException += UnhandledException;
        }

        private void UnhandledException(object sender, Exception exception)
        {
            MessageBox.Show(exception.ToString(), "Unhandled Exception (Ctrl+C)", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Text += Environment.Is64BitProcess ? " [64-bit]" : " [32-bit]";
        }

        protected override void OnClosed(EventArgs e)
        {
            StopRecording();

            _droneClient.Dispose();
            _videoPacketDecoderWorker.Dispose();

            base.OnClosed(e);
        }

        private void OnNavigationPacketAcquired(NavigationPacket packet)
        {
            if (_packetRecorderWorker != null && _packetRecorderWorker.IsAlive)
                _packetRecorderWorker.EnqueuePacket(packet);

            _navigationPacket = packet;
        }

        private void OnVideoPacketAcquired(VideoPacket packet)
        {
            if (_packetRecorderWorker != null && _packetRecorderWorker.IsAlive)
                _packetRecorderWorker.EnqueuePacket(packet);
            if (_videoPacketDecoderWorker.IsAlive)
                _videoPacketDecoderWorker.EnqueuePacket(packet);
        }

        private void OnVideoPacketDecoded(VideoFrame frame)
        {
            _frame = frame;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            _droneClient.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _droneClient.Stop();
        }

        int T = 0;

        private void tmrVideoUpdate_Tick(object sender, EventArgs e)
        {
            if (_frame == null || _frameNumber == _frame.Number)
                return;
            _frameNumber = _frame.Number;

            if (_frameBitmap == null)
                _frameBitmap = VideoHelper.CreateBitmap(ref _frame);
            else
                VideoHelper.UpdateBitmap(ref _frameBitmap, ref _frame);

            if (T ==1)
            {
                //_tcpSender.SendVideoData(_frame);
                T = 0;
            }
            _pbVideo.Image = _frameBitmap;
        }

        private void tmrStateUpdate_Tick(object sender, EventArgs e)
        {
            _tvInfo.BeginUpdate();

            TreeNode node = _tvInfo.Nodes.GetOrCreate("ClientActive");
            node.Text = string.Format("Client Active: {0}", _droneClient.IsActive);

            node = _tvInfo.Nodes.GetOrCreate("Navigation Data");
            if (_navigationData != null) DumpBranch(node.Nodes, _navigationData);

            node = _tvInfo.Nodes.GetOrCreate("Configuration");
            if (_settings != null) DumpBranch(node.Nodes, _settings);

            TreeNode vativeNode = _tvInfo.Nodes.GetOrCreate("Native");
            TreeNode ultraSoundNode = _tvInfo.Nodes.GetOrCreate("UltraSound");

            NavdataBag navdataBag;
            if (_navigationPacket.Data != null && NavdataBagParser.TryParse(ref _navigationPacket, out navdataBag))
            {
                var ctrl_state = (CTRL_STATES) (navdataBag.demo.ctrl_state >> 0x10);
                node = vativeNode.Nodes.GetOrCreate("ctrl_state");
                node.Text = string.Format("Ctrl State: {0}", ctrl_state);

                var flying_state = (FLYING_STATES) (navdataBag.demo.ctrl_state & 0xffff);
                node = vativeNode.Nodes.GetOrCreate("flying_state");
                node.Text = string.Format("Ctrl State: {0}", flying_state);

                DumpBranch(vativeNode.Nodes, navdataBag);

                ShowUltraSound(ultraSoundNode, "front", null);
                ShowUltraSound(ultraSoundNode, "back", null);
                ShowUltraSound(ultraSoundNode, "left", null);
                ShowUltraSound(ultraSoundNode, "right", null);
            }
            _tvInfo.EndUpdate();
        }

        void ShowUltraSound(TreeNode father, String sonName, object value)
        {
            TreeNode son = father.Nodes.GetOrCreate(sonName);
            son.Text = son.Name + ":" + value;
        }

        private void DumpBranch(TreeNodeCollection nodes, object o)
        {
            Type type = o.GetType();

            foreach (FieldInfo fieldInfo in type.GetFields())
            {
                TreeNode node = nodes.GetOrCreate(fieldInfo.Name);
                object value = fieldInfo.GetValue(o);

                DumpValue(fieldInfo.FieldType, node, value);
            }

            foreach (PropertyInfo propertyInfo in type.GetProperties())
            {
                TreeNode node = nodes.GetOrCreate(propertyInfo.Name);
                object value = propertyInfo.GetValue(o, null);

                DumpValue(propertyInfo.PropertyType, node, value);
            }
        }

        private void DumpValue(Type type, TreeNode node, object value)
        {
            if (value == null)
                node.Text = node.Name + ": null";
            else
            {
                if (type.Namespace.StartsWith("System") || type.IsEnum)
                    node.Text = node.Name + ": " + value;
                else
                    DumpBranch(node.Nodes, value);
            }
        }

        private void btnFlatTrim_Click(object sender, EventArgs e)
        {
            _droneClient.FlatTrim();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _droneClient.Takeoff();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _droneClient.Land();
        }

        private void btnEmergency_Click(object sender, EventArgs e)
        {
            _droneClient.Emergency();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            _droneClient.ResetEmergency();
        }

        private void btnSwitchCam_Click(object sender, EventArgs e)
        {
            var configuration = new Settings();
            configuration.Video.Channel = VideoChannelType.Next;
            _droneClient.Send(configuration);
        }

        private void btnHover_Click(object sender, EventArgs e)
        {
            _droneClient.Hover();
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            _droneClient.Progress(FlightMode.Progressive, gaz: 0.25f);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            _droneClient.Progress(FlightMode.Progressive, gaz: -0.25f);
        }

        private void btnTurnLeft_Click(object sender, EventArgs e)
        {
            _droneClient.Progress(FlightMode.Progressive, yaw: 0.25f);
        }

        private void btnTurnRight_Click(object sender, EventArgs e)
        {
            _droneClient.Progress(FlightMode.Progressive, yaw: -0.25f);
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            _droneClient.Progress(FlightMode.Progressive, roll: -0.05f);
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            _droneClient.Progress(FlightMode.Progressive, roll: 0.05f);
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            _droneClient.Progress(FlightMode.Progressive, pitch: -0.05f);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            _droneClient.Progress(FlightMode.Progressive, pitch: 0.05f);
        }

        private void btnReadConfig_Click(object sender, EventArgs e)
        {
            Task<Settings> configurationTask = _droneClient.GetConfigurationTask();
            configurationTask.ContinueWith(delegate(Task<Settings> task)
                {
                    if (task.Exception != null)
                    {
                        Trace.TraceWarning("Get configuration task is faulted with exception: {0}", task.Exception.InnerException.Message);
                        return;
                    }

                    _settings = task.Result;
                });
            configurationTask.Start();
        }

        private void btnSendConfig_Click(object sender, EventArgs e)
        {
            var sendConfigTask = new Task(() =>
                {
                    if (_settings == null) _settings = new Settings();
                    Settings settings = _settings;

                    if (string.IsNullOrEmpty(settings.Custom.SessionId) ||
                        settings.Custom.SessionId == "00000000")
                    {
                        // set new session, application and profile
                        _droneClient.AckControlAndWaitForConfirmation(); // wait for the control confirmation

                        settings.Custom.SessionId = Settings.NewId();
                        _droneClient.Send(settings);

                        _droneClient.AckControlAndWaitForConfirmation();

                        settings.Custom.ProfileId = Settings.NewId();
                        _droneClient.Send(settings);

                        _droneClient.AckControlAndWaitForConfirmation();

                        settings.Custom.ApplicationId = Settings.NewId();
                        _droneClient.Send(settings);

                        _droneClient.AckControlAndWaitForConfirmation();
                    }

                    settings.General.NavdataDemo = false;
                    settings.General.NavdataOptions = NavdataOptions.All;

                    settings.Video.BitrateCtrlMode = VideoBitrateControlMode.Dynamic;
                    settings.Video.Bitrate = 1000;
                    settings.Video.MaxBitrate = 2000;

                    //settings.Leds.LedAnimation = new LedAnimation(LedAnimationType.BlinkGreenRed, 2.0f, 2);
                    //settings.Control.FlightAnimation = new FlightAnimation(FlightAnimationType.Wave);

                    // record video to usb
                    //settings.Video.OnUsb = true;
                    // usage of MP4_360P_H264_720P codec is a requirement for video recording to usb
                    //settings.Video.Codec = VideoCodecType.MP4_360P_H264_720P;
                    // start
                    //settings.Userbox.Command = new UserboxCommand(UserboxCommandType.Start);
                    // stop
                    //settings.Userbox.Command = new UserboxCommand(UserboxCommandType.Stop);


                    //send all changes in one pice
                    _droneClient.Send(settings);
                });
            sendConfigTask.Start();
        }

        private void StopRecording()
        {
            if (_packetRecorderWorker != null)
            {
                _packetRecorderWorker.Stop();
                _packetRecorderWorker.Join();
                _packetRecorderWorker = null;
            }
            if (_recorderStream != null)
            {
                _recorderStream.Dispose();
                _recorderStream = null;
            }
        }

        private void btnStartRecording_Click(object sender, EventArgs e)
        {
            string path = string.Format("flight_{0:yyyy_MM_dd_HH_mm}" + ARDroneTrackFileExt, DateTime.Now);

            using (var dialog = new SaveFileDialog { DefaultExt = ARDroneTrackFileExt, Filter = ARDroneTrackFilesFilter, FileName = path })
            {
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    StopRecording();

                    _recorderStream = new FileStream(dialog.FileName, FileMode.OpenOrCreate);
                    _packetRecorderWorker = new PacketRecorder(_recorderStream);
                    _packetRecorderWorker.Start();
                }
            }
        }

        private void btnStopRecording_Click(object sender, EventArgs e)
        {
            StopRecording();
        }

        private void btnReplay_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog { DefaultExt = ARDroneTrackFileExt, Filter = ARDroneTrackFilesFilter })
            {
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    StopRecording();

                    var playerForm = new PlayerForm { FileName = dialog.FileName };
                    playerForm.Closed += (o, args) => _playerForms.Remove(o as PlayerForm);
                    _playerForms.Add(playerForm);
                    playerForm.Show(this);
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            T = 1;
        }
    }
}