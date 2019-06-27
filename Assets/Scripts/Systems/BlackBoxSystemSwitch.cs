using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBoxSystemSwitch : MonoBehaviour {

    MainManager mainManager;

    AudioSource audioSource;

    [SerializeField] GameObject gameObjectBlackBoxSystem;
    [SerializeField] Sprite spriteOn;
    [SerializeField] Sprite spriteOff;

    BlackBoxSystem blackBoxSystem;

    bool isSwitchOn = false;

	// Use this for initialization
	void Start () {
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
        blackBoxSystem = gameObjectBlackBoxSystem.GetComponent<BlackBoxSystem>();
        audioSource = this.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ball" || collision.gameObject.tag == "PoweredBall" || collision.gameObject.tag == "BUllet")
        {
            if (isSwitchOn)
            {
                isSwitchOn = false;
                this.GetComponent<SpriteRenderer>().sprite = spriteOff;
                blackBoxSystem.Hide();
            }
            else
            {
                isSwitchOn = true;
                this.GetComponent<SpriteRenderer>().sprite = spriteOn;
                blackBoxSystem.Show();
            }
            audioSource.time = 0.1f;
            audioSource.Play();
        }
    }
}
