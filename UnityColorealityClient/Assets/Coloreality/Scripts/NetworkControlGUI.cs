using UnityEngine;
using System.Collections;

namespace Coloreality
{
	public class NetworkControlGUI : MonoBehaviour {
		ColorealityManager cManager;

		string inputIp = "";
		string inputPort = "";

		bool showGUI = true;

		void Start () {
			infoStyle.alignment = TextAnchor.MiddleCenter;
	        infoStyle.normal.textColor = Color.white;

			cManager = GetComponent<ColorealityManager>();
			if (PlayerPrefs.HasKey("UseIp")) {
				inputIp = PlayerPrefs.GetString("UseIp");
			} else {
				inputIp = cManager.network.Ip;
			}

			if (PlayerPrefs.HasKey("UsePort")) {
				inputPort = PlayerPrefs.GetInt("UsePort").ToString();
			} else {
				inputPort = cManager.network.Port.ToString();
			}

			cManager.network.OnConnected += (object sender, System.EventArgs e) => {
				info += "\r\nConnected!";
				showGUI = false;
			};
			cManager.network.OnDisconnected += (object sender, System.EventArgs e) => {
				info += "\r\nDisconnected.";
				showGUI = true;
			};
			cManager.network.OnError += (object sender, ErrorEventArgs e) => info += "\r\nError: " + e.Message;
		}
		
		string info = "Please enter the server's IP and Port, then click [Connect].";

		Rect rectGUI = new Rect (100, 10, 400, 265);
	    GUIStyle infoStyle = new GUIStyle();

		private void GUIWindow(int id){
			GUI.skin.box.fontSize = 20;
			GUI.skin.textField.fontSize = 20;
			GUI.skin.label.fontSize = 20;
			GUI.Label(new Rect(10, 25, 50, 30), "IP:");
			inputIp = GUI.TextField(new Rect(90, 25, 300, 30), inputIp);
			GUI.Label(new Rect(10, 65, 50, 30), "Port:");
			inputPort = GUI.TextField(new Rect(90, 65, 300, 30), inputPort);
			if(GUI.Button(new Rect (10, 105, 380, 40), "Connect")) {
				int portResult;
				if (int.TryParse (inputPort, out portResult)) {
					cManager.TryConnect(inputIp, portResult);
				} else {
					info = "Port is not in right format.";
				}
			}

			if(info.Length > 6000) info = info.Substring(info.Length - 6000);
	        GUI.TextArea(new Rect (10, 155, 380, 100), info, infoStyle);

		}

		void OnGUI(){
			if (showGUI) 
			{
				GUI.Window(0, rectGUI, GUIWindow, "Coloreality");
			}
		}

		void OnApplicationQuit(){
			PlayerPrefs.SetString("UseIp", inputIp);
			PlayerPrefs.SetInt("UsePort", cManager.network.Port);
			PlayerPrefs.Save();
		}
	}

}