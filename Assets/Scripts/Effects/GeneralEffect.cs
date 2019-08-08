using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralEffect : MonoBehaviour {

    [SerializeField] float soundStart;
	// Use this for initialization
	void Start () {
        this.GetComponent<AudioSource>().time = soundStart;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Eliminate()
    {
        Destroy(this.gameObject);
    }

    public void Halt()
    {
        this.gameObject.SetActive(false);
    }
}
