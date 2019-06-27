using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage29th : Stage {

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
            for (int y = 0; y < 3; y++)
            {
                float positionX = x * 50.0f - 150.0f;
                float positionY = y * 20.0f + 315.0f;
                int colorCode = x;
                if (x == 3 && y == 2) prefabCreator.CreateCountLevelUpBlock(positionX, positionY, 16);
                else prefabCreator.CreateCountBlock(positionX, positionY, colorCode, 4);
            }
        }
        prefabCreator.CreateTimeBomberSystem();
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
