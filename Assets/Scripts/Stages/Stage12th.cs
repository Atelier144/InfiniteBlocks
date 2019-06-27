using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage12th : Stage {

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

        for (int i = 0; i < 7; i++)
        {
            float px = i * 100.0f - 350.0f;
            float py = i % 2 == 0 ? 260.0f : 140.0f;
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    float positionX = x * 50.0f + px;
                    float positionY = y * 20.0f + py;
                    int colorCode = i;
                    if (x != 1 || y == 0 || y == 3) prefabCreator.CreateNormalBlock(positionX, positionY, colorCode);
                }
            }
            float positionFX = px + 50.0f;
            float positionFY = py + 30.0f;
            prefabCreator.CreateFlashBlock(positionFX, positionFY);
        }

        for (int x = 0; x < 2; x++)
        {
            for (int y = 0; y < 7; y++)
            {
                float positionX = x * 900.0f - 450.0f;
                float positionY = y * 60.0f - 40.0f;
                int colorCode = y;
                prefabCreator.CreateRoundBlock(positionX, positionY, colorCode);
            }
        }

        for (int x = 0; x < 2; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                float positionX = x * 700.0f - 350.0f;
                float positionY = y * 20.0f + 140.0f;
                int itemCode = y % 2 == 0 ? 9 : 11; // 9: itemCode of Protector 11: itemCode of TrapGuard
                prefabCreator.CreateItemBlock(positionX, positionY, itemCode);
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
