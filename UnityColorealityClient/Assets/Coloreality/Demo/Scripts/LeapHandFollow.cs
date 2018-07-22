using UnityEngine;
using System.Collections.Generic;
using Coloreality.LeapWrapper;

namespace Coloreality
{
    public class LeapHandFollow : MonoBehaviour {
		ColorealityManager cManager;

        public GameObject HandLeftObject;
        public GameObject HandRightObject;

        GameObject[] handObjects;

        Material[] matObjects;
        Vector3[] originalScales;

		float lastUpdateTime = 0;
		float cancelInterval = 0.5f;

		Vector3[] targetScales;

    	// Use this for initialization
    	void Start () {
            cManager = ColorealityManager.Instance;
            if(cManager == null)
            {
                Debug.LogError("Cannot find ColorealityManager Instance.");
                enabled = false;
            }

            handObjects = new GameObject[2]{ HandLeftObject, HandRightObject };

            matObjects = new Material[2];
            originalScales = new Vector3[2];
			targetScales = new Vector3[2];
            // side: 0 for left, 1 for right
            for(int side = 0; side < 2; side++)
            {
                matObjects[side] = handObjects[side].GetComponentInChildren<Renderer>().material;
                originalScales[side] = handObjects[side].transform.localScale;
				targetScales[side] = Vector3.zero;
            }

    	}
    	
    	void FixedUpdate () {
            bool[] hasHandSide = new bool[2] { false, false }; // bool hasLeft = false, hasRight = false;
			if (cManager.Leap.Data != null) {
				List<LeapHand> hands = cManager.Leap.Data.frame.Hands;
				for (int i = 0; i < hands.Count; i++) {
					int curSide = hands [i].IsLeft ? 0 : 1;
					hasHandSide [curSide] = true;
					float objScale = 1 - hands [i].GrabStrength;
					if (objScale == 0 && handObjects[curSide].transform.localScale.x != 0) {
						matObjects [curSide].color = new Color (Random.value, Random.value, Random.value);
					}
					targetScales[curSide] = objScale * originalScales[curSide];
					handObjects[curSide].transform.localPosition = hands[i].PalmPosition.ToHMDVector3 ();
					handObjects[curSide].transform.localRotation = hands[i].Rotation.ToHMDQuaternion ();
				}

				for (int side = 0; side < 2; side++) {
					handObjects[side].SetActive(hasHandSide [side]);
				}
				lastUpdateTime = Time.time;
			} else if(Time.time - lastUpdateTime > cancelInterval) {
				for (int side = 0; side < 2; side++) {
					handObjects[side].SetActive(false);
				}
			}

			for (int side = 0; side < 2; side++) {
				if (handObjects[side].activeSelf) {
					handObjects[side].transform.localScale = Vector3.Lerp (handObjects [side].transform.localScale, targetScales [side], Time.deltaTime * 30f); //originalScales[side] * Mathf.Lerp(handObjects[side].transform.localScale.x, targetScales[side] * , Time.deltaTime * 35f);
				}
			}
    	}
    }
}
