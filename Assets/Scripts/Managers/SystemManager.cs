using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : MonoBehaviour {

    /*
    [SerializeField] GameObject topWall;

    [SerializeField] GameObject sixGatesSystem;
    [SerializeField] GameObject verticalLooperSystem;
    [SerializeField] GameObject steelBlocksWheelSystem;
    [SerializeField] GameObject bonusStageSystem;
    [SerializeField] GameObject blackBoxSystem;
    [SerializeField] GameObject jackpotChallengeSystem;
    [SerializeField] GameObject level26System;
    [SerializeField] GameObject level27System;
    [SerializeField] GameObject bigMonsterSystem;
    [SerializeField] GameObject timeBomberSystem;
    [SerializeField] GameObject infiniteBlocksSystem;

    [SerializeField] GameObject aurora;

    bool isVerticalLooperActive;
    bool isLevelUpSystemActive;

    */

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /*
    public void StopAllSystems()
    {
        topWall.SetActive(true);
        sixGatesSystem.SetActive(false);
        verticalLooperSystem.SetActive(false);
        steelBlocksWheelSystem.SetActive(false);
        bonusStageSystem.SetActive(false);
        blackBoxSystem.SetActive(false);
        jackpotChallengeSystem.SetActive(false);
        level26System.SetActive(false);
        level27System.SetActive(false);
        bigMonsterSystem.SetActive(false);
        timeBomberSystem.SetActive(false);
        infiniteBlocksSystem.SetActive(false);

        isVerticalLooperActive = false;
        isLevelUpSystemActive = false;

        aurora.GetComponent<Aurora>().SetActiveVerticalLooperAurora(false);
        aurora.GetComponent<Aurora>().SetActiveLevelUpAurora(false);
    }

    public void StartSixGatesSystem()
    {
        sixGatesSystem.SetActive(true);
    }

    public void StartVerticalLooperSystem()
    {
        topWall.SetActive(false);
        verticalLooperSystem.SetActive(true);
        isVerticalLooperActive = true;

        aurora.GetComponent<Aurora>().SetActiveVerticalLooperAurora(true);
    }

    public void StartSteelBlocksWheelSystem()
    {
        steelBlocksWheelSystem.SetActive(true);
    }
    public void StartLevelUpSystem()
    {
        isLevelUpSystemActive = true;

        aurora.GetComponent<Aurora>().SetActiveLevelUpAurora(true);
    }

    public void StartBonusStageSystem()
    {
        bonusStageSystem.SetActive(true);
    }

    public void StartBlackBoxSystem()
    {
        blackBoxSystem.SetActive(true);
    }

    public void StartJackpotChallengeSystem()
    {
        jackpotChallengeSystem.SetActive(true);
    }

    public void StartLevel26System()
    {
        level26System.SetActive(true);
    }

    public void StartLevel27System()
    {
        level27System.SetActive(true);
    }

    public void StartBigMonsterSystem()
    {
        bigMonsterSystem.SetActive(true);
    }

    public void StartTimeBomberSystem()
    {
        timeBomberSystem.SetActive(true);
    }

    public void StartInfiniteBlocksSystem()
    {
        infiniteBlocksSystem.SetActive(true);
    }

    public bool IsVerticalLooperActive()
    {
        return isVerticalLooperActive;
    }

    public bool IsLevelUpSystemActive()
    {
        return isLevelUpSystemActive;
    }
    */
}
