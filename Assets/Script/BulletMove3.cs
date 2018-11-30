using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove3 : MonoBehaviour
{

    Rigidbody rigidbody;
    public float speed = 10f;
    public GameObject ship;
    Transform transform;
    Vector3 dir;
    public float fireWait=2f;
    public int num=0;
    public static BulletMove3 bm3;


    void Start()
    {
        ship = GameObject.Find("shippos");
        transform = GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody>();
        bm3 = this;
        StartCoroutine(FireWait());

    }

    // Update is called once per frame
    void Update()
    {
        if(num==1){
            transform.Translate(dir.normalized * speed * Time.deltaTime);
        }

    }
    IEnumerator FireWait()
    {
        yield return new WaitForSeconds(fireWait);
        num++;
        dir = ship.transform.position - this.transform.position;  //获取目标位置其实就是用目标位置减这个目标位置   
        
       
    }
}
