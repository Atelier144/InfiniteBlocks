using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4th : Stage {

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
