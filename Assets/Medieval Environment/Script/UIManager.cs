using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;


public enum pageType
{
    GlobalUI1,
    GlobalUI2,
    GlobalUI3,
    GlobalUI4,
    GlobalUI5,
    GlobalUI6,
    GlobalUI7,
    GlobalUI8,
    GlobalUI9,
    GlobalUI10,
    ImageUI1,
    ImageUI2,
    ImageUI3,
    ImageUI4,
    ImageUI5,
    ImageUI6,
    ImageUI7,
    ImageUI8,
    ImageUI9,
    Video1,
    DaTi,
}

[Serializable]
public class UIPair
{
    public pageType type;
    public GameObject uiRoot;
}

public class UIManager : MonoBehaviour {

    public List<UIPair> uiList; 

    public static UIManager Instance = null;

    DataManager dataMgr = null;

    public UILabel textObj;
     
    public UILabel GlobalDesc;

    bool isShow = false;

    public pageType previousPage = pageType.GlobalUI1;
    public pageType currPage = pageType.GlobalUI1;
    public bool IsShow
    {
        get
        {
            return isShow;
        }
    }

    void Awake()
    {
        Instance = this;

        for (int i = 0; i < uiList.Count;i++ )
        {
            uiList[i].uiRoot.SetActive(false); 
        }
    }


    void Start()
    {
        dataMgr = FindObjectOfType(typeof(DataManager)) as DataManager; 
    }

    void OnDestroy()
    { 
        Instance = null;
    }

    public void Show(pageType type)
    { 
        if (type== pageType.GlobalUI1)
        {
            GlobalDesc.text = dataMgr.GetString("GlobaDesc");
        }

        isShow = true;

        if (previousPage != currPage &&previousPage!= currPage)
            previousPage = currPage;

        currPage = type;
        
        for (int i = 0; i < uiList.Count; i++)
        {
            if (type == uiList[i].type) 
                uiList[i].uiRoot.SetActive(true);
            else
                uiList[i].uiRoot.SetActive(false);
        }
    }

    public void HideAll()
    {
        isShow = false;
        for (int i = 0; i < uiList.Count; i++)
        {
            uiList[i].uiRoot.SetActive(false);
        }
    }

    public void Hide(pageType type)
    {  
        for (int i = 0; i < uiList.Count; i++)
        {
            if (type == uiList[i].type)
                uiList[i].uiRoot.SetActive(false); 
        } 
    }

    public void OpenPrePage()
    {
        Show(previousPage);
    } 
}
