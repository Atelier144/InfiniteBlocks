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

    int levelStage = 13;
    int timerCount;
    bool isLevelUp;

	// Use this for initialization
	void Start () {
        prefabCreator = GameObject.Find("PrefabCreator").GetComponent<PrefabCreator>();

        for (int i = 0; i < 15; i++) spriteRenderersSignals[i] = gameObjectsSignals[i].GetComponent<SpriteRenderer>();

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
                break;
            case 5:
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
                break;
            case 8:
                break;
            case 9:
                break;
            case 10:
                break;
            case 11:
                break;
            case 12:
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
                prefabCreator.CreateSilverBlock(0.0f, 100.0f);
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
