using System;
using System.Windows.Forms;
using Coloreality;
using Coloreality.Server;
using Coloreality.LeapWrapper.Sender;
using Coloreality.LeapWrapper;
using Leap;
using ConnectionEventArgs = Coloreality.Server.ConnectionEventArgs;

namespace ColorealityServer
{
    public partial class MainForm : Form
    {
        SocketServer server;
        LeapReader leapReader;

        #region Leap object offset/scale configs.
        TrackBar[] LeapConfigBars;
        TextBox[] LeapConfigTextboxs;

        public event SerializationReadyEventHandler OnConfigChanged;
        LeapHmdConfig leapConfig = new LeapHmdConfig();
        #endregion

        #region Form control paramters.
        string beforeConnectButtonText = "&Start Server";
        string afterConnectButtonText = "&Close Server";

        string beforeConnectConnectionLabel = "None";
        string afterConnectConnectionLabel = "Waiting...";
        #endregion

        bool serverStarted = false;
        string logStartText = "Coloreality PC Server\r\nhttp://TangoChen.com\r\n";

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
                usePort = NetworkUtil.GetOpenPort(Globals.SERVER_DEFAULT_PORT);
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

            StartServerButton.Text = beforeConnectButtonText;
            UpdateLabel(ConnectionLabel, beforeConnectConnectionLabel);

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
                UpdateLabel(ConnectionLabel, afterConnectConnectionLabel);
                StartServerButton.Text = afterConnectButtonText;
            }
            else
            {
                if (server != null)
                {
                    server.Close();
                }

                AppendLog("Closed server.");
                UpdateLabel(ConnectionLabel, beforeConnectConnectionLabel);
                StartServerButton.Text = beforeConnectButtonText;
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

        private void LeapConfigBar_ValueChanged(object sender, EventArgs e)
        {
            TrackBar thisBar = (TrackBar)sender;
            LeapConfigTextboxs[int.Parse(thisBar.Tag.ToString())].Text = (thisBar.Value * CONFIG_VALUE_PRECISION).ToString("0.####");
            UpdateLeapConfig();
        }

        private void LeapConfigBar_ValueChanged(object sender, MouseEventArgs e)
        {
            LeapConfigBar_ValueChanged(sender, EventArgs.Empty);
        }

        const float CONFIG_VALUE_PRECISION = 0.0001f;
        const int CONFIG_VALUE_PRECISION_TIMES = (int)(1 / CONFIG_VALUE_PRECISION);
        private void LeapConfigInput_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox thisInput = (TextBox)sender;
            if (e.KeyCode == Keys.Enter)
            {
                TrackBar trackBar = LeapConfigBars[int.Parse(thisInput.Tag.ToString())];
                float value;
                if (float.TryParse(thisInput.Text, out value))
                {
                    if (value > trackBar.Maximum * CONFIG_VALUE_PRECISION)
                    {
                        trackBar.Value = trackBar.Maximum;
                    }
                    else if (value < trackBar.Minimum * CONFIG_VALUE_PRECISION)
                    {
                        trackBar.Value = trackBar.Minimum;
                    }
                    else
                    {
                        trackBar.Value = (int)(value * CONFIG_VALUE_PRECISION_TIMES);
                    }

                    LeapConfigBar_ValueChanged(trackBar, EventArgs.Empty);
                }
                else
                {
                    thisInput.Text = (trackBar.Value * CONFIG_VALUE_PRECISION).ToString("0.###");
                }
            }
        }

        private void UpdateLeapConfig()
        {
            leapConfig.OffsetX = LeapOffsetXBar.Value * CONFIG_VALUE_PRECISION;
            leapConfig.OffsetY = LeapOffsetYBar.Value * CONFIG_VALUE_PRECISION;
            leapConfig.OffsetZ = LeapOffsetZBar.Value * CONFIG_VALUE_PRECISION;
            leapConfig.Scale = LeapScaleBar.Value * CONFIG_VALUE_PRECISION;
            if (OnConfigChanged != null) OnConfigChanged.Invoke(this, new SerializationEventArgs(LeapHmdConfig.DATA_INDEX, SerializationUtil.Serialize(leapConfig)));
        }

        private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            WindowState = FormWindowState.Normal;
        }
    }
}
