using UnityEngine;
using Coloreality.Client;
using Coloreality.LeapWrapper;
using Coloreality.LeapWrapper.Receiver;

namespace Coloreality
{
	public class ColorealityManager : MonoBehaviour {
		public SocketClient network = new SocketClient();
		public LeapSimulator Leap{ get; private set; }

		public static ColorealityManager Instance;

		void Awake () {
			if(Instance == null) Instance = this;
			Leap = new LeapSimulator();
            Leap.AddSource(network);

			Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        public void TryConnect(string ip, int port){
			network.Ip = ip;
			network.Port = port;
			network.Connect();
		}

		void OnApplicationQuit(){
			if (network.IsConnected) {
				network.Close (true);
			}
		}

	}

}