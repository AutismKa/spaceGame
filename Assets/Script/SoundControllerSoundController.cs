using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControllerSoundController : MonoBehaviour {
    public static SoundControllerSoundController sg=null;
	// Use this for initialization
	void Start () {
        if (sg != null)
        {
            Destroy(this.gameObject);
            return;
        }
        sg = this;
        DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
