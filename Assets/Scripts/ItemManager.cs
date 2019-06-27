using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {

    const int NOTHING = 0;
    const int POINT_100 = 1;
    const int POINT_200 = 2;
    const int POINT_500 = 3;
    const int POINT_1000 = 4;
    const int EXPAND_RACKET = 5;
    const int SHRINK_RACKET = 6;
    const int EXTRA_BALL = 7;
    const int POWER_UP = 8;
    const int PROTECTOR = 9;
    const int LEVEL_UP = 10;
    const int TRAP_GUARD = 11;
    const int GAME_OVER = 12;
    const int PRECIPITATE = 13;
    const int SHOOTING = 14;
    const int HOSTAGE_300 = 15;
    const int NEGATIVE_POINT_200 = 16;
    const int FLASH = 17;
    const int COUNTERFEIT = 18;
    const int MAGNET = 19;
    const int STICKY = 20;
    const int DECELERATE = 21;
    const int ACCELERATE = 22;
    const int MAX_SPEED = 23;

    MainManager mainManager;

    int[][] itemSelectors =
    {
        new int[] {1},
        new int[] {1,1,1,1,1,2,2,5,8,9,13,14,19,20},
        new int[] {1,1,1,2,5,8,8,9,13,14,19,20},
        new int[] {1},
        new int[] {21,22,23},
        new int[] {1},
        new int[] {1},
        new int[] {1},
        new int[] {1},
        new int[] {1},
        new int[] {1},
        new int[] {1},
        new int[] {1},
        new int[] {1},
        new int[] {1},
        new int[] {1},
        new int[] {1},
        new int[] {1},
        new int[] {1},
        new int[] {1},
        new int[] {1},
        new int[] {1},
        new int[] {1},
        new int[] {1},
        new int[] {1},
        new int[] {1},
        new int[] {1},
        new int[] {1},
        new int[] {1},
        new int[] {1},
        new int[] {1}
    };

    int[] itemCountsMin = { 0, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
    int[] itemCountsMax = { 1, 20, 20, 20, 10, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20 };

    int[] extraBallAppearancesLevel = { 0, 1, 2 };
    int[] extraBallAppearances = { 0, 1, 2 };
    int[] levelUpAppearancesForLv1 = { 0, 1 };
    int[] levelUpAppearancesForLv2 = { 0, 1, 2 };
    int[] levelUpAppearancesForLv3 = { 0, 1, 2, 3 };
    int[] gameOverAppearancesForLv22 = { 0, 1, 2 };

    int itemCount;
    int limitedItemCount;

    // Use this for initialization
    void Start () {
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
        extraBallAppearancesLevel[0] = Random.Range(5, 8);
        extraBallAppearancesLevel[1] = Random.Range(11, 17);
        extraBallAppearancesLevel[2] = 23;

        levelUpAppearancesForLv1[0] = Random.Range(5, 10);
        levelUpAppearancesForLv1[1] = Random.Range(11, 20);

        levelUpAppearancesForLv2[0] = Random.Range(2, 5);
        levelUpAppearancesForLv2[1] = Random.Range(5, 7);
        levelUpAppearancesForLv2[2] = Random.Range(7, 10);

        levelUpAppearancesForLv3[0] = Random.Range(2, 5);
        levelUpAppearancesForLv3[1] = Random.Range(5, 7);
        levelUpAppearancesForLv3[2] = Random.Range(7, 10);
        levelUpAppearancesForLv3[3] = Random.Range(10, 13);

        gameOverAppearancesForLv22[0] = Random.Range(21, 50);
        gameOverAppearancesForLv22[1] = Random.Range(51, 100);
        gameOverAppearancesForLv22[2] = Random.Range(101, 200);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void InitializeItemCount()
    {
        int level = mainManager.GetLevel();
        itemCount = 0;
        limitedItemCount = Random.Range(itemCountsMin[level], itemCountsMax[level]);
    }

    public int GenerateItemCode()
    {
        int level = mainManager.GetLevel();
        itemCount++;
        if(itemCount >= limitedItemCount)
        {
            itemCount = 0;
            limitedItemCount = Random.Range(itemCountsMin[level], itemCountsMax[level]);
            int selectedItemCodeIndex = Random.Range(0, itemSelectors[level].Length);

            Debug.Log("limitedItemCount is " + limitedItemCount);
            return itemSelectors[level][selectedItemCodeIndex];
        }
        return NOTHING;
    }

    /*
    public int GenerateItemCode(int i)
    {
        int level = mainManager.GetComponent<MainManager>().GetLevel();
        int restOfBlocks = mainManager.GetComponent<MainManager>().GetRestOfBlocks();
        int retval = NOTHING;

        if (level == 1)
        {
            foreach(int levelUpAppearance in levelUpAppearancesForLv1)
            {
                if(levelUpAppearance == restOfBlocks)
                {
                    return LEVEL_UP;
                }
            }
            if (Random.Range(0,50) == 0)
            {
                return POINT_100;
            }
            if(Random.Range(0,120) == 0)
            {
                return PROTECTOR;
            }
            if(Random.Range(0,120) == 0)
            {
                return POWER_UP;
            }
            if(Random.Range(0,120) == 0)
            {
                return EXPAND_RACKET;
            }
            if(Random.Range(0,150) == 0)
            {
                return PRECIPITATE;
            }
        }
        if(level == 2)
        {
            foreach(int levelUpAppearance in levelUpAppearancesForLv2)
            {
                if(levelUpAppearance == restOfBlocks)
                {
                    return LEVEL_UP;
                }
            }
            if(Random.Range(0,60) == 0)
            {
                return POWER_UP;
            }
            if(Random.Range(0,50) == 0)
            {
                return POINT_100;
            }
            if(Random.Range(0,120) == 0)
            {
                return PROTECTOR;
            }
            if(Random.Range(0,150) == 0)
            {
                return PRECIPITATE;
            }
            if(Random.Range(0,150) == 0)
            {
                return POINT_200;
            }
        }
        if(level == 3)
        {
            foreach (int levelUpAppearance in levelUpAppearancesForLv3)
            {
                if (levelUpAppearance == restOfBlocks)
                {
                    return LEVEL_UP;
                }
            }
            if (Random.Range(0,50) == 0)
            {
                return POINT_100;
            }
            if(Random.Range(0,750) == 0)
            {
                return SHOOTING;
            }
        }
        if(level == 4)
        {
            if(Random.Range(0,50) == 0)
            {
                return POINT_100;
            }
            if(Random.Range(0,100) == 0)
            {
                return PROTECTOR;
            }
            if(Random.Range(0,100) == 0)
            {
                return POINT_200;
            }
        }
        if(level == 5)
        {
            if(Random.Range(0,50) == 0)
            {
                return POINT_100;
            }
        }
        if (level == 22)
        {
            /*
            foreach(int gameOverAppearance in gameOverAppearancesForLv22)
            {
                if(restOfBlocks == gameOverAppearance)
                {
                    return GAME_OVER;
                }
            }
  
        }
        if(level == 30)
        {
            if(Random.Range(0,50) == 0)
            {
                return POINT_1000;
            }
        }

        if (true)
        {
            if(Random.Range(0,50) == 0)
            {
                return POINT_100;
            }
            if(Random.Range(0,150) == 0)
            {
                return PROTECTOR;
            }
            if(Random.Range(0,200) == 0)
            {
                return POWER_UP;
            }
            if(Random.Range(0,200) == 0)
            {
                return MAGNET;
            }
            if(Random.Range(0,200) == 0)
            {
                return STICKY;
            }
            if(Random.Range(0,200) == 0)
            {
                return PRECIPITATE;
            }
            if(Random.Range(0,240) == 0)
            {
                return SHOOTING;
            }
            if(Random.Range(0,200) == 0)
            {
                return POINT_200;
            }
            if(Random.Range(0,300) == 0)
            {
                return EXPAND_RACKET;
            }
        }
        return retval;
    }
    */
}

