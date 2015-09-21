using UnityEngine;
using System.Collections;

public class StarrynightUIManager : MonoBehaviour 
{
    public GameObject uiRoot;
    public UILabel textObj;

    static StarrynightUIManager instance;
    public static StarrynightUIManager Instance
    {
        get
        {
            return instance;
        }
    }
   
    void Awake()
    {
        instance = this;
    }

    void OnDestroy()
    {
        instance = null;
    }

    public void ReturnToMainScene()
    {
        uiRoot.SetActive(false);
    }

    public bool IsShow()
    {
        return uiRoot.activeSelf;
    }

    public void Show(string text)
    {
        uiRoot.SetActive(true);
        textObj.text = text;
    }

    public void Hide()
    {
        uiRoot.SetActive(false);
    }

}
