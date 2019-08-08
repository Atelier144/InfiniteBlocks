using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3rd : Stage {

    int[] numbersOfAppearItem = new int[15];
    int[] numbersOfAppearItemCode;

    int brokenBlocks;

    protected override void Start()
    {
        base.Start();
        int[][] numbersOfAppearItemCodes =
        {
            new int[]{13,1,1,8,5,20,2,21,5,8,9,1,10,10,10},
            new int[]{14,1,2,8,5,19,1,21,8,5,9,1,10,10,10},
            new int[]{13,1,1,5,8,19,1,21,9,5,8,2,10,10,10}

        };

        int[] numbersMin = { 10, 30, 60, 90, 120, 150, 180, 210, 240, 270, 300, 330, 340, 350, 360 };
        int[] numbersMax = { 15, 40, 70, 100, 130, 160, 190, 220, 250, 280, 310, 335, 345, 355, 363 };
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
        for (int x = 0; x < 42; x++)
        {
            for (int y = 0; y < 14; y++)
            {
                int[] height = { 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10, 11, 11, 12, 12, 13, 13, 12, 12, 11, 11, 10, 10, 9, 9, 8, 8, 7, 7, 6, 6, 5, 5, 4, 4, 3, 3 };
                float positionX = x * 20.0f - 410.0f;
                float positionY = y * 20.0f + 50.0f;
                int colorCode = x / 6 % 7;
                if (height[x] >= y) prefabCreator.CreateSmallBlock(positionX, positionY, colorCode);
            }
        }
        prefabCreator.CreatePointBumper(-400.0f, 200.0f, 0);
        prefabCreator.CreatePointBumper(-400.0f, 320.0f, 0);
        prefabCreator.CreatePointBumper(-296.0f, 260.0f, 0);
        prefabCreator.CreatePointBumper(-192.0f, 320.0f, 0);
        prefabCreator.CreatePointBumper(400.0f, 200.0f, 0);
        prefabCreator.CreatePointBumper(400.0f, 320.0f, 0);
        prefabCreator.CreatePointBumper(296.0f, 260.0f, 0);
        prefabCreator.CreatePointBumper(192.0f, 320.0f, 0);
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
