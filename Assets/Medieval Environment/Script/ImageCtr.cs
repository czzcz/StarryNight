using UnityEngine;
using System.Collections;

public class ImageCtr : MonoBehaviour {

    public GameObject imageObj;
    bool isRotate = false;

    public void ScaleImage(float scaleValue)
    {
        imageObj.transform.localScale += imageObj.transform.localScale * scaleValue;
    }
	  
    public void RotateImage(float value)
    {
        imageObj.transform.Rotate(Vector3.forward,value, Space.Self);
    }

    public void ExitRotate()
    {
        imageObj.transform.localRotation = Quaternion.identity;
    }
}
