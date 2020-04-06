using System;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

//CodeProjectSerialComms program 
//23/04/2013   16:29

namespace CodeProjectSerialComms
{
    public partial class Form1 : Form
    {
        SerialPort ComPort = new SerialPort();
        private ConstantVelocityProcess process;
        private Kalman kalman;
        private Trilateration trilateration;
        internal delegate void SerialDataReceivedEventHandlerDelegate(object sender, SerialDataReceivedEventArgs e);
        internal delegate void SerialPinChangedEventHandlerDelegate(object sender, SerialPinChangedEventArgs e);
        private SerialPinChangedEventHandler SerialPinChangedEventHandler1;
        delegate void SetTextCallback(string text);
        string InputData = String.Empty;

        public Form1()
        {
            InitializeComponent();
            initializeChart();

            trilateration = new Trilateration();
            process = new ConstantVelocityProcess();
            initializeKalman();

            SerialPinChangedEventHandler1 = new SerialPinChangedEventHandler(PinChanged);
            ComPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(port_DataReceived_1);
        }

        private void initializeKalman()
        {
            float processNoise = (float)numProcessNoise.Value;
            float measurementNoise = (float)numMeasurementNoise.Value;

            kalman = new Kalman();
            kalman.VarDistance = measurementNoise;
            kalman.VarProcess = processNoise;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            float accelNoise = (float)numProcessNoise.Value;
            float measurementNoise = (float)numMeasurementNoise.Value;

            /**************************************** get data *******************************************/
            var processPosition = process.GetNoisyState(accelNoise).Position;

            bool measurementExist;
            var measurementPosition = process.TryGetNoisyMeasurement(measurementNoise, out measurementExist);
            var measurement = trilateration.GetDistancesGivenPosition(new Vector(measurementPosition.X, measurementPosition.Y),
                                                                      new Vector(Beacon1PositionTextBox.Text),
                                                                      new Vector(Beacon2PositionTextBox.Text),
                                                                      new Vector(Beacon3PositionTextBox.Text));
            Vector correctedPosition = default(Vector);
            if (measurementExist)
            {
                var v = kalman.Filter(measurement.ToFloatArray());
                correctedPosition = trilateration.GetIntersectionPoint(Beacon1PositionTextBox.Text, Beacon2PositionTextBox.Text, Beacon3PositionTextBox.Text, v.ToString());

                if (!correctedPosition.valid)
                    measurementExist = false;
            }
            /**************************************** get data *******************************************/

            //plot process state (what we do not know)
            plotData(processPosition, 0);
            //try plot measuremnt (what we see)
            if (measurementExist)
            {
                plotData(new Vector(measurementPosition.X, measurementPosition.Y), 1);
                //plot corrected state (Kalman)
                plotData(correctedPosition, 2, new DataPoint { BorderWidth = 5 });
            }

            bool doneFullCycle;
            process.GoToNextState(out doneFullCycle);

            if (doneFullCycle) clearChart();
        }

        #region GUI

        private void AlternateState_CheckedChanged(object sender, EventArgs e)
        {
            if (this.AlternateStateCheckBox.Checked == true)
            {
                enableOrDisableGeneratedReadingsButtons(false);
                removeGeneratedReadingsButtons();
                addSerialCommsButtons();
                enableOrDisableSerialCommsButtons(true);
            }
            else
            {
                enableOrDisableSerialCommsButtons(false);
                removeSerialCommsButtons();
                addGeneratedReadingsButtons();
                enableOrDisableGeneratedReadingsButtons(true);
            }
        }

        #region Real Readings GUI

        private void btnGetSerialPorts_Click(object sender, EventArgs e)
        {
            string[] ArrayComPortsNames = null;
            int index = -1;
            string ComPortName = null;

            //Com Ports
            ArrayComPortsNames = SerialPort.GetPortNames();
            do
            {
                index += 1;
                cboPorts.Items.Add(ArrayComPortsNames[index]);


            } while (!((ArrayComPortsNames[index] == ComPortName) || (index == ArrayComPortsNames.GetUpperBound(0))));
            Array.Sort(ArrayComPortsNames);

            if (index == ArrayComPortsNames.GetUpperBound(0))
            {
                ComPortName = ArrayComPortsNames[0];
            }
            //get first item print in text
            cboPorts.Text = ArrayComPortsNames[0];
            //Baud Rate

            cboBaudRate.Items.Add(115200);
            cboBaudRate.Items.ToString();
            //get first item print in text
            cboBaudRate.Text = cboBaudRate.Items[0].ToString();
            //Data Bits
            cboDataBits.Items.Add(7);
            cboDataBits.Items.Add(8);
            //get the first item print it in the text 
            cboDataBits.Text = cboDataBits.Items[0].ToString();

            //Stop Bits
            cboStopBits.Items.Add("One");
            cboStopBits.Items.Add("OnePointFive");
            cboStopBits.Items.Add("Two");
            //get the first item print in the text
            cboStopBits.Text = cboStopBits.Items[0].ToString();
            //Parity 
            cboParity.Items.Add("None");
            cboParity.Items.Add("Even");
            cboParity.Items.Add("Mark");
            cboParity.Items.Add("Odd");
            cboParity.Items.Add("Space");
            //get the first item print in the text
            cboParity.Text = cboParity.Items[0].ToString();
            //Handshake
            cboHandShaking.Items.Add("None");
            cboHandShaking.Items.Add("XOnXOff");
            cboHandShaking.Items.Add("RequestToSend");
            cboHandShaking.Items.Add("RequestToSendXOnXOff");
            //get the first item print it in the text 
            cboHandShaking.Text = cboHandShaking.Items[0].ToString();

        }

        private void port_DataReceived_1(object sender, SerialDataReceivedEventArgs e)
        {
            InputData = ComPort.ReadLine();

            if (InputData != String.Empty)
            {
                //this.BeginInvoke(new SetTextCallback(SetText), new object[] { InputData });
                this.BeginInvoke(new SetTextCallback(trialetrate), new object[] { InputData });
            }
        }

        //private void SetText(string text)
        //{
        //    this.rtbIncoming.Text += text;
        //}

        private void trialetrate(string text)
        {
            var positions = trilateration.GetIntersectionPoint("0,0,0", "200,0,0", "0,0,200", text);

            if (!positions.valid)
            {
                this.rtbOutgoing.Text = "NaN";
            }
            else
            {
                this.rtbOutgoing.Text = positions.ToString();
            }
        }

        internal void PinChanged(object sender, SerialPinChangedEventArgs e)
        {
            SerialPinChange SerialPinChange1 = 0;
            bool signalState = false;

            SerialPinChange1 = e.EventType;
            lblCTSStatus.BackColor = Color.Green;
            lblDSRStatus.BackColor = Color.Green;
            lblRIStatus.BackColor = Color.Green;
            lblBreakStatus.BackColor = Color.Green;
            switch (SerialPinChange1)
            {
                case SerialPinChange.Break:
                    lblBreakStatus.BackColor = Color.Red;
                    //MessageBox.Show("Break is Set");
                    break;
                case SerialPinChange.CDChanged:
                    signalState = ComPort.CtsHolding;
                    //  MessageBox.Show("CD = " + signalState.ToString());
                    break;
                case SerialPinChange.CtsChanged:
                    signalState = ComPort.CDHolding;
                    lblCTSStatus.BackColor = Color.Red;
                    //MessageBox.Show("CTS = " + signalState.ToString());
                    break;
                case SerialPinChange.DsrChanged:
                    signalState = ComPort.DsrHolding;
                    lblDSRStatus.BackColor = Color.Red;
                    // MessageBox.Show("DSR = " + signalState.ToString());
                    break;
                case SerialPinChange.Ring:
                    lblRIStatus.BackColor = Color.Red;
                    //MessageBox.Show("Ring Detected");
                    break;
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            //SerialPinChangedEventHandler1 = new SerialPinChangedEventHandler(PinChanged);
            //ComPort.PinChanged += SerialPinChangedEventHandler1;
            //ComPort.Open();

            //ComPort.RtsEnable = true;
            //ComPort.DtrEnable = true;
            //btnTest.Enabled = false;

        }

        private void btnPortState_Click(object sender, EventArgs e)
        {
            if (btnPortState.Text == "Closed")
            {
                btnPortState.Text = "Open";
                ComPort.PortName = Convert.ToString(cboPorts.Text);
                ComPort.BaudRate = Convert.ToInt32(cboBaudRate.Text);
                ComPort.DataBits = Convert.ToInt16(cboDataBits.Text);
                ComPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cboStopBits.Text);
                ComPort.Handshake = (Handshake)Enum.Parse(typeof(Handshake), cboHandShaking.Text);
                ComPort.Parity = (Parity)Enum.Parse(typeof(Parity), cboParity.Text);
                ComPort.Open();
            }
            else if (btnPortState.Text == "Open")
            {
                btnPortState.Text = "Closed";
                ComPort.Close();
            }
        }

        private void rtbOutgoing_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13) // enter key  
            {
                ComPort.Write("\r\n");
                rtbOutgoing.Text = "";
            }
            else if (e.KeyChar < 32 || e.KeyChar > 126)
            {
                e.Handled = true; // ignores anything else outside printable ASCII range  
            }
            else
            {
                ComPort.Write(e.KeyChar.ToString());
            }
        }

        private void btnHello_Click(object sender, EventArgs e)
        {
            ComPort.Write("Hello World!");
        }

        private void btnHyperTerm_Click(object sender, EventArgs e)
        {
            string Command1 = txtCommand.Text;
            string CommandSent;
            int Length, j = 0;

            Length = Command1.Length;

            for (int i = 0; i < Length; i++)
            {
                CommandSent = Command1.Substring(j, 1);
                ComPort.Write(CommandSent);
                j++;
            }
        }

        private void addSerialCommsButtons()
        {
            this.Controls.Add(this.txtCommand);
            this.Controls.Add(this.btnHyperTerm);
            this.Controls.Add(this.rtbOutgoing);
            this.Controls.Add(this.btnHello);
            this.Controls.Add(this.btnPortState);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.lblRIStatus);
            this.Controls.Add(this.lblDSRStatus);
            this.Controls.Add(this.lblCTSStatus);
            this.Controls.Add(this.lblBreakStatus);
            this.Controls.Add(this.cboHandShaking);
            this.Controls.Add(this.cboParity);
            this.Controls.Add(this.cboStopBits);
            this.Controls.Add(this.cboDataBits);
            this.Controls.Add(this.cboBaudRate);
            this.Controls.Add(this.cboPorts);
            this.Controls.Add(this.btnGetSerialPorts);
        }

        private void removeSerialCommsButtons()
        {
            this.Controls.Remove(this.txtCommand);
            this.Controls.Remove(this.btnHyperTerm);
            this.Controls.Remove(this.rtbOutgoing);
            this.Controls.Remove(this.btnHello);
            this.Controls.Remove(this.btnPortState);
            this.Controls.Remove(this.btnTest);
            this.Controls.Remove(this.lblRIStatus);
            this.Controls.Remove(this.lblDSRStatus);
            this.Controls.Remove(this.lblCTSStatus);
            this.Controls.Remove(this.lblBreakStatus);
            this.Controls.Remove(this.cboHandShaking);
            this.Controls.Remove(this.cboParity);
            this.Controls.Remove(this.cboStopBits);
            this.Controls.Remove(this.cboDataBits);
            this.Controls.Remove(this.cboBaudRate);
            this.Controls.Remove(this.cboPorts);
            this.Controls.Remove(this.btnGetSerialPorts);
        }

        private void enableOrDisableSerialCommsButtons(bool state)
        {
            this.txtCommand.Visible = state;
            this.txtCommand.Enabled = state;
            this.btnHyperTerm.Visible = state;
            this.btnHyperTerm.Enabled = state;
            this.rtbOutgoing.Visible = state;
            this.rtbOutgoing.Enabled = state;
            this.btnHello.Visible = state;
            this.btnHello.Enabled = state;
            this.btnPortState.Visible = state;
            this.btnPortState.Enabled = state;
            this.btnTest.Visible = state;
            this.btnTest.Enabled = state;
            this.lblRIStatus.Visible = state;
            this.lblRIStatus.Enabled = state;
            this.lblDSRStatus.Visible = state;
            this.lblDSRStatus.Enabled = state;
            this.lblCTSStatus.Visible = state;
            this.lblCTSStatus.Enabled = state;
            this.lblBreakStatus.Visible = state;
            this.lblBreakStatus.Enabled = state;
            this.cboHandShaking.Visible = state;
            this.cboHandShaking.Enabled = state;
            this.cboParity.Visible = state;
            this.cboParity.Enabled = state;
            this.cboStopBits.Visible = state;
            this.cboStopBits.Enabled = state;
            this.cboDataBits.Visible = state;
            this.cboDataBits.Enabled = state;
            this.cboBaudRate.Visible = state;
            this.cboBaudRate.Enabled = state;
            this.cboPorts.Visible = state;
            this.cboPorts.Enabled = state;
            this.btnGetSerialPorts.Visible = state;
            this.btnGetSerialPorts.Enabled = state;
        }

        #endregion// end Real Readings GUI

        #region Generated Readings GUI

        private void addGeneratedReadingsButtons()
        {
            this.Controls.Add(this.DisatancesSeedGroupBox);
            this.Controls.Add(this.MeasurementNoiseGroupBox);
            this.Controls.Add(this.ProcessNosieGroupBox);
            this.Controls.Add(this.StopOrResumeButton);
            this.Controls.Add(this.Beacon3Label);
            this.Controls.Add(this.Beacon2Label);
            this.Controls.Add(this.Beacon1Label);
            this.Controls.Add(this.BeaconPositionsGroupBox);
            this.Controls.Add(this.ClearChartButton);
        }

        private void removeGeneratedReadingsButtons()
        {
            this.Controls.Remove(this.DisatancesSeedGroupBox);
            this.Controls.Remove(this.MeasurementNoiseGroupBox);
            this.Controls.Remove(this.ProcessNosieGroupBox);
            this.Controls.Remove(this.StopOrResumeButton);
            this.Controls.Remove(this.Beacon3Label);
            this.Controls.Remove(this.Beacon2Label);
            this.Controls.Remove(this.Beacon1Label);
            this.Controls.Remove(this.BeaconPositionsGroupBox);
            this.Controls.Remove(this.ClearChartButton);
        }

        private void enableOrDisableGeneratedReadingsButtons(bool state)
        {
            this.DisatancesSeedGroupBox.Visible     = state;
            this.DisatancesSeedGroupBox.Enabled     = state;
            this.MeasurementNoiseGroupBox.Visible   = state;
            this.MeasurementNoiseGroupBox.Enabled   = state;
            this.ProcessNosieGroupBox.Visible       = state;
            this.ProcessNosieGroupBox.Enabled       = state;
            this.StopOrResumeButton.Visible         = state;
            this.StopOrResumeButton.Enabled         = state;
            this.Beacon1Label.Visible               = state;
            this.Beacon1Label.Enabled               = state;
            this.Beacon2Label.Visible               = state;
            this.Beacon2Label.Enabled               = state;
            this.Beacon3Label.Visible               = state;
            this.Beacon3Label.Enabled               = state;
            this.BeaconPositionsGroupBox.Visible    = state;
            this.BeaconPositionsGroupBox.Enabled    = state;
            this.ClearChartButton.Visible           = state;
            this.ClearChartButton.Enabled           = state;

            if (state)
            {
                foreach(Control con in BeaconPositionsGroupBox.Controls)
                {
                    if (con is TextBox)
                    {
                        var tBox = con as TextBox;
                        tBox.Text = Vector.zero.ToString();
                    }
                }

                foreach (Control con in DisatancesSeedGroupBox.Controls)
                {
                    if (con is TextBox)
                    {
                        var tBox = con as TextBox;
                        tBox.Text = Vector.zero.ToString();
                    }
                }
            }
        }

        private void StopOrResumeButton_Click(object sender, EventArgs e)
        {
            timer.Enabled = !timer.Enabled;
        }

        private void num_ValueChanged(object sender, EventArgs e)
        {
            initializeKalman();
        }

        private void ClearChartButton_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;
            clearChart();
            process = new ConstantVelocityProcess();
            initializeKalman();
        }

        #endregion// end Generated Readings GUI

        #region Chart

        private void clearChart()
        {
            foreach (var series in chart.Series)
            {
                series.Points.Clear();
            }
        }

        private void initializeChart()
        {
            const int BORDER_OFFSET = 5;

            chart.ChartAreas[0].AxisX.Title = "X";
            chart.ChartAreas[0].AxisY.Title = "Y";

            chart.ChartAreas[0].AxisX.Minimum = -BORDER_OFFSET;
            chart.ChartAreas[0].AxisY.Minimum = -BORDER_OFFSET;
            chart.ChartAreas[0].AxisX.Maximum = ConstantVelocityProcess.WorkingArea.Width + BORDER_OFFSET;
            chart.ChartAreas[0].AxisY.Maximum = ConstantVelocityProcess.WorkingArea.Height + BORDER_OFFSET;

            chart.Series.Clear();

            //process data
            var s = chart.Series.Add("Process noise");
            s.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            s.Color = Color.Red;

            //measurement data
            s = chart.Series.Add("Measurement noise");
            s.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            s.Color = Color.Black;

            //kalman data
            s = chart.Series.Add("Kalman corrected");
            s.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            s.Color = Color.Green;
        }

        private void tryRemoveLastMarker(int seriesIdx, DataPoint ordinaryDataPtStyle = null)
        {
            ordinaryDataPtStyle = (ordinaryDataPtStyle != null) ? ordinaryDataPtStyle : new DataPoint();
            Series s = chart.Series[seriesIdx];

            if (s.Points.Count > 0)
            {
                var lastPt = s.Points.Last();
                s.Points.Remove(lastPt);

                ordinaryDataPtStyle.XValue = lastPt.XValue;
                ordinaryDataPtStyle.YValues = lastPt.YValues;
                s.Points.Add(ordinaryDataPtStyle);
            }
        }

        private void addMarker(int seriesIdx, DataPoint markerStyle = null)
        {
            Series s = chart.Series[seriesIdx];

            markerStyle = (markerStyle != null) ? markerStyle : new DataPoint
            {
                MarkerColor = s.Color,
                MarkerSize = 15,
                MarkerStyle = MarkerStyle.Diamond
            };

            //remove last marker
            if (s.Points.Count > 0)
            {
                var lastPt = s.Points.Last();
                s.Points.RemoveAt(s.Points.Count - 1);

                markerStyle.XValue = lastPt.XValue;
                markerStyle.YValues = lastPt.YValues;
                s.Points.Add(markerStyle);
            }
        }

        private void plotData(Vector point, int seriesIdx, DataPoint dataPointStyle = null, bool markLastPt = true)
        {
            Series s = chart.Series[seriesIdx];

            dataPointStyle = dataPointStyle ?? new DataPoint();

            if (markLastPt)
                tryRemoveLastMarker(seriesIdx, dataPointStyle);

            var dataPt = dataPointStyle.Clone();
            dataPt.XValue = point.x;
            dataPt.YValues = new double[] { point.y };

            s.Points.Add(dataPt);

            if (markLastPt)
                addMarker(seriesIdx);
        }

        #endregion// chart

        #endregion// GUI
    }
}
