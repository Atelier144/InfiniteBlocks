using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SKL144SystemBigBar : MonoBehaviour {

    SignalManager signalManager;
    Ball ball;

    [SerializeField] Sprite spriteBigBarNormal;
    [SerializeField] Sprite spriteBigBarTrapGuard;

    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider2D;
	// Use this for initialization
	void Start () {
        signalManager = GameObject.Find("SignalManager").GetComponent<SignalManager>();
        ball = GameObject.Find("TheBall").GetComponent<Ball>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
        spriteRenderer.sprite = signalManager.IsActiveTrapGuard() ? spriteBigBarTrapGuard : spriteBigBarNormal;
	}

    private void FixedUpdate()
    {
        if (signalManager.IsActiveTrapGuard()) boxCollider2D.isTrigger = ball.IsMovingAbove();
        else boxCollider2D.isTrigger = ball.IsMovingBelow();
    }
}
