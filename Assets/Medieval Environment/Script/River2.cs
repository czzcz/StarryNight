using UnityEngine;
using System.Collections;

public class River2 : MonoBehaviour {
	public float speed;
	public float rota;
	float abs;
	// Use this for initialization
	void Start () {
		speed = 0.12f;
		rota = 12f;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (0, 0, speed);
		if (transform.localEulerAngles.z > rota && this.GetComponentInChildren <ParticleSystem> ().emissionRate != 0)
			this.GetComponentInChildren <ParticleSystem> ().emissionRate = 0;
		if (transform.localEulerAngles.z>0 && transform.localEulerAngles.z < 1 && this.GetComponentInChildren <ParticleSystem> ().emissionRate == 0)
			this.GetComponentInChildren <ParticleSystem> ().emissionRate = 20;
		abs = 1 - (transform.localEulerAngles.z) / 90;
		transform.localScale = new Vector3 (abs, abs, abs);
	}
}
