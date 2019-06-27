using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleGateSystemOpener : MonoBehaviour {

    [SerializeField] GameObject gameObjectSingleGateSystem;

    [SerializeField] int triggerCode;

    SingleGateSystem singleGateSystem;

    // Use this for initialization
    void Start () {
        singleGateSystem = gameObjectSingleGateSystem.GetComponent<SingleGateSystem>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball" || collision.gameObject.tag == "PoweredBall") singleGateSystem.OnTriggerOn(triggerCode);
    }
}
