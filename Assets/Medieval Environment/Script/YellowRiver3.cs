using UnityEngine;
using System.Collections;

public class YellowRiver3 : MonoBehaviour {
	public float speed;
	public float rotateBegin;
	public float rotateEnd;
	public int emissionRate;
	
	// Use this for initialization
	void Start () {
		emissionRate = 20;
		this.GetComponentInChildren <ParticleSystem> ().emissionRate = emissionRate;
		rotateBegin = 90f;
		rotateEnd = 100f;
		speed = 0.2f;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3 (0, 0, -speed));
		if (rotateBegin < rotateEnd) {
			if (transform.localEulerAngles.z > rotateEnd && this.GetComponentInChildren <ParticleSystem> ().emissionRate != 0)
				this.GetComponentInChildren <ParticleSystem> ().emissionRate = 0;
			if (transform.localEulerAngles.z > rotateBegin && transform.localEulerAngles.z < rotateBegin + 5
			    && this.GetComponentInChildren <ParticleSystem> ().emissionRate == 0)
				this.GetComponentInChildren <ParticleSystem> ().emissionRate = emissionRate;
		}
		else if (rotateEnd < rotateBegin) {
			if (transform.localEulerAngles.z > rotateEnd && transform.localEulerAngles.z < rotateBegin 
			    && this.GetComponentInChildren <ParticleSystem> ().emissionRate != 0)
				this.GetComponentInChildren <ParticleSystem> ().emissionRate = 0;
			if (transform.localEulerAngles.z > rotateBegin && transform.localEulerAngles.z < rotateBegin + 5
			    && this.GetComponentInChildren <ParticleSystem> ().emissionRate == 0)
				this.GetComponentInChildren <ParticleSystem> ().emissionRate = emissionRate;
		}
		
	}
}
