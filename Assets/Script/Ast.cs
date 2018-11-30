using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ast : MonoBehaviour {
    Rigidbody rigidbody;
    public static Ast ast;
    public float r_speed=5f;
    public float a_speed = -5f;
    public int Health = 20;
    public int GetScore = 10;
    public AudioClip hit;
    AudioSource audio;
	// Use this for initialization
    //爆炸效果
    public GameObject a_Explode;
    Transform transform;
    
	void Start () {
        if (GameController.gc.score >= 150 && GameController.gc.score < 300)
        {
            a_speed -= 1f;
        }
        else if (GameController.gc.score >= 500 && GameController.gc.score < 800)
        {
            a_speed -= 2f;
        }
        else if (GameController.gc.score >= 800)
        {
            a_speed -= 2f;
        }
        rigidbody = GetComponent<Rigidbody>();
        transform = GetComponent<Transform>();
        audio = GetComponent<AudioSource>();
        rigidbody.angularVelocity = Random.insideUnitSphere*r_speed;
        rigidbody.velocity=new Vector3(0, a_speed, 0);
        ast = this;
       
	}
 
	// Update is called once per frame
	void Update () {

	}
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Border")
        {
            Destroy(this.gameObject);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerBullet")
        {
            HealthChange(-5);
            //实例化爆炸
            this.audio.PlayOneShot(hit);
            Destroy(other.gameObject);
        }
        else if (other.tag == "Player")
        {
            HealthChange(-20);
        }
    }
    void HealthChange(int num)
    {
        Health += num;
        if (Health <= 0)
        {
            Instantiate(a_Explode, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            GameController.gc.ReScore(GetScore);
        }
    }
}
