using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackpotChallengeSystemSwitch : MonoBehaviour {

    [SerializeField] GameObject gameObjectJackPotChallengeSystem;

    [SerializeField] int switchCode;

    JackpotChallengeSystem jackpotChallengeSystem;
    Animator animator;
	// Use this for initialization
	void Start () {
        jackpotChallengeSystem = gameObjectJackPotChallengeSystem.GetComponent<JackpotChallengeSystem>();

        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball") SwitchOn();
        if (collision.gameObject.tag == "PoweredBall") SwitchOn();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet") SwitchOn();
    }

    void SwitchOn()
    {
        jackpotChallengeSystem.OnJackpotSwitchOn(switchCode);
    }

    public void ChangeColor(string s)
    {
        animator.SetTrigger(s);
    }
}
