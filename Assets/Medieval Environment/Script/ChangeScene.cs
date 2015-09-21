using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour {

    public GameObject CurrScene;
    public GameObject TarScene;

	// Use this for initialization
	public void OnClick(Object obj)
    {
        CurrScene.SetActive(false);
        TarScene.SetActive(true);
    }
}
