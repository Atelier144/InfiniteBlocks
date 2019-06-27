using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage8th : Stage {

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
        for (int x = 0; x < 20; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                float positionX = x * 50.0f - 475.0f;
                float positionY = y * 40.0f + 80.0f;
                int colorCode = y;
                int breakCount = y + 2;
                prefabCreator.CreateCountBlock(positionX, positionY, colorCode, breakCount);
            }
        }
        for (int y = 0; y < 3; y++)
        {
            float positionX1 = -400.0f;
            float positionX2 = -350.0f;
            float positionX3 = 350.0f;
            float positionX4 = 400.0f;
            float positionY = y * 20.0f + 240.0f;
            int colorCode = 3;
            prefabCreator.CreateCountBlock(positionX1, positionY, colorCode, 2);
            prefabCreator.CreateCountBlock(positionX2, positionY, colorCode, 3);
            prefabCreator.CreateCountBlock(positionX3, positionY, colorCode, 3);
            prefabCreator.CreateCountBlock(positionX4, positionY, colorCode, 2);
        }
        for (int y = 0; y < 5; y++)
        {
            float positionX1 = -250.0f;
            float positionX2 = 250.0f;
            float positionY = y * 20.0f + 220.0f;
            int colorCode = 4;
            int breakCount = 4;
            prefabCreator.CreateCountBlock(positionX1, positionY, colorCode, breakCount);
            prefabCreator.CreateCountBlock(positionX2, positionY, colorCode, breakCount);
        }
        for (int x = 0; x < 7; x++)
        {
            float positionX = x * 50.0f - 150.0f;
            float positionY1 = 200.0f;
            float positionY2 = 320.0f;
            int colorCode = 5;
            int breakCount = 5;
            prefabCreator.CreateCountBlock(positionX, positionY1, colorCode, breakCount);
            prefabCreator.CreateCountBlock(positionX, positionY2, colorCode, breakCount);
        }
        for (int y = 0; y < 5; y++)
        {
            float positionX1 = -150.0f;
            float positionX2 = 150.0f;
            float positionY = y * 20.0f + 220.0f;
            int colorCode = 5;
            int breakCount = 5;
            prefabCreator.CreateCountBlock(positionX1, positionY, colorCode, breakCount);
            prefabCreator.CreateCountBlock(positionX2, positionY, colorCode, breakCount);
        }
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                int[,] pattern = { { 0, 0, 0 }, { 0, 1, 0 }, { 0, 0, 0 } };
                float positionX = x * 50.0f - 50.0f;
                float positionY = y * 20.0f + 240.0f;
                if (pattern[x, y] == 0) prefabCreator.CreateCountBlock(positionX, positionY, 6, 6);
                else prefabCreator.CreateCountLevelUpBlock(positionX, positionY, 10);
            }
        }
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
