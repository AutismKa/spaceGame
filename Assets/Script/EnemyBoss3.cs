using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss3 : MonoBehaviour
{
    Rigidbody rigidbody;
    Transform transform;
    AudioSource audio;
    public float speed = 5f;
    public int Health = 800;
    public int GetScore = 50;
    public GameObject[] b_bullet;
    public GameObject b_Explode;
    public Transform []b_firepos;
    public Transform b_firepos2;
    public GameObject BossHp;
    public AudioClip hit;
    public float WaitFire = 0.25f; //子弹发射每颗等待时间
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
           

                for (int k = 1; k < b_firepos.Length;k++ )
                {
                    Instantiate(b_bullet[0], b_firepos[k].position, Quaternion.identity);
                }
                yield return new WaitForSeconds(WaitFire);

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
        if (this.transform.position.y >= 2.962813)
        {
            rigidbody.velocity = new Vector3(0, -speed, 0);
        }
        else
        {
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
            GameController.gc.num=1;
            GameController.gc.Switch = true;
            BossHp.SetActive(false);


        }
        else if (Health <= 400 && Switch == true)
        {
            StartCoroutine(OnFireStarBullet());
            Switch = false;
            
        }
    }
}
