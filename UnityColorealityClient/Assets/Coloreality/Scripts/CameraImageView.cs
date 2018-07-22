using UnityEngine;
using System.Collections;

namespace Coloreality
{
	public class CameraImageView : MonoBehaviour {

		public Material matCameraView;

		public int cameraRequestWidth = 1920;
		public int cameraRequestHeight = 1080;

		public bool useFrontCamera = false;

		public bool openCameraAtStart = true;

		WebCamTexture texWebcam;

		#if UNITY_EDITOR
		public bool enabledInEditorMode = false;
		#endif

		void Start () {

			#if UNITY_EDITOR
			if (!enabledInEditorMode) {
				enabled = false;
				return;
			}
			#endif

			if (openCameraAtStart) {
				OpenCamera();
			}

		}

		public void OpenCamera(bool playCameraAfterward = true){
			StartCoroutine(CorOpenCamera());
		}

		public void PauseCamera(){
			if (texWebcam != null) {
				texWebcam.Pause();
			}
		}

		public void PlayCamera(){
			if (texWebcam != null) {
				texWebcam.Play();
			}
		}

		public void StopCamera(){
			if (texWebcam != null) {
				texWebcam.Stop();
			}
		}

		IEnumerator CorOpenCamera(bool playCameraAfterward = true){
			yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);  
			if (Application.HasUserAuthorization(UserAuthorization.WebCam))  
			{  
				WebCamDevice[] devices = WebCamTexture.devices; 

				for(int i = 0; i < devices.Length; i++){
					if(devices[i].isFrontFacing == useFrontCamera || devices.Length == 1){ // Need more specific doings for various camera cases
						if (cameraRequestWidth == -1 || cameraRequestHeight == -1) {
							texWebcam = new WebCamTexture(devices [i].name);
						} else {
							texWebcam = new WebCamTexture(devices [i].name, cameraRequestWidth, cameraRequestHeight);
						}
						if (playCameraAfterward) {
							texWebcam.Play();
						}
						matCameraView.mainTexture = texWebcam;
						break;
					}
				}


			}  
		}

		void OnApplicationQuit(){
			StopCamera();
		}

	}
}