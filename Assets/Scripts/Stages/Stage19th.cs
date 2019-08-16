using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage19th : Stage {

    int[] numbersOfAppearItem = new int[8];
    int[] numbersOfAppearItemCode = { 3, 3, 5, 8, 9, 11, 11, 101, 102 };

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
        numbersOfAppearItemCode[5] = 17;
        numbersOfAppearItemCode[7] = 10;

        int[] numbersMin = { 10, 25, 40, 55, 70, 85, 100, 110 };
        int[] numbersMax = { 15, 30, 45, 60, 75, 90, 105, 111 };
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
        int baseColorCode = 0;
        for(int i = 0; i < 3; i++)
        {
            float[] positionsPx = { -450.0f, -100.0f, 250.0f };
            float[] positionsPy = { 120.0f, 240.0f, 120.0f };
            for (int x = 0; x < 5; x++)
            {
                for(int y = 0; y < 6; y++)
                {
                    float positionX = x * 50.0f + positionsPx[i];
                    float positionY = y * 20.0f + positionsPy[i];
                    int colorCode1 = (baseColorCode + y) % 7;
                    int colorCode2 = (baseColorCode + y + 6) % 7;
                    int colorCode = i == 1 ? colorCode2 : colorCode1;
                    if (x == 0 || x == 4 || y == 0 || y == 5) prefabCreator.CreateNormalBlock(positionX, positionY, colorCode);
                }
            }
            float positionXp = positionsPx[i] + 100.0f;
            float positionYp = positionsPy[i] + 50.0f;
            prefabCreator.CreatePrecipitateBlock(positionXp, positionYp);
        }
        for (int i = 0; i < 3; i++)
        {
            float[] positionsPx = { -400.0f, -50.0f, 300.0f };
            float[] positionsPy = { 280.0f, 140.0f, 280.0f };
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    float positionX = x * 50.0f + positionsPx[i];
                    float positionY = y * 20.0f + positionsPy[i];
                    int colorCode = (baseColorCode + y + 1) % 7;
                    if (x == 1 && y == 1)
                    {
                        int itemCode = i == 1 ? 9 : 11;
                        prefabCreator.CreateItemBlock(positionX, positionY, itemCode);
                    }
                    else
                    {
                        prefabCreator.CreateNormalBlock(positionX, positionY, colorCode);
                    }

                }
            }
        }
        for (int i = 0; i < 16; i++)
        {
            float[] positionsX = { 215.0f, 195.0f, 175.0f, 175.0f, 175.0f, 175.0f, 175.0f, 175.0f, 175.0f, 175.0f, 175.0f, 175.0f, 175.0f, 175.0f, 155.0f, 135.0f };
            float[] positionsY = { 120.0f, 120.0f, 120.0f, 140.0f, 160.0f, 180.0f, 200.0f, 220.0f, 240.0f, 260.0f, 280.0f, 300.0f, 320.0f, 340.0f, 340.0f, 340.0f };
            int[] colorCodes = { 0, 0, 0, 1, 2, 3, 4, 5, 6, 0, 1, 2, 3, 4, 4, 4 };
            float positionX1 = positionsX[i];
            float positionX2 = -positionsX[i];
            float positionY = positionsY[i];
            int colorCode = (baseColorCode + colorCodes[i]) % 7;
            prefabCreator.CreateSmallBlock(positionX1, positionY, colorCode);
            prefabCreator.CreateSmallBlock(positionX2, positionY, colorCode);
        }
        prefabCreator.CreateCeilingSystem();
    }

    public override int GenerateItemCode(int itemCode)
    {
        int resultItemCode = 0;
        brokenBlocks++;
        if (itemCode != 0) return itemCode;
        for (int i = 0; i < numbersOfAppearItem.Length; i++) if (numbersOfAppearItem[i] == brokenBlocks) resultItemCode = numbersOfAppearItemCode[i];
        if (resultItemCode == 101) resultItemCode = Random.Range(0, 2) == 0 ? 13 : 14;
        if (resultItemCode == 102) resultItemCode = Random.Range(0, 2) == 0 ? 19 : 20;
        return resultItemCode;
    }

    public override bool IsLevelUp()
    {
        return GameObject.FindGameObjectsWithTag("Block").Length == 0;
    }
}
