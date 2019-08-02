using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrecipitateBlock : MonoBehaviour {

    [SerializeField] GameObject blockEffect;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartEffect()
    {
        blockEffect.SetActive(false);
        blockEffect.SetActive(true);
    }
}
