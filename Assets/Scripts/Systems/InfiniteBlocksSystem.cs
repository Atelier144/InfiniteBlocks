using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteBlocksSystem : MonoBehaviour {

    const int HARD_BLOCK = 8;
    const int SILVER_BLOCK = 9;
    const int GOLD_BLOCK = 10;
    const int BLACK_BLOCK = 11;

    MainManager mainManager;
    SignalManager signalManager;
    PrefabCreator prefabCreator;
    Ball theBall;

    [SerializeField] GameObject[] gameObjectsSignals = new GameObject[20];
    [SerializeField] GameObject[] gameObjectsNumbers = new GameObject[4];
    [SerializeField] GameObject gameObjectBody;

    [SerializeField] Sprite[] spritesSignals = new Sprite[25];
    [SerializeField] Sprite[] spritesNumbers = new Sprite[10];
    [SerializeField] Sprite[] spritesBodies = new Sprite[2];

    SpriteRenderer[] spriteRenderersSignals = new SpriteRenderer[20];
    SpriteRenderer[] spriteRenderersNumbers = new SpriteRenderer[4];
    SpriteRenderer spriteRendererBody;

    AudioSource[] audioSources;

    int restOfSteps = 5;
    int systemLevel = 1;
    int timerCount = 0;
    int signalIndex = 0;

    int[] preparedBlocks = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    int[] maxTimerCounts = { 40, 30, 28, 26, 24, 22, 20, 19, 18, 17, 16, 15, 15 };
    // Use this for initialization
    void Start () {
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
        signalManager = GameObject.Find("SignalManager").GetComponent<SignalManager>();
        prefabCreator = GameObject.Find("PrefabCreator").GetComponent<PrefabCreator>();
        theBall = GameObject.Find("TheBall").GetComponent<Ball>();

        for (int i = 0; i < 20; i++) spriteRenderersSignals[i] = gameObjectsSignals[i].GetComponent<SpriteRenderer>();
        for (int i = 0; i < 4; i++) spriteRenderersNumbers[i] = gameObjectsNumbers[i].GetComponent<SpriteRenderer>();
        spriteRendererBody = gameObjectBody.GetComponent<SpriteRenderer>();

        audioSources = GetComponents<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < 20; i++) spriteRenderersSignals[i].sprite = spritesSignals[preparedBlocks[i]];
        spriteRenderersNumbers[0].sprite = spritesNumbers[restOfSteps / 10 % 10];
        spriteRenderersNumbers[1].sprite = spritesNumbers[restOfSteps % 10];
        spriteRenderersNumbers[2].sprite = spritesNumbers[systemLevel / 10 % 10];
        spriteRenderersNumbers[3].sprite = spritesNumbers[systemLevel % 10];
	}

    private void FixedUpdate()
    {
        if (mainManager.GetDialogStatus() == 2)
        {
            if(timerCount > maxTimerCounts[systemLevel])
            {
                timerCount = 0;
                if(signalIndex == 20)
                {
                    signalIndex = 21;
                    StartCoroutine(PrepareLowerBlocks());
                }
                else if(signalIndex < 20)
                {
                    preparedBlocks[signalIndex] = GetSignalType();
                    audioSources[0].time = 0.03f;
                    audioSources[0].Play();
                    signalIndex++;
                }
            }
            else
            {
                timerCount++;
            }
        }
        else
        {
            timerCount = 0;
        }
    }

    void GenerateBlocks()
    {
        for(int i=0; i<preparedBlocks.Length; i++)
        {
            float positionX = i * 50.0f - 475.0f;
            float positionY = 202.0f;
            if(preparedBlocks[i] > 0 && preparedBlocks[i] < 8)
            {
                int colorCode = preparedBlocks[i] - 1;
                prefabCreator.CreateNormalBlock(positionX, positionY, colorCode);
            }
        }
    }

    void LowerBlocks()
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
        foreach(GameObject block in blocks)
        {
            block.transform.position += new Vector3(0.0f, -20.0f, 0.0f);
            if (block.transform.position.y < -180.0f && block.transform.position.x < 1000.0f) Destroy(block);
        }
    }

    IEnumerator PrepareLowerBlocks()
    {
        audioSources[1].time = 0.0f;
        audioSources[1].Play();
        spriteRendererBody.sprite = spritesBodies[1];
        yield return new WaitForSeconds(0.5f);
        spriteRendererBody.sprite = spritesBodies[0];
        yield return new WaitForSeconds(0.5f);
        audioSources[1].time = 0.0f;
        audioSources[1].Play();
        spriteRendererBody.sprite = spritesBodies[1];
        yield return new WaitForSeconds(0.5f);
        spriteRendererBody.sprite = spritesBodies[0];
        yield return new WaitForSeconds(0.5f);

        audioSources[2].time = 0.0f;
        audioSources[2].Play();
        signalIndex = 0;
        LowerBlocks();
        GenerateBlocks();
        for (int i = 0; i < preparedBlocks.Length; i++) preparedBlocks[i] = 0;
        if (restOfSteps > 0) restOfSteps--;
        if (restOfSteps == 0 && systemLevel <= 12)
        {
            audioSources[3].time = 0.0f;
            audioSources[3].Play();
            systemLevel++;
            restOfSteps = 5;
        }
    }

    int GetSignalType()
    {
        return Random.Range(1, 8);
    }
}

//このシステムは無限にブロックを生み出すシステムである
//12段生成するとレベルが上がり、登場するブロックのパターンや速度が変わる
//最大12レベル
//なおブロックの段数が16段に達するとマシンは緊急停止し、規定時間以内に16段に達したブロックの列を壊さなければシステムは爆発しゲームオーバーとなる
// 0 None
// 1 Red
// 2 Orange
// 3 Yellow
// 4 Green
// 5 Blue
// 6 Indigo
// 7 Violet
// 8 Silver
// 9 Gold
// 10 PowerUp
// 11 Protector
// 12 Shooting
// 13 Magnet
// 14 Sticky
// 15 ExtraBall
// 16 GameOver
// 17 Decelerate
