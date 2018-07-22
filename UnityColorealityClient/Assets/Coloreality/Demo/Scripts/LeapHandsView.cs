using UnityEngine;
using System.Collections.Generic;
using Coloreality.LeapWrapper;
using Coloreality.LeapWrapper.Receiver;

namespace Coloreality
{
	public class LeapHandsView : MonoBehaviour {
		ColorealityManager cManager;

		public GameObject handLeft;
		public GameObject handRight;

		LeapSingleHandView[] handViews;

		float lastUpdateTime = 0;
		float cancelInterval = 0.5f;

		void Start () {
			cManager = ColorealityManager.Instance;
            if(cManager == null)
            {
                Debug.LogError("Cannot find ColorealityManager Instance.");
                enabled = false;
            }

            handViews = new LeapSingleHandView[2];
			handViews[0] = handLeft.GetComponent<LeapSingleHandView>();
            handViews[1] = handRight.GetComponent<LeapSingleHandView>();
		}
		
		void FixedUpdate () {
            bool[] hasHandSide = new bool[2] { false, false };
			if (cManager.Leap.Data != null) {
				List<LeapHand> hands = cManager.Leap.Data.frame.Hands;
				for (int i = 0; i < hands.Count; i++) {
					int curSide = hands [i].IsLeft ? 0 : 1;
					hasHandSide[curSide] = true;
					handViews[curSide].UpdateHand(hands [i]);
				}

				for (int side = 0; side < 2; side++) {
					handViews[side].gameObject.SetActive (hasHandSide [side]);
				}
				lastUpdateTime = Time.time;
			} else  if(Time.time - lastUpdateTime > cancelInterval) {
				for (int side = 0; side < 2; side++) {
					handViews[side].gameObject.SetActive(false);
				}
			}
		}

	}
}
