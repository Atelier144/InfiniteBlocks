using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level27System : MonoBehaviour {

    [SerializeField] GameObject[] gameObjectsSignals = new GameObject[15];

    [SerializeField] Sprite spriteSignalOff;
    [SerializeField] Sprite spriteSignalOn;
    [SerializeField] Sprite[] spritesSignals = new Sprite[12];

    SpriteRenderer[] spriteRenderersSignals = new SpriteRenderer[15];

    PrefabCreator prefabCreator;

    AudioSource audioSource;

    int levelStage = 7;
    int timerCount;
    bool isLevelUp;

	// Use this for initialization
	void Start () {
        prefabCreator = GameObject.Find("PrefabCreator").GetComponent<PrefabCreator>();

        for (int i = 0; i < 15; i++) spriteRenderersSignals[i] = gameObjectsSignals[i].GetComponent<SpriteRenderer>();

        audioSource = GetComponent<AudioSource>();

        CreateLevelStage();
        StartCoroutine(Timer());
    }
	
	// Update is called once per frame
	void Update () {
        if (isLevelUp)
        {
            for (int i = 0; i < 15; i++)
            {
                int colorCode = (i + 12 - timerCount) % 12;
                spriteRenderersSignals[i].sprite = spritesSignals[colorCode];
            }
        }
        else
        {
            for (int i = 0; i < 15; i++)
            {
                if (i < levelStage) spriteRenderersSignals[i].sprite = spriteSignalOn;
                else spriteRenderersSignals[i].sprite = spriteSignalOff;
            }
        }
    }

    private void FixedUpdate()
    {
        if (levelStage >= 14) if (GameObject.FindGameObjectsWithTag("Block").Length == 0) isLevelUp = true;
    }

    void CreateLevelStage()
    {
        switch (levelStage)
        {
            case 0:
                for (int i = 0; i < 2; i++)
                {
                    for (int x = 0; x < 7; x++)
                    {
                        for (int y = 0; y < 4; y++)
                        {
                            float positionX = x * 50.0f + i * 450.0f - 375.0f;
                            float positionY = y * 20.0f + 122.0f;
                            int colorCode = x;
                            prefabCreator.CreateNormalBlock(positionX, positionY, colorCode);
                        }
                    }
                }
                for (int x = 0; x < 3; x++)
                {
                    float positionX = x * 450.0f - 450.0f;
                    float positionY = 182.0f;
                    prefabCreator.CreateItemBlock(positionX, positionY, 8);
                }
                break;
            case 1:
                for (int x = 0; x < 12; x++)
                {
                    float positionX = x * 80.0f - 440.0f;
                    float positionY = 157.0f;
                    prefabCreator.CreateHardRoundBlock(positionX, positionY);
                }
                for (int x = 0; x < 20; x++)
                {
                    float positionX = x * 50.0f - 475.0f;
                    float positionY = 112.0f;
                    prefabCreator.CreateHardBlock(positionX, positionY);
                }
                break;
            case 2:
                int baseColorCodeForPhase2 = Random.Range(0, 7);
                for (int x = 0; x < 50; x++)
                {
                    for (int y = 0; y < 3; y++)
                    {
                        float positionX = x * 20.0f - 490.0f;
                        float positionY = y * 20.0f + 142.0f;
                        int colorCode = (baseColorCodeForPhase2 + x + 7 - y) % 7;
                        prefabCreator.CreateSmallBlock(positionX, positionY, colorCode);
                    }
                }
                break;
            case 3:
                for (int x = 0; x < 7; x++)
                {
                    for (int y = 0; y < 3; y++)
                    {
                        float positionX = x * 140.0f - 420.0f;
                        float positionY = y * 20.0f + 142.0f;
                        int colorCode = x;
                        prefabCreator.CreateCountBlock(positionX, positionY, colorCode, 10);
                    }
                }
                for (int x = 0; x < 6; x++)
                {
                    float positionX = x * 140.0f - 350.0f;
                    float positionY = 182.0f;
                    prefabCreator.CreateItemBlock(positionX, positionY, 14);
                }
                break;
            case 4:
                int baseColorCodeForPhase4a = Random.Range(0, 7);
                int baseColorCodeForPhase4b = Random.Range(0, 7);
                for (int x = 0; x < 16; x++)
                {
                    float positionX = x * 60.0f - 455.0f;
                    float positionY = 154.0f;
                    int colorCode = (baseColorCodeForPhase4a + x + 1) % 7;
                    prefabCreator.CreateRoundBlock(positionX, positionY, colorCode);
                }
                for (int x = 0; x < 15; x++)
                {
                    float positionX = x * 60.0f - 425.0f;
                    float positionY = 102.0f;
                    int colorCode = (baseColorCodeForPhase4a + x + 1) % 7;
                    prefabCreator.CreateRoundBlock(positionX, positionY, colorCode);
                }
                for (int x = 0; x < 16; x++)
                {
                    float positionX = x * 60.0f - 455.0f;
                    float positionY = 50.0f;
                    int colorCode = (baseColorCodeForPhase4a + x) % 7;
                    prefabCreator.CreateRoundBlock(positionX, positionY, colorCode);
                }
                for (int x = 0; x < 50; x++)
                {
                    float positionX = x * 20.0f - 490.0f;
                    float positionY = 0.0f;
                    int colorCode = (baseColorCodeForPhase4b + x) % 7;
                    prefabCreator.CreateSmallBlock(positionX, positionY, colorCode);
                }
                break;
            case 5:
                for (int i = 0; i < 4; i++)
                {
                    for (int x = 0; x < 3; x++)
                    {
                        for (int y = 0; y < 3; y++)
                        {
                            int[,] pattern = { { 1, 0, 1 }, { 0, 2, 0 }, { 1, 0, 1 } };
                            float positionX = i * 200.0f + x * 50.0f - 350.0f;
                            float positionY = y * 20.0f + 120.0f;
                            switch (pattern[x, y])
                            {
                                case 0:
                                    prefabCreator.CreateNormalBlock(positionX, positionY, 1);
                                    break;
                                case 1:
                                    prefabCreator.CreateNormalBlock(positionX, positionY, 0);
                                    break;
                                case 2:
                                    prefabCreator.CreateItemBlock(positionX, positionY, 21);
                                    break;
                            }
                        }
                    }
                }
                break;
            case 6:
                for (int x = 0; x < 6; x++)
                {
                    float positionX = x * 140.0f - 350.0f;
                    float positionY = 140.0f;
                    prefabCreator.CreateFlashBlock(positionX, positionY);
                }
                for (int x = 0; x < 7; x++)
                {
                    float positionX = x * 140.0f - 420.0f;
                    float positionY = 140.0f;
                    int itemCode = x % 2 == 0 ? 11 : 9;
                    prefabCreator.CreateItemBlock(positionX, positionY, itemCode);
                }
                break;
            case 7:
                int[] colorCodesForPhase7 = { 0, 1, 2, 3, 4, 5, 6 };
                {
                    int a = colorCodesForPhase7.Length;
                    while (a > 0)
                    {
                        int i = a - 1;
                        int j = Random.Range(0, a);
                        int tmp = colorCodesForPhase7[i];
                        colorCodesForPhase7[i] = colorCodesForPhase7[j];
                        colorCodesForPhase7[j] = tmp;
                        a--;
                    }
                }
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        float[] positionXs = { 0.0f, 60.0f, 30.0f, -30.0f, -60.0f, -30.0f, 30.0f };
                        float[] positionYs = { 0.0f, 0.0f, 52.0f, 52.0f, 0.0f, -52.0f, -52.0f };
                        float positionX = positionXs[j] + i * 200.0f - 400.0f;
                        float positionY = positionYs[j] + 100.0f;
                        if (j == 0) prefabCreator.CreateHardRoundBlock(positionX, positionY);
                        else prefabCreator.CreateRoundBlock(positionX, positionY, colorCodesForPhase7[i]);
                    }
                }
                break;
            case 8:
                break;
            case 9:
                for (int x = 0; x < 19; x++)
                {
                    for (int y = 0; y < 4; y++)
                    {
                        int[,] pattern =
                        {

                            {1,0,1,1,1,0,1,1,1,0,1,1,1,0,1,1,1,0,1},
                            {1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1},
                            {1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1},
                            {1,1,1,0,1,1,1,0,1,1,1,0,1,1,1,0,1,1,1}

                        };
                        float positionX = x * 50.0f - 450.0f;
                        float positionY = y * 20.0f + 90.0f;
                        if (pattern[y, x] == 1) prefabCreator.CreateTransparentBlock(positionX, positionY);
                    }
                }
                break;
            case 10:
                for (int i = 0; i < 3; i++) 
                {
                    for (int x = 0; x < 5; x++)
                    {
                        for (int y = 0; y < 5; y++)
                        {
                            int[,] pattern = { { 0, 0, 0, 0, 0 }, { 0, 1, 1, 1, 0 }, { 0, 1, 2, 1, 0 }, { 0, 1, 1, 1, 0 }, { 0, 0, 0, 0, 0 } };
                            float positionX = x * 50.0f + i * 300.0f - 400.0f;
                            float positionY = y * 20.0f + 75.0f;
                            switch (pattern[x, y])
                            {
                                case 0:
                                    prefabCreator.CreateHardBlock(positionX, positionY);
                                    break;
                                case 1:
                                    prefabCreator.CreateTransparentBlock(positionX, positionY);
                                    break;
                                case 2:
                                    prefabCreator.CreateItemBlock(positionX, positionY, 8);
                                    break;
                            }
                        }
                    }
                }
                break;
            case 11:
                for (int x = 0; x < 9; x++)
                {
                    int[] pattern = { 0, 1, 0, 1, 0, 1, 0, 1, 0 };
                    float positionX = x * 100.0f - 400.0f;
                    float positionY = 120.0f;
                    switch (pattern[x])
                    {
                        case 0:
                            prefabCreator.CreateAccelerateBlock(positionX, positionY);
                            break;
                        case 1:
                            prefabCreator.CreateItemBlock(positionX, positionY, 21);
                            break;
                    }
                }
                break;
            case 12:
                for (int x = 0; x < 7; x++)
                {
                    float positionX = x * 140.0f - 420.0f;
                    float positionY = 140.0f;
                    int colorCode = x;
                    prefabCreator.CreateCountRoundBlock(positionX, positionY, colorCode, 10);
                }
                for (int x = 0; x < 7; x++)
                {
                    float positionX = x * 110.0f - 330.0f;
                    float positionY = 80.0f;
                    int colorCode = x;
                    prefabCreator.CreateCountRoundBlock(positionX, positionY, colorCode, 6);
                }
                for (int x = 0; x < 7; x++)
                {
                    float positionX = x * 80.0f - 240.0f;
                    float positionY = 20.0f;
                    int colorCode = x;
                    prefabCreator.CreateCountRoundBlock(positionX, positionY, colorCode, 3);
                }
                break;
            case 13:
                for (int x = 0; x < 3; x++)
                {
                    float positionX = x * 300.0f - 300.0f;
                    float positionY = 182.0f;
                    prefabCreator.CreateItemBlock(positionX, positionY, 12);
                }
                break;
            case 14:
                for (int x = 0; x < 20; x++)
                {
                    for (int y = 0; y < 4; y++)
                    {
                        float positionX = x * 50.0f - 475.0f;
                        float positionY = y * 20.0f + 122.0f;
                        if (y % 2 == 0) prefabCreator.CreateSilverBlock(positionX, positionY);
                        else prefabCreator.CreateGoldBlock(positionX, positionY);
                    }
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
