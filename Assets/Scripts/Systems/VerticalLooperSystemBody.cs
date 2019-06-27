using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalLooperSystemBody : MonoBehaviour {

    Ball ball;
	// Use this for initialization
	void Start () {
        ball = GameObject.Find("TheBall").GetComponent<Ball>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ball")
        {
            ball.VerticalLoop();
        }
        if(collision.tag == "Bullet")
        {
            Destroy(collision);
        }
    }
}
