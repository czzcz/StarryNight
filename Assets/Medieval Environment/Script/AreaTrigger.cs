using UnityEngine;
using System.Collections;

public class AreaTrigger : MonoBehaviour {
     
    public string showText = "基弗尔工作室";

    void OnTriggerEnter(Collider other)
    {
        SystemTips.Instance.SetLblText(showText);
    }
}
