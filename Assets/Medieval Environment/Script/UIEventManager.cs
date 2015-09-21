using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[Serializable]
public class ImageAtlasInfo
{
    public int imageId;
    public UIAtlas atlas;
}

public class UIEventManager : MonoBehaviour 
{
    DataManager dataMgr = null;

    public UISprite SingleImage;

    public List<ImageAtlasInfo> imageAtlasInfos = new List<ImageAtlasInfo>();

    public int CurrClickImageID = 1;

    public bool showTestBtn = true;

    void Start()
    {
        dataMgr = FindObjectOfType(typeof(DataManager)) as DataManager; 
    }

    public void ShowAllImage()
    {
        UIManager.Instance.Show(pageType.GlobalUI2);
    }
	 
    public void CloseShowAllImagePage()
    {
        UIManager.Instance.Show(pageType.GlobalUI1);
    }

    public void ShowSingleImage(GameObject obj)
    {
        UIManager.Instance.Show(pageType.ImageUI2);
        UISprite uiObj = obj.GetComponent<UISprite>();
        if (uiObj != null && imageAtlasInfos.Find((ImageAtlasInfo iai) => { if (iai.imageId.ToString() == uiObj.spriteName) return true; return false; })!=null)
        {
            SingleImage.atlas = uiObj.atlas;
            SingleImage.spriteName = uiObj.spriteName;
            SingleImage.MakePixelPerfect();
            //SingleImage.gameObject.transform.localScale = new Vector3(2,2,1);
        }
        else
        { 
            ImageAtlasInfo imageAtlasInfo = imageAtlasInfos.Find((ImageAtlasInfo iai) => { if (iai.imageId == CurrClickImageID) return true; return false; });
            if (imageAtlasInfo != null)
            {
                SingleImage.atlas = imageAtlasInfo.atlas;
                SingleImage.spriteName = imageAtlasInfo.imageId.ToString();
                SingleImage.MakePixelPerfect();
            }
        } 
    }

    public void CloseSingleImage()
    {
        UIManager.Instance.Hide(pageType.ImageUI2);
        UIManager.Instance.OpenPrePage();
    } 

    void playMovie<T>(string path, bool skipIt = false, ushort audidID = 0) where T : MoviePlayer
    {
        GameObject newMovieObj = new GameObject("MoviePlayer");
        T player = newMovieObj.AddComponent<T>();
        player.skippable = skipIt;
        player.audioID = audidID;
        player.Initialise();
        player.PlayMovie(path);
    }

    void PlayVideo(string name)
    {
        playMovie<HandHeldPlayer>("Movies/" + name, false);
//#if UNITY_IOS
//        iPhoneGeneration iOSGen = iPhone.generation;
//        if (iOSGen == iPhoneGeneration.iPhone4 || iOSGen == iPhoneGeneration.iPhone4S ||
//            iOSGen == iPhoneGeneration.iPhone5 || iOSGen == iPhoneGeneration.iPhone5C || iOSGen == iPhoneGeneration.iPhone5S)
//        {
//            playMovie<HandHeldPlayer>("Movies/" + name, CloseMovie, false);
//        }
//        else
//        {
//            playMovie<MobileMovieTexPlayer>("Movies/" + name, CloseMovie, true);
//        }
//#elif UNITY_ANDROID
//        playMovie<HandHeldPlayer>("Movies/" + name, CloseMovie, true);
//#else
//        playMovie<MobileMovieTexPlayer>("Movies/" + name, CloseMovie, true);
//#endif
    }
     
    public void PlayGlobalVideo()
    {
        //UIManager.Instance.Show(pageType.Video1);
        PlayVideo(dataMgr.GetString("GlobalVideo"));
    }

    public void PlayImageVideo()
    {
       // UIManager.Instance.Show(pageType.Video1);
        PlayVideo(dataMgr.GetString("Image" + CurrClickImageID+"Video"));
    }

    public void CloseMovie()
    {

        //MobileMovieTexPlayer mmtp = FindObjectOfType(typeof(MobileMovieTexPlayer)) as MobileMovieTexPlayer;
        //if (mmtp != null)
        //{
        //    mmtp.SkipMovie();
        //    Destroy(mmtp);
        //}

        HandHeldPlayer hhp = FindObjectOfType(typeof(HandHeldPlayer)) as HandHeldPlayer;
        if (hhp != null)
        {
            Destroy(hhp);
        }

        if (UIManager.Instance.currPage == pageType.Video1)
        {
            UIManager.Instance.Hide(pageType.Video1); 
            UIManager.Instance.OpenPrePage();
        }
    }


    public void ShowImage()
    {
        UIManager.Instance.Show(pageType.ImageUI1);
    } 


    public void OpenQuestionsPage()
    {
        UIManager.Instance.Show(pageType.DaTi);
    }

    public void CloseQuestionsPage()
    {
        UIManager.Instance.Hide(pageType.DaTi);
        UIManager.Instance.OpenPrePage();
    } 
}
