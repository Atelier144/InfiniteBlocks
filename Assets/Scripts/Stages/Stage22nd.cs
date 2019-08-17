using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage22nd : Stage {

    int[] numbersOfAppearItem = new int[10];
    int[] numbersOfAppearItemCode = { 2, 2, 3, 3, 8, 9, 11, 11, 11, 101, 102 };

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
        numbersOfAppearItemCode[3] = 16;
        numbersOfAppearItemCode[4] = 6;
        numbersOfAppearItemCode[5] = 22;
        numbersOfAppearItemCode[7] = 15;
        numbersOfAppearItemCode[9] = 10;
        int[] numbersMin = { 10, 25, 40, 55, 70, 85, 100, 115, 130, 136 };
        int[] numbersMax = { 15, 30, 45, 60, 75, 90, 105, 120, 135, 138 };
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
        for (int x = 0; x < 7; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                float positionX1 = x * 50.0f + 50.0f;
                float positionX2 = -positionX1;
                float positionY = x * 20.0f + y * 20.0f + 62.0f;
                int colorCode = x;
                prefabCreator.CreateNormalBlock(positionX1, positionY, colorCode);
                prefabCreator.CreateNormalBlock(positionX2, positionY, colorCode);
            }
        }
        for (int i = 0; i < 3; i++)
        {
            for(int x = 0; x < 3;x++) 
            {
                for (int y = 0; y < 3; y++)
                {
                    float[] positionsPx = { -400.0f, -50.0f, 300.0f };
                    float[] positionsPy = { 42.0f, 282.0f, 42.0f };
                    float positionX = x * 50.0f + positionsPx[i];
                    float positionY = y * 20.0f + positionsPy[i];
                    if (x == 1 && y == 1) prefabCreator.CreateItemBlock(positionX, positionY, 12);
                    else prefabCreator.CreateSilverBlock(positionX, positionY);
                }
            }
        }
        for (int x = 0; x < 2; x++) 
        {
            for (int y = 0; y < 9; y++)
            {
                float positionX = x * 950.0f - 475.0f;
                float positionY = y * 20.0f + 202.0f;
                prefabCreator.CreateSteelBlock(positionX, positionY);
            }
        }
        prefabCreator.CreatePointBumper(-150.0f, 322.0f, 2);
        prefabCreator.CreatePointBumper(150.0f, 322.0f, 2);
        prefabCreator.CreatePointBumper(-200.0f, 42.0f, 1);
        prefabCreator.CreatePointBumper(200.0f, 42.0f, 1);
        prefabCreator.CreatePrecipitateBlock(-475.0f, 172.0f);
        prefabCreator.CreatePrecipitateBlock(475.0f, 172.0f);
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
