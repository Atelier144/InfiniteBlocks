using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotSystem : MonoBehaviour {

    [SerializeField] GameObject[] gameObjectsSlotSwitches = new GameObject[4];
    [SerializeField] GameObject[] gameObjectsSlotDrums = new GameObject[3];
    [SerializeField] GameObject gameObjectSlotBoard;

    [SerializeField] GameObject prefabItem;

    SlotSystemSwitch[] slotSwitches = new SlotSystemSwitch[4];
    SlotSystemDrum[] slotDrums = new SlotSystemDrum[3];
    SlotSystemBoard slotBoard;

    GameObject gameObjectLevelUpTelop;

    int[] feverNumbers = { 0, 0, 0, 0, 0, 1, 1, 1, 2, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 5, 5, 6, 6, 7, 7, 7, 8, 8, 8, 8, 8 };
    bool isIdle = true;
    bool[] vsDrumRotating = { false, false, false };
    int[] decidedNumbers = { 0, 0, 0 };

    bool isRiggedReach;
    int riggedReachNumber;

    // Use this for initialization
    void Start () {
        for (int i = 0; i < 4; i++) slotSwitches[i] = gameObjectsSlotSwitches[i].GetComponent<SlotSystemSwitch>();
        for (int i = 0; i < 3; i++) slotDrums[i] = gameObjectsSlotDrums[i].GetComponent<SlotSystemDrum>();
        slotBoard = gameObjectSlotBoard.GetComponent<SlotSystemBoard>();

        gameObjectLevelUpTelop = GameObject.Find("Telops").transform.Find("LevelUpTelop").gameObject;   //非アクティブなGameObjectを取得するための手段
        StartCoroutine(StartAnimators());
    }
	
	// Update is called once per frame
	void Update () {
        if (gameObjectLevelUpTelop.activeSelf) slotBoard.SetTrigger("Finish");
    }

    public void OnSwtichOn(int switchCode)
    {
        if (isIdle)
        {
            if (switchCode == 3)
            {
                isIdle = false;
                slotSwitches[switchCode].SetTrigger("Off");
                slotBoard.SetTrigger("Rotating");
                for (int i = 0; i < 3; i++) 
                {
                    vsDrumRotating[i] = true;
                    slotDrums[i].StartDrum();
                    slotSwitches[i].SetTrigger("Skyblue");
                }
  
                int randomNumber = Random.Range(0, 60);
                if(randomNumber == 0)
                {
                    decidedNumbers[0] = 0;
                    decidedNumbers[1] = 1;
                    decidedNumbers[2] = 1;
                }
                else if(randomNumber <= 10)
                {
                    isRiggedReach = false;
                    int randomNumber2 = feverNumbers[Random.Range(0, feverNumbers.Length)];
                    for (int i = 0; i < 3; i++) decidedNumbers[i] = randomNumber2;
                }
                else if(randomNumber <= 30)
                {
                    isRiggedReach = true;
                    riggedReachNumber = feverNumbers[Random.Range(0, feverNumbers.Length)];

                }
                else
                {
                    isRiggedReach = false;
                    for (int i = 0; i < 3; i++) decidedNumbers[i] = Random.Range(0, 9);
                }
            }
        }
        else
        {
            if(switchCode==0 || switchCode == 1 || switchCode == 2)
            {
                if (vsDrumRotating[switchCode])
                {
                    vsDrumRotating[switchCode] = false;
                    slotSwitches[switchCode].SetTrigger("Off");
                    if (isRiggedReach)
                    {
                        if (GetDramStatus() != 0) 
                        {
                            decidedNumbers[switchCode] = riggedReachNumber;
                        }
                        else
                        {
                            decidedNumbers[switchCode] = Random.Range(0, 9);
                        }
                    }
                    slotDrums[switchCode].StopDrum(decidedNumbers[switchCode]);
                    switch (GetDramStatus())
                    {
                        case 0:
                            if(decidedNumbers[0] == decidedNumbers[1] && decidedNumbers[1] == decidedNumbers[2])
                            {
                                string[] triggers = { "Fever", "Ouch", "Excellent", "Fever", "Fever", "Fever", "Excellent", "Ouch", "Fever" };
                                GenerateItem();
                                slotBoard.SetTrigger(triggers[decidedNumbers[0]]);
                            }
                            else if(decidedNumbers[0] == 0 && decidedNumbers[1] == 1 && decidedNumbers[2] == 1)
                            {
                                slotBoard.SetTrigger("AtelierFever");
                            }
                            else
                            {
                                slotBoard.SetTrigger("Idle");
                            }
                            isIdle = true;
                            slotSwitches[3].SetTrigger("Rainbow");
                            break;
                        case 1:
                            if(decidedNumbers[1] == decidedNumbers[2])
                            {
                                string[] triggers = { "Reach", "AtelierPinch", "Reach", "Reach", "Reach", "Reach", "Reach", "Pinch", "Reach" };
                                slotBoard.SetTrigger(triggers[decidedNumbers[1]]);
                            }
                            break;
                        case 2:
                            if(decidedNumbers[0] == decidedNumbers[2])
                            {
                                string[] triggers = { "Reach", "Pinch", "Reach", "Reach", "Reach", "Reach", "Reach", "Pinch", "Reach" };
                                slotBoard.SetTrigger(triggers[decidedNumbers[0]]);
                            }
                            else if(decidedNumbers[0] == 0 && decidedNumbers[2] == 1)
                            {
                                slotBoard.SetTrigger("AtelierReach");
                            }
                            break;
                        case 4:
                            if(decidedNumbers[0] == decidedNumbers[1])
                            {
                                string[] triggers = { "Reach", "Pinch", "Reach", "Reach", "Reach", "Reach", "Reach", "Pinch", "Reach" };
                                slotBoard.SetTrigger(triggers[decidedNumbers[0]]);
                            }
                            else if(decidedNumbers[0] == 0 && decidedNumbers[1] == 1)
                            {
                                slotBoard.SetTrigger("AtelierReach");
                            }
                            break;
                    }
                }
            }
        }
    }

    int GetDramStatus()
    {
        int retval = 0;
        if (vsDrumRotating[0]) retval += 1;
        if (vsDrumRotating[1]) retval += 2;
        if (vsDrumRotating[2]) retval += 4;
        return retval;
    }

    void GenerateItem()
    {
        int[][] itemCodes =
        {
            new int[]{8,9},
            new int[]{22,16,6},
            new int[]{7},
            new int[]{2,2,3,3,3,4},
            new int[]{8,9,13,14},
            new int[]{2,2,3,3,3,4},
            new int[]{10},
            new int[]{23,17,18},
            new int[]{13,14}
        };
        int itemCode = itemCodes[decidedNumbers[0]][Random.Range(0, itemCodes[decidedNumbers[0]].Length)];
        Instantiate(prefabItem, new Vector3(190.0f, 137.0f, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)).GetComponent<Item>().Initialize(itemCode);
    }

    IEnumerator StartAnimators()
    {
        Debug.Log("こルーチン作動");
        yield return new WaitForSeconds(0.1f);
    }
}

// 7,3,3,4,4,4,6,6,6,5,5,5,5,1,1,1,1,1,9,9,9,9,9,2,2,2,2,2,2,8,8,8,8,8,8,
// 0,0,0,0,0,1,1,1,2,3,3,3,3,3,3,4,4,4,4,5,5,5,5,5,5,6,6,7,7,7,8,8,8,8,8
// 111: PowerUp or Protector
// 222: 200pts or 500pts or 1000pts
// 333: LevelUp
// 444: SpeedUp or -200pts or RacketShrink
// 555: PowerUp or Protector or Precipitate or Shooting
// 666: MaxSpeed or Flash or Counterfeit
// 777: ExtraBall
// 888: 200pts or 500pts or 1000pts
// 999: Precipitate or Shooting