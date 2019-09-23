using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    
    MainManager mainManager;
    Rigidbody2D rigidbody2D;
    AudioSource soundShooting;

    [SerializeField] GameObject[] afterimages = new GameObject[5];

    Vector3[] previousPositions = new Vector3[5];

    // Use this for initialization
    void Start () {
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
        rigidbody2D = this.GetComponent<Rigidbody2D>();
        soundShooting = this.GetComponent<AudioSource>();

        for(int i=0; i<5; i++)
        {
            previousPositions[i] = this.transform.position;
        }
        rigidbody2D.velocity = new Vector2(0.0f, 1000.0f);
        soundShooting.time = 0.15f;
        soundShooting.Play();
    }
	
	// Update is called once per frame
	void Update () {
        for(int i=0; i<5; i++)
        {
            afterimages[i].transform.position = previousPositions[i];
        }
    }

    private void FixedUpdate()
    {
        previousPositions[4] = previousPositions[3];
        previousPositions[3] = previousPositions[2];
        previousPositions[2] = previousPositions[1];
        previousPositions[1] = previousPositions[0];
        previousPositions[0] = this.gameObject.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Block":
            case "BlockSupport":
            case "Rigidbody":
            case "PrecipitateBlock":
            case "RigidbodyPrecipitate":
                Destroy(gameObject);
                break;
        }
    }
}
