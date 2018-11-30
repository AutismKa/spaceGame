using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatHealth : MonoBehaviour {
    Rigidbody rigidbody;
    Transform transform;
    public float h_speed = -5f;
	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
        transform = GetComponent<Transform>();
        rigidbody.velocity = new Vector3(0, h_speed, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

                PlayerController.pc.HealthChange(10);

            Destroy(this.gameObject);


        }
    }
}
