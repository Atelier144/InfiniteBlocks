using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage27th : Stage {

    [SerializeField] GameObject prefabCeilingSystem;
    [SerializeField] GameObject prefabLevel27System;

    Level27System level27System;

    int brokenBlocks;

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
        CreateLevel27System();
        CreateCeilingSystem();
    }

    public override int GenerateItemCode(int itemCode)
    {
        return itemCode;
    }

    public override bool IsLevelUp()
    {
        return level27System.IsLevelUp();
    }

    void CreateCeilingSystem()
    {
        Instantiate(prefabCeilingSystem, new Vector3(0.0f, 0.0f, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
    }

    void CreateLevel27System()
    {
        level27System = Instantiate(prefabLevel27System, new Vector3(0.0f, 0.0f, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)).GetComponent<Level27System>();
    }
}
