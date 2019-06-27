using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage26th : Stage {

    [SerializeField] GameObject prefabCeilingSystem;
    [SerializeField] GameObject prefabLevel26System;

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
        CreateCeilingSystem();
        CreateLevel26System();
    }

    public override int GenerateItemCode(int itemCode)
    {
        return itemCode;
    }

    public override bool IsLevelUp()
    {
        return level26System.IsLevelUp();
    }


    void CreateCeilingSystem()
    {
        Instantiate(prefabCeilingSystem, new Vector3(0.0f, 0.0f, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
    }

    void CreateLevel26System()
    {
        level26System = Instantiate(prefabLevel26System, new Vector3(0.0f, 0.0f, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)).GetComponent<Level26System>();
    }
}
