using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    const int NOTHING = 0;
    const int POINT_100 = 1;
    const int POINT_200 = 2;
    const int POINT_500 = 3;
    const int POINT_1000 = 4;
    const int EXPAND_RACKET = 5;
    const int SHRINK_RACKET = 6;
    const int EXTRA_BALL = 7;
    const int POWER_UP = 8;
    const int PROTECTOR = 9;
    const int LEVEL_UP = 10;
    const int TRAP_GUARD = 11;
    const int GAME_OVER = 12;
    const int PRECIPITATE = 13;
    const int SHOOTING = 14;
    const int HOSTAGE_300 = 15;
    const int NEGATIVE_POINT_200 = 16;
    const int FLASH = 17;
    const int COUNTERFEIT = 18;
    const int MAGNET = 19;
    const int STICKY = 20;
    const int DECELERATE = 21;
    const int ACCELERATE = 22;
    const int MAX_SPEED = 23;

    MainManager mainManager;
    ItemManager itemManager;
    SignalManager signalManager;
    PrefabSounds prefabSounds;

    Ball ball;
    Racket racket;

    public Sprite[] spritesItem = new Sprite[24];

    int itemCode;

	// Use this for initialization
	void Start () {
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
        itemManager = GameObject.Find("ItemManager").GetComponent<ItemManager>();
        signalManager = GameObject.Find("SignalManager").GetComponent<SignalManager>();
        prefabSounds = GameObject.Find("PrefabSounds").GetComponent<PrefabSounds>();

        ball = GameObject.Find("TheBall").GetComponent<Ball>();
        racket = GameObject.Find("TheRacket").GetComponent<Racket>();

        if (itemCode >= 0) itemCode = mainManager.GetCurrentStage().GenerateItemCode(itemCode);
        else itemCode *= -1;

        if (itemCode == NOTHING)
        {
            Destroy(this.gameObject);
        }
        else
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, -200.0f);
            this.GetComponent<SpriteRenderer>().sprite = spritesItem[itemCode];
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Initialize(int itemCode)
    {
        this.itemCode = itemCode;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Racket")
        {
            switch (itemCode)
            {
                case POINT_100:
                    mainManager.AddGameScore(100);
                    prefabSounds.GetPoint();
                    break;
                case POINT_200:
                    mainManager.AddGameScore(200);
                    prefabSounds.GetPoint();
                    break;
                case POINT_500:
                    mainManager.AddGameScore(500);
                    prefabSounds.GetPoint();
                    break;
                case POINT_1000:
                    mainManager.AddGameScore(1000);
                    prefabSounds.GetPoint();
                    break;
                case EXPAND_RACKET:
                    racket.Expand();
                    break;
                case SHRINK_RACKET:
                    if (signalManager.IsActiveTrapGuard()) prefabSounds.GetNothing();
                    else racket.Shrink();
                    break;
                case EXTRA_BALL:
                    mainManager.AddRestOfBall();
                    prefabSounds.GetExtraBall();
                    break;
                case POWER_UP:
                    signalManager.StartPoweredBall(1000);
                    break;
                case PROTECTOR:
                    signalManager.StartProtector(750);
                    break;
                case LEVEL_UP:
                    mainManager.AddJackpotScore(100);
                    mainManager.LevelUp();
                    break;
                case TRAP_GUARD:
                    signalManager.StartTrapGuard(1500);
                    break;
                case GAME_OVER:
                    if (signalManager.IsActiveTrapGuard()) prefabSounds.GetNothing();
                    else mainManager.StartGameOver();
                    break;
                case PRECIPITATE:
                    signalManager.StartPrecipitate(1500);
                    break;
                case SHOOTING:
                    signalManager.StartShooting(750);
                    break;
                case HOSTAGE_300:
                    mainManager.AddGameScore(300);
                    prefabSounds.GetPoint();
                    break;
                case NEGATIVE_POINT_200:
                    if (signalManager.IsActiveTrapGuard()) prefabSounds.GetNothing();
                    else mainManager.AddGameScore(-200);
                    break;
                case FLASH:
                    if (signalManager.IsActiveTrapGuard()) prefabSounds.GetNothing();
                    else mainManager.StartFlash();
                    break;
                case COUNTERFEIT:
                    if (signalManager.IsActiveTrapGuard()) prefabSounds.GetNothing();
                    else
                    {
                        mainManager.AddGameScore(-1000);
                        signalManager.StopAllSignals();
                        racket.SetStepOfLength(1);
                        prefabSounds.GetCounterfeit();
                    }
                    break;
                case MAGNET:
                    signalManager.StartMagnet(1000);
                    break;
                case STICKY:
                    signalManager.StartSticky(1500);
                    break;
                case DECELERATE:
                    ball.Decelerate();
                    break;
                case ACCELERATE:
                    if (signalManager.IsActiveTrapGuard()) prefabSounds.GetNothing();
                    else ball.Accelerate();
                    break;
                case MAX_SPEED:
                    if (signalManager.IsActiveTrapGuard()) prefabSounds.GetNothing();
                    else ball.MaxSpeed();
                    break;
            }
            Destroy(this.gameObject);

        }
        if (collision.gameObject.tag == "FailZone")
        {
            switch (itemCode)
            {
                case EXTRA_BALL:
                    //複雑なシステムに使う
                    break;
                case HOSTAGE_300:
                    mainManager.MissingHostage300();
                    break;
            }
            Destroy(this.gameObject);
        }
    }

    public int GetItemCode()
    {
        return itemCode;
    }
}
