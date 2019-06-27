using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownGate : MonoBehaviour {

    Ball ball;

    [SerializeField] Sprite spriteUpDownGate;
    [SerializeField] Sprite spriteUpDownGateDown;
    [SerializeField] Sprite spriteUpDownGateUp;

    [SerializeField] int directionCode;   //0:Down 1:Up

    BoxCollider2D boxCollider2D;

	// Use this for initialization
	void Start () {
        ball = GameObject.Find("TheBall").GetComponent<Ball>();
        boxCollider2D = GetComponent<BoxCollider2D>();

        if (directionCode == 0) GetComponent<SpriteRenderer>().sprite = spriteUpDownGateDown;
        if (directionCode == 1) GetComponent<SpriteRenderer>().sprite = spriteUpDownGateUp;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        boxCollider2D.enabled = true;
        if (directionCode == 0) boxCollider2D.enabled = ball.IsMovingAbove();
        if (directionCode == 1) boxCollider2D.enabled = ball.IsMovingBelow();
    }

    public void Initialize(int directionCode)
    {
        this.directionCode = directionCode;
    }
}
