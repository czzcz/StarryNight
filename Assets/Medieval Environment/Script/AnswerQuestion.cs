using UnityEngine;
using System.Collections;
using System;

public class AnswerQuestion : MonoBehaviour
{
    public TweenScale UIAni;
    public GameObject[] AnswerObjs;
    public UISprite[] Toggles;
    public UILabel[] AnsTexts;
    public UILabel Title;

    public DataManager dataMgr;

    int currIndex = 0;
    int answer = 0;

    void OnEnable()
    {
        currIndex = 0;
        setQues();
    }

    void setQues()
    {
        Question que = dataMgr.GetQues(currIndex);
        if (que == null)
            return;
        Title.text = que.title;
        for(int i=0;i<Toggles.Length;i++ )
        {
            Toggles[i].enabled = false;
        }

        for (int i = 0; i < que.anss.Count;i++)
        {
            AnsTexts[i].text = que.anss[i];
        }
        playEffect();
        answer = -1;
    }

    void playEffect()
    {
        UIAni.gameObject.transform.localScale = Vector3.zero;
        UIAni.from = Vector3.zero;
        TweenScale.Begin(UIAni.gameObject, 0.3f, UIAni.to);
    }

    public void ClickToggle(GameObject obj)
    {
        for (int i = 0; i < Toggles.Length; i++)
        {
            if (obj == Toggles[i].gameObject)
            {
                if (Toggles[i].enabled)
                { 
                    Toggles[i].enabled = false;
                    answer = -1;
                }else
                {
                    Toggles[i].enabled = true;
                    answer = Convert.ToInt32(obj.name);
                }
            }else
            { 
               Toggles[i].enabled = false; 
            }
        }
    }

    public void NextQue()
    {
        int count = dataMgr.GetQuesCount();
        if (count == 0) return;
        currIndex = (currIndex + 1) % dataMgr.GetQuesCount();
        setQues();
    }

    public void CommAns()
    {
        Question que = dataMgr.GetQues(currIndex);
        if(que.rightAns==answer)
        {
            SystemTips.Instance.SetLblText("回答正确");
        }else
        {
            SystemTips.Instance.SetLblText("回答错误");
        }
    }
}
