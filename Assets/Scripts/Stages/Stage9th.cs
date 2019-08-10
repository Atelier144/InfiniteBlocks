using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage9th : Stage {

    int[] numbersOfAppearItem = new int[3];
    int[] numbersOfAppearItemCode;

    int brokenBlocks;

    protected override void Start()
    {
        base.Start();
        int[][] numbersOfAppearItemCodes =
        {
            new int[]{9,6,101},
            new int[]{6,9,101},
            new int[]{9,101,6},
            new int[]{6,101,9},
            new int[]{101,9,6},
            new int[]{101,6,9}
        };
        int[] numbersMin = { 1, 3, 5 };
        int[] numbersMax = { 3, 5, 7 };
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
        for (int y = 0; y < 7; y++)
        {
            float positionY = y * 50.0f + 50.0f;
            int colorCode = 6 - y;
            prefabCreator.CreateSlidingCountBlock(0.0f, positionY, colorCode, 10);
        }
        prefabCreator.CreateCeilingSystem();
    }

    public override int GenerateItemCode(int itemCode)
    {
        int[] itemCodesFor101 = { 5, 14, 19, 20 };
        int resultItemCode = 0;
        brokenBlocks++;
        if (itemCode != 0) return itemCode;
        for (int i = 0; i < numbersOfAppearItem.Length; i++) if (numbersOfAppearItem[i] == brokenBlocks) resultItemCode = numbersOfAppearItemCode[i];
        if (resultItemCode == 101) resultItemCode = itemCodesFor101[Random.Range(0, itemCodesFor101.Length)];
        return resultItemCode;
    }

    public override bool IsLevelUp()
    {
        return GameObject.FindGameObjectsWithTag("Block").Length == 0;
    }
}
