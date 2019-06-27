using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage22nd : Stage {

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
