using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpTelop : MonoBehaviour {

    [SerializeField] GameObject mainManager;
    [SerializeField] float soundStartTime;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnAnimationEnd()
    {
        mainManager.GetComponent<MainManager>().OnAnimationEndFromLevelUpTelop();
    }
}
