using UnityEngine;
using System.Collections;

public class SetBackgroundImage : MonoBehaviour 
{
	[Tooltip("GUI-texture used to display the color camera feed on the scene background.")]
	public GUITexture backgroundImage;

	[Tooltip("Camera that will be set-up to display 3D-models in the Kinect FOV.")]
	public Camera mainCamera;


	void Start()
	{
		KinectManager manager = KinectManager.Instance;
		
		if(manager && manager.IsInitialized())
		{
			KinectInterop.SensorData sensorData = manager.GetSensorData();

			if(backgroundImage && (backgroundImage.texture == null))
			{
				backgroundImage.texture = manager.GetUsersClrTex();
			}

			if(mainCamera != null && sensorData != null && sensorData.sensorInterface != null)
			{
				mainCamera.fieldOfView = sensorData.colorCameraFOV;

				Vector3 mainCamPos = mainCamera.transform.position;
				mainCamPos.x += sensorData.faceOverlayOffset;
				mainCamera.transform.position = mainCamPos;
			}
		}
	}

}
