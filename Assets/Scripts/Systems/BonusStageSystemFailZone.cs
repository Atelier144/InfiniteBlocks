using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusStageSystemFailZone : MonoBehaviour {

    [SerializeField] GameObject gameObjectBonusStageSystem;

    BonusStageSystem bonusStageSystem;
    SignalManager signalManager;

	// Use this for initialization
	void Start () {
        bonusStageSystem = gameObjectBonusStageSystem.GetComponent<BonusStageSystem>();
        signalManager = GameObject.Find("SignalManager").GetComponent<SignalManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ball" && !signalManager.IsActiveReplayMode()) bonusStageSystem.OnTriggerFailZoneBall();
        if (collision.gameObject.tag == "Item") if (collision.gameObject.GetComponent<Item>().GetItemCode() == 7) bonusStageSystem.OnTriggerFailZoneExtraBall();
    }
}
