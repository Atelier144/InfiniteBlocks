using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBoxesSystemWindow : MonoBehaviour {

    SignalManager signalManager;

    SpriteRenderer spriteRenderer;

    bool isOpen;
	// Use this for initialization
	void Start () {
        signalManager = GameObject.Find("SignalManager").GetComponent<SignalManager>();

        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (isOpen) spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        else if (signalManager.IsActiveTrapGuard()) spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        else spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
	}

    public void SetOpen(bool s)
    {
        isOpen = s;
    }
}
