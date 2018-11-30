using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove2 : MonoBehaviour {

    Rigidbody rigidbody;
    public float speed = 10f;
    public GameObject ship;
    Transform transform;
    Vector3 dir;
   

    void Start()
    {
       if (GameController.gc.score >= 800)
        {
            speed += 2f;
        }
        ship = GameObject.Find("shippos");
        transform = GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody>();
        dir = ship.transform.position- this.transform.position ;  //获取目标位置其实就是用目标位置减这个目标位置   
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
        transform.Translate(dir.normalized* speed *Time.deltaTime);
    }
}
