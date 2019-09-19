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
