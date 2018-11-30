using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class STOP : MonoBehaviour {

    public GameObject[] btn;
    public Button[] stop;
    bool Switch = true;
	// Use this for initialization
	void Start () {
        stop[0].onClick.AddListener(Stop);
        stop[1].onClick.AddListener(Stop);
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void Stop()
    {
        if (Switch)
        {
            Time.timeScale = 0;
            btn[0].SetActive(false);
            btn[1].SetActive(true);
            Switch = false;
        }
        else
        {
            Time.timeScale =1;
            btn[0].SetActive(true);
            btn[1].SetActive(false);
            Switch = true;
        }
    }
}
