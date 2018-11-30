using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUP : MonoBehaviour {
    Rigidbody rigidbody;
    Transform transform;
    public float p_speed = -5f;
	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
        transform = GetComponent<Transform>();
        rigidbody.velocity = new Vector3(0, p_speed, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {

            PlayerController.pc.PowerUP += 1;
            Destroy(this.gameObject);
            

        }
    }
}
