using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusStageSystemSwitch : MonoBehaviour {

    [SerializeField] GameObject bonusStageSystem;
    [SerializeField] int switchId;

    Animator animator;

    bool isSwitchOn;
	// Use this for initialization
	void Start () {
        animator = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OpenGate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OpenGate();
    }

    void OpenGate()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("BonusStageSystemSwitchOff") && !isSwitchOn)
        {
            isSwitchOn = true;
            animator.SetTrigger("SwitchOn");
            bonusStageSystem.GetComponent<BonusStageSystem>().OnCollisionSwitch(switchId);
        }
    }
}
