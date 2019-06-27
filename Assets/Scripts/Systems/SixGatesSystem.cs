using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SixGatesSystem : MonoBehaviour {

    MainManager mainManager;

    [SerializeField] GameObject[] bars = new GameObject[12];
    [SerializeField] GameObject[] signals = new GameObject[12];

    [SerializeField] Sprite spriteRedSignal;
    [SerializeField] Sprite spriteGreenSignal;

    bool[] isGateOpen = { false, false, false, false, false, false };
    // Use this for initialization
	void Start () {
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void OpenGate(int gateId)
    {
        if (!isGateOpen[gateId])
        {
            isGateOpen[gateId] = true;
            int indexLeft = gateId * 2;
            int indexRight = gateId * 2 + 1;

            bars[indexLeft].GetComponent<SixGatesSystemBar>().Open();
            bars[indexRight].GetComponent<SixGatesSystemBar>().Open();

            signals[indexLeft].GetComponent<SpriteRenderer>().sprite = spriteGreenSignal;
            signals[indexRight].GetComponent<SpriteRenderer>().sprite = spriteGreenSignal;

            this.GetComponent<AudioSource>().Play();
        }
    }
}
