using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteBlocksSystem : MonoBehaviour {

    const int POWER_BLOCK = 8;
    const int PROTECTOR_BLOCK = 9;
    const int TRAPGUARD_BLOCK = 10;
    const int PRECIPITATE_BLOCK = 11;
    const int SHOOTING_BLOCK = 12;
    const int MAGNET_BLOCK = 13;
    const int STICKY_BLOCK = 14;
    const int SILVER_BLOCK = 15;
    const int GOLD_BLOCK = 16;
    const int EXTRABALL_BLOCK = 17;
    const int DECELERATE_BLOCK = 18;
    const int ACCELERATE_BLOCK = 19;
    const int BLACK_BLOCK = 20;

    MainManager mainManager;
    SignalManager signalManager;
    PrefabCreator prefabCreator;
    Ball theBall;

    [SerializeField] GameObject[] gameObjectsSignals = new GameObject[19];
    [SerializeField] GameObject[] gameObjectsNumbers = new GameObject[4];
    [SerializeField] GameObject gameObjectBody;

    [SerializeField] Sprite[] spritesSignals = new Sprite[21];
    [SerializeField] Sprite[] spritesNumbers = new Sprite[10];
    [SerializeField] Sprite[] spritesBodies = new Sprite[2];

    SpriteRenderer[] spriteRenderersSignals = new SpriteRenderer[20];
    SpriteRenderer[] spriteRenderersNumbers = new SpriteRenderer[4];
    SpriteRenderer spriteRendererBody;

    AudioSource[] audioSources;

    int restOfSteps = 3;
    int systemLevel = 1;
    int timerCount;
    int signalIndex;

    int[] decidedBlocks = new int[20];
    int[] preparedBlocks = new int[20];

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

        DecideBlocks();
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
                    preparedBlocks[signalIndex] = decidedBlocks[signalIndex];
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
        for (int i = 0; i < preparedBlocks.Length; i++) 
        {
            float positionX = i * 50.0f - 475.0f;
            float positionY = 202.0f;
            switch (preparedBlocks[i])
            {
                case 1:
                    prefabCreator.CreateNormalBlock(positionX, positionY, 0);
                    break;
                case 2:
                    prefabCreator.CreateNormalBlock(positionX, positionY, 1);
                    break;
                case 3:
                    prefabCreator.CreateNormalBlock(positionX, positionY, 2);
                    break;
                case 4:
                    prefabCreator.CreateNormalBlock(positionX, positionY, 3);
                    break;
                case 5:
                    prefabCreator.CreateNormalBlock(positionX, positionY, 4);
                    break;
                case 6:
                    prefabCreator.CreateNormalBlock(positionX, positionY, 5);
                    break;
                case 7:
                    prefabCreator.CreateNormalBlock(positionX, positionY, 6);
                    break;
                case 8:
                    prefabCreator.CreateItemBlock(positionX, positionY, 8);
                    break;
                case 9:
                    prefabCreator.CreateItemBlock(positionX, positionY, 9);
                    break;
                case 10:
                    prefabCreator.CreateItemBlock(positionX, positionY, 11);
                    break;
                case 11:
                    prefabCreator.CreateItemBlock(positionX, positionY, 13);
                    break;
                case 12:
                    prefabCreator.CreateItemBlock(positionX, positionY, 14);
                    break;
                case 13:
                    prefabCreator.CreateItemBlock(positionX, positionY, 19);
                    break;
                case 14:
                    prefabCreator.CreateItemBlock(positionX, positionY, 20);
                    break;
                case 15:
                    prefabCreator.CreateSilverBlock(positionX, positionY);
                    break;
                case 16:
                    prefabCreator.CreateGoldBlock(positionX, positionY);
                    break;
                case 17:
                    prefabCreator.CreateItemBlock(positionX, positionY, 7);
                    break;
                case 18:
                    prefabCreator.CreateItemBlock(positionX, positionY, 21);
                    break;
                case 19:
                    prefabCreator.CreateItemBlock(positionX, positionY, 22);
                    break;
                case 20:
                    prefabCreator.CreateItemBlock(positionX, positionY, 12);
                    break;
            }
        }
    }

    void LowerBlocks()
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
        foreach(GameObject block in blocks)
        {
            block.transform.position += new Vector3(0.0f, -20.0f, 0.0f);
            if (block.transform.position.x < 30.0f && block.transform.position.x > -30.0f && block.transform.position.y < -100.0f) Destroy(block);
            if (block.transform.position.y < -160.0f) Destroy(block);
        }
    }

    IEnumerator PrepareLowerBlocks()
    {
        for (int i = 0; i < 2; i++)
        {
            audioSources[1].time = 0.0f;
            audioSources[1].Play();
            spriteRendererBody.sprite = spritesBodies[1];
            yield return new WaitForSeconds(0.5f);
            spriteRendererBody.sprite = spritesBodies[0];
            yield return new WaitForSeconds(0.5f);
        }

        audioSources[2].time = 0.0f;
        audioSources[2].Play();
        signalIndex = 0;
        LowerBlocks();
        GenerateBlocks();

        for (int i = 0; i < preparedBlocks.Length; i++) preparedBlocks[i] = 0;
        if (restOfSteps > 0) restOfSteps--;
        if (restOfSteps == 0 && systemLevel < 12)
        {
            int[] maxNumbersOfRestOfSteps = { 0, 3, 8, 8, 8, 6, 6, 6, 8, 10, 12, 16, 0 };
            audioSources[3].time = 0.0f;
            audioSources[3].Play();
            systemLevel++;
            restOfSteps = maxNumbersOfRestOfSteps[systemLevel];
        }

        DecideBlocks();
    }

    void DecideBlocks()
    {
        int[] retval = new int[20];
        switch (systemLevel)
        {
            case 1:
                int[] decidedBlocksForLv1 = { 1, 1, 1, 2, 2, 2, 3, 3, 3, 4, 4, 4, 5, 5, 5, 6, 6, 6, 7, 7, 7 };
                if (true)
                {
                    int a = decidedBlocksForLv1.Length;
                    while (a > 0)
                    {
                        int i = a - 1;
                        int j = Random.Range(0, a);
                        int tmp = decidedBlocksForLv1[i];
                        decidedBlocksForLv1[i] = decidedBlocksForLv1[j];
                        decidedBlocksForLv1[j] = tmp;
                        a--;
                    }
                }
                for (int i = 0; i < 20; i++) decidedBlocks[i] = decidedBlocksForLv1[i];
                break;
            case 2:
                int[] decidedBlocksForLv2 = { 1, 1, 1, 2, 2, 2, 3, 3, 3, 4, 4, 4, 5, 5, 5, 6, 6, 6, 7, 7, 7 };
                if (true)
                {
                    int a = decidedBlocksForLv2.Length;
                    while (a > 0)
                    {
                        int i = a - 1;
                        int j = Random.Range(0, a);
                        int tmp = decidedBlocksForLv2[i];
                        decidedBlocksForLv2[i] = decidedBlocksForLv2[j];
                        decidedBlocksForLv2[j] = tmp;
                        a--;
                    }
                }
                for (int i = 0; i < 20; i++) decidedBlocks[i] = decidedBlocksForLv2[i];
                decidedBlocks[Random.Range(0, 20)] = Random.Range(0, 2) == 0 ? POWER_BLOCK : PROTECTOR_BLOCK;
                break;
            case 3:
                int[] decidedBlocksForLv3 = { 1, 1, 1, 2, 2, 2, 3, 3, 3, 4, 4, 4, 5, 5, 5, 6, 6, 6, 7, 7, 7 };
                if (true)
                {
                    int a = decidedBlocksForLv3.Length;
                    while (a > 0)
                    {
                        int i = a - 1;
                        int j = Random.Range(0, a);
                        int tmp = decidedBlocksForLv3[i];
                        decidedBlocksForLv3[i] = decidedBlocksForLv3[j];
                        decidedBlocksForLv3[j] = tmp;
                        a--;
                    }
                }
                for (int i = 0; i < 20; i++) decidedBlocks[i] = decidedBlocksForLv3[i];
                decidedBlocks[Random.Range(0, 20)] = Random.Range(0, 2) == 0 ? SHOOTING_BLOCK : PROTECTOR_BLOCK;
                break;
            case 4:
                int[] decidedBlocksForLv4 = { 1, 1, 1, 2, 2, 2, 3, 3, 3, 4, 4, 4, 5, 5, 5, 6, 6, 6, 7, 7, 7 };
                if (true)
                {
                    int a = decidedBlocksForLv4.Length;
                    while (a > 0)
                    {
                        int i = a - 1;
                        int j = Random.Range(0, a);
                        int tmp = decidedBlocksForLv4[i];
                        decidedBlocksForLv4[i] = decidedBlocksForLv4[j];
                        decidedBlocksForLv4[j] = tmp;
                        a--;
                    }
                }
                for (int i = 0; i < 20; i++) decidedBlocks[i] = decidedBlocksForLv4[i];
                decidedBlocks[Random.Range(0, 20)] = Random.Range(0, 2) == 0 ? STICKY_BLOCK : MAGNET_BLOCK;
                break;
            case 5:
                int[] decidedBlocksForLv5 = { 1, 1, 1, 2, 2, 2, 3, 3, 3, 4, 4, 4, 5, 5, 5, 6, 6, 6, 7, 7, 7 };
                if (true)
                {
                    int a = decidedBlocksForLv5.Length;
                    while (a > 0)
                    {
                        int i = a - 1;
                        int j = Random.Range(0, a);
                        int tmp = decidedBlocksForLv5[i];
                        decidedBlocksForLv5[i] = decidedBlocksForLv5[j];
                        decidedBlocksForLv5[j] = tmp;
                        a--;
                    }
                }
                for (int i = 0; i < 20; i++) decidedBlocks[i] = decidedBlocksForLv5[i];
                for (int i = 0; i < 3; i++) decidedBlocks[Random.Range(0, 20)] = SILVER_BLOCK;
                break;
            case 6:
                int[] decidedBlocksForLv6 = { 1, 1, 1, 2, 2, 2, 3, 3, 3, 4, 4, 4, 5, 5, 5, 6, 6, 6, 7, 7, 7 };
                if (true)
                {
                    int a = decidedBlocksForLv6.Length;
                    while (a > 0)
                    {
                        int i = a - 1;
                        int j = Random.Range(0, a);
                        int tmp = decidedBlocksForLv6[i];
                        decidedBlocksForLv6[i] = decidedBlocksForLv6[j];
                        decidedBlocksForLv6[j] = tmp;
                        a--;
                    }
                }
                for (int i = 0; i < 20; i++) decidedBlocks[i] = decidedBlocksForLv6[i];
                if (restOfSteps == 2 || restOfSteps == 5) decidedBlocks[Random.Range(0, 20)] = DECELERATE_BLOCK;
                break;
            case 7:
                int[] decidedBlocksForLv7 = { 1, 1, 1, 2, 2, 2, 3, 3, 3, 4, 4, 4, 5, 5, 5, 6, 6, 6, 7, 7, 7 };
                if (true)
                {
                    int a = decidedBlocksForLv7.Length;
                    while (a > 0)
                    {
                        int i = a - 1;
                        int j = Random.Range(0, a);
                        int tmp = decidedBlocksForLv7[i];
                        decidedBlocksForLv7[i] = decidedBlocksForLv7[j];
                        decidedBlocksForLv7[j] = tmp;
                        a--;
                    }
                }
                for (int i = 0; i < 20; i++) decidedBlocks[i] = decidedBlocksForLv7[i];
                if (restOfSteps == 3) decidedBlocks[Random.Range(0, 20)] = EXTRABALL_BLOCK;
                break;
            case 8:
                int[] decidedBlocksForLv8 = { 1, 2, 3, 4, 5, 6, 7, 1, 2, 3, 4, 5, 6, 7, 15, 15, 15, 15, 15, 15 };
                if (true)
                {
                    int a = decidedBlocksForLv8.Length;
                    while (a > 0)
                    {
                        int i = a - 1;
                        int j = Random.Range(0, a);
                        int tmp = decidedBlocksForLv8[i];
                        decidedBlocksForLv8[i] = decidedBlocksForLv8[j];
                        decidedBlocksForLv8[j] = tmp;
                        a--;
                    }
                }
                for (int i = 0; i < 20; i++) decidedBlocks[i] = decidedBlocksForLv8[i];
                break;
            case 9:
                int[] decidedBlocksForLv9 = { 1, 2, 3, 4, 5, 6, 7, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15 };
                if (true)
                {
                    int a = decidedBlocksForLv9.Length;
                    while (a > 0)
                    {
                        int i = a - 1;
                        int j = Random.Range(0, a);
                        int tmp = decidedBlocksForLv9[i];
                        decidedBlocksForLv9[i] = decidedBlocksForLv9[j];
                        decidedBlocksForLv9[j] = tmp;
                        a--;
                    }
                }
                for (int i = 0; i < 20; i++) decidedBlocks[i] = decidedBlocksForLv9[i];
                break;
            case 10:
                for (int i = 0; i < 20; i++) decidedBlocks[i] = SILVER_BLOCK;
                decidedBlocks[Random.Range(0, 20)] = ACCELERATE_BLOCK;
                break;
            case 11:
                int[] decidedBlocksForLv11 = { 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 16, 16, 16, 16, 16, 20 };
                if (true)
                {
                    int a = decidedBlocksForLv11.Length;
                    while (a > 0)
                    {
                        int i = a - 1;
                        int j = Random.Range(0, a);
                        int tmp = decidedBlocksForLv11[i];
                        decidedBlocksForLv11[i] = decidedBlocksForLv11[j];
                        decidedBlocksForLv11[j] = tmp;
                        a--;
                    }
                }
                for (int i = 0; i < 20; i++) decidedBlocks[i] = decidedBlocksForLv11[i];
                break;
            case 12:
                int[] decidedBlocksForLv12 = { 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 20, 20 };
                if (true)
                {
                    int a = decidedBlocksForLv12.Length;
                    while (a > 0)
                    {
                        int i = a - 1;
                        int j = Random.Range(0, a);
                        int tmp = decidedBlocksForLv12[i];
                        decidedBlocksForLv12[i] = decidedBlocksForLv12[j];
                        decidedBlocksForLv12[j] = tmp;
                        a--;
                    }
                }
                for (int i = 0; i < 20; i++) decidedBlocks[i] = decidedBlocksForLv12[i];
                break;
        }
    }
}
