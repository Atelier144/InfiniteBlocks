using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage10th : Stage {

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
        for (int px = 0; px < 3; px++)
        {
            for (int py = 0; py < 2; py++)
            {
                for (int x = 0; x < 5; x++)
                {
                    for (int y = 0; y < 5; y++)
                    {
                        int[,] pattern =
                        {
                                    { 3, 1, 2, 1, 3 },
                                    { 1, 1, 1, 1, 1 },
                                    { 2, 1, 3, 1, 2 },
                                    { 1, 1, 1, 1, 1 },
                                    { 3, 1, 2, 1, 3 }
                                };
                        float positionX = x * 50.0f + px * 300.0f - 400.0f;
                        float positionY = y * 20.0f + py * 140.0f + 100.0f;
                        switch (pattern[x, y])
                        {
                            case 1:
                                prefabCreator.CreateSilverBlock(positionX, positionY);
                                break;
                            case 2:
                                prefabCreator.CreateGoldBlock(positionX, positionY);
                                break;
                            case 3:
                                prefabCreator.CreateItemBlock(positionX, positionY, 10);
                                break;
                        }
                    }
                }
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
