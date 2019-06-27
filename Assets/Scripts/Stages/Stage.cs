using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour {

    protected MainManager mainManager;
    protected SignalManager signalManager;
    protected PrefabCreator prefabCreator;
    protected Aurora aurora;

    [SerializeField] protected int levelUpBonus;
    [SerializeField] protected int musicChannel;
    [SerializeField] protected bool isLevelUpFailZone;

	// Use this for initialization
	protected virtual void Start () {
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
        signalManager = GameObject.Find("SignalManager").GetComponent<SignalManager>();
        prefabCreator = GameObject.Find("PrefabCreator").GetComponent<PrefabCreator>();
        aurora = GameObject.Find("Aurora").GetComponent<Aurora>();

        aurora.SetActiveLevelUpAurora(isLevelUpFailZone);
    }
	
	// Update is called once per frame
	protected virtual void Update () {
		
	}

    protected virtual void FixedUpdate()
    {

    }

    public virtual void GenerateStage()
    {

    }

    public virtual int GenerateItemCode(int itemCode)
    {
        return itemCode;
    }

    public virtual bool IsLevelUp()
    {
        return false;
    }

    public int GetLevelUpBonus()
    {
        return levelUpBonus;
    }

    public int GetMusicChannel()
    {
        return musicChannel;
    }

    public bool IsLevelUpFailZone()
    {
        return isLevelUpFailZone;
    }
}
