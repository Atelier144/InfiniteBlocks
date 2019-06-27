using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleGateSystemSwitch : MonoBehaviour {

    [SerializeField] GameObject gameObjectSingleGateSystem;

    SingleGateSystem singleGateSystem;

    Animator animator;
	// Use this for initialization
	void Start () {
        singleGateSystem = gameObjectSingleGateSystem.GetComponent<SingleGateSystem>();

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
        singleGateSystem.OnSwitchOn();
    }

    public void ChangeColor(string triggerName)
    {
        animator.SetTrigger(triggerName);
    }
}
