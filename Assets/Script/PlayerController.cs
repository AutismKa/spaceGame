using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
//边界设置
[System.Serializable]
public class FlyEdge
{
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
}

public class PlayerController : MonoBehaviour {
    Rigidbody rigidbody; //刚体
    Transform transform;
    //飞船XY轴速度变化量
    private float moveX = 0;
    private float moveY = 0;
    public int Health = 20;
    public GameObject p_Explode;
    public float speed = 1f;
    public FlyEdge border; //边界
    public float FlyRotation = 10f; //飞机偏斜幅度
    //子弹设置
    public GameObject [] m_bullet;
    public Transform m_firepos;
    public Transform m_firepos1;
    public Transform m_firepos2;
    public float fireWait=0.2f;
    public float nextFireTime = 0f;
    public int PowerUP = 1;
    public int BulletType = 0;
    public AudioClip OpenFire;
    public AudioClip OpenFireLight;
    public AudioClip eq;
    public GameObject gameover;
    public static PlayerController pc;
    AudioSource audio;
	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
        transform = GetComponent<Transform>();
        audio = GetComponent<AudioSource>();
       GameController.gc.ReHealth(Health);
       pc = this;
       EasyJoystick.On_JoystickMove += onJoystickMoveHandler; //虚拟摇杆
       EasyJoystick.On_JoystickMoveEnd += onJoystickMoveEndHandler; //虚拟摇杆
	}
    private void onJoystickMoveHandler(MovingJoystick joystick)//虚拟摇杆
    {

        moveX = joystick.joystickAxis.x;
        moveY = joystick.joystickAxis.y;


    }
    private void onJoystickMoveEndHandler(MovingJoystick joystick)//虚拟摇杆
    {
        moveX = moveY = 0;
    }
	
	// Update is called once per frame
	void FixedUpdate () {    
        //通过键盘来控制飞机
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");
        rigidbody.velocity = new Vector3(moveX, moveY, 0)*speed;
        transform.position = new Vector3(Mathf.Clamp(this.transform.position.x,border.minX,border.maxX),Mathf.Clamp(this.transform.position.y,border.minY,border.maxY),0); //通过MathClamp进行边界最大最小设置
        transform.rotation = Quaternion.Euler(0f, (150f- this.rigidbody.velocity.x) * FlyRotation, 0); //如何计算的150的值？ 180-180/FlyRotation的值
   /*  if (Input.GetMouseButton(0))
        {

            Shot();
        }  */ //通过鼠标点击屏幕来发射子弹
	}
    void Shot()  //发射子弹
    {
        if (Time.time >= nextFireTime)
        {
        nextFireTime = Time.time + fireWait; //延迟发射，现在的时间+延迟的时间为下次开火时间，并且现在的时间等待到下次开火时间才能够开火

        if (PowerUP == 1)
        {
            this.audio.PlayOneShot(OpenFire);
            Instantiate(m_bullet[BulletType], m_firepos1.position, m_firepos1.rotation);
            Instantiate(m_bullet[BulletType], m_firepos2.position, m_firepos2.rotation);
        }
        else if (PowerUP == 2)
        {
            this.audio.PlayOneShot(OpenFire);
            Instantiate(m_bullet[BulletType], m_firepos.position, m_firepos.rotation);
            Instantiate(m_bullet[BulletType], m_firepos1.position, m_firepos1.rotation);
            Instantiate(m_bullet[BulletType], m_firepos2.position, m_firepos2.rotation);

        }
        else if (PowerUP == 3)
        {

            BulletType = 1;
            this.audio.PlayOneShot(OpenFireLight);
            Instantiate(m_bullet[BulletType], m_firepos.position, m_firepos.rotation);
            Instantiate(m_bullet[BulletType], m_firepos1.position, m_firepos1.rotation);
            Instantiate(m_bullet[BulletType], m_firepos2.position, m_firepos2.rotation);
        }
        else if (PowerUP >= 4)
        {
            BulletType = 1;
            this.audio.PlayOneShot(OpenFireLight);
            fireWait = 0.15f;
            Instantiate(m_bullet[BulletType], m_firepos.position, m_firepos.rotation);
            Instantiate(m_bullet[BulletType], m_firepos1.position, m_firepos1.rotation);
            Instantiate(m_bullet[BulletType], m_firepos2.position, m_firepos2.rotation);
        }
        else
        {
            this.audio.PlayOneShot(OpenFire);
            Instantiate(m_bullet[BulletType], m_firepos.position, m_firepos.rotation);
        }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyBullet")
        {
            Damage.damage.TakeDamage();
            HealthChange(-5);
            Destroy(other.gameObject);
            
        }
        else if (other.tag == "Enemy")
        {
            Damage.damage.TakeDamage();
            HealthChange(-10);

        }
        else if (other.tag == "Ast")
        {
            Damage.damage.TakeDamage();
            HealthChange(-10);
        }
        else if (other.tag == "Power")
        {
            this.audio.PlayOneShot(eq);
        }
        else if(other.tag=="Boss"){
            Damage.damage.TakeDamage();
            HealthChange(-20);
        }
        else if (other.tag == "Enemy2")
        {
            Damage.damage.TakeDamage();
            HealthChange(-20);
        }
    }
    public void HealthChange(int num)
    {
        Health += num;
        Health = Health <= 0 ? 0 : Health;
        Health = Health >= 20 ? 20 : Health;
        GameController.gc.ReHealth(Health);
        if (Health <= 0)
        {
            Instantiate(p_Explode, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            gameover.SetActive(true);
        }
    }
}
