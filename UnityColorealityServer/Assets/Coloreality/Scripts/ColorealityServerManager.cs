using UnityEngine;
using System.Collections;
using Coloreality.LeapWrapper;
using Coloreality.LeapWrapper.Sender;
using Leap;
using ConnectionEventArgs = Coloreality.Server.ConnectionEventArgs;
using System;

namespace Coloreality.Server
{
    public class ColorealityServerManager : MonoBehaviour {
        public SocketServer server;
        public LeapReader leapReader;

        public bool showGUI = true;

        // Too low interval may cause stucking.
        int sendInterval = 100;

        string ip = "";
        string inputPort = "";

        public bool ServerStarted{
            get{
                return serverStarted;
            }
        }
        private bool serverStarted = false;

        public EventHandler<SerializationEventArgs> OnConfigChanged;
        LeapHmdConfig leapConfig = new LeapHmdConfig();

        string info = "Coloreality PC Server\r\nhttp://TangoChen.com\r\n";

        string leapStatusLabel = "None";
        string connectionLabel = "None";

        const string ServerButtonStart = "Start Server";
        const string ServerButtonClose = "Close Server";

        const string ConnectionNone = "None";
        const string ConnectionWaiting = "Waiting...";

        Rect rectMainGUI = new Rect(10, 10, 415, 400);
        GUIStyle selectedAbleStyle;
        GUIStyle unselectedAbleStyle;

        Rect rectLeapConfigGUI = new Rect(435, 10, 415, 285);

        bool autoStartServer = false;

    	// Use this for initialization
    	void Awake () {
            rectMainGUI.height = Screen.height - 20;
            int usePort = Coloreality.Globals.ServerDefaultPort;
            if(PlayerPrefs.HasKey("ColorealityServerPort")){
                usePort = PlayerPrefs.GetInt("ColorealityServerPort");
            }
            if(PlayerPrefs.HasKey("ColorealitySendInterval")){
                sendInterval = PlayerPrefs.GetInt("ColorealitySendInterval");
            }

            server = new SocketServer(Network.player.ipAddress, usePort);
            server.OnConnected += Server_OnAddedConnection;
            server.OnDisconnected += Server_OnDisconnected;
            server.OnError += Server_OnError;
            server.SendInterval = sendInterval;
            ip = server.Ip.ToString();

            inputPort = usePort.ToString();

            leapReader = new LeapReader(true);
            leapReader.Device += LeapController_Device;
            leapReader.Connect += LeapController_Connect;
            leapReader.DeviceLost += LeapController_DeviceLost;
            leapReader.DeviceFailure += LeapController_DeviceFailure;

            if(PlayerPrefs.HasKey("ColorealityAutoStartServer")){
                autoStartServer = PlayerPrefs.GetInt("ColorealityAutoStartServer") == 1;
            }
            if(autoStartServer)
            {
                ServerSwitch(true);
            }
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

        void Server_OnError(object sender, ConnectionErrorEventArgs e)
        {
            AppendLog("Error[" + e.ConnectionName + "]: " + e.Message);
        }

        void LeapController_Device(object sender, DeviceEventArgs e)
        {
            UpdateLeapStatus("Connected", "Device Connected.");
        }

        void LeapController_Connect(object sender, Leap.ConnectionEventArgs e)
        {
            UpdateLeapStatus("Service connected");
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
            leapStatusLabel = value;
            AppendLog((log == "" ? value + "." : log));
        }

        public void AppendLog(string message){
            info += "\r\n" + message;
        }

        private void UpdateConnectionCountLabel()
        {
            int count = server.ConnectionCount;
            if (count == 1)
            {
                connectionLabel = "Connected!";
            }
            else if (count == 0)
            {
                connectionLabel = "No connection";
            }
            else
            {
                connectionLabel = server.ConnectionCount.ToString() + " connected!";
            }

            showConfigWindow = count > 0;
        }

        public void ServerSwitch(bool on = true)
        {
            if (on)
            {
                int portNum;
                if(int.TryParse(inputPort, out portNum) && server.TrySetPort(portNum))
                {
                    server.Listen();
                    inputPort = server.Port.ToString();

                    PlayerPrefs.SetInt("ColorealityServerPort", server.Port);
                    PlayerPrefs.Save();

                    AppendLog("Started server, waiting for connection...");

                    connectionLabel = ConnectionWaiting;
                }
            }
            else
            {
                if (server != null)
                {
                    server.Close();
                }

                AppendLog("Closed server.");
                connectionLabel = ConnectionNone;
            }

            serverStarted = on;
        }

        bool showConfigWindow = false;

        float leapConfigOffsetX = 0;
        float leapConfigOffsetY = 0;
        float leapConfigOffsetZ = 0;
        float leapConfigScale = 1;

        private void UpdateLeapConfig()
        {
            leapConfig.OffsetX = leapConfigOffsetX;
            leapConfig.OffsetY = leapConfigOffsetY;
            leapConfig.OffsetZ = leapConfigOffsetZ;
            leapConfig.Scale = leapConfigScale;
            if(OnConfigChanged != null) OnConfigChanged.Invoke(this, new SerializationEventArgs(LeapHmdConfig.DataIndex, SerializationUtil.Serialize(leapConfig)));
        }

        private void MainWindowGUI(int id){
            if(selectedAbleStyle == null)
            {
                selectedAbleStyle = new GUIStyle(GUI.skin.box);
                selectedAbleStyle.fontSize = 20;
                selectedAbleStyle.alignment = TextAnchor.MiddleCenter;

                unselectedAbleStyle = new GUIStyle(GUI.skin.label);
                unselectedAbleStyle.alignment = TextAnchor.MiddleCenter;
            }

            if(GUI.Button(new Rect(10, 25, 390, 40), serverStarted ? ServerButtonClose : ServerButtonStart))
            {
                ServerSwitch(!serverStarted);
            }

            GUI.Box(new Rect(10, 75, 180, 30), "Leap Status", unselectedAbleStyle);
            GUI.Box(new Rect(200, 75, 200, 30), leapStatusLabel, selectedAbleStyle);

            GUI.Box(new Rect(10, 110, 180, 30), "Connection", unselectedAbleStyle);
            GUI.Box(new Rect(200, 110, 200, 30), connectionLabel, selectedAbleStyle);

            GUI.Box(new Rect(10, 150, 100, 30), "IP:", unselectedAbleStyle);
            GUI.TextField(new Rect(120, 150, 280, 30), ip, selectedAbleStyle);
            GUI.Box(new Rect(10, 185, 100, 30), "Port:", unselectedAbleStyle);
            inputPort = GUI.TextField(new Rect(120, 185, 280, 30), inputPort, selectedAbleStyle);

            GUI.Label(new Rect(10, 220, 100, 30), "Interval:", unselectedAbleStyle);
            int tempInterval = (int)GUI.HorizontalSlider(new Rect(120, 230, 230, 30), sendInterval, 0, 500);
            GUI.Box(new Rect(355, 220, 45, 30), sendInterval.ToString(), unselectedAbleStyle);
            if(tempInterval != sendInterval)
            {
                sendInterval = tempInterval;
                server.SendInterval = sendInterval;
                server.SetAllSendInterval(sendInterval);
            }

            GUI.TextArea(new Rect(10, 255, 390, rectMainGUI.height - 290), info, selectedAbleStyle);

            bool tempAutoStartServer = GUI.Toggle(new Rect(10, rectMainGUI.height - 30, 390, 30), autoStartServer, "Auto start server next time");
            if(tempAutoStartServer != autoStartServer)
            {
                autoStartServer = tempAutoStartServer;
                PlayerPrefs.SetInt("ColorealityAutoStartServer", autoStartServer ? 1 : 0);
                PlayerPrefs.Save();
            }
        }
            
        private void LeapConfigGUI(int id){

            bool valueChanged = false;

            float offsetX = GUI.HorizontalSlider(new Rect(10, 55, 390, 30), leapConfigOffsetX, -3.5f, 3.5f);
            if(offsetX != leapConfigOffsetX)
            {
                leapConfigOffsetX = offsetX;
                valueChanged = true;
            }
            GUI.Label(new Rect(10, 25, 390, 30), "Offset X: " + leapConfigOffsetX.ToString("f2"));

            float offsetY = GUI.HorizontalSlider(new Rect(10, 125, 390, 30), leapConfigOffsetY, -3.5f, 3.5f);
            if(offsetY != leapConfigOffsetY)
            {
                leapConfigOffsetY = offsetY;
                valueChanged = true;
            }
            GUI.Label(new Rect(10, 90, 390, 30), "Offset Y: " + leapConfigOffsetY.ToString("f2"));


            float offsetZ = GUI.HorizontalSlider(new Rect(10, 190, 390, 30), leapConfigOffsetZ, -3.5f, 3.5f);
            if(offsetZ != leapConfigOffsetZ)
            {
                leapConfigOffsetZ = offsetZ;
                valueChanged = true;
            }
            GUI.Label(new Rect(10, 160, 390, 30), "Offset Z: " + leapConfigOffsetZ.ToString("f2"));


            float scale = GUI.HorizontalSlider(new Rect(10, 255, 390, 30), leapConfigScale, 0.01f, 2f);
            if(scale != leapConfigScale)
            {
                leapConfigScale = scale;
                valueChanged = true;
            }
            GUI.Label(new Rect(10, 225, 390, 30), "Scale: " + leapConfigScale.ToString("f2"));

            if(valueChanged)
            {
                UpdateLeapConfig();
            }
        }

        void OnGUI(){
            if (showGUI)
            {
                GUI.Window(0, rectMainGUI, MainWindowGUI, "Coloreality");

                if(showConfigWindow)
                {
                    GUI.Window(1, rectLeapConfigGUI, LeapConfigGUI, "Leap Config");
                }
            }

        }

        void OnApplicationQuit(){
            PlayerPrefs.SetInt("ColorealitySendInterval", sendInterval);
            PlayerPrefs.Save();

            if (server != null)
            {
                if (serverStarted) AppendLog("Closing server...");
                server.Close();
            }

            leapReader.StopConnection();
        }
    }
}
