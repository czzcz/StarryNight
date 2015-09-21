using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;


//[System.Serializable]
public class Question
{
    public string title;
    public List<string> anss = new List<string>();
    public int rightAns = 0;
}

public class DataManager : MonoBehaviour
{
    Dictionary<string, string> datas = new Dictionary<string,string>();
	// Use this for initialization

    List<Question> quesList = new List<Question>();

    string baseResPath = null;
	void Awake () { 
        if (Application.platform == RuntimePlatform.Android)
        {
            baseResPath = "jar:file://" + Application.dataPath + "!/assets/";
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            baseResPath = "file://" + Application.dataPath + "/Raw/";
        }
        else
        {
            baseResPath = "file://" + Application.dataPath + "/StreamingAssets/";
        }

        StartCoroutine(LoadData());
        StartCoroutine(LoadQuestions());
	}

    IEnumerator LoadQuestions()
    {
        WWW www = new WWW(baseResPath + "questions.txt");
        yield return www;

        if (www.error != null)
        {
            Debug.LogError("加载数据错误" + www.error);
            yield break;
        }
        parseQusData(www.text); 
    }

    void parseQusData(string str)
    {
        TextReader reader = new StringReader(str);
        string line; 

        while ((line = reader.ReadLine()) != null)
        {

            if (line == string.Empty)
                continue;

            string[] values = line.Split('=');
            if (values.Length < 2)
                continue;

            if (values[0].Contains("Question"))
            {
                Question que = new Question();
                que.title = values[1].Trim().Replace("\\n", "\n");
                for (int i = 0; i < 5; i++)
                {
                    line = reader.ReadLine();
                    if (line != null)
                    {
                        string[] strs = line.Split('=');
                        if (strs.Length==1)
                        { 
                            que.anss.Add(line.Trim().Replace("\\n", "\n"));
                        }else
                        {
                            if(strs[0].Contains("RightAns"))
                            {
                                que.rightAns = Convert.ToInt32(strs[1]) - 1;
                            }
                        }
                    }
                }
                quesList.Add(que);
                //Debug.LogError( "que.title  "+ que.title);
                //Debug.LogError(que.anss[0]);
                //Debug.LogError(que.anss[1]);
                //Debug.LogError(que.anss[2]);
                //Debug.LogError(que.anss[3]);
                //Debug.LogError(que.rightAns);
            }
        } 
        reader.Close();
    }


    public int GetQuesCount()
    {
        return quesList.Count;
    }

    public Question GetQues(int index)
    {
        if (index<0 || quesList.Count < index + 1)
        {
            return null;
        }

        return quesList[index];
    }
     
    IEnumerator LoadData()
    { 
        WWW www = new WWW(baseResPath+"data.txt");
        yield return www;

        if (www.error != null)
        {
            Debug.LogError("加载数据错误" + www.error);
            yield break;
        } 
        parseData(www.text); 
    }


    void parseData(string str)
    {
        TextReader reader = new StringReader(str);
        string line;
        while ((line = reader.ReadLine()) != null)
        {

            if (line == string.Empty)
                continue;

            string[] values = line.Split('=');
            if (values.Length < 2)
                continue;

            values[1] = values[1].Trim().Replace("\\n", "\n"); 

            datas.Add(values[0], values[1]);
        }

        reader.Close();
    }

	
    public string GetString(string key)
    {
        string str = "";
        datas.TryGetValue(key,out str);
        return str;
    }

}
