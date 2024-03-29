﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage18th : Stage {

    protected override void Start()
    {
        base.Start();
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
        for(int x = 0; x < 7; x++)
        {
            for (int y = 0; y < 16; y++) 
            {
                float positionX1 = x * 35.0f - 465.0f;
                float positionX2 = x * 35.0f + 255.0f;
                float positionY = y * 20.0f;
                int colorCode1 = x;
                int colorCode2 = 6 - x;
                if (x % 2 == 0)
                {
                    prefabCreator.CreateSmallBlock(positionX1, positionY, colorCode1);
                    prefabCreator.CreateSmallBlock(positionX2, positionY, colorCode2);
                }
                else
                {
                    prefabCreator.CreateNormalBlock(positionX1, positionY, colorCode1);
                    prefabCreator.CreateNormalBlock(positionX2, positionY, colorCode2);
                }
            }
        }
        for(int x = 0; x < 3; x++)
        {
            float positionX1 = x * 90.0f - 450.0f;
            float positionX2 = x * 90.0f + 270.0f;
            float positionY = 340.0f;
            prefabCreator.CreateFlashBlock(positionX1, positionY);
            prefabCreator.CreateFlashBlock(positionX2, positionY);
        }
        prefabCreator.CreateSlotSystem();
        prefabCreator.CreateCeilingSystem();
    }

    public override int GenerateItemCode(int itemCode)
    {
        return itemCode;
    }

    public override bool IsLevelUp()
    {
        return GameObject.FindGameObjectsWithTag("Block").Length == 0;
    }
}
