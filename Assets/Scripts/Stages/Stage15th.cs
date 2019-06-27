using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage15th : Stage {

    BonusStageSystem bonusStageSystem;

    int[] numbersOfAppearItem = { };
    int[] numbersOfAppearItemCode = { };

    int brokenBlocks;
    int currentRestOfBalls;
    bool hasGotten3ExtraBalls;

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
        for (int x = 0; x < 13; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                int[,] blockPattern =
                        {
                            {3,4,1,4,3,4,1,4,3,4,1,4,3},
                            {4,3,4,3,4,3,4,3,4,3,4,3,4},
                            {3,1,3,4,4,3,1,3,4,4,3,1,3},
                            {1,2,1,3,3,1,2,1,3,3,1,2,1}
                        };
                float positionX = x * 50.0f - 300.0f;
                float positionY = y * 20.0f + 242.0f;
                switch (blockPattern[y, x])
                {
                    case 1:
                        prefabCreator.CreateItemBlock(positionX, positionY, 10); // 10:itemCode of LevelUp
                        break;
                    case 2:
                        prefabCreator.CreateItemBlock(positionX, positionY, 7); // 7:itemCode of ExtraBall
                        break;
                    case 3:
                        prefabCreator.CreateGoldBlock(positionX, positionY);
                        break;
                    case 4:
                        prefabCreator.CreateSilverBlock(positionX, positionY);
                        break;
                }
            }
        }
        for (int x = 0; x < 14; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                float positionX = x * 50.0f - 325.0f;
                float positionY = y * 20.0f + 72.0f;
                int colorCode = x / 2;
                prefabCreator.CreateNormalBlock(positionX, positionY, colorCode);
            }
        }
        for (int x = 0; x < 35; x++)
        {
            for (int y = 0; y < 2; y++)
            {
                float positionX = x * 20.0f - 340.0f;
                float positionY = y * 20.0f + 132.0f;
                int colorCode = x / 5;
                prefabCreator.CreateSmallBlock(positionX, positionY, colorCode);
            }
        }
        for (int x = 0; x < 14; x++)
        {
            for (int y = 0; y < 2; y++)
            {
                int[] countsForLv15 = { 2, 4 };
                float positionX = x * 50.0f - 325.0f;
                float positionY = y * 20.0f + 172.0f;
                int colorCode = x / 2;
                prefabCreator.CreateCountBlock(positionX, positionY, colorCode, countsForLv15[y]);
            }
        }
        for (int y = 0; y < 5; y++)
        {
            float positionY = y * 20.0f + 72.0f;
            prefabCreator.CreateHardBlock(-425.0f, positionY);
            prefabCreator.CreateHardBlock(425.0f, positionY);
        }
        bonusStageSystem = prefabCreator.CreateBonusStageSystem();
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
        return bonusStageSystem.HasNoExtraBalls() && GameObject.FindGameObjectsWithTag("Block").Length == 0;
    }
}
