using UnityEngine;
using System.Collections;

public class River17 : MonoBehaviour {
	public float speed;
	public float rota;
	float abs;
	
	// Use this for initialization
	void Start () {
		this.GetComponentInChildren <ParticleSystem> ().emissionRate = 10;
		rota = 30;
		speed = 0.2f;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3 (0, 0, speed));
		if (transform.localEulerAngles.z > rota && this.GetComponentInChildren <ParticleSystem> ().emissionRate != 0)
			this.GetComponentInChildren <ParticleSystem> ().emissionRate = 0;
		if (transform.localEulerAngles.z>0&&transform.localEulerAngles.z<1 && this.GetComponentInChildren <ParticleSystem> ().emissionRate == 0)
			this.GetComponentInChildren <ParticleSystem> ().emissionRate = 10;
		/*if (transform.localEulerAngles.z> 0 && transform.localEulerAngles.z < 135)
		   abs = 1 - Mathf.Abs (180 - transform.localEulerAngles.z) / 300;
		transform.localScale = new Vector3 (abs, abs, abs);*/
	}
}
