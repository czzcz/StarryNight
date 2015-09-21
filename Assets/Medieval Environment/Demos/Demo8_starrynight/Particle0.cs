using UnityEngine;
using System.Collections;

public class Particle0 : MonoBehaviour {
	public Vector3 pos,pos0; 
	public bool over = false;
	int i;
	float sp;
	public GameObject clone;
	public int time;
	// Use this for initialization
	void Start () {
		this.GetComponent<ParticleSystem> ().emissionRate = 20;
		transform.position = pos0;
		sp = 30;
		if (Vector3.Distance (pos0, pos) > 5)
			sp = 6 * Vector3.Distance(pos0,pos);
	}

	// Update is called once per frame
	void Update () {
		i++;
		if (i < 60)
		transform.position += (pos - pos0) / sp; 
		if (i < 30)
			this.GetComponent<ParticleSystem> ().emissionRate = 30;
		if (i == 40) { 
			this.GetComponent<ParticleSystem> ().emissionRate = 0;
			if(time >= 0){
				GameObject go = Instantiate (clone, pos0, Quaternion.identity) as GameObject;
				go.transform.parent = transform.parent;
				go.GetComponent<Particle0> ().pos = pos;
				go.GetComponent<Particle0> ().pos0 = pos0;
				go.GetComponent<Particle0> ().time = time - 1;
			}
		}
		if (i == 80) 
			Destroy (this.gameObject);
	}
}
