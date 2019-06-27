using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayTelop : MonoBehaviour {

    MainManager mainManager;
	// Use this for initialization
	void Start () {
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnAnimationEnd()
    {
        mainManager.OnAnimationEndFromReplayTelop();
    }
}
