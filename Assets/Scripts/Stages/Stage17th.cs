﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage17th : Stage {

    int[] numbersOfAppearItem = { };
    int[] numbersOfAppearItemCode = { };

    int brokenBlocks;

    protected override void Start()
    {
        base.Start();
        int[] numbersMin = { };
        int[] numbersMax = { };
        for (int i = 0; i < numbersOfAppearItem.Length; i++) numbersOfAppearItem[i] = Random.Range(numbersMin[i], numbersMax[i]);

        brokenBlocks = 0;
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void GenerateStage()
    {
        for(int i = 0; i < 2; i++)
        {
            for(int x = 0; x < 2; x++)
            {
                for(int y = 0; y < 7; y++)
                {
                    float positionX = x * 300.0f + i * 550.0f - 425.0f;
                    float positionY = y * 20.0f + 120.0f;
                    int colorCode = y;
                    prefabCreator.CreateNormalBlock(positionX, positionY, colorCode);
                }
            }
            for(int x = 0; x < 7; x++)
            {
                for(int y = 0; y < 2; y++)
                {
                    float positionX = x * 20.0f + i * 550.0f - 335.0f;
                    float positionY = y * 240.0f + 60.0f;
                    int colorCode = i == 0 ? x : 6 - x;
                    prefabCreator.CreateSmallBlock(positionX, positionY, colorCode);
                }
            }
            for (int j = 0; j < 7; j++)
            {
                float[] positionsX = { -350.0f, -350.0f, -350.0f, -275.0f, -200.0f, -200.0f, -200.0f };
                float[] positionsY = { 250.0f, 180.0f, 110.0f, 145.0f, 110.0f, 180.0f, 250.0f };
                float positionX = positionsX[j] + i * 550.0f;
                float positionY = positionsY[j];
                int colorCode = i == 0 ? j : 6 - j;
                prefabCreator.CreateCountRoundBlock(positionX, positionY, colorCode, 4);
            }
            if (true)
            {
                float positionX = i * 550.0f - 275.0f;
                float positionY = 215.0f;
                prefabCreator.CreateCountRoundLevelUpBlock(positionX, positionY, 20);
            }
            for(int x = 0; x < 2; x++)
            {
                for(int y = 0; y < 2; y++)
                {
                    float positionX = x * 300.0f + i * 550.0f - 425.0f;
                    float positionY = y * 240.0f + 60.0f;
                    prefabCreator.CreateSteelBlock(positionX, positionY);
                }
            }
        }
        for (int x = 0; x < 2; x++)
        {
            for (int y = 0; y < 7; y++)
            {
                float positionX = x * 50.0f - 25.0f;
                float positionY = y * 20.0f + 80.0f;
                int colorCode = y;
                prefabCreator.CreateNormalBlock(positionX, positionY, colorCode);
            }
        }
        prefabCreator.CreatePointBumper(0.0f, 330.0f, 1);
        prefabCreator.CreateBlackBoxesSystem();
        prefabCreator.CreateCeilingSystem();
    }

    public override int GenerateItemCode(int itemCode)
    {
        brokenBlocks++;
        if (itemCode != 0) return itemCode;
        for (int i = 0; i < numbersOfAppearItem.Length; i++) if (numbersOfAppearItem[i] == brokenBlocks) return numbersOfAppearItemCode[i];
        return 0;
    }

    public override bool IsLevelUp()
    {
        return GameObject.FindGameObjectsWithTag("Block").Length == 0;
    }
}
