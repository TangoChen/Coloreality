using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coloreality.LeapWrapper;

namespace Coloreality
{
	[RequireComponent(typeof(LineRenderer))]
	public class LeapHandConnect : MonoBehaviour {
		ColorealityManager cManager;

		LineRenderer line;

		void Start () {
			cManager = ColorealityManager.Instance;
			if(cManager == null)
			{
				Debug.LogError("Cannot find ColorealityManager Instance.");
				enabled = false;
			}

			line = GetComponent<LineRenderer>();
			line.positionCount = 2;
		}
		
		void FixedUpdate () {
			if (cManager.Leap.Data != null) {
				List<LeapHand> hands = cManager.Leap.Data.frame.Hands;
				if (hands.Count >= 2) {
					line.SetPositions (new Vector3[2] {
						hands[0].PalmPosition.ToHMDVector3(), 
						hands[1].PalmPosition.ToHMDVector3()
					});
					line.enabled = true;
				} else {
					line.enabled = false;
				}
			} else {
				line.enabled = false;
			}

		}
	}
}