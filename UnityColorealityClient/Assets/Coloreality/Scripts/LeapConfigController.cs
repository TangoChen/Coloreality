using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coloreality.Client;
using Coloreality.LeapWrapper;
using Coloreality.LeapWrapper.Receiver;

namespace Coloreality
{
	public class LeapConfigController : MonoBehaviour {
		public ColorealityManager cManager;
		LeapHmdConfigReceiver leapConfigReceiver = new LeapHmdConfigReceiver();
		LeapHmdConfig leapConfig = new LeapHmdConfig();

		bool isUpdated = false;

		void Start() {
			cManager = ColorealityManager.Instance;
			if(cManager == null)
			{
				Debug.LogError("Cannot find ColorealityManager Instance.");
				enabled = false;
				return;
			}

			leapConfigReceiver.AddSource(cManager.network);
			leapConfigReceiver.OnUpdatedData += UpdateConfig;
		}

		void FixedUpdate(){
			if (isUpdated && leapConfig != null) {
				transform.localPosition = new Vector3(leapConfig.OffsetX, leapConfig.OffsetY, leapConfig.OffsetZ);
				transform.localScale = Vector3.one * leapConfig.Scale;

				isUpdated = false;
			}
		}
		
		private void UpdateConfig(object sender, UpdateDataEventArgs<LeapHmdConfig> e){
			leapConfig = e.Data;
			isUpdated = true;
		}
	}

}