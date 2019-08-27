using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level26System : MonoBehaviour {

    [SerializeField] GameObject[] gameObjectsSignals = new GameObject[8];

    [SerializeField] Sprite spriteSignalOff;
    [SerializeField] Sprite spriteSignalOn;
    [SerializeField] Sprite[] spritesSignals = new Sprite[12];

    SpriteRenderer[] spriteRenderersSignals = new SpriteRenderer[8];

    PrefabCreator prefabCreator;

    AudioSource audioSource;

    int levelStage;
    int timerCount;
    bool isLevelUp;

	// Use this for initialization
	void Start () {
        prefabCreator = GameObject.Find("PrefabCreator").GetComponent<PrefabCreator>();

        for (int i = 0; i < 8; i++) spriteRenderersSignals[i] = gameObjectsSignals[i].GetComponent<SpriteRenderer>();

        audioSource = GetComponent<AudioSource>();

        CreateLevelStage();
        StartCoroutine(Timer());
	}
	
	// Update is called once per frame
	void Update () {
		if(isLevelUp)
        {
            for(int i=0; i < 8; i++)
            {
                int colorCode = (i + 12 - timerCount) % 12;
                spriteRenderersSignals[i].sprite = spritesSignals[colorCode];
            }
        }
        else
        {
            for (int i = 0; i < 8; i++)
            {
                if (i < levelStage) spriteRenderersSignals[i].sprite = spriteSignalOn;
                else spriteRenderersSignals[i].sprite = spriteSignalOff;
            }
        }
    }

    private void FixedUpdate()
    {
        if (levelStage >= 7) if (GameObject.FindGameObjectsWithTag("Block").Length == 0) isLevelUp = true;
    }

    void CreateLevelStage()
    {
        switch (levelStage)
        {
            case 0:
                for (int x = 0; x < 7; x++)
                {
                    for (int y = 0; y < 3; y++)
                    {
                        float positionX = x * 70.0f - 210.0f;
                        float positionY = y * 40.0f + 162.0f;
                        int colorCode = x;
                        prefabCreator.CreateNormalBlock(positionX, positionY, colorCode);
                    }
                }
                break;
            case 1:
                for (int x = 0; x < 7; x++)
                {
                    int[] numberOfBlocks = { 1, 2, 3, 4, 3, 2, 1 };
                    for (int y = 0; y < numberOfBlocks[x]; y++)
                    {
                        float[] positionsPy = { 160.0f, 140.0f, 120.0f, 100.0f, 120.0f, 140.0f, 160.0f };
                        float positionX = x * 100.0f - 300.0f;
                        float positionY = y * 40.0f + positionsPy[x];
                        prefabCreator.CreateHardBlock(positionX, positionY);
                    }
                }
                break;
            case 2:
                for (int i = 0; i < 7; i++) 
                {
                    for (int x = 0; x < 5; x++)
                    {
                        for (int y = 0; y < 3; y++)
                        {
                            int[,] pattern = { { 0, 0, 1 }, { 0, 1, 1 }, { 1, 1, 1 }, { 0, 1, 1 }, { 0, 0, 1 } };
                            float positionX = x * 20.0f + i * 120.0f - 400.0f;
                            float positionY = y * 20.0f + 222.0f;
                            int colorCode = i;
                            if (pattern[x,y] == 1) prefabCreator.CreateSmallBlock(positionX, positionY, colorCode);
                        }
                    }
                }
                break;
            case 3:
                for (int i = 0; i < 7; i++)
                {
                    float[] positionXs = { 40.0f, 120.0f, 198.0f, 265.0f, 309.0f, 325.0f, 325.0f };
                    float[] positionYs = { -45.0f, -45.0f, -29.0f, 15.0f, 82.0f, 160.0f, 240.0f };
                    float positionX1 = positionXs[i];
                    float positionX2 = -positionXs[i];
                    float positionY = positionYs[i];
                    int colorCode = i;
                    prefabCreator.CreateRoundBlock(positionX1, positionY, colorCode);
                    prefabCreator.CreateRoundBlock(positionX2, positionY, colorCode);
                }
                break;
            case 4:
                for (int x = 0; x < 7; x++)
                {
                    float positionXn = x * 120.0f - 360.0f;
                    float positionXs1 = x * 120.0f - 340.0f;
                    float positionXs2 = x * 120.0f - 360.0f;
                    float positionXs3 = x * 120.0f - 380.0f;
                    float positionXr = x * 120.0f - 360.0f;
                    float positionYn1 = 0.0f;
                    float positionYn2 = 200.0f;
                    float positionYs1 = 50.0f;
                    float positionYs2 = 150.0f;
                    float positionYr = 100.0f;
                    int colorCode = 6 - x;
                    prefabCreator.CreateNormalBlock(positionXn, positionYn1, colorCode);
                    prefabCreator.CreateNormalBlock(positionXn, positionYn2, colorCode);
                    prefabCreator.CreateSmallBlock(positionXs1, positionYs1, colorCode);
                    prefabCreator.CreateSmallBlock(positionXs2, positionYs1, colorCode);
                    prefabCreator.CreateSmallBlock(positionXs3, positionYs1, colorCode);
                    prefabCreator.CreateSmallBlock(positionXs1, positionYs2, colorCode);
                    prefabCreator.CreateSmallBlock(positionXs2, positionYs2, colorCode);
                    prefabCreator.CreateSmallBlock(positionXs3, positionYs2, colorCode);
                    prefabCreator.CreateRoundBlock(positionXr, positionYr, colorCode);
                }
                for (int y = 0; y < 5; y++)
                {
                    float positionX1 = -450.0f;
                    float positionX2 = 450.0f;
                    float positionY = y * 40.0f + 20.0f;
                    prefabCreator.CreateHardBlock(positionX1, positionY);
                    prefabCreator.CreateHardBlock(positionX2, positionY);
                }
                break;
            case 5:
                for (int x = 0; x < 15; x++)
                {
                    int[] blockCode = { 0, 1, 0, 2, 0, 3, 0, 4, 0, 5, 0, 6, 0, 7, 0 };
                    float positionX = x * 50.0f - 350.0f;
                    float positionY = 262.0f;
                    switch (blockCode[x])
                    {
                        case 0:
                            prefabCreator.CreateSilverBlock(positionX, positionY);
                            break;
                        case 1:
                            prefabCreator.CreateItemBlock(positionX, positionY, 8);
                            break;
                        case 2:
                            prefabCreator.CreateItemBlock(positionX, positionY, 9);
                            break;
                        case 3:
                            prefabCreator.CreateItemBlock(positionX, positionY, 11);
                            break;
                        case 4:
                            prefabCreator.CreateItemBlock(positionX, positionY, 13);
                            break;
                        case 5:
                            prefabCreator.CreateItemBlock(positionX, positionY, 14);
                            break;
                        case 6:
                            prefabCreator.CreateItemBlock(positionX, positionY, 19);
                            break;
                        case 7:
                            prefabCreator.CreateItemBlock(positionX, positionY, 20);
                            break;
                    }
                }
                break;
            case 6:

                for (int x = 0; x < 36; x++)
                {
                    for (int y = 0; y < 7; y++)
                    {
                        int[,] pattern =
                        {
                            {0,1,0,0,1,1,1,0,1,1,1,0,1,0,0,0,1,0,1,1,1,0,1,1,0,0,1,1,0,1,0,1,0,1,0,1},
                            {1,0,1,0,0,1,0,0,1,0,0,0,1,0,0,0,1,0,1,0,0,0,1,0,1,0,0,1,0,1,0,1,0,1,0,1},
                            {1,0,1,0,0,1,0,0,1,0,0,0,1,0,0,0,1,0,1,0,0,0,1,0,1,0,0,1,0,1,0,1,0,1,0,1},
                            {1,1,1,0,0,1,0,0,1,1,1,0,1,0,0,0,1,0,1,1,1,0,1,1,0,0,0,1,0,1,1,1,0,1,1,1},
                            {1,0,1,0,0,1,0,0,1,0,0,0,1,0,0,0,1,0,1,0,0,0,1,0,1,0,0,1,0,0,0,1,0,0,0,1},
                            {1,0,1,0,0,1,0,0,1,0,0,0,1,0,0,0,1,0,1,0,0,0,1,0,1,0,0,1,0,0,0,1,0,0,0,1},
                            {1,0,1,0,0,1,0,0,1,1,1,0,1,1,1,0,1,0,1,1,1,0,1,0,1,0,0,1,0,0,0,1,0,0,0,1},
                        };
                        float positionX = x * 20.0f - 350.0f;
                        float positionY = y * -20.0f + 200.0f;
                        int colorCode = y;
                        if (pattern[y, x] == 1) prefabCreator.CreateSmallBlock(positionX, positionY, colorCode);
                    }
                }
                break;
            case 7:
                for (int x = 0; x < 7; x++)
                {
                    float positionX = x * 140.0f - 420.0f;
                    float positionY1 = 20.0f;
                    float positionY2 = 200.0f;
                    int colorCode = x;
                    int breakCount1 = 5;
                    int breakCount2 = 10;
                    prefabCreator.CreateCountBlock(positionX, positionY1, colorCode, breakCount1);
                    prefabCreator.CreateCountBlock(positionX, positionY2, colorCode, breakCount2);
                }

                break;
        }
    }

    public bool IsLevelUp()
    {
        return isLevelUp;
    }

    public void OnTriggerEnter2DFromChecker()
    {
        if (GameObject.FindGameObjectsWithTag("Block").Length == 0)
        {
            levelStage++;
            CreateLevelStage();
            audioSource.time = 0.0f;
            audioSource.Play();
        }
    }

    IEnumerator Timer()
    {
        while (true)
        {
            timerCount++;
            if (timerCount >= 12) timerCount = 0;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
