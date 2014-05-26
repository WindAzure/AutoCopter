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

        private int _imageHeight;
        private int _imageWidth;
        private bool _isPress = false;
        private Shape _tempShape = new MyDefLine();
        private PresentationModel _presentationModel = new PresentationModel();

        private IBarcodeReader reader = new BarcodeReader();

        void ShowInputImage()
        {
            if (_inputPictureBox.InvokeRequired)
            {
                InvokeShowImageData showData = new InvokeShowImageData(ShowInputImage);
                _inputPictureBox.Invoke(showData, new Object[] { });
            }
            else
            {
                _inputPictureBox.Image = _input.ToBitmap();

                Bitmap map = _inputPictureBox.Image as Bitmap;
                Graphics graphic = Graphics.FromImage(map);
                graphic.DrawLine(new Pen(Brushes.Red, 5), Convert.ToInt32(_tempShape.StartPoint.X), Convert.ToInt32(_tempShape.StartPoint.Y), Convert.ToInt32(_tempShape.EndPoint.X), Convert.ToInt32(_tempShape.EndPoint.Y));
            }
        }

        void ShowOutputImage()
        {
            if (_outputPictureBox.InvokeRequired)
            {
                InvokeShowImageData showData = new InvokeShowImageData(ShowOutputImage);
                _outputPictureBox.Invoke(showData, new Object[] { });
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
                    _output = _input.Convert<Gray, Byte>();
                    /*  CircleF[][] circles = _input.Convert<Gray, Byte>().HoughCircles(
                              new Gray(200),
                              new Gray(100),
                              1,
                              200,
                              0,
                              0
                          );

                      foreach (var circle in circles[0])
                      {
                          _input.Draw(circle, new Bgr(0, 0, 255), 5);
                       //   _output.Draw(circle,new Gray(0),3);
                      }*/

                    // _output = _output.Canny(100, 200);
                    /*  var result = reader.Decode(_input.ToBitmap());
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
                _tempShape = new MyDefLine();
                _capture = new Capture("rtsp://192.168.0.250/h264");
                _imageWidth = _capture.Width;
                _imageHeight = _capture.Height;

                ThreadStart start1 = new ThreadStart(ReadImage);
                _fectchImageThread = new Thread(start1);
                _fectchImageThread.Start();

                ThreadStart start2 = new ThreadStart(ProcessImage);
                _processImageThread = new Thread(start2);
                _processImageThread.Start();

                _presentationModel.InitializeStateButton();
                _startButton.Enabled = false;
            }
            catch (Exception)
            {
                MessageBox.Show("連結錯誤，請確認攝影機是否已連結");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*try
            {
                _capture = new Capture(0);
                double fps = _capture.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FPS);
                _movieTimer.Interval = Convert.ToInt16(1);
                _movieTimer.Tick += TickMovieTimer;
                _movieTimer.Start();
            }
            catch (Exception)
            {
                MessageBox.Show("連結錯誤，請確認攝影機是否已連結");
            }*/
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

        public Point ConvertRatioOfPictureBoxAndImage(Point point)
        {
            if (_inputPictureBox.Image == null)
            {
                return point;
            }
            double ratioX = Convert.ToDouble(_imageWidth) / Convert.ToDouble(_inputPictureBox.Width);
            double ratioY = Convert.ToDouble(_imageHeight) / Convert.ToDouble(_inputPictureBox.Height);
            return new Point(Convert.ToInt32(point.X * ratioX), Convert.ToInt32(point.Y * ratioY));
        }

        private void MouseDownInputBox(object sender, MouseEventArgs e)
        {
            _isPress = true;
            Point point = ConvertRatioOfPictureBoxAndImage(e.Location);
            _tempShape.SetStartPoint(point.X, point.Y);
            _tempShape.SetEndPoint(point.X, point.Y);
        }

        private void MouseMoveInputBox(object sender, MouseEventArgs e)
        {
            if (_isPress)
            {
                Point point = ConvertRatioOfPictureBoxAndImage(e.Location);
                _tempShape.SetEndPoint(point.X, point.Y);
            }
        }

        private void MouseUpInputBox(object sender, MouseEventArgs e)
        {
            if (e.Location.X < 0 || e.Location.X > _inputPictureBox.Width || e.Location.Y < 0 || e.Location.Y > _inputPictureBox.Height)
            {
                _isPress = false;
                _tempShape = new MyDefLine();
            }

            if (_isPress)
            {
                Point point = ConvertRatioOfPictureBoxAndImage(e.Location);
                _tempShape.SetEndPoint(point.X, point.Y);
                _isPress = false;
            }
        }

        private void ClickTable1StateChangeButton(object sender, EventArgs e)
        {
            _presentationModel.IsTable1StateChanged = true;
            _presentationModel.Index = 1;
        }

        private void ClickTable2StateChangeButton(object sender, EventArgs e)
        {
            _presentationModel.IsTable2StateChanged = true;
            _presentationModel.Index = 2;
        }

        private void ClickTable3StateChangeButton(object sender, EventArgs e)
        {
            _presentationModel.IsTable3StateChanged = true;
            _presentationModel.Index = 3;
        }

        private void ClickTable4StateChangeButton(object sender, EventArgs e)
        {
            _presentationModel.IsTable4StateChanged = true;
            _presentationModel.Index = 4;
        }
    }
}
