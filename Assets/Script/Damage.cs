using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  
public class Damage : MonoBehaviour {
    public Image damage_Image;
    public Color flash_Color;
    public float flash_Speed = 5;
    bool damaged = false;
    public static Damage damage;
	// Use this for initialization
	void Start () {
        damage = this;
	}
	
	// Update is called once per frame
	void Update () {

        PlayDamagedEffect(); 
	}
    void PlayDamagedEffect()
    {
        if (damaged)
        {
            damage_Image.color = Color.red;
        }
        else
        {
            damage_Image.color = Color.Lerp(damage_Image.color, Color.clear, flash_Speed * Time.deltaTime);

        }
        damaged = false;

    }
    public void TakeDamage()
    {
        damaged = true;

    }  
}
