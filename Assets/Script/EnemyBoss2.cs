using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss2 : MonoBehaviour
{
    Rigidbody rigidbody;
    Transform transform;
    AudioSource audio;
    public float speed = 5f;
    public int Health = 500;
    public int GetScore = 50;
    public GameObject[] b_bullet;
    public GameObject b_Explode;
    public Transform b_firepos;
    public Transform b_firepos2;
    public GameObject BossHp;
    public AudioClip hit;
    public float WaitFire = 0.25f; //子弹发射每颗等待时间
    public float WaitFireWave = 1f;
    public float WaitFire2 = 1f;
    bool Switch = true; //是否在此出现限制


    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        transform = GetComponent<Transform>();
        audio = GetComponent<AudioSource>();
        StartCoroutine(OnFireBaseBullet());
        BossHp = GameObject.Find("Canvas/BOSSHP");
        GameController.gc.ReBossHp(Health);
    }
    IEnumerator OnFireBaseBullet()
    {
        while (true)
        {
            int rd = Random.Range(1, 5);
            for (int i = 1; i < rd; i++)
            {
                Instantiate(b_bullet[0], b_firepos.position, Quaternion.identity);
                yield return new WaitForSeconds(WaitFire);
            }
            yield return new WaitForSeconds(WaitFireWave);
        }
    }
    IEnumerator OnFireStarBullet()
    {
       
        while (true)
        {
              
                Instantiate(b_bullet[1], b_firepos2.position, Quaternion.identity);
                yield return new WaitForSeconds(WaitFire2);
                
        }
       
        
    }


    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y >= 2.2)
        {
            rigidbody.velocity = new Vector3(0, -speed, 0);
        }else{
            rigidbody.velocity = new Vector3(0, 0, 0);
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
            GameController.gc.num = 3;
            GameController.gc.Switch = true;
            BossHp.SetActive(false);


        }
        else if (Health <= 300 && Switch==true)
        {
            StartCoroutine(OnFireStarBullet());
            Switch =false;
        }
    }
}
