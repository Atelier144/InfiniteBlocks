using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashEffectManager : MonoBehaviour {

    int count = 180;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        count--;
        if(count > 0)
        {
            if(count < 100)
            {
                float alpha = count * 0.01f;
                Color color = new Color(1.0f, 1.0f, 1.0f, alpha);
                this.GetComponent<SpriteRenderer>().color = color;
            }

        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
