using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level26System : MonoBehaviour {

    PrefabCreator prefabCreator;

    [SerializeField] GameObject prefabNormalBlock;
    int levelStage = 2;
    bool isLevelUp;
	// Use this for initialization
	void Start () {
        prefabCreator = GameObject.Find("PrefabCreator").GetComponent<PrefabCreator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        if(GameObject.FindGameObjectsWithTag("Block").Length == 0)
        {
            levelStage++;
            if (levelStage > 12) isLevelUp = true;
            else CreateLevelStage();
        }
    }

    void CreateLevelStage()
    {
        switch (levelStage)
        {
            case 1:
                for (int x = 0; x < 7; x++)
                {
                    for (int y = 0; y < 3; y++)
                    {
                        float positionX = x * 60.0f - 180.0f;
                        float positionY = y * 30.0f + 100.0f;
                        int colorCode = x;
                        CreateNormalBlock(positionX, positionY, colorCode);
                    }
                }
                break;
            case 2:
                for (int x = 0; x < 7; x++)
                {
                    for (int y = 0; y < 5; y++)
                    {
                        float positionX = x * 100.0f - 300.0f;
                        float positionY = y * 20.0f + 150.0f;
                        if (x % 2 == 0 || (y != 0 && y != 4)) prefabCreator.CreateHardBlock(positionX, positionY);
                    }
                }
                break;
            case 3:
                for (int i = 0; i < 7; i++)
                {
                    for (int x = 0; x < 5; x++)
                    {
                        for (int y = 0; y < 5; y++)
                        {
                            bool[,] pattern =
                            {
                                {false, false, true, false, false},
                                {false, true, true, true, false},
                                {true, true, true, true, true},
                                {false, true, true, true, false},
                                {false, false, true, false, false}
                            };

                            float[] positionPx = { -340.0f, -240.0f, -140.0f, -40.0f, 60.0f, 160.0f, 260.0f };
                            float[] positionPy = { 50.0f, 100.0f, 50.0f, 0.0f, 50.0f, 100.0f, 50.0f };
                            float positionX = x * 20.0f + positionPx[i];
                            float positionY = y * 20.0f + positionPy[i];
                            int colorCode = i;
                            if (pattern[x, y]) prefabCreator.CreateSmallBlock(positionX, positionY, colorCode);
                        }
                    }
                }
                break;
            case 4:
                for (int i = 0; i < 21; i++)
                {
                    float degree = 190.0f * Mathf.Deg2Rad - 10.0f * Mathf.Deg2Rad * i;
                    float positionX = Mathf.Round(400.0f * Mathf.Cos(degree));
                    float positionY = Mathf.Round(400.0f * Mathf.Sin(degree)) - 80.0f;
                    int colorCode = i % 7;
                    prefabCreator.CreateRoundBlock(positionX, positionY, colorCode);
                }
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                for (int x = 0; x < 36; x++)
                {
                    for (int y = 0; y < 7; y++)
                    {
                        int[,] pattern =
                        {
                            {0,1,0,0,1,1,1,0,1,1,1,0,1,0,0,0,1,0,1,1,1,0,1,1,0,0,1,1,0,1,0,1,0,1,0,1},
                            {1,0,1,0,0,1,0,0,1,0,0,0,1,0,0,0,1,0,1,0,0,0,1,0,1,0,0,1,0,1,0,1,0,1,0,1},
                            {1,0,1,0,0,1,0,0,1,0,0,0,1,0,0,0,1,0,1,0,0,0,1,0,1,0,0,1,0,1,0,1,0,1,0,1},
                            {1,1,1,0,0,1,0,0,1,1,1,0,1,0,0,0,1,0,1,1,1,0,1,1,0,0,0,1,0,1,1,1,0,1,1,1},
                            {1,0,1,0,0,1,0,0,1,0,0,0,1,0,0,0,1,0,1,0,0,0,1,0,1,0,0,1,0,0,0,1,0,0,0,1},
                            {1,0,1,0,0,1,0,0,1,0,0,0,1,0,0,0,1,0,1,0,0,0,1,0,1,0,0,1,0,0,0,1,0,0,0,1},
                            {1,0,1,0,0,1,0,0,1,1,1,0,1,1,1,0,1,0,1,1,1,0,1,0,1,0,0,1,0,0,0,1,0,0,0,1},

                        };
                        float positionX = x * 20.0f - 350.0f;
                        float positionY = y * -20.0f + 200.0f;
                        int colorCode = y;
                        if (pattern[y, x] == 1) prefabCreator.CreateSmallBlock(positionX, positionY, colorCode);
                    }

                }
                break;
            case 8:
                break;
        }
    }

    public bool IsLevelUp()
    {
        return isLevelUp;
    }


    void CreateNormalBlock(float positionX, float positionY, int colorCode)
    {
        Instantiate(prefabNormalBlock, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)).GetComponent<NormalBlock>().Initialize(colorCode);
    }
}
