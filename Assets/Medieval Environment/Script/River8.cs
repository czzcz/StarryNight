using UnityEngine;
using System.Collections;

public class River8 : MonoBehaviour {
	public float speed;
	public float rota;
	float abs;
	// Use this for initialization
	void Start () {
		speed = 0.24f;
		rota = 280f;
		this.GetComponentInChildren <ParticleSystem> ().emissionRate = 0;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (0, 0, speed);
		if (transform.localEulerAngles.z > rota && this.GetComponentInChildren <ParticleSystem> ().emissionRate != 0)
			this.GetComponentInChildren <ParticleSystem> ().emissionRate = 0;
		if (transform.localEulerAngles.z > 235 && transform.localEulerAngles.z < 236 && this.GetComponentInChildren <ParticleSystem> ().emissionRate == 0)
			this.GetComponentInChildren <ParticleSystem> ().emissionRate = 20;
	}
}
