using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1st : Stage {

    int[] numbersOfAppearItem = { 10, 25, 40, 65, 80, 105, 115, 120 };
    int[] numbersOfAppearItemCodes = { 1, 8, 1, 13, 5, 19, 10, 10 };

    int brokenBlocks;

    protected override void Start()
    {
        base.Start();

        int[] numbersMin = { 10, 25, 40, 65, 80, 105, 115, 120 };
        int[] numbersMax = { 15, 30, 45, 70, 85, 110, 120, 123 };
        for (int i = 0; i < numbersOfAppearItem.Length; i++) numbersOfAppearItem[i] = Random.Range(numbersMin[i], numbersMax[i]);

        int[] randomPatterns = mainManager.GetRandomPatterns(3);
        int[] randomItemIndexes = { 8, 13, 19 };

        randomItemIndexes[0] = Random.Range(0, 2) == 0 ? 8 : 9;
        randomItemIndexes[1] = Random.Range(0, 2) == 0 ? 13 : 14;
        randomItemIndexes[2] = Random.Range(0, 2) == 0 ? 19 : 20;

        numbersOfAppearItemCodes[1] = randomItemIndexes[randomPatterns[0]];
        numbersOfAppearItemCodes[2] = Random.Range(0, 5) == 0 ? 2 : 1;
        numbersOfAppearItemCodes[3] = randomItemIndexes[randomPatterns[1]];
        numbersOfAppearItemCodes[5] = randomItemIndexes[randomPatterns[2]];

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
            for (int y = 0; y < 7; y++)
            {
                float positionX = x * 50.0f - 425.0f;
                float positionY = y * 20.0f + 200.0f;
                int colorCode = y;
                prefabCreator.CreateNormalBlock(positionX, positionY, colorCode);
            }
        }
        prefabCreator.CreateCeilingSystem();
    }

    public override int GenerateItemCode(int itemCode)
    {
        brokenBlocks++;
        if (itemCode != 0) return itemCode;
        for (int i = 0; i < numbersOfAppearItem.Length; i++) if (numbersOfAppearItem[i] == brokenBlocks) return numbersOfAppearItemCodes[i];
        return 0;
    }

    public override bool IsLevelUp()
    {
        return GameObject.FindGameObjectsWithTag("Block").Length == 0;
    }
}
