using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage16th : Stage {

    int[] numbersOfAppearItem = new int[8];
    int[] numbersOfAppearItemCode = { 2, 3, 3, 5, 8, 8, 9, 11, 14 };

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
        numbersOfAppearItemCode[2] = 16;
        numbersOfAppearItemCode[4] = 11;
        numbersOfAppearItemCode[6] = 18;

        int[] numbersMin = { 13, 29, 45, 61, 77, 93, 109, 125 };
        int[] numbersMax = { 16, 32, 48, 64, 80, 96, 112, 128 };
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
        for (int x = 0; x < 18; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                float positionX = x * 50.0f - 425.0f;
                float positionY = y * 40.0f + 40.0f;
                prefabCreator.CreateTransparentBlock(positionX, positionY);
            }
        }
        prefabCreator.CreateCeilingSystem();
    }

    public override int GenerateItemCode(int itemCode)
    {
        int resultItemCode = 0;
        brokenBlocks++;
        if (itemCode != 0) return itemCode;
        for (int i = 0; i < numbersOfAppearItem.Length; i++) if (numbersOfAppearItem[i] == brokenBlocks) resultItemCode = numbersOfAppearItemCode[i];
        return resultItemCode;
    }

    public override bool IsLevelUp()
    {
        return GameObject.FindGameObjectsWithTag("Block").Length == 0;
    }
}
