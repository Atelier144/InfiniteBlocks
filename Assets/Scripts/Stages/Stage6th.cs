using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage6th : Stage {

    int[] numbersOfAppearItem = new int[5];
    int[] numbersOfAppearItemCode;

    int brokenBlocks;

    protected override void Start()
    {
        base.Start();
        int[][] numbersOfAppearItemCodes =
        {
            new int[]{2,8,14,5,10},
            new int[]{14,5,8,2,10},
            new int[]{5,1,8,14,10},
            new int[]{8,2,5,14,10},
            new int[]{2,14,5,8,10}

        };
        int[] numbersMin = { 20, 50, 80, 110, 123 };
        int[] numbersMax = { 30, 60, 90, 120, 126 };
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
        for (int x = 0; x < 7; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                float positionX = x * 50.0f - 150.0f;
                float positionY = y * 20.0f;
                int colorCode = x;
                prefabCreator.CreateNormalBlock(positionX, positionY, colorCode);
            }
        }
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                float positionX1 = x * 50.0f - 400.0f;
                float positionX2 = x * 50.0f + 250.0f;
                float positionY = y * 20.0f + 40.0f;
                prefabCreator.CreateHardBlock(positionX1, positionY);
                prefabCreator.CreateHardBlock(positionX2, positionY);
            }
        }
        for (int x = 0; x < 7; x++)
        {
            for (int y = 0; y < 2; y++)
            {
                float positionX1 = x * 60.0f - 420.0f;
                float positionX2 = x * 60.0f + 60.0f;
                float positionY = y * 100.0f + 220.0f;
                int colorCode = x;
                prefabCreator.CreateRoundBlock(positionX1, positionY, colorCode);
                prefabCreator.CreateRoundBlock(positionX2, positionY, colorCode);
            }
        }
        for(int x = 0; x < 21; x++)
        {
            float positionX1 = x * 20.0f - 440.0f;
            float positionX2 = x * 20.0f + 40.0f;
            float positionY = 270.0f;
            int colorCode = x % 7;
            prefabCreator.CreateSmallBlock(positionX1, positionY, colorCode);
            prefabCreator.CreateSmallBlock(positionX2, positionY, colorCode);
        }
        prefabCreator.CreateSingleGateSystem();
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
