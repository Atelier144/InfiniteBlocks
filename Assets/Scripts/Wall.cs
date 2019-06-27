using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

    [SerializeField] GameObject bounceEffect;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ball" || collision.gameObject.tag == "PoweredBall")
        {
            Vector2 collisionPoint = collision.contacts[0].point;
            Instantiate(bounceEffect, collisionPoint, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
        }
    }
}
