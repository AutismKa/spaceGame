using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    Rigidbody rigidbody;
    Transform transform;
    public float speed = 3f;
    public GameObject m_bullet;
    public GameObject e_Explode;
    public Transform m_firepos;
    public float WaitFire=0.25f; //子弹发射每颗等待时间
    public float WaitFireWave = 1f;
    public int Health = 15;
    public int GetScore=15;
    public AudioClip hit;
    AudioSource audio;
	// Use this for initialization
	void Start () {
        if (GameController.gc.score >= 800)
        {
            speed -= 1f;
        }
        rigidbody = GetComponent<Rigidbody>();
        transform = GetComponent<Transform>();
        audio = GetComponent<AudioSource>();
        rigidbody.velocity = new Vector3(0,speed,0);
        StartCoroutine(OnFire());
        
	}
	
	// Update is called once per frame
    //敌军发射子弹调整
    IEnumerator OnFire()
    {
        while (true)
        {
            int rd = Random.Range(1, 5);
            for (int i = 1; i < rd; i++)
            {
                Instantiate(m_bullet, m_firepos.position, Quaternion.identity);
                yield return new WaitForSeconds(WaitFire);
            }
            yield return new WaitForSeconds(WaitFireWave);
        }
    }
    void Update()
    {

	}
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerBullet")
        {
            HealthChange(-5);
            this.audio.PlayOneShot(hit);
            Destroy(other.gameObject);
            
        }else if(other.tag=="Player"){
            HealthChange(-15);
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
            int rd2 = Random.Range(0, 9);
            if (rd2 == 1 && GameController.gc.score>=200)
            {
                GameController.gc.addPower();
            }
            
            
        }
    }
}
