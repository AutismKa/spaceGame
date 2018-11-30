using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour {
    Rigidbody rigidbody;
    Transform transform;
    AudioSource audio;
    public float speed=5f;
    public int Health = 300;
    public int GetScore = 50;
    public GameObject b_bullet;
    public GameObject b_Explode;
    public Transform b_firepos;
    public GameObject BossHp;
    public AudioClip hit;
    public float WaitFire = 0.25f; //子弹发射每颗等待时间
    public float WaitFireWave = 1f;
    int num=2;//给一个2的力

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
        transform = GetComponent<Transform>();
        audio = GetComponent<AudioSource>();
        StartCoroutine(OnFire());
        BossHp = GameObject.Find("Canvas/BOSSHP");
        GameController.gc.ReBossHp(Health);
	}
    IEnumerator OnFire()
    {
        while (true)
        {
            int rd = Random.Range(1, 5);
            for (int i = 1; i < rd; i++)
            {
                Instantiate(b_bullet, b_firepos.position, Quaternion.identity);
                yield return new WaitForSeconds(WaitFire);
            }
            yield return new WaitForSeconds(WaitFireWave);
        }
    }

	// Update is called once per frame
	void Update () {
        if (this.transform.position.y >= 3.13)
        {
            rigidbody.velocity = new Vector3(0, -speed, 0);
        }
        else if (PlayerController.pc.border.minX+0.5 >this.transform.position.x)
        {
            rigidbody.velocity = new Vector3(0, 0, 0);
            rigidbody.velocity = new Vector3(speed, 0, 0);

        }
        else if (PlayerController.pc.border.maxX - 0.5 < this.transform.position.x)
        {
            rigidbody.velocity = new Vector3(0, 0, 0);
            rigidbody.velocity = new Vector3(-speed, 0, 0);
        }else if(this.transform.position.y<3.13 &&num==2){
            rigidbody.velocity = new Vector3(num, 0, 0);
            num = 0;
        }
        
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerBullet")
        {
            HealthChange(-5);
            this.audio.PlayOneShot(hit);
            Destroy(other.gameObject);

        }
        else if (other.tag == "Player")
        {
            HealthChange(-0);
        }
    }
    void HealthChange(int num)
    {
        
        Health += num;
        Health = Health <= 0 ? 0 : Health;
        GameController.gc.ReBossHp(Health);
        if (Health <= 0)
        {

            Instantiate(b_Explode, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            GameController.gc.ReScore(GetScore);
            GameController.gc.addPower();
            GameController.gc.addHealth();
            GameController.gc.num=2;
            GameController.gc.Switch = true;
            BossHp.SetActive(false);


        }
    }
}
