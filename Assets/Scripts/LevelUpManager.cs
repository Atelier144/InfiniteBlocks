using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpManager : MonoBehaviour {

    MainManager mainManager;
	// Use this for initialization
	void Start () {
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        if(mainManager.GetDialogStatus() == 2)
        {
            int countOfBlocks = GameObject.FindGameObjectsWithTag("Block").Length;
            int level = mainManager.GetLevel();
            InspectLevelUp(countOfBlocks, level);
        }
    }

    void InspectLevelUp(int countOfBlocks, int level)
    {
        if (countOfBlocks == 0)
        {
            if (level == 15)
            {
                return;
            }
            if (level == 25)
            {
                return;
            }
            if (level == 26)
            {
                return;
            }
            if (level == 27)
            {
                return;
            }
            if (level == 30)
            {
                return;
            }
            mainManager.LevelUp();
        }
    }
}
