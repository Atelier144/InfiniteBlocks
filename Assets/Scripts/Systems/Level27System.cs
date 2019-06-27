using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level27System : MonoBehaviour {

    int levelStage;
    bool isLevelUp;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        if (GameObject.FindGameObjectsWithTag("Block").Length == 0)
        {
            levelStage++;
            if (levelStage > 20) isLevelUp = true;
            else CreateLevelStage();
        }
    }

    void CreateLevelStage()
    {
        switch (levelStage)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
            case 9:
                break;
            case 10:
                break;
            case 11:
                break;
            case 12:
                break;
            case 13:
                break;
            case 14:
                break;
            case 15:
                break;
            case 16:
                break;
            case 17:
                break;
            case 18:
                break;
            case 19:
                break;
            case 20:
                break;

        }
    }

    public bool IsLevelUp()
    {
        return isLevelUp;
    }
}
