using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public GameObject[] ast;
    public Vector3 astPOS;
    public int WaveCreateCount=10;
    public float WaitCreate = 1;
    public float Waitwave = 1f;
    public float E_Waitwave = 1f;
    public float E2_Waitwave = 1f;
    public float E2_WaitCreate = 1f;
    public int enemy2Count = 1; 
    public GameObject m_enemy;
    public GameObject PowerUp;
    public GameObject m_enemy2;
    public int enemyCount = 2;
    public float E_WaitCreate = 1;
    public Text txt_health;
    public Text txt_score;
    public Text txt_BossHp;
    public GameObject BossHp;
    public int score=0;
    public static GameController gc;
    public Button[] btnBack;
    AudioSource audio;
    public AudioClip CreatePower;
    public GameObject bosspos;
    Transform transform;
    public GameObject boss1;
    public GameObject HealthUp;
    public GameObject boss2;
    public GameObject boss3;
    public int num=1;
    public int num2=1; //出现次数限制
    public bool Switch = true;
    bool Switch2 = false;
    bool Switch3 = true;
    public int round=0;
    public float B_WaitCreate=45;
	// Use this for initialization
	void Start () {
        gc = this;
        transform = GetComponent<Transform>();
        audio = GetComponent<AudioSource>();
        StartCoroutine(addWaveAst());
        StartCoroutine(addWaveEnemy());
        StartCoroutine(addWaveEnemy2());
        txt_score.text = "0";
        btnBack[0].onClick.AddListener(BackTitle);
        btnBack[1].onClick.AddListener(BackTitle);
        
        
	}
    void BackTitle()
    {
        Time.timeScale = 1;
        Application.LoadLevel("1");
    }
    //分波创建陨石和敌人
    IEnumerator addWaveAst()
    {
        while (true)
        {
            for (int i = 0; i < WaveCreateCount; i++)
            {
                addAst();
                yield return new WaitForSeconds(WaitCreate); //等待创建一个陨石的间隔
            }
            yield return new WaitForSeconds(Waitwave); //等待创建一波陨石的间隔
        }
    }
    IEnumerator addWaveEnemy()
    {
        while (true)
        {
            for (int j = 0; j < enemyCount; j++)
            {
                addEnemy();
                yield return new WaitForSeconds(E_WaitCreate); //创建敌人的间隔
            }
            yield return new WaitForSeconds(E_Waitwave); //等待创建一波陨石的间隔
        }
    }
    IEnumerator addBossTime()
    {
        while (true)
        {
            round ++;
            yield return new WaitForSeconds(B_WaitCreate); //创建敌人的间隔
                addBoss();                              
        }
    }
    IEnumerator addWaveEnemy2()
    {
        while (true)
        {
            for (int l = 0; l < enemy2Count; l++)
            {
                addEnemy2();
                yield return new WaitForSeconds(E2_WaitCreate);
            }
            yield return new WaitForSeconds(E2_Waitwave);
        }
    }
    public void addPower()
    {
        Vector3 pos = new Vector3(Random.Range(-astPOS.x, astPOS.x),astPOS.y,0);
        Quaternion r = Quaternion.Euler(0, 0, 0);
        Instantiate(PowerUp,pos,r);
        this.audio.PlayOneShot(CreatePower);
    }
    public void addHealth()
    {
        Vector3 pos = new Vector3(Random.Range(-astPOS.x, astPOS.x), astPOS.y, 0);
        Quaternion r = Quaternion.Euler(0, 0, 0);
        Instantiate(HealthUp, pos, r);
        this.audio.PlayOneShot(CreatePower);
    }
    //创建单个敌人
    private void addEnemy()
    {
        Vector3 pos = new Vector3(Random.Range(-astPOS.x, astPOS.x),astPOS.y,0);
        Quaternion r=Quaternion.Euler(90,0,-180);
        Instantiate(m_enemy, pos, r);
        
    }
    private void addEnemy2()
    {
        Vector3 pos = new Vector3(Random.Range(-astPOS.x, astPOS.x), astPOS.y, 0);
        Quaternion r = Quaternion.identity;
        Instantiate(m_enemy2, pos, r);
    }
    //创建单个陨石
    private void addAst()
    {
        GameObject o=ast[Random.Range(0,ast.Length)];
        Vector3 pos = new Vector3(Random.Range(-astPOS.x, astPOS.x),astPOS.y,0);
        Quaternion r = Quaternion.identity;
        Instantiate(o,pos,r);
    }
    private void addBoss()
    {
                if(num==1 && num2==1){
        
        Vector3 pos = new Vector3(bosspos.transform.position.x, bosspos.transform.position.y, 0);
        Quaternion r = Quaternion.Euler(90,0,180);
        Instantiate(boss1, pos, r);  
        BossHp.SetActive(true);
        num2=2;
        //陨石
        Switch = false;
        WaveCreateCount = 0;
        WaitCreate = 0;
        Waitwave = 0f;
        //第一型号敌机
        enemyCount = 1+round;
        E_WaitCreate = 2-round*0.3f;
        E_Waitwave = 2-round*0.3f;
        //第二型号敌机
        E2_Waitwave = 0f;
        E2_WaitCreate =0f;
        enemy2Count = 0;
                }
                else if (num == 2&&num2==2)
                {
                    Vector3 pos = new Vector3(bosspos.transform.position.x, bosspos.transform.position.y, 0);
                    Quaternion r = Quaternion.identity;
                    Instantiate(boss2, pos, r);          
                    BossHp.SetActive(true);
                    num2=3;
                    //陨石
                    Switch = false;
                    WaveCreateCount = 10;
                    WaitCreate = 0.5f;
                    Waitwave = 0.3f;
                    //第一型号敌机
                    enemyCount = 0+round;
                    E_WaitCreate = 1;
                    E_Waitwave = 1;
                    //第二型号敌机
                    E2_Waitwave = 0f;
                    E2_WaitCreate = 0f;
                    enemy2Count = 0;

                }
                else if (num == 3 && num2 == 3)
                {
                    Vector3 pos = new Vector3(bosspos.transform.position.x, bosspos.transform.position.y, 0);
                    Quaternion r = Quaternion.identity;
                    Instantiate(boss3, pos, r);
                    BossHp.SetActive(true);
                    num2=1;
                    //陨石
                    Switch = false;
                    WaveCreateCount = 0;
                    WaitCreate = 0;
                    Waitwave = 0f;
                    //第一型号敌机
                    enemyCount = 0;
                    E_WaitCreate = 0;
                    E_Waitwave = 0;   
                    //第二型号敌机
                    E2_Waitwave = 2-round*0.3f;
                    E2_WaitCreate = 2-round*0.3f;
                    enemy2Count = 2+round;

                }
    }
    public void ReBossHp(int life)
    {
        txt_BossHp.text = life.ToString();
    }
	// Update is called once per frame
	void Update () {
        if(Switch2==true &&Switch3==true){

        StartCoroutine(addBossTime());
        Switch3 = false;
            
        }
	}
    public void ReHealth(int life)
    {
        txt_health.text = life.ToString();
    }
    public void ReScore(int num)
    {
        score += num;
        txt_score.text = score.ToString();
        if (score >= 50 && score < 100 && Switch == true)
        {
            WaveCreateCount = 3;
            WaitCreate = 1;
            Waitwave = 0.5f;
        }
        else if (score >= 100 && score < 150 && Switch == true)
        {   
            WaveCreateCount = 6;
            WaitCreate = 0.8f;
            Waitwave = 0.5f;
            addBoss();
            
        }
        else if (score >= 150 && score < 300 && Switch == true)
        {
            E2_Waitwave = 1f;
            E2_WaitCreate = 1f;
            enemy2Count = 1;
            WaveCreateCount = 6;
            WaitCreate = 0.8f;
            Waitwave = 0.5f;
            enemyCount = 1;
            E_WaitCreate = 1;
            E_Waitwave = 3;

        }
        else if (score >= 300 && score < 400 && Switch == true)
        {
            E2_Waitwave = 1f;
            E2_WaitCreate = 1f;
            enemy2Count = 1;
            WaveCreateCount = 10;
            WaitCreate = 0.5f;
            Waitwave = 0.3f;
            enemyCount = 1;
            E_WaitCreate = 1;
            E_Waitwave = 3;
            
        }else if(score>=400 &&score<500 &&Switch==true)
        {
            E2_Waitwave = 1f;
            E2_WaitCreate = 1f;
            enemy2Count = 1;
            WaveCreateCount = 10;
            WaitCreate = 0.5f;
            Waitwave = 0.3f;
            enemyCount = 1;
            E_WaitCreate = 1;
            E_Waitwave = 3;
            addBoss();
        }
        else if (score >= 500 && score < 800 && Switch == true)
        {
            E2_Waitwave = 1f;
            E2_WaitCreate = 1f;
            enemy2Count = 1;
            WaveCreateCount = 10;
            WaitCreate = 0.5f;
            Waitwave = 0.3f;
            enemyCount = 2;
            E_WaitCreate = 1;
            E_Waitwave = 3;
            
        }
        else if (score >= 800 && score < 900 && Switch == true)
        {
            E2_Waitwave = 1f;
            E2_WaitCreate = 1f;
            enemy2Count = 1;
            WaveCreateCount = 10;
            WaitCreate = 0.5f;
            Waitwave = 0.3f;
            enemyCount = 4;
            E_WaitCreate = 1;
            E_Waitwave = 2;
            addBoss();
        }
        else if (score >= 900 && Switch == true)
        {
            E2_Waitwave = 1f;
            E2_WaitCreate = 1f;
            enemy2Count = 1;
            WaveCreateCount = 10;
            WaitCreate = 0.5f;
            Waitwave = 0.3f;
            enemyCount = 5;
            E_WaitCreate = 0.5f;
            E_Waitwave = 1;
            Switch2 = true;
        }
    }
}
