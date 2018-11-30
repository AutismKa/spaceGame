using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Controller : MonoBehaviour {
    Transform transform;
    Rigidbody rigidbody;
    public int Health=10;
    public float speed = 5f;
    public GameObject shippos;
    public GameObject e_Explode;
    public AudioClip hit;
    public float FlyRotation = 8f;
    public GameObject e_bullet;
    public Transform e_firepos;
    public float WaitFire=1;
    public bool OpenFireOK=false;
    GameObject jet;
    int GetScore = 15;
    AudioSource audio;
    Vector3 dir;
    bool Switch = true;
    public static Enemy2Controller e2c;
	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
        transform = GetComponent<Transform>();
        audio = GetComponent<AudioSource>();
        shippos = GameObject.Find("shippos");
        dir = shippos.transform.position - this.transform.position;
        
        e2c = this;
        
	}
    IEnumerator OnFire()
    {
        while (OpenFireOK)
        {

                Instantiate(e_bullet, e_firepos.position, Quaternion.identity);
                yield return new WaitForSeconds(WaitFire);
        }
    }
	
	// Update is called once per frame
	void Update () {
       transform.Translate(dir.normalized*Time.deltaTime*speed);
       if (GameController.gc.score >= 500)
       {
          OpenFireOK = true;
           if(Switch){
          fire();
          Switch = false;
           }
       }
       
	}
    void fire()
    {
        StartCoroutine(OnFire());
    }
    void  OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player"){
            
            HealthChange(-10);
            

        }else if(other.tag=="PlayerBullet"){
            HealthChange(-5);
            this.audio.PlayOneShot(hit);
            Destroy(other.gameObject);
            
        }

    }
    void HealthChange(int num)
    {
        Health += num;
        if (Health <= 0)
        {

            Instantiate(e_Explode, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            GameController.gc.ReScore(GetScore);
        }
    }
}
