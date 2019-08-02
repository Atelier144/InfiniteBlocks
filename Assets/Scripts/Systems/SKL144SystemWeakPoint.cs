using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SKL144SystemWeakPoint : MonoBehaviour {

    [SerializeField] GameObject gameObjectSKL144System;

    SKL144Sytem skl144System;
	// Use this for initialization
	void Start () {
        skl144System = gameObjectSKL144System.GetComponent<SKL144Sytem>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball") skl144System.Damage(1);
        if (collision.gameObject.tag == "PoweredBall") skl144System.Damage(3);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet") skl144System.Damage(1);
    }
}
