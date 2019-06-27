using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenNormalBlockManager : MonoBehaviour {

    int count = 50;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        if(count > 0)
        {
            count--;
            Color theColor = this.GetComponent<SpriteRenderer>().color;
            theColor.a = 0.02f * count;
            this.GetComponent<SpriteRenderer>().color = theColor;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
