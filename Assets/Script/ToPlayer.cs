using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToPlayer : MonoBehaviour {
    Transform transform;
    Rigidbody rigidbody;
    public GameObject shippos;
    Vector3 dir;
    
	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
        transform = GetComponent<Transform>();
        shippos = GameObject.Find("shippos");
        dir = shippos.transform.position - this.transform.position;
        transform.LookAt(shippos.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
