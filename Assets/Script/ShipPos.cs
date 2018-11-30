using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPos : MonoBehaviour {
    Transform transform;
    public GameObject ship;
	// Use this for initialization
	void Start () {
		transform=GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        if(ship){
        this.transform.position = ship.transform.position;
        }
	}
}
