using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage21st : Stage {

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
        int randomColorCode = Random.Range(0, 7);
        for (int px = 0; px < 3; px++)
        {
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    float positionX = x * 50.0f + px * 300.0f - 350.0f;
                    float positionY = y * 20.0f + 140.0f;
                    int colorCode = (randomColorCode + y + 2) % 7;
                    if (x != 1 || y == 0 || y == 3) prefabCreator.CreateNormalBlock(positionX, positionY, colorCode);
                }
            }
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 2; y++)
                {
                    float positionX = x * 50.0f + px * 300.0f - 350.0f;
                    float positionY = y * 140.0f + 100.0f;
                    prefabCreator.CreateHardBlock(positionX, positionY);
                }
            }
        }
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                float positionX = x * 300.0f - 450.0f;
                float positionY = y * 20.0f + 100.0f;
                int colorCode = (randomColorCode + y) % 7;
                prefabCreator.CreateNormalBlock(positionX, positionY, colorCode);
            }
        }
        for (int x = 0; x < 20; x++)
        {
            for (int y = 0; y < 2; y++)
            {
                float positionX = x * 50.0f - 475.0f;
                float positionY = y * 220.0f + 60.0f;
                prefabCreator.CreateTransparentBlock(positionX, positionY);
            }
        }
        for (int x = 0; x < 3; x++)
        {
            float positionX = x * 300.0f - 300.0f;
            float positionY = 170.0f;
            prefabCreator.CreateAccelerateBlock(positionX, positionY);
        }
        prefabCreator.CreateSlidingLongSteelBlocksSystem();
        prefabCreator.CreateVerticalLooperSystem();
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
