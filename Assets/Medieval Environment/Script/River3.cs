﻿using UnityEngine;
using System.Collections;

public class River4 : MonoBehaviour {
	public float speed;
	public float rota;
	float abs;
	// Use this for initialization
	void Start () {
		speed = 0.5f;
		rota = 260;
		this.GetComponentInChildren <ParticleSystem> ().emissionRate = 0;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (0, 0, -speed);
		if (transform.localEulerAngles.z < rota && this.GetComponentInChildren <ParticleSystem> ().emissionRate != 0)
			this.GetComponentInChildren <ParticleSystem> ().emissionRate = 0;
		if (transform.localEulerAngles.z > 304 && transform.localEulerAngles.z < 305 && this.GetComponentInChildren <ParticleSystem> ().emissionRate == 0)
			this.GetComponentInChildren <ParticleSystem> ().emissionRate = 20;
	}
}