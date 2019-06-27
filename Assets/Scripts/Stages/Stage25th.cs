using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage25th : Stage {

    JackpotChallengeSystem jackpotChallengeSystem;

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

        for (int x = 0; x < 14; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                float positionX = x * 50.0f - 325.0f;
                float positionY = y * 20.0f + 40.0f;
                int colorCode = 6 - x / 2;
                prefabCreator.CreateNormalBlock(positionX, positionY, colorCode);
            }
        }

        jackpotChallengeSystem = prefabCreator.CreateJackpotChallengeSystem();
        prefabCreator.CreateCeilingSystem();
    }

    public override int GenerateItemCode(int itemCode)
    {
        return itemCode;
    }

    public override bool IsLevelUp()
    {
        return jackpotChallengeSystem.HasGottenJackpot();
    }
}
