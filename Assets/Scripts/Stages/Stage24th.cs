using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage24th : Stage {

    SixGatesSystem sixGatesSystem;

    int[] numbersOfAppearItem = new int[8];
    int[] numbersOfAppearItemCode = { 5, 5, 11, 21, 11, 21, 11, 21 };

    int brokenBlocks;

    protected override void Start()
    {
        base.Start();
        int[] numbersMin = { 4, 10, 13, 25, 37, 53, 69, 85 };
        int[] numbersMax = { 6, 12, 24, 36, 52, 68, 84,100 };
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
        if (brokenBlocks >= 12) sixGatesSystem.OpenGate(0);
        if (brokenBlocks >= 24) sixGatesSystem.OpenGate(1);
        if (brokenBlocks >= 38) sixGatesSystem.OpenGate(2);
        if (brokenBlocks >= 52) sixGatesSystem.OpenGate(3);
        if (brokenBlocks >= 68) sixGatesSystem.OpenGate(4);
        if (brokenBlocks >= 84) sixGatesSystem.OpenGate(5);
    }

    public override void GenerateStage()
    {
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 7; y++)
            {
                float positionX = x * 50.0f - 225.0f;
                float positionY = y * 60.0f + 0.0f;
                int colorCode = 6 - y;
                int breakCount = y + 1;
                if (y == 6 && (x == 3 || x == 6)) prefabCreator.CreateCountLevelUpBlock(positionX, positionY, 12);
                else prefabCreator.CreateCountBlock(positionX, positionY, colorCode, breakCount);
            }
        }
        for (int i = 0; i < 6; i++)
        {
            float[] positionXs = { -300.0f, 300.0f, -350.0f, 350.0f, -400.0f, 400.0f };
            float[] positionYs = { 0.0f, 0.0f, 120.0f, 120.0f, 240.0f, 240.0f };
            float positionX = positionXs[i];
            float positionY = positionYs[i];
            prefabCreator.CreateItemBlock(positionX, positionY, 11);
        }
        for (int i = 0; i < 12; i++)
        {
            float[] positionXs = { -300.0f, 300.0f, -300.0f, 300.0f, -350.0f, 350.0f, -300.0f, 300.0f, -400.0f, 400.0f, -350.0f, 350.0f };
            float[] positionYs = { 50.0f, 50.0f, 170.0f, 170.0f, 230.0f, 230.0f, 290.0f, 290.0f, 290.0f, 290.0f, 350.0f, 350.0f };
            float positionX = positionXs[i];
            float positionY = positionYs[i];
            prefabCreator.CreateAccelerateBlock(positionX, positionY);
        }
        for (int i = 0; i < 12; i++)
        {
            float[] positionXs = { -300.0f, 300.0f, -350.0f, 350.0f, -300.0f, 300.0f, -350.0f, 350.0f, -300.0f, 300.0f, -400.0f, 400.0f };
            float[] positionYs = { 110.0f, 110.0f, 170.0f, 170.0f, 230.0f, 230.0f, 290.0f, 290.0f, 350.0f, 350.0f, 350.0f, 350.0f };
            float positionX = positionXs[i];
            float positionY = positionYs[i];
            prefabCreator.CreateFlashBlock(positionX, positionY);
        }
        prefabCreator.CreateCountLevelUpBlock(-450.0f, 360.0f, 8);
        prefabCreator.CreateCountLevelUpBlock(450.0f, 360.0f, 8);
        sixGatesSystem = prefabCreator.CreateSixGatesSystem();
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
