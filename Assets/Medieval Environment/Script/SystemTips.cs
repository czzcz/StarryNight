using UnityEngine;
using System;
using System.Collections;

public class SystemTips:MonoBehaviour {
	bool bFirstTime = true;
	bool bStart = false;
	Vector3 orgSpriteScale;
    public UILabel lblTips;
	public UISprite bgSprite;


    public static SystemTips Instance = null;

    void Awake()
    {
        Instance = this;
    }

    void OnDestroy()
    {
        bgSprite = null;
        Instance = null;
    }

	void Start() 
	{
		InitData();
	}
	
	void InitData()
	{
		bStart = true;
		bFirstTime = true;
		orgSpriteScale = new Vector3(); 
	}
		
	void OnEnable()
	{
		if (!bStart)
			return;
	}
 
	
	public void SetLblText(string text)
	{
        lblTips.text = text; 
		if(bFirstTime)
		{
            orgSpriteScale = bgSprite.transform.localScale;
			bFirstTime = false;
		}
		else
		{
            bgSprite.transform.localScale = orgSpriteScale;
		}

        Vector3 test = bgSprite.transform.localScale + new Vector3(0, 1, 0);
        iTween.ScaleTo(bgSprite.gameObject, iTween.Hash("isLocal", true, "scale", test, "time", 0.2f, "easeType", "easeInCubic", "onComplete", "_OnScaleFinish", "oncompletetarget", gameObject));
	}
	
	void _OnScaleFinish()
	{
		iTween.MoveTo(lblTips.gameObject,iTween.Hash("islocal", true, "position", lblTips.transform.localPosition,"loopType", "none",
													 "onComplete", "_OnFinishOne", "time", 0.2f,"oncompletetarget", gameObject));
	}
	void _OnFinishOne()
	{	 
		iTween.MoveTo(lblTips.gameObject,iTween.Hash("islocal", true, "position", lblTips.transform.localPosition,"loopType", "none","time",1.2f,"onComplete", "_OnFinishTwo", "oncompletetarget", gameObject));
	}
	
	void _OnFinishTwo()
	{
		lblTips.text = "";
        Vector3 test = bgSprite.transform.localScale + new Vector3(0, -1, 0);
        iTween.ScaleTo(bgSprite.gameObject, iTween.Hash("isLocal", true, "scale", test, "time", 0.2f, "easeType", "easeInCubic", "onComplete", "_OnFinish", "oncompletetarget", gameObject));
	}
	
	void _OnFinish()
	{
	} 
}
