using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5th : Stage {

    int[] numbersOfAppearItem = new int[9];
    int[] numbersOfAppearItemCode;

    int brokenBlocks;

    protected override void Start()
    {
        base.Start();
        int[][] numbersOfAppearItemCodes =
        {
            new int[]{1,9,1,20,1,19,2,10,10},
            new int[]{1,19,1,9,1,21,2,10,10},
            new int[]{1,1,5,1,9,20,2,10,10},
            new int[]{9,1,1,1,5,21,2,10,10}
        };
        int[] numbersMin = { 10, 20, 40, 60, 80, 100, 120, 140, 145 };
        int[] numbersMax = { 12, 25, 45, 65, 85, 105, 125, 142, 146 };
        for (int i = 0; i < numbersOfAppearItem.Length; i++) numbersOfAppearItem[i] = Random.Range(numbersMin[i], numbersMax[i]);
        numbersOfAppearItemCode = numbersOfAppearItemCodes[Random.Range(0, numbersOfAppearItemCodes.Length)];
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
        int randomColorCode = Random.Range(7, 14);
        for(int x = 0; x < 7; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                float positionX = x * 50.0f - 150.0f;
                float positionY = y * 20.0f + 260.0f;
                int colorCode = 6 - x;
                prefabCreator.CreateNormalBlock(positionX, positionY, colorCode);
            }
        }
        for (int x = 0; x < 2; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                float positionX1 = x * 50.0f - 375.0f;
                float positionX2 = x * 50.0f + 325.0f;
                float positionY = y * 20.0f + 100.0f;
                prefabCreator.CreateHardBlock(positionX1, positionY);
                prefabCreator.CreateHardBlock(positionX2, positionY);
            }
        }
        for(int x = 0; x < 7; x++)
        {
            for(int y = 0; y < 7; y++)
            {
                float positionX1 = x * 20.0f - 430.0f;
                float positionX2 = x * 20.0f + 310.0f;
                float positionY = y * 20.0f + 200.0f;
                int colorCode = (x - y + randomColorCode) % 7;
                prefabCreator.CreateSmallBlock(positionX1, positionY, colorCode);
                prefabCreator.CreateSmallBlock(positionX2, positionY, colorCode);
            }
        }
        for (int x = 0; x < 7; x++)
        {
            float positionX = x * 80.0f - 240.0f;
            float positionY = 120.0f;
            int colorCode = 6 - x;
            prefabCreator.CreateRoundBlock(positionX, positionY, colorCode);
        }
        for (int x = 0; x < 12; x++)
        {
            float[] positionsX = { -462.5f, -387.5f, -312.5f, -237.5f, -112.5f, -37.5f, 37.5f, 112.5f, 237.5f, 312.5f, 387.5f, 462.5f };
            int[] directionCodes = { 1, 1, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1 };
            float positionX = positionsX[x];
            float positionY = 0.0f;
            int directionCode = directionCodes[x];
            prefabCreator.CreateUpDownGate(positionX, positionY, directionCode);
        }
        for(int x = 0; x < 10; x++)
        {
            float positionX = x * 50.0f - 225.0f;
            float positionY = 240.0f;
            prefabCreator.CreateSteelBlock(positionX, positionY);
        }
        prefabCreator.CreateSteelBlock(-175.0f, 0.0f);
        prefabCreator.CreateSteelBlock(175.0f, 0.0f);
        prefabCreator.CreateMagnetGeneratorSystem();
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
