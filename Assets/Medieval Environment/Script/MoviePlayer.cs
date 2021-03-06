using UnityEngine;
using System.Collections;

public delegate void OnFinished();
public abstract class MoviePlayer : MonoBehaviour
{
	public bool m_Skippable = false;
	protected ushort m_AudioID;
	protected OnFinished m_OnMovieFinished;
	
	public bool skippable
	{
		set { m_Skippable = value; }
		get { return m_Skippable; }
	}

	public ushort audioID
	{
		set { m_AudioID = value; }
		get { return m_AudioID; }
	}
	
	public virtual void Initialise() {}
	public abstract void PlayMovie(string path);
	
	protected virtual void _NotifyMovieFinished()
	{
		if (null != m_OnMovieFinished)
			m_OnMovieFinished();
	}
}