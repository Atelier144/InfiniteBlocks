using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissTelop : MonoBehaviour {

    [SerializeField] GameObject mainManager;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnAnimationEnd()
    {
        mainManager.GetComponent<MainManager>().OnAnimationEndFromMissTelop();
    }
}
