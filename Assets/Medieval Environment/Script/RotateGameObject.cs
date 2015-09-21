using UnityEngine;
using System.Collections;

public class RotateGameObject : MonoBehaviour {

    public float speed = 1;  
	void Update () {
        transform.Rotate(Vector3.forward, speed*Time.deltaTime * 100, Space.Self);	}
}
