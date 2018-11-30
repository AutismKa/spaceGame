using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour {
    public float DestroyTime = 2f;
	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, DestroyTime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
