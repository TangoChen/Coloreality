using System;
using System.Windows.Forms;
using Coloreality;
using Coloreality.Server;
using Coloreality.LeapWrapper;
using Coloreality.LeapWrapper.Sender;
using Leap;
using ConnectionEventArgs = Coloreality.Server.ConnectionEventArgs;

namespace ColorealityServer
{
    public partial class MainForm : Form
    {
        SocketServer server;
        LeapReader leapReader;

        public bool serverStarted = false;

        #region Leap object offset/scale configs.
        TrackBar[] LeapConfigBars;
        TextBox[] LeapConfigTextboxs;

        public event SerializationReadyEventHandler OnConfigChanged;
        LeapHmdConfig leapConfig = new LeapHmdConfig();

        const float ConfigValuePrecision = 0.0001f;
        const int ConfigValuePrecisionTimes = (int)(1 / ConfigValuePrecision);
        #endregion

        #region Form controls.
        const string ServerButtonStart = "&Start Server";
        const string ServerButtonClose = "&Close Server";

        const string ConnectionNone = "None";
        const string ConnectionWaiting = "Waiting...";

        string logStartText = "Coloreality PC Server\r\nhttp://TangoChen.com\r\n";
        #endregion
        
        public MainForm()
        {
            InitializeComponent();
            LeapConfigBars = new TrackBar[4] { LeapOffsetXBar, LeapOffsetYBar, LeapOffsetZBar, LeapScaleBar };
            LeapConfigTextboxs = new TextBox[4] { LeapOffsetXInput, LeapOffsetYInput, LeapOffsetZInput, LeapScaleInput };
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            int usePort = Properties.Settings.Default.ServerPort;
            if (!NetworkUtil.IsPortAvailable(usePort))
            {
                usePort = NetworkUtil.GetOpenPort(Globals.ServerDefaultPort);
            }
            server = new SocketServer(false, usePort);
            server.OnConnected += Server_OnAddedConnection;
            server.OnDisconnected += Server_OnDisconnected;
            server.OnError += Server_OnError;

            IpTextBox.Text = NetworkUtil.GetIp().ToString();
            PortInput.Text = usePort.ToString();

            leapReader = new LeapReader(true);
            leapReader.Device += LeapController_Device;
            leapReader.Connect += LeapController_Connect;
            leapReader.DeviceLost += LeapController_DeviceLost;
            leapReader.DeviceFailure += LeapController_DeviceFailure;

            LogTextbox.Text = logStartText + "\r\nLog " + DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss");

            leapReader.StartConnection();

            StartServerButton.Text = ServerButtonStart;
            UpdateLabel(ConnectionLabel, ConnectionNone);

            AutoStartServerToggle.Checked = Properties.Settings.Default.AutoStartServer;
            if (AutoStartServerToggle.Checked)
            {
                ServerSwitch(true);
            }

        }

        void Server_OnError(object sender, ConnectionErrorEventArgs e)
        {
            AppendLog("Error[" + e.ConnectionName + "]: " + e.Message);
        }

        void LeapController_DeviceFailure(object sender, DeviceFailureEventArgs e)
        {
            UpdateLeapStatus("Device failure");
        }

        void LeapController_DeviceLost(object sender, DeviceEventArgs e)
        {
            UpdateLeapStatus("Device lost");
        }

        private void UpdateLeapStatus(string value, string log = "")
        {
            LeapStatusLabel.Text = value;
            AppendLog((log == "" ? value + "." : log));
        }

        void LeapController_Device(object sender, DeviceEventArgs e)
        {
            UpdateLeapStatus("Connected", "Device Connected.");
        }

        void LeapController_Connect(object sender, Leap.ConnectionEventArgs e)
        {
            UpdateLeapStatus("Service connected");
        }

        void Server_OnAddedConnection(object sender, ConnectionEventArgs e)
        {
            leapReader.OnSerializationReady += e.Connection.OnReaderReady;
            OnConfigChanged += e.Connection.OnReaderReady;

            AppendLog("New connection: " + e.Connection.Name);
            UpdateConnectionCountLabel();
        }

        void Server_OnDisconnected(object sender, ConnectionEventArgs e)
        {
            leapReader.OnSerializationReady -= e.Connection.OnReaderReady;
            OnConfigChanged -= e.Connection.OnReaderReady;
            AppendLog("Disconnected: " + e.Connection.Name);
            UpdateConnectionCountLabel();
        }

        private void UpdateConnectionCountLabel()
        {
            int count = server.ConnectionCount;
            if (count == 1)
            {
                UpdateLabel(ConnectionLabel, "Connected!");
            }
            else if (count == 0)
            {
                UpdateLabel(ConnectionLabel, "No connection");
            }
            else
            {
                UpdateLabel(ConnectionLabel, server.ConnectionCount.ToString() + " connected!");
            }

            SetConfigPanelActive(count > 0);
        }

        private delegate void PanelActiveCallback(bool enabled);
        public void SetConfigPanelActive(bool enabled)
        {
            if (PanelLeapConfig.InvokeRequired)
            {
                while (!PanelLeapConfig.IsHandleCreated)
                {
                    if (PanelLeapConfig.Disposing || PanelLeapConfig.IsDisposed)
                        return;
                }
                PanelActiveCallback d = new PanelActiveCallback(SetConfigPanelActive);
                PanelLeapConfig.Invoke(d, new object[] { enabled });
            }
            else
            {
                PanelLeapConfig.Enabled = enabled;
            }
        }

        private void StartServerButton_Click(object sender, EventArgs e)
        {
            StartServerButton.Enabled = false;

            if (!serverStarted)
            {
                ServerSwitch(true);
            }
            else
            {
                ServerSwitch(false);
            }
        }

        public void ServerSwitch(bool on = true)
        {
            if (on)
            {
                server.Listen();
                PortInput.Text = server.Port.ToString();
                AppendLog("Started server, waiting for connection...");
                LogTextbox.Select();
                UpdateLabel(ConnectionLabel, ConnectionWaiting);
                StartServerButton.Text = ServerButtonClose;
            }
            else
            {
                if (server != null)
                {
                    server.Close();
                }

                AppendLog("Closed server.");
                UpdateLabel(ConnectionLabel, ConnectionNone);
                StartServerButton.Text = ServerButtonStart;
            }
            PortInput.ReadOnly = on;
            serverStarted = on;
            StartServerButton.Enabled = true;
        }

        private void PortInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Pressed Return key.
            if (e.KeyChar == 13 && PortInput.Text != server.Port.ToString())
            {
                int inputPort;
                if (int.TryParse(PortInput.Text, out inputPort) && NetworkUtil.IsPortAvailable(inputPort))
                {
                    server.TrySetPort(inputPort);
                    Properties.Settings.Default.ServerPort = inputPort;
                    Properties.Settings.Default.Save();
                    AppendLog("Changed port to " + inputPort.ToString() + ".");
                }
                else
                {
                    PortInput.Text = server.Port.ToString();
                }
            }
        }

        private delegate void AppendLogCallback(string message);
        public void AppendLog(string message)
        {
            if (LogTextbox.InvokeRequired)
            {
                while (!LogTextbox.IsHandleCreated)
                {
                    if (LogTextbox.Disposing || LogTextbox.IsDisposed)
                        return;
                }
                AppendLogCallback d = new AppendLogCallback(AppendLog);
                LogTextbox.Invoke(d, new object[] { message });
            }
            else
            {
                LogTextbox.Text += "\r\n" + message;
                LogTextbox.SelectionStart = LogTextbox.TextLength;
                LogTextbox.ScrollToCaret();
            }
        }

        private delegate void UpdateLabelCallback(Label control, string value);
        public void UpdateLabel(Label control, string value)
        {
            if (control.InvokeRequired)
            {
                while (!control.IsHandleCreated)
                {
                    if (control.Disposing || control.IsDisposed)
                        return;
                }
                UpdateLabelCallback updateLabelDelegate = new UpdateLabelCallback(UpdateLabel);
                control.Invoke(updateLabelDelegate, new object[] { control, value });
            }
            else
            {
                control.Text = value;
            }
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Visible = false;
            if (server != null)
            {
                if (serverStarted) AppendLog("Closing server...");
                server.Close();
            }
            leapReader.StopConnection();
            notifyIcon.Visible = false;
        }

        private void AutoStartServerToggle_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.AutoStartServer = AutoStartServerToggle.Checked;
            Properties.Settings.Default.Save();
        }

        private void LeapConfigBar_ValueChanged(object sender)
        {
            TrackBar thisBar = (TrackBar)sender;
            LeapConfigTextboxs[int.Parse(thisBar.Tag.ToString())].Text = (thisBar.Value * ConfigValuePrecision).ToString("0.####");
            UpdateLeapConfig();
        }

        private void LeapConfigBar_MouseUp(object sender, MouseEventArgs e)
        {
            LeapConfigBar_ValueChanged(sender);
        }
        
        private void LeapConfigInput_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox thisInput = (TextBox)sender;
            if (e.KeyCode == Keys.Enter)
            {
                TrackBar trackBar = LeapConfigBars[int.Parse(thisInput.Tag.ToString())];
                float value;
                if (float.TryParse(thisInput.Text, out value))
                {
                    if (value > trackBar.Maximum * ConfigValuePrecision)
                    {
                        trackBar.Value = trackBar.Maximum;
                    }
                    else if (value < trackBar.Minimum * ConfigValuePrecision)
                    {
                        trackBar.Value = trackBar.Minimum;
                    }
                    else
                    {
                        trackBar.Value = (int)(value * ConfigValuePrecisionTimes);
                    }

                    LeapConfigBar_ValueChanged(trackBar);
                }
                else
                {
                    thisInput.Text = (trackBar.Value * ConfigValuePrecision).ToString("0.###");
                }
            }
        }

        private void UpdateLeapConfig()
        {
            leapConfig.OffsetX = LeapOffsetXBar.Value * ConfigValuePrecision;
            leapConfig.OffsetY = LeapOffsetYBar.Value * ConfigValuePrecision;
            leapConfig.OffsetZ = LeapOffsetZBar.Value * ConfigValuePrecision;
            leapConfig.Scale = LeapScaleBar.Value * ConfigValuePrecision;
            if (OnConfigChanged != null) OnConfigChanged.Invoke(this, new SerializationEventArgs(LeapHmdConfig.DataIndex, SerializationUtil.Serialize(leapConfig)));
        }

        private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            WindowState = FormWindowState.Normal;
        }

        private void SendIntervalBar_MouseUp(object sender, MouseEventArgs e)
        {
            SendIntervalInput.Text = SendIntervalBar.Value.ToString();
            SetInterval(SendIntervalBar.Value);
        }

        private void SendIntervalInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int value;
                if (int.TryParse(SendIntervalInput.Text, out value))
                {
                    if (value > SendIntervalBar.Maximum)
                    {
                        SendIntervalBar.Value = SendIntervalBar.Maximum;
                    }
                    else if (value < SendIntervalBar.Minimum)
                    {
                        SendIntervalBar.Value = SendIntervalBar.Minimum;
                    }
                    else
                    {
                        SendIntervalBar.Value = value;
                    }

                    SetInterval(SendIntervalBar.Value);
                }
                else
                {
                    SendIntervalInput.Text = SendIntervalBar.Value.ToString();
                }
            }
        }

        private void SetInterval(int value)
        {
            server.SendInterval = value;
            server.SetAllSendInterval(SendIntervalBar.Value);
        }
    }
}
