using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SKL144Sytem : MonoBehaviour {

    bool isLevelUp;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool IsLevelUp()
    {
        return isLevelUp;
    }

    public void SetLevelUp(bool s)
    {
        isLevelUp = s;
    }
}
