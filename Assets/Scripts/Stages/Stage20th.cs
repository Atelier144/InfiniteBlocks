using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage20th : Stage {

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
        for (int x = 0; x < 5; x++) 
        {
            for (int y = 0; y < 5; y++) 
            {
                int[,] blockPattern = { { 2, 0, 1, 0, 2 }, { 0, 3, 0, 3, 0 }, { 1, 0, 4, 0, 1 }, { 0, 3, 0, 3, 0 }, { 2, 0, 1, 0, 2 } };
                float positionX = x * 50.0f - 100.0f;
                float positionY = y * 20.0f + 180.0f;
                int blockCode = blockPattern[x, y];
                if (blockCode == 0) prefabCreator.CreateSilverBlock(positionX, positionY);
                if (blockCode == 1) prefabCreator.CreateGoldBlock(positionX, positionY);
                if (blockCode == 2) prefabCreator.CreateItemBlock(positionX, positionY, 11);
                if (blockCode == 3) prefabCreator.CreateItemBlock(positionX, positionY, 10);
                if (blockCode == 4) prefabCreator.CreateItemBlock(positionX, positionY, 7);
            }
        }
        for (int px = 0; px < 5; px++) 
        {
            for (int py = 0; py < 2; py++) 
            {
                for (int x = 0; x < 3; x++)
                {
                    for (int y = 0; y < 3; y++)
                    {
                        int[,] blockPattern = { { 1, 0, 1 }, { 0, 2, 0 }, { 1, 0, 1 } };
                        float positionX = x * 50.0f + px * 200.0f - 450.0f;
                        float positionY = y * 20.0f + py * 200.0f + 100.0f;
                        int blockCode = blockPattern[x, y];
                        if (blockCode == 0) prefabCreator.CreateSilverBlock(positionX, positionY);
                        if (blockCode == 1) prefabCreator.CreateGoldBlock(positionX, positionY);
                        if (blockCode == 2) prefabCreator.CreateItemBlock(positionX, positionY, 10);
                    }
                }
            }
        }
        for (int px = 0; px < 2; px++)
        {
            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    int[,] blockPattern = { { 0, 1, 0 }, { 1, 2, 1 }, { 0, 1, 0 }, { 1, 2, 1 }, { 0, 1, 0 } };
                    float positionX = x * 50.0f + px * 600.0f - 400.0f;
                    float positionY = y * 20.0f + 200.0f;
                    int blockCode = blockPattern[x, y];
                    if (blockCode == 0) prefabCreator.CreateSilverBlock(positionX, positionY);
                    if (blockCode == 1) prefabCreator.CreateGoldBlock(positionX, positionY);
                    if (blockCode == 2)
                    {
                        if ((px == 0 && x == 1) || (px == 1 && x == 3)) prefabCreator.CreateItemBlock(positionX, positionY, 9);
                        else prefabCreator.CreateItemBlock(positionX, positionY, 8);
                    }

                }
            }
        }
        for (int x = 0; x < 17; x++)
        {
            int[] blockPatterns = { 1, 2, 1, 0, 0, 0, 0, 1, 2, 1, 0, 0, 0, 0, 1, 2, 1 };
            float positionX = x * 50.0f - 400.0f;
            float positionY = 40.0f;
            int blockCode = blockPatterns[x];
            if (blockCode == 0) prefabCreator.CreateHardBlock(positionX, positionY);
            if (blockCode == 1) prefabCreator.CreateSteelBlock(positionX, positionY);
            if (blockCode == 2) prefabCreator.CreatePrecipitateBlock(positionX, positionY);
        }
        prefabCreator.CreateUpDownGate(-462.5f, 40.0f, 0);
        prefabCreator.CreateUpDownGate(462.5f, 40.0f, 0);
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

/*
 *                 if (blockCode == 1) prefabCreator.CreateSilverBlock(positionX, positionY);
                if (blockCode == 2) prefabCreator.CreateGoldBlock(positionX, positionY);
                if (blockCode == 3) prefabCreator.CreateItemBlock(positionX, positionY, 8); // 8:itemCode of PowerUp
                if (blockCode == 4) prefabCreator.CreateItemBlock(positionX, positionY, 9); // 9:itemCode of Protector;
                if (blockCode == 5) prefabCreator.CreateItemBlock(positionX, positionY, 11); // 11:itemCode of TrapGuard;
                if (blockCode == 6) prefabCreator.CreateItemBlock(positionX, positionY, 10); // 10:itemCode of LevelUp;
                if (blockCode == 7) prefabCreator.CreateItemBlock(positionX, positionY, 7); // 7:itemCode of ExtraBall;
*/
