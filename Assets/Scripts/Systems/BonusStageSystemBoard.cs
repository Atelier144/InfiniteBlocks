using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusStageSystemBoard : MonoBehaviour {

    [SerializeField] GameObject gameObjectBonusStageSystem;

    BonusStageSystem bonusStageSystem;

    int loopCount;
	// Use this for initialization
	void Start () {
        bonusStageSystem = gameObjectBonusStageSystem.GetComponent<BonusStageSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void loop()
    {
        loopCount++;
        if(loopCount > 10)
        {
            loopCount = 0;
            bonusStageSystem.OnAnimationEnd();
        }
    }
}
