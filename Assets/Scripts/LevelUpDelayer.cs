using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpDelayer : MonoBehaviour {

    GameObject mainManager;
	// Use this for initialization
	void Start () {
        mainManager = GameObject.Find("MainManager");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void LateUpdate()
    {
        //mainManager.GetComponent<MainManager>().ConfirmLevelUp();
        Destroy(this.gameObject);
    }
}
