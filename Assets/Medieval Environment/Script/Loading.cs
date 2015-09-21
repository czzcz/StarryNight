using UnityEngine;
using System.Collections;

public class Loading : MonoBehaviour 
{ 
	// Use this for initialization
    public UISlider slider;
    public void UpdateValue(float value)
    {
        slider.value = value;
    }

}
