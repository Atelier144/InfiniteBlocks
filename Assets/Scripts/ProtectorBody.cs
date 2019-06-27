using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectorBody : MonoBehaviour {

    Ball ball;

    [SerializeField] GameObject prefabProtectorBounceEffect;

    EdgeCollider2D edgeCollider2D;
	// Use this for initialization
	void Start () {
        ball = GameObject.Find("TheBall").GetComponent<Ball>();

        edgeCollider2D = GetComponent<EdgeCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        edgeCollider2D.enabled = ball.IsMovingBelow();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ball" || collision.gameObject.tag == "PoweredBall")
        {
            float collisionPointX = collision.contacts[0].point.x;
            Instantiate(prefabProtectorBounceEffect, new Vector3(collisionPointX, -220.0f, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
        }
    }
}
