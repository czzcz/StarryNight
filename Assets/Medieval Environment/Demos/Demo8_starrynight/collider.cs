using UnityEngine;
using System.Collections;

public class collider : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "particle") {
			GetComponent<ParticleSystem> ().emissionRate = 0;
			GetComponent<ParticleSystem> ().Clear();
		} 
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "particle") {
			GetComponent<ParticleSystem> ().emissionRate = 10;
		} 
	}
}
