using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage29th : Stage {

    int[] numbersOfAppearItem = new int[7];
    int[] numbersOfAppearItemCode = { 4, 5, 8, 9, 11, 19, 20 };

    int brokenBlocks;

    protected override void Start()
    {
        base.Start();
        {
            int a = numbersOfAppearItemCode.Length;
            while (a > 0)
            {
                int i = a - 1;
                int j = Random.Range(0, a);
                int tmp = numbersOfAppearItemCode[i];
                numbersOfAppearItemCode[i] = numbersOfAppearItemCode[j];
                numbersOfAppearItemCode[j] = tmp;
                a--;
            }
        }
        numbersOfAppearItemCode[5] = 21;

        int[] numbersMin = { 9, 27, 45, 63, 81, 99, 117 };
        int[] numbersMax = { 18, 36, 54, 72, 90, 108, 120 };
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
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 7; y++)
            {
                float positionX1 = x * 50.0f - 450.0f;
                float positionX2 = x * 50.0f + 350.0f;
                float positionY = y * 20.0f + 100.0f;
                int colorCode = y;
                prefabCreator.CreateCountBlock(positionX1, positionY, colorCode, 5);
                prefabCreator.CreateCountBlock(positionX2, positionY, colorCode, 5);
            }
        }
        for (int x = 0; x < 7; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                float positionX1 = x * 20.0f - 160.0f;
                float positionX2 = x * 20.0f + 40.0f;
                float positionY = y * 20.0f + 315.0f;
                int colorCode1 = x;
                int colorCode2 = 6 - x;
                prefabCreator.CreateSmallBlock(positionX1, positionY, colorCode1);
                prefabCreator.CreateSmallBlock(positionX2, positionY, colorCode2);
            }
        }
        for (int x = 0; x < 7; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                int[,] pattern = { { 0, 0, 0, 0, 0, 0, 0 }, { 0, 1, 0, 1, 0, 1, 0 }, { 0, 0, 0, 0, 0, 0, 0 } };
                float positionX = x * 50.0f - 150.0f;
                float positionY = y * 20.0f;
                if (pattern[y, x] == 1) prefabCreator.CreateItemBlock(positionX, positionY, 11);
                else prefabCreator.CreateSilverBlock(positionX, positionY);
            }
        }
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                int[,] pattern = { { 0, 0, 0 }, { 0, 1, 0 }, { 0, 0, 0 } };
                float positionX1 = x * 50.0f - 450.0f;
                float positionX2 = x * 50.0f + 350.0f;
                float positionY = y * 20.0f + 280.0f;
                int colorCode = y;
                switch (pattern[x, y])
                {
                    case 0:
                        prefabCreator.CreateSilverBlock(positionX1, positionY);
                        prefabCreator.CreateSilverBlock(positionX2, positionY);
                        break;
                    case 1:
                        switch (Random.Range(0, 2))
                        {
                            case 0:
                                prefabCreator.CreateItemBlock(positionX1, positionY, 19);
                                prefabCreator.CreateItemBlock(positionX2, positionY, 8);
                                break;
                            case 1:
                                prefabCreator.CreateItemBlock(positionX1, positionY, 8);
                                prefabCreator.CreateItemBlock(positionX2, positionY, 19);
                                break;
                        }
                        break;
                }
            }
        }
        prefabCreator.CreateCountLevelUpBlock(0.0f, 335.0f, 10);
        prefabCreator.CreateTimeBomberSystem();
        prefabCreator.CreateCeilingSystem();
    }

    public override int GenerateItemCode(int itemCode)
    {
        int resultItemCode = 0;
        brokenBlocks++;
        if (itemCode != 0) return itemCode;
        for (int i = 0; i < numbersOfAppearItem.Length; i++) if (numbersOfAppearItem[i] == brokenBlocks) resultItemCode = numbersOfAppearItemCode[i];
        return resultItemCode;
    }

    public override bool IsLevelUp()
    {
        return GameObject.FindGameObjectsWithTag("Block").Length == 0;
    }
}
