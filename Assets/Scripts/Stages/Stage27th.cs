using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage27th : Stage {

    Level27System level27System;

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
        level27System = prefabCreator.CreateLv27System();
        prefabCreator.CreateCeilingSystem();
    }

    public override int GenerateItemCode(int itemCode)
    {
        return itemCode;
    }

    public override bool IsLevelUp()
    {
        return level27System.IsLevelUp();
    }
}
