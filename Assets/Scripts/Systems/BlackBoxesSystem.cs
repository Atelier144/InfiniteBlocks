using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBoxesSystem : MonoBehaviour {

    [SerializeField] GameObject[] gameObjectsSwitches = new GameObject[2];
    [SerializeField] GameObject[] gameObjectsSignals = new GameObject[2];
    [SerializeField] GameObject[] gameObjectsWindows = new GameObject[2];

    BlackBoxesSystemSwitch[] switches = new BlackBoxesSystemSwitch[2];
    GateSignal[] signals = new GateSignal[2];
    BlackBoxesSystemWindow[] windows = new BlackBoxesSystemWindow[2];

    bool[] vsWindodwOpen = { false, false };
    
	// Use this for initialization
	void Start () {
        switches[0] = gameObjectsSwitches[0].GetComponent<BlackBoxesSystemSwitch>();
        switches[1] = gameObjectsSwitches[1].GetComponent<BlackBoxesSystemSwitch>();
        signals[0] = gameObjectsSignals[0].GetComponent<GateSignal>();
        signals[1] = gameObjectsSignals[1].GetComponent<GateSignal>();
        windows[0] = gameObjectsWindows[0].GetComponent<BlackBoxesSystemWindow>();
        windows[1] = gameObjectsWindows[1].GetComponent<BlackBoxesSystemWindow>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnSwitchOn(int switchCode)
    {
        vsWindodwOpen[switchCode] = !vsWindodwOpen[switchCode];
        for (int i = 0; i < 2; i++)
        {
            switches[i].ChangeColor(vsWindodwOpen[i]);
            signals[i].ChangeSignal(vsWindodwOpen[i]);
            windows[i].SetOpen(vsWindodwOpen[i]);
        }
    }
}
