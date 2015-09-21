using UnityEngine;
using System.Collections;

public class RiverF : MonoBehaviour {
	//初始化新的数组记录鼠标位置
	public Vector3 pos;
	public Vector3 pos0;
	public GameObject particle;
	public bool newparticle = true;
	int i;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {   

	}

	Vector3 getposition(Vector3 vec){
		Ray ray = Camera.main.ScreenPointToRay(Event.current.mousePosition);
		RaycastHit hit;
		Physics.Raycast(ray, out hit);
		return new Vector3(hit.point.x, -hit.point.y,30);
	}

	void creatparticle(){
	    GameObject go = Instantiate (particle, pos0, Quaternion.identity) as GameObject;
		go.transform.parent = transform;
		go.GetComponent<Particle0> ().pos = getposition(Event.current.mousePosition);
		go.GetComponent<Particle0> ().pos0 = pos0;
		go.GetComponent<Particle0> ().time = 3;
	}
	
	void OnGUI(){
		if (Event.current.type == EventType.MouseDown) {
			pos0 = getposition (Event.current.mousePosition);
		}
		if (Event.current.type == EventType.MouseDrag) {
			if(Vector3.Distance(pos0,getposition (Event.current.mousePosition)) > 3)
			{
		       creatparticle ();
		       pos0 = getposition (Event.current.mousePosition);		
			}
		}
	
	}

}
