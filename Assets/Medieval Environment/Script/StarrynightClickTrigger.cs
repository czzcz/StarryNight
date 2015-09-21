using UnityEngine;
using System.Collections;

public class StarrynightClickTrigger : MonoBehaviour {

    public string showText;
     
    void OnClick()
    {
        StarrynightUIManager.Instance.Show(showText); 
    }
     
}
