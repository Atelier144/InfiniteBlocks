using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage7th : Stage {

    int[] numbersOfAppearItem = new int[6];
    int[] numbersOfAppearItemCode = { 13, 5, 8, 21, 13, 10 };

    int brokenBlocks;

    protected override void Start()
    {
        base.Start();
        int[] numbersMin = { 25, 50, 75, 100, 125, 150 };
        int[] numbersMax = { 35, 60, 85, 110, 135, 152 };
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
        //Atelier144
        //Small block x:-454,y:26 pitch xy:28
        //ATELIER x:-46,y:314 pitch x:80
        //144 x:-17,163,391 y:142
        for (int x = 0; x < 12; x++)
        {
            for (int y = 0; y < 12; y++)
            {
                float positionX = x * 28.0f - 454.0f;
                float positionY = y * 28.0f + 26.0f;
                int colorCode = 7;
                prefabCreator.CreateSmallBlock(positionX, positionY, colorCode);
            }
        }
        for (int x = 0; x < 7; x++)
        {
            int[] characterCodes = { 0, 1, 2, 3, 4, 2, 5 };
            float positionX = x * 80.0f - 46.0f;
            float positionY = 314.0f;
            int characterCode = characterCodes[x];
            prefabCreator.CreateAtelierHardBlock(positionX, positionY, characterCode);
        }
        for (int x = 0; x < 3; x++)
        {
            int[] characterCodes = { 0, 1, 1 };
            float[] positionsX = { -17.0f, 163.0f, 391.0f };
            float positionX = positionsX[x];
            float positionY = 142.0f;
            int characterCode = characterCodes[x];
            prefabCreator.CreateAtelierHardestBlock(positionX, positionY, characterCode);
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
