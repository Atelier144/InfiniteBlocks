using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusStageSystem : MonoBehaviour {

    MainManager mainManager;
    Animator animator;

    [SerializeField] GameObject[] gates = new GameObject[2];
    [SerializeField] GameObject[] signals = new GameObject[2];

    [SerializeField] GameObject board;

    [SerializeField] Sprite spriteRedSignal;
    [SerializeField] Sprite spriteGreenSignal;

    GameObject gameObjectLevelUpTelop;

    int currentRestOfBalls;

    int countMissedExtraBalls;
    int countGotExtraBalls;

	// Use this for initialization
	void Start () {
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
        gameObjectLevelUpTelop = GameObject.Find("Telops").transform.Find("LevelUpTelop").gameObject;   //非アクティブなGameObjectを取得するための手段
        animator = board.GetComponent<Animator>();
        currentRestOfBalls = mainManager.GetRestOfBalls();
	}
	
	// Update is called once per frame
	void Update () {
        int tmpRestOfBalls = mainManager.GetRestOfBalls();
        if(tmpRestOfBalls > currentRestOfBalls)
        {
            countGotExtraBalls++;
            if (countGotExtraBalls == 3) animator.SetTrigger("Excellent");
            else animator.SetTrigger("Nice");
        }
        currentRestOfBalls = tmpRestOfBalls;

        if (gameObjectLevelUpTelop.activeSelf) animator.SetTrigger("Finish");
    }

    public void OnCollisionSwitch(int switchId)
    {
        mainManager.AddGameScore(50);
        signals[switchId].GetComponent<SpriteRenderer>().sprite = spriteGreenSignal;
        gates[switchId].GetComponent<BonusStageSystemGateBar>().Open();
    }

    public void OnTriggerFailZoneBall()
    {
        //animator.SetTrigger("Finish");
    }

    public void OnTriggerFailZoneExtraBall()
    {
        countMissedExtraBalls++;
        animator.SetTrigger("Missing");
    }

    public void OnAnimationEnd()
    {
        if (countGotExtraBalls + countMissedExtraBalls >= 3) animator.SetTrigger("End");
        else animator.SetTrigger("Idle");
    }

    public bool HasNoExtraBalls()
    {
        return countGotExtraBalls + countMissedExtraBalls >= 3;
    }
}
