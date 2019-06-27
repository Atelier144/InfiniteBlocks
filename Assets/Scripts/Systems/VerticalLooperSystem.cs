using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalLooperSystem : MonoBehaviour {

    MainManager mainManaer;
    Aurora aurora;

	// Use this for initialization
	void Start () {
        mainManaer = GameObject.Find("MainManager").GetComponent<MainManager>();
        aurora = GameObject.Find("Aurora").GetComponent<Aurora>();

        aurora.SetActiveVerticalLooperAurora(true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
