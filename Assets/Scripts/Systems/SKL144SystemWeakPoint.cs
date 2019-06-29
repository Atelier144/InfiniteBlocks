using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SKL144SystemWeakPoint : MonoBehaviour {

    [SerializeField] GameObject gameObjectBody;

    SKL144SystemBody body;
	// Use this for initialization
	void Start () {
        body = gameObjectBody.GetComponent<SKL144SystemBody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball") Debug.Log("");
        if (collision.gameObject.tag == "PoweredBall") Debug.Log("");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet") Debug.Log("");
    }
}
