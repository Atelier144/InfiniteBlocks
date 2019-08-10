﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level27System : MonoBehaviour {

    [SerializeField] GameObject[] gameObjectsSignals = new GameObject[15];

    [SerializeField] Sprite spriteSignalOff;
    [SerializeField] Sprite spriteSignalOn;
    [SerializeField] Sprite[] spritesSignals = new Sprite[12];

    SpriteRenderer[] spriteRenderersSignals = new SpriteRenderer[15];

    PrefabCreator prefabCreator;

    int levelStage = 1;
    int timerCount;

	// Use this for initialization
	void Start () {
        prefabCreator = GameObject.Find("PrefabCreator").GetComponent<PrefabCreator>();

        for (int i = 0; i < 15; i++) spriteRenderersSignals[i] = gameObjectsSignals[i].GetComponent<SpriteRenderer>();

        CreateLevelStage();
        StartCoroutine(Timer());
    }
	
	// Update is called once per frame
	void Update () {
        if (levelStage >= 15)
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
        if (GameObject.FindGameObjectsWithTag("Block").Length == 0)
        {
            levelStage++;
            CreateLevelStage();
        }
    }

    void CreateLevelStage()
    {
        switch (levelStage)
        {
            case 0:
                for (int x = 0; x < 5; x++)
                {
                    for (int y = 0; y < 7; y++)
                    {
                        float positionX1 = x * 50.0f - 400.0f;
                        float positionX2 = x * 50.0f - 100.0f;
                        float positionX3 = x * 50.0f + 200.0f;
                        float positionY = y * 20.0f + 20.0f;
                        int colorCode = 6 - y;
                        if (x == 2 && y == 3)
                        {
                            prefabCreator.CreateItemBlock(positionX1, positionY, 8);
                            prefabCreator.CreateItemBlock(positionX2, positionY, 9);
                            prefabCreator.CreateItemBlock(positionX3, positionY, 8);
                        }
                        else
                        {
                            prefabCreator.CreateNormalBlock(positionX1, positionY, colorCode);
                            prefabCreator.CreateNormalBlock(positionX2, positionY, colorCode);
                            prefabCreator.CreateNormalBlock(positionX3, positionY, colorCode);
                        }
                    }
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
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
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
                break;
            case 14:
                prefabCreator.CreateSilverBlock(0.0f, 100.0f);
                break;
        }
    }

    public bool IsLevelUp()
    {
        return levelStage >= 15;
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
