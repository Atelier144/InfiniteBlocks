using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtelierDamage : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetSprite(Sprite sprite)
    {
        this.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public void OnAnimationEnd()
    {
        this.gameObject.SetActive(false);
    }
}
