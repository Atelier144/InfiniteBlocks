using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotSystemSwitch : MonoBehaviour {

    [SerializeField] int switchCode;
    [SerializeField] GameObject gameObjectSlotSystem;

    SlotSystem slotSystem;

    Animator animator;
	// Use this for initialization
	void Start () {
        slotSystem = gameObjectSlotSystem.GetComponent<SlotSystem>();

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
        slotSystem.OnSwtichOn(switchCode);
    }

    public void SetTrigger(string triggerName)
    {
        animator.SetTrigger(triggerName);
    }
}
