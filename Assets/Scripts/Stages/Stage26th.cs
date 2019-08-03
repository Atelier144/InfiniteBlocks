using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage26th : Stage {

    Level26System level26System;

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
        level26System = prefabCreator.CreateLv26System();
        prefabCreator.CreateCeilingSystem();
    }

    public override int GenerateItemCode(int itemCode)
    {
        return itemCode;
    }

    public override bool IsLevelUp()
    {
        return level26System.IsLevelUp();
    }
}
