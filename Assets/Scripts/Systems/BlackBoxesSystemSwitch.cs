using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBoxesSystemSwitch : MonoBehaviour {

    [SerializeField] GameObject gameObjectBlackBoxesSystem;

    [SerializeField] int switchCode;

    BlackBoxesSystem blackBoxesSystem;
    Animator animator;

	// Use this for initialization
	void Start () {
        blackBoxesSystem = gameObjectBlackBoxesSystem.GetComponent<BlackBoxesSystem>();
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
        blackBoxesSystem.OnSwitchOn(switchCode);
    }

    public void ChangeColor(bool s)
    {
        animator.SetTrigger(s ? "Green" : "Red");
    }
}
