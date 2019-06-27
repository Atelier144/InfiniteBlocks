using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrecipitateBlock : MonoBehaviour {

    [SerializeField] GameObject blockEffect;
    MainManager mainManager;
    Ball ball;
	// Use this for initialization
	void Start () {
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
        ball = GameObject.Find("TheBall").GetComponent<Ball>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ball" || collision.gameObject.tag == "PoweredBall")
        {
            
        }
    }

    public void StartEffect()
    {
        blockEffect.SetActive(false);
        blockEffect.SetActive(true);
    }
}
