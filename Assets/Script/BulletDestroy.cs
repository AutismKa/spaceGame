using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerExit(Collider other)
    {
        //把背景添加tag为border并且选择为border 判断是否子弹撞在border，然后决定是否销毁子弹
        if (other.tag == "Border")
        {
            Destroy(this.gameObject);
        }
    }
}
