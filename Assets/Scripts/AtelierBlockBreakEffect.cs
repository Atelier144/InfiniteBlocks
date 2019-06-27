using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtelierBlockBreakEffect : MonoBehaviour {

    [SerializeField] Sprite[] spritesAtelierBlockBreakEffect = new Sprite[8];
    [SerializeField] float soundStart;

	// Use this for initialization
	void Start () {
        this.GetComponent<AudioSource>().time = soundStart;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Initialize(int characterCode)
    {
        this.GetComponent<SpriteRenderer>().sprite = spritesAtelierBlockBreakEffect[characterCode];
    }

    public void OnAnimationEnd()
    {
        Destroy(this.gameObject);
    }
}
