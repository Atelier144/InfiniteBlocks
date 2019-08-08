using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4th : Stage {

    int[] numbersOfAppearItem = new int[6];
    int[] numbersOfAppearItemCode;

    int brokenBlocks;

    protected override void Start()
    {
        base.Start();
        int[][] numbersOfAppearItemCodes = {
            new int[]{1,8,5,9,19,10},
            new int[]{1,19,8,5,3,10},
            new int[]{5,1,1,19,8,10},
            new int[]{3,8,20,14,5,10},
            new int[]{14,1,5,8,9,10}

        };
        int[] numbersMin = { 7, 20, 40, 55, 63, 72 };
        int[] numbersMax = { 10, 24, 42, 59, 65, 76 };
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
        int startColorCode = Random.Range(0, 7);
        for (int x = 0; x < 16; x++)
        {
            float positionX = x * 60.0f - 455.0f;
            int colorCode1 = (startColorCode + x + 3) % 7;
            int colorCode2 = (startColorCode + x + 2) % 7;
            int colorCode3 = (startColorCode + x + 1) % 7;
            prefabCreator.CreateRoundBlock(positionX, 124.0f, colorCode1);
            prefabCreator.CreateRoundBlock(positionX, 228.0f, colorCode2);
            prefabCreator.CreateRoundBlock(positionX, 332.0f, colorCode3);
        }
        for (int x = 0; x < 15; x++)
        {
            float positionX = x * 60.0f - 425.0f;
            int colorCode4 = (startColorCode + x + 3) % 7;
            int colorCode5 = (startColorCode + x + 2) % 7;
            prefabCreator.CreateRoundBlock(positionX, 176.0f, colorCode4);
            prefabCreator.CreateRoundBlock(positionX, 280.0f, colorCode5);
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
