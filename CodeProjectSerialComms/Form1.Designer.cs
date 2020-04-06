namespace CodeProjectSerialComms
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.btnGetSerialPorts = new System.Windows.Forms.Button();
            this.cboPorts = new System.Windows.Forms.ComboBox();
            this.cboBaudRate = new System.Windows.Forms.ComboBox();
            this.cboDataBits = new System.Windows.Forms.ComboBox();
            this.cboStopBits = new System.Windows.Forms.ComboBox();
            this.cboParity = new System.Windows.Forms.ComboBox();
            this.cboHandShaking = new System.Windows.Forms.ComboBox();
            this.lblBreakStatus = new System.Windows.Forms.Label();
            this.lblCTSStatus = new System.Windows.Forms.Label();
            this.lblDSRStatus = new System.Windows.Forms.Label();
            this.lblRIStatus = new System.Windows.Forms.Label();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnPortState = new System.Windows.Forms.Button();
            this.btnHello = new System.Windows.Forms.Button();
            this.rtbOutgoing = new System.Windows.Forms.RichTextBox();
            this.btnHyperTerm = new System.Windows.Forms.Button();
            this.txtCommand = new System.Windows.Forms.TextBox();
            this.AlternateStateCheckBox = new System.Windows.Forms.CheckBox();
            this.StopOrResumeButton = new System.Windows.Forms.Button();
            this.numProcessNoise = new System.Windows.Forms.NumericUpDown();
            this.ProcessNosieGroupBox = new System.Windows.Forms.GroupBox();
            this.MeasurementNoiseGroupBox = new System.Windows.Forms.GroupBox();
            this.numMeasurementNoise = new System.Windows.Forms.NumericUpDown();
            this.DisatancesSeedGroupBox = new System.Windows.Forms.GroupBox();
            this.PositionSeed = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.BeaconPositionsGroupBox = new System.Windows.Forms.GroupBox();
            this.Beacon3PositionTextBox = new System.Windows.Forms.TextBox();
            this.Beacon2PositionTextBox = new System.Windows.Forms.TextBox();
            this.Beacon1PositionTextBox = new System.Windows.Forms.TextBox();
            this.Beacon1Label = new System.Windows.Forms.Label();
            this.Beacon2Label = new System.Windows.Forms.Label();
            this.Beacon3Label = new System.Windows.Forms.Label();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.ClearChartButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numProcessNoise)).BeginInit();
            this.ProcessNosieGroupBox.SuspendLayout();
            this.MeasurementNoiseGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMeasurementNoise)).BeginInit();
            this.DisatancesSeedGroupBox.SuspendLayout();
            this.BeaconPositionsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGetSerialPorts
            // 
            this.btnGetSerialPorts.Enabled = false;
            this.btnGetSerialPorts.Location = new System.Drawing.Point(12, 27);
            this.btnGetSerialPorts.Name = "btnGetSerialPorts";
            this.btnGetSerialPorts.Size = new System.Drawing.Size(75, 23);
            this.btnGetSerialPorts.TabIndex = 0;
            this.btnGetSerialPorts.Text = "Ports";
            this.btnGetSerialPorts.UseVisualStyleBackColor = true;
            this.btnGetSerialPorts.Visible = false;
            this.btnGetSerialPorts.Click += new System.EventHandler(this.btnGetSerialPorts_Click);
            // 
            // cboPorts
            // 
            this.cboPorts.Enabled = false;
            this.cboPorts.FormattingEnabled = true;
            this.cboPorts.Location = new System.Drawing.Point(102, 29);
            this.cboPorts.Name = "cboPorts";
            this.cboPorts.Size = new System.Drawing.Size(121, 21);
            this.cboPorts.TabIndex = 2;
            this.cboPorts.Visible = false;
            // 
            // cboBaudRate
            // 
            this.cboBaudRate.Enabled = false;
            this.cboBaudRate.FormattingEnabled = true;
            this.cboBaudRate.Location = new System.Drawing.Point(102, 56);
            this.cboBaudRate.Name = "cboBaudRate";
            this.cboBaudRate.Size = new System.Drawing.Size(121, 21);
            this.cboBaudRate.TabIndex = 3;
            this.cboBaudRate.Visible = false;
            // 
            // cboDataBits
            // 
            this.cboDataBits.Enabled = false;
            this.cboDataBits.FormattingEnabled = true;
            this.cboDataBits.Location = new System.Drawing.Point(102, 83);
            this.cboDataBits.Name = "cboDataBits";
            this.cboDataBits.Size = new System.Drawing.Size(121, 21);
            this.cboDataBits.TabIndex = 4;
            this.cboDataBits.Visible = false;
            // 
            // cboStopBits
            // 
            this.cboStopBits.Enabled = false;
            this.cboStopBits.FormattingEnabled = true;
            this.cboStopBits.Location = new System.Drawing.Point(102, 110);
            this.cboStopBits.Name = "cboStopBits";
            this.cboStopBits.Size = new System.Drawing.Size(121, 21);
            this.cboStopBits.TabIndex = 5;
            this.cboStopBits.Visible = false;
            // 
            // cboParity
            // 
            this.cboParity.Enabled = false;
            this.cboParity.FormattingEnabled = true;
            this.cboParity.Location = new System.Drawing.Point(102, 137);
            this.cboParity.Name = "cboParity";
            this.cboParity.Size = new System.Drawing.Size(121, 21);
            this.cboParity.TabIndex = 6;
            this.cboParity.Visible = false;
            // 
            // cboHandShaking
            // 
            this.cboHandShaking.Enabled = false;
            this.cboHandShaking.FormattingEnabled = true;
            this.cboHandShaking.Location = new System.Drawing.Point(102, 164);
            this.cboHandShaking.Name = "cboHandShaking";
            this.cboHandShaking.Size = new System.Drawing.Size(121, 21);
            this.cboHandShaking.TabIndex = 7;
            this.cboHandShaking.Visible = false;
            // 
            // lblBreakStatus
            // 
            this.lblBreakStatus.AutoSize = true;
            this.lblBreakStatus.Location = new System.Drawing.Point(24, 206);
            this.lblBreakStatus.Name = "lblBreakStatus";
            this.lblBreakStatus.Size = new System.Drawing.Size(35, 13);
            this.lblBreakStatus.TabIndex = 8;
            this.lblBreakStatus.Text = "Break";
            this.lblBreakStatus.Visible = false;
            // 
            // lblCTSStatus
            // 
            this.lblCTSStatus.AutoSize = true;
            this.lblCTSStatus.Location = new System.Drawing.Point(79, 206);
            this.lblCTSStatus.Name = "lblCTSStatus";
            this.lblCTSStatus.Size = new System.Drawing.Size(28, 13);
            this.lblCTSStatus.TabIndex = 9;
            this.lblCTSStatus.Text = "CTS";
            this.lblCTSStatus.Visible = false;
            // 
            // lblDSRStatus
            // 
            this.lblDSRStatus.AutoSize = true;
            this.lblDSRStatus.Location = new System.Drawing.Point(130, 206);
            this.lblDSRStatus.Name = "lblDSRStatus";
            this.lblDSRStatus.Size = new System.Drawing.Size(30, 13);
            this.lblDSRStatus.TabIndex = 10;
            this.lblDSRStatus.Text = "DSR";
            this.lblDSRStatus.Visible = false;
            // 
            // lblRIStatus
            // 
            this.lblRIStatus.AutoSize = true;
            this.lblRIStatus.Location = new System.Drawing.Point(188, 206);
            this.lblRIStatus.Name = "lblRIStatus";
            this.lblRIStatus.Size = new System.Drawing.Size(18, 13);
            this.lblRIStatus.TabIndex = 11;
            this.lblRIStatus.Text = "RI";
            this.lblRIStatus.Visible = false;
            // 
            // btnTest
            // 
            this.btnTest.Enabled = false;
            this.btnTest.Location = new System.Drawing.Point(12, 180);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 12;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Visible = false;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnPortState
            // 
            this.btnPortState.Enabled = false;
            this.btnPortState.Location = new System.Drawing.Point(12, 56);
            this.btnPortState.Name = "btnPortState";
            this.btnPortState.Size = new System.Drawing.Size(75, 23);
            this.btnPortState.TabIndex = 13;
            this.btnPortState.Text = "Closed";
            this.btnPortState.UseVisualStyleBackColor = true;
            this.btnPortState.Visible = false;
            this.btnPortState.Click += new System.EventHandler(this.btnPortState_Click);
            // 
            // btnHello
            // 
            this.btnHello.Enabled = false;
            this.btnHello.Location = new System.Drawing.Point(12, 85);
            this.btnHello.Name = "btnHello";
            this.btnHello.Size = new System.Drawing.Size(75, 23);
            this.btnHello.TabIndex = 14;
            this.btnHello.Text = "Send Hello";
            this.btnHello.UseVisualStyleBackColor = true;
            this.btnHello.Visible = false;
            this.btnHello.Click += new System.EventHandler(this.btnHello_Click);
            // 
            // rtbOutgoing
            // 
            this.rtbOutgoing.Enabled = false;
            this.rtbOutgoing.Location = new System.Drawing.Point(39, 231);
            this.rtbOutgoing.Name = "rtbOutgoing";
            this.rtbOutgoing.Size = new System.Drawing.Size(214, 31);
            this.rtbOutgoing.TabIndex = 15;
            this.rtbOutgoing.Text = "";
            this.rtbOutgoing.Visible = false;
            this.rtbOutgoing.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rtbOutgoing_KeyPress);
            // 
            // btnHyperTerm
            // 
            this.btnHyperTerm.Enabled = false;
            this.btnHyperTerm.Location = new System.Drawing.Point(265, 81);
            this.btnHyperTerm.Name = "btnHyperTerm";
            this.btnHyperTerm.Size = new System.Drawing.Size(75, 23);
            this.btnHyperTerm.TabIndex = 16;
            this.btnHyperTerm.Text = "HyperTerm";
            this.btnHyperTerm.UseVisualStyleBackColor = true;
            this.btnHyperTerm.Visible = false;
            this.btnHyperTerm.Click += new System.EventHandler(this.btnHyperTerm_Click);
            // 
            // txtCommand
            // 
            this.txtCommand.Enabled = false;
            this.txtCommand.Location = new System.Drawing.Point(252, 117);
            this.txtCommand.Name = "txtCommand";
            this.txtCommand.Size = new System.Drawing.Size(100, 20);
            this.txtCommand.TabIndex = 17;
            this.txtCommand.Visible = false;
            // 
            // AlternateStateCheckBox
            // 
            this.AlternateStateCheckBox.AutoSize = true;
            this.AlternateStateCheckBox.Location = new System.Drawing.Point(239, 27);
            this.AlternateStateCheckBox.Name = "AlternateStateCheckBox";
            this.AlternateStateCheckBox.Size = new System.Drawing.Size(96, 17);
            this.AlternateStateCheckBox.TabIndex = 18;
            this.AlternateStateCheckBox.Text = "Alternate State";
            this.AlternateStateCheckBox.UseVisualStyleBackColor = true;
            this.AlternateStateCheckBox.CheckedChanged += new System.EventHandler(this.AlternateState_CheckedChanged);
            // 
            // StopOrResumeButton
            // 
            this.StopOrResumeButton.Location = new System.Drawing.Point(12, 27);
            this.StopOrResumeButton.Name = "StopOrResumeButton";
            this.StopOrResumeButton.Size = new System.Drawing.Size(98, 23);
            this.StopOrResumeButton.TabIndex = 19;
            this.StopOrResumeButton.Text = "Stop / Resume";
            this.StopOrResumeButton.UseVisualStyleBackColor = true;
            this.StopOrResumeButton.Click += new System.EventHandler(this.StopOrResumeButton_Click);
            // 
            // numProcessNoise
            // 
            this.numProcessNoise.DecimalPlaces = 1;
            this.numProcessNoise.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numProcessNoise.Location = new System.Drawing.Point(6, 19);
            this.numProcessNoise.Name = "numProcessNoise";
            this.numProcessNoise.Size = new System.Drawing.Size(95, 20);
            this.numProcessNoise.TabIndex = 20;
            this.numProcessNoise.Value = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numProcessNoise.ValueChanged += new System.EventHandler(this.num_ValueChanged);
            // 
            // ProcessNosieGroupBox
            // 
            this.ProcessNosieGroupBox.Controls.Add(this.numProcessNoise);
            this.ProcessNosieGroupBox.Location = new System.Drawing.Point(12, 78);
            this.ProcessNosieGroupBox.Name = "ProcessNosieGroupBox";
            this.ProcessNosieGroupBox.Size = new System.Drawing.Size(107, 47);
            this.ProcessNosieGroupBox.TabIndex = 21;
            this.ProcessNosieGroupBox.TabStop = false;
            this.ProcessNosieGroupBox.Text = "Process noise";
            // 
            // MeasurementNoiseGroupBox
            // 
            this.MeasurementNoiseGroupBox.Controls.Add(this.numMeasurementNoise);
            this.MeasurementNoiseGroupBox.Location = new System.Drawing.Point(12, 168);
            this.MeasurementNoiseGroupBox.Name = "MeasurementNoiseGroupBox";
            this.MeasurementNoiseGroupBox.Size = new System.Drawing.Size(113, 47);
            this.MeasurementNoiseGroupBox.TabIndex = 22;
            this.MeasurementNoiseGroupBox.TabStop = false;
            this.MeasurementNoiseGroupBox.Text = "Measurement noise";
            // 
            // numMeasurementNoise
            // 
            this.numMeasurementNoise.DecimalPlaces = 1;
            this.numMeasurementNoise.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numMeasurementNoise.Location = new System.Drawing.Point(6, 19);
            this.numMeasurementNoise.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numMeasurementNoise.Name = "numMeasurementNoise";
            this.numMeasurementNoise.Size = new System.Drawing.Size(95, 20);
            this.numMeasurementNoise.TabIndex = 0;
            this.numMeasurementNoise.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numMeasurementNoise.ValueChanged += new System.EventHandler(this.num_ValueChanged);
            // 
            // DisatancesSeedGroupBox
            // 
            this.DisatancesSeedGroupBox.Controls.Add(this.PositionSeed);
            this.DisatancesSeedGroupBox.Location = new System.Drawing.Point(12, 258);
            this.DisatancesSeedGroupBox.Name = "DisatancesSeedGroupBox";
            this.DisatancesSeedGroupBox.Size = new System.Drawing.Size(113, 47);
            this.DisatancesSeedGroupBox.TabIndex = 23;
            this.DisatancesSeedGroupBox.TabStop = false;
            this.DisatancesSeedGroupBox.Text = "Distances Seed";
            this.toolTip1.SetToolTip(this.DisatancesSeedGroupBox, "r1,r2,r3 no spaces (yes commas)");
            // 
            // PositionSeed
            // 
            this.PositionSeed.Location = new System.Drawing.Point(6, 21);
            this.PositionSeed.Name = "PositionSeed";
            this.PositionSeed.Size = new System.Drawing.Size(100, 20);
            this.PositionSeed.TabIndex = 0;
            this.PositionSeed.Text = "0,0,0";
            // 
            // BeaconPositionsGroupBox
            // 
            this.BeaconPositionsGroupBox.Controls.Add(this.Beacon3PositionTextBox);
            this.BeaconPositionsGroupBox.Controls.Add(this.Beacon2PositionTextBox);
            this.BeaconPositionsGroupBox.Controls.Add(this.Beacon1PositionTextBox);
            this.BeaconPositionsGroupBox.Location = new System.Drawing.Point(12, 348);
            this.BeaconPositionsGroupBox.Name = "BeaconPositionsGroupBox";
            this.BeaconPositionsGroupBox.Size = new System.Drawing.Size(322, 47);
            this.BeaconPositionsGroupBox.TabIndex = 24;
            this.BeaconPositionsGroupBox.TabStop = false;
            this.BeaconPositionsGroupBox.Text = "Beacon Positions";
            // 
            // Beacon3PositionTextBox
            // 
            this.Beacon3PositionTextBox.Location = new System.Drawing.Point(215, 20);
            this.Beacon3PositionTextBox.Name = "Beacon3PositionTextBox";
            this.Beacon3PositionTextBox.Size = new System.Drawing.Size(100, 20);
            this.Beacon3PositionTextBox.TabIndex = 2;
            this.Beacon3PositionTextBox.Text = "0,0,0";
            // 
            // Beacon2PositionTextBox
            // 
            this.Beacon2PositionTextBox.Location = new System.Drawing.Point(111, 20);
            this.Beacon2PositionTextBox.Name = "Beacon2PositionTextBox";
            this.Beacon2PositionTextBox.Size = new System.Drawing.Size(100, 20);
            this.Beacon2PositionTextBox.TabIndex = 1;
            this.Beacon2PositionTextBox.Text = "0,0,0";
            // 
            // Beacon1PositionTextBox
            // 
            this.Beacon1PositionTextBox.Location = new System.Drawing.Point(7, 20);
            this.Beacon1PositionTextBox.Name = "Beacon1PositionTextBox";
            this.Beacon1PositionTextBox.Size = new System.Drawing.Size(100, 20);
            this.Beacon1PositionTextBox.TabIndex = 0;
            this.Beacon1PositionTextBox.Text = "0,0,0";
            // 
            // Beacon1Label
            // 
            this.Beacon1Label.AutoSize = true;
            this.Beacon1Label.Location = new System.Drawing.Point(15, 391);
            this.Beacon1Label.Name = "Beacon1Label";
            this.Beacon1Label.Size = new System.Drawing.Size(53, 13);
            this.Beacon1Label.TabIndex = 3;
            this.Beacon1Label.Text = "Beacon 1";
            // 
            // Beacon2Label
            // 
            this.Beacon2Label.AutoSize = true;
            this.Beacon2Label.Location = new System.Drawing.Point(119, 391);
            this.Beacon2Label.Name = "Beacon2Label";
            this.Beacon2Label.Size = new System.Drawing.Size(53, 13);
            this.Beacon2Label.TabIndex = 3;
            this.Beacon2Label.Text = "Beacon 2";
            // 
            // Beacon3Label
            // 
            this.Beacon3Label.AutoSize = true;
            this.Beacon3Label.Location = new System.Drawing.Point(223, 391);
            this.Beacon3Label.Name = "Beacon3Label";
            this.Beacon3Label.Size = new System.Drawing.Size(53, 13);
            this.Beacon3Label.TabIndex = 3;
            this.Beacon3Label.Text = "Beacon 3";
            // 
            // chart
            // 
            this.chart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea2.Name = "ChartArea1";
            this.chart.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chart.Legends.Add(legend2);
            this.chart.Location = new System.Drawing.Point(362, 12);
            this.chart.Name = "chart";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chart.Series.Add(series2);
            this.chart.Size = new System.Drawing.Size(551, 399);
            this.chart.TabIndex = 25;
            this.chart.Text = "chart";
            // 
            // timer
            // 
            this.timer.Interval = 30;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // ClearChartButton
            // 
            this.ClearChartButton.Location = new System.Drawing.Point(239, 97);
            this.ClearChartButton.Name = "ClearChartButton";
            this.ClearChartButton.Size = new System.Drawing.Size(75, 23);
            this.ClearChartButton.TabIndex = 1;
            this.ClearChartButton.Text = "Clear Chart";
            this.ClearChartButton.UseVisualStyleBackColor = true;
            this.ClearChartButton.Click += new System.EventHandler(this.ClearChartButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(925, 423);
            this.Controls.Add(this.ClearChartButton);
            this.Controls.Add(this.chart);
            this.Controls.Add(this.Beacon3Label);
            this.Controls.Add(this.Beacon2Label);
            this.Controls.Add(this.Beacon1Label);
            this.Controls.Add(this.BeaconPositionsGroupBox);
            this.Controls.Add(this.DisatancesSeedGroupBox);
            this.Controls.Add(this.MeasurementNoiseGroupBox);
            this.Controls.Add(this.ProcessNosieGroupBox);
            this.Controls.Add(this.StopOrResumeButton);
            this.Controls.Add(this.AlternateStateCheckBox);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.numProcessNoise)).EndInit();
            this.ProcessNosieGroupBox.ResumeLayout(false);
            this.MeasurementNoiseGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numMeasurementNoise)).EndInit();
            this.DisatancesSeedGroupBox.ResumeLayout(false);
            this.DisatancesSeedGroupBox.PerformLayout();
            this.BeaconPositionsGroupBox.ResumeLayout(false);
            this.BeaconPositionsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGetSerialPorts;
        private System.Windows.Forms.ComboBox cboPorts;
        private System.Windows.Forms.ComboBox cboBaudRate;
        private System.Windows.Forms.ComboBox cboDataBits;
        private System.Windows.Forms.ComboBox cboStopBits;
        private System.Windows.Forms.ComboBox cboParity;
        private System.Windows.Forms.ComboBox cboHandShaking;
        private System.Windows.Forms.Label lblBreakStatus;
        private System.Windows.Forms.Label lblCTSStatus;
        private System.Windows.Forms.Label lblDSRStatus;
        private System.Windows.Forms.Label lblRIStatus;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnPortState;
        private System.Windows.Forms.Button btnHello;
        private System.Windows.Forms.RichTextBox rtbOutgoing;
        private System.Windows.Forms.Button btnHyperTerm;
        private System.Windows.Forms.TextBox txtCommand;
        private System.Windows.Forms.CheckBox AlternateStateCheckBox;
        private System.Windows.Forms.Button StopOrResumeButton;
        private System.Windows.Forms.NumericUpDown numProcessNoise;
        private System.Windows.Forms.GroupBox ProcessNosieGroupBox;
        private System.Windows.Forms.GroupBox MeasurementNoiseGroupBox;
        private System.Windows.Forms.NumericUpDown numMeasurementNoise;
        private System.Windows.Forms.GroupBox DisatancesSeedGroupBox;
        private System.Windows.Forms.TextBox PositionSeed;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox BeaconPositionsGroupBox;
        private System.Windows.Forms.TextBox Beacon3PositionTextBox;
        private System.Windows.Forms.TextBox Beacon2PositionTextBox;
        private System.Windows.Forms.TextBox Beacon1PositionTextBox;
        private System.Windows.Forms.Label Beacon1Label;
        private System.Windows.Forms.Label Beacon2Label;
        private System.Windows.Forms.Label Beacon3Label;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Button ClearChartButton;
    }
}
