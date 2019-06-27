using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingSystem : MonoBehaviour {

    Aurora aurora;

	// Use this for initialization
	void Start () {
        aurora = GameObject.Find("Aurora").GetComponent<Aurora>();
        aurora.SetActiveVerticalLooperAurora(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
