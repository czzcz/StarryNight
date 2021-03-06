using UnityEngine;
using System.Collections;

public class HandHeldPlayer : MoviePlayer
{
	bool b_AlreadyClicked = false;

	public override void PlayMovie(string path)
	{
		StartCoroutine(__PlayMovie(path));
	}
	
	IEnumerator __PlayMovie(string path)
	{
		Handheld.PlayFullScreenMovie(path, Color.black,FullScreenMovieControlMode.CancelOnInput, FullScreenMovieScalingMode.Fill); 
		yield return new WaitForSeconds(4.5f);
         
		if (b_AlreadyClicked)
		{
			Debug.Log("b_AlreadyClicked == true");
			yield break;
		}
		
		Debug.Log("b_AlreadyClicked == false");
		b_AlreadyClicked = true;
         
		GameObject.Destroy(gameObject); 
		//if (null != callback)
		//	callback();
	}

	/*
	MobileMovieTexture m_MobileMovieTexture;
	GameObject m_Container;
	OnFinished m_OnMovieFinished;
	
	public override void Initialise()
	{
		Object template = Resources.Load("Others/TouchSkipPlayer");
		if (null == template)
		{
			GameObject.Destroy(gameObject);
			return;
		}
		
		m_Container = GameObject.Instantiate(template) as GameObject;
		m_Container.transform.parent = transform;
		m_Container.transform.localPosition = Vector3.zero;
		m_Container.transform.localScale = Vector3.one;
		
		m_MobileMovieTexture = m_Container.GetComponentInChildren<MobileMovieTexture>();
		if (null != m_MobileMovieTexture)
			m_MobileMovieTexture.onFinished += __OnMovieFinished;
	}
	
	public void SkipMovie()
	{
		__OnMovieFinished(null);
	}
	
	public override void PlayMovie(string path, OnFinished callback)
	{
		string suffix = path.Substring(path.LastIndexOf("."));
		path = path.Replace(suffix, ".ogv");
		
		m_OnMovieFinished = callback;
		if (null == m_MobileMovieTexture)
			__OnMovieFinished(null);
		else
		{
			m_MobileMovieTexture.Path = path;
			m_MobileMovieTexture.Play();
		}
	}
	
	void __OnMovieFinished(MobileMovieTexture tex)
	{
		if (null != m_OnMovieFinished)
			m_OnMovieFinished();
		
		GameObject.Destroy(gameObject);
	}
	*/
//#endif
}