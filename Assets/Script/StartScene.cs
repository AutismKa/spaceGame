using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartScene : MonoBehaviour {
    public Button btnStart;  //开始游戏
    public Button btnExit; //退出游戏
	// Use this for initialization
	void Start () {
        btnStart.onClick.AddListener(PlayGame);
        btnExit.onClick.AddListener(ExitGame);
	}
	
	// Update is called once per frame
	void Update () {
        
	}
    private void PlayGame()
    {
        Application.LoadLevel("2");
    }
    private void ExitGame()
    {
        Application.Quit();
    }
}
