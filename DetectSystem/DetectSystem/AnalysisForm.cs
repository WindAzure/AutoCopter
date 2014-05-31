using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;

namespace DetectSystem
{
    public partial class AnalysisForm : Form
    {
        private Capture _capture = null;
        private Image<Bgr, Byte> _input = null;
        private Image<Gray, Byte> _output = null;
        private Thread _fectchImageThread = null;
        private Thread _processImageThread = null;
        public delegate void InvokeShowImageData();
        public delegate void InvokeShowTextData(String data);

        private PresentationModel _presentationModel = new PresentationModel();

        private IBarcodeReader reader = new BarcodeReader();

        void ShowInputImage()
        {
            if (_inputPictureBox.InvokeRequired)
            {
                InvokeShowImageData showData = new InvokeShowImageData(ShowInputImage);
                this.Invoke(showData, new Object[] { });
            }
            else
            {
                _inputPictureBox.Image = _input.ToBitmap();
                Bitmap map = _inputPictureBox.Image as Bitmap;
                Graphics graphic = Graphics.FromImage(map);
                graphic.DrawLine(new Pen(Brushes.Red, 5), Convert.ToInt32(_presentationModel.TempShape.StartPoint.X), Convert.ToInt32(_presentationModel.TempShape.StartPoint.Y), Convert.ToInt32(_presentationModel.TempShape.EndPoint.X), Convert.ToInt32(_presentationModel.TempShape.EndPoint.Y));

                for (int i = 1; i <= _presentationModel.DataModel.PointPer; i++)
                {
                    graphic.DrawLine(new Pen(Brushes.Red, 5), Convert.ToInt32(_presentationModel.DataModel.Polygons[i - 1].X), Convert.ToInt32(_presentationModel.DataModel.Polygons[i - 1].Y), Convert.ToInt32(_presentationModel.DataModel.Polygons[i].X), Convert.ToInt32(_presentationModel.DataModel.Polygons[i].Y));
                }
                if (_presentationModel.DataModel.PointPer == 3)
                {
                    graphic.DrawLine(new Pen(Brushes.Red, 5), Convert.ToInt32(_presentationModel.DataModel.Polygons[3].X), Convert.ToInt32(_presentationModel.DataModel.Polygons[3].Y), Convert.ToInt32(_presentationModel.DataModel.Polygons[0].X), Convert.ToInt32(_presentationModel.DataModel.Polygons[0].Y));
                }
            }
        }

        void ShowOutputImage()
        {
            if (_outputPictureBox.InvokeRequired)
            {
                InvokeShowImageData showData = new InvokeShowImageData(ShowOutputImage);
                this.Invoke(showData, new Object[] { });
            }
            else
            {
                _outputPictureBox.Image = _output.ToBitmap();
            }
        }

        void ShowData(String data)
        {
            if (_qrTextLabel.InvokeRequired)
            {
                InvokeShowTextData showData = new InvokeShowTextData(ShowData);
                this.Invoke(showData, new Object[] { data });
            }
            else
            {
                _qrTextLabel.Text = data;
            }
        }

        public void showImage(Image<Bgr, Byte> input, Image<Gray, Byte> output)
        {
            Adapter.Invoke(new SendOrPostCallback(o =>
            {
                _inputPictureBox.Image = input.ToBitmap();
                _outputPictureBox.Image = output.ToBitmap();
            }), null);
        }

        public void ReadImage()
        {
            while (true)
            {
                _input = _capture.QueryFrame();
            }
        }

        public void ProcessImage()
        {
            while (true)
            {
                if (_input != null)
                {
                    //_output = _input.Convert<Ycc, Byte>().InRange(new Ycc(0, 0, 0), new Ycc(255, 110, 130)); //green
                    _output = _input.Convert<Ycc, Byte>().InRange(new Ycc(0, 170, 0), new Ycc(110, 255, 150)); //red
                        // _output = new Image<Ycc, byte>(_presentationModel.InputPictureBoxImageWidth,_presentationModel.InputPictureBoxImageHeight,new Ycc(0, 192, 0));
                    /*      var result = reader.Decode(_input.ToBitmap());
                         ShowData("");
                         if (result != null)
                         {
                             ShowData(result.Text);
                         }*/
                    ShowInputImage();
                    ShowOutputImage();
                }
            }
        }

        private void ReadQR()
        {
            while (true)
            {
                if (_input != null)
                {
                    var result = reader.Decode(_input.ToBitmap());
                    ShowData("");
                    if (result != null)
                    {
                        ShowData(result.Text);
                    }
                }
            }
        }

        public AnalysisForm()
        {
            InitializeComponent();
            _presentationModel._presentationModelChanged += PresentationModelStateChangeButtonEnableChanged;
            _presentationModel.InputPictureBoxWidth = _inputPictureBox.Width;
            _presentationModel.InputPictureBoxHeight = _inputPictureBox.Height;
        }

        void PresentationModelStateChangeButtonEnableChanged()
        {
            _table1StateChangeButton.Enabled = _presentationModel.Table1StateChangeButtonEnable;
            _table2StateChangeButton.Enabled = _presentationModel.Table2StateChangeButtonEnable;
            _table3StateChangeButton.Enabled = _presentationModel.Table3StateChangeButtonEnable;
            _table4StateChangeButton.Enabled = _presentationModel.Table4StateChangeButtonEnable;
        }

        private void ClickStartButton(object sender, EventArgs e)
        {
            try
            {
                Adapter.Initialize();
                _presentationModel.TempShape = new MyDefLine();
                _capture = new Capture("rtsp://192.168.0.250/h264");
                _presentationModel.InputPictureBoxImageWidth = _capture.Width;
                _presentationModel.InputPictureBoxImageHeight = _capture.Height;

                ThreadStart start1 = new ThreadStart(ReadImage);
                _fectchImageThread = new Thread(start1);
                _fectchImageThread.Start();

                ThreadStart start2 = new ThreadStart(ProcessImage);
                _processImageThread = new Thread(start2);
                _processImageThread.Start();
                  _startButton.Enabled = false;
            }
            catch (Exception)
            {
                MessageBox.Show("連結錯誤，請確認攝影機是否已連結");
            }
        }

        private void ClosingAnalysisForm(object sender, FormClosingEventArgs e)
        {
            if (_fectchImageThread != null)
            {
                _fectchImageThread.Abort();
            }

            if (_processImageThread != null)
            {
                _processImageThread.Abort();
            }
        }

        private void MouseDownInputBox(object sender, MouseEventArgs e)
        {
            if (_inputPictureBox.Image != null)
            {
                _presentationModel.MouseDownInputBox(e.Location);
            }
        }

        private void MouseMoveInputBox(object sender, MouseEventArgs e)
        {
            if (_inputPictureBox.Image != null)
            {
                _presentationModel.MouseMoveInputBox(e.Location);
            }
        }

        private void MouseUpInputBox(object sender, MouseEventArgs e)
        {
            if (_inputPictureBox.Image != null)
            {
                _presentationModel.MouseUpInputBox(e.Location);
            }
        }

        private void ClickTable1StateChangeButton(object sender, EventArgs e)
        {
            _presentationModel.IsTable1StateChanged = true;
            _table1AreaLabel.Text = _presentationModel.CalSelectRangeArea();
        }

        private void ClickTable2StateChangeButton(object sender, EventArgs e)
        {
            _presentationModel.IsTable2StateChanged = true;
            _table2AreaLabel.Text = _presentationModel.CalSelectRangeArea();
        }

        private void ClickTable3StateChangeButton(object sender, EventArgs e)
        {
            _presentationModel.IsTable3StateChanged = true;
            _table3AreaLabel.Text = _presentationModel.CalSelectRangeArea();
        }

        private void ClickTable4StateChangeButton(object sender, EventArgs e)
        {
            _presentationModel.IsTable4StateChanged = true;
            _table4AreaLabel.Text = _presentationModel.CalSelectRangeArea();
        }
    }
}
