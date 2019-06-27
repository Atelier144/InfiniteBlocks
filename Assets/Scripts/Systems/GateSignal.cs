using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateSignal : MonoBehaviour {

    [SerializeField] Sprite spriteOpen;
    [SerializeField] Sprite spriteShut;

    SpriteRenderer spriteRenderer;
	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeSignal(bool s)
    {
        if (s) spriteRenderer.sprite = spriteOpen;
        else spriteRenderer.sprite = spriteShut;
    }
}
