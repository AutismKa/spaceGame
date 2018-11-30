using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour {
    Rigidbody rigidbody;
    public float speed = 10f;


	void Start () {
        rigidbody=GetComponent<Rigidbody>();
        rigidbody.velocity = new Vector3(0,speed,0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider other)
    {

    }

}
