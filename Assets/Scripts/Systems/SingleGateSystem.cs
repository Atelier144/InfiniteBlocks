using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleGateSystem : MonoBehaviour {

    [SerializeField] GameObject gameObjectGateSwitch;
    [SerializeField] GameObject gameObjectGateSignal;
    [SerializeField] GameObject[] gameObjectsGateBar = new GameObject[2];

    SingleGateSystemSwitch gateSwitch;
    GateSignal gateSignal;
    GateBar[] gateBars = new GateBar[2];

    AudioSource audioSource;

    int gateOpenCount;

    bool isGateOpen;
	// Use this for initialization
	void Start () {
        gateSwitch = gameObjectGateSwitch.GetComponent<SingleGateSystemSwitch>();
        gateSignal = gameObjectGateSignal.GetComponent<GateSignal>();
        gateBars[0] = gameObjectsGateBar[0].GetComponent<GateBar>();
        gateBars[1] = gameObjectsGateBar[1].GetComponent<GateBar>();

        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnSwitchOn()
    {
        audioSource.time = 0.0f;
        audioSource.Play();

        isGateOpen = !isGateOpen;
        if (isGateOpen)
        {
            gateSwitch.ChangeColor("Green");
            gateSignal.ChangeSignal(true);
            gateBars[0].Open();
            gateBars[1].Open();

        }
        else
        {
            gateSwitch.ChangeColor("Red");
            gateSignal.ChangeSignal(false);
            gateBars[0].Shut();
            gateBars[1].Shut();
        }
    }

    public void OnTriggerOn(int triggerCode)
    {
        if (isGateOpen)
        {
            gateOpenCount = 0;
        }
        else if(triggerCode == 0)
        {
            gateOpenCount++;
            if (gateOpenCount >= 50)
            {
                gateOpenCount = 0;
                isGateOpen = true;
                gateSwitch.ChangeColor("Green");
                gateSignal.ChangeSignal(true);
                gateBars[0].Open();
                gateBars[1].Open();
            }
        }
        else if(triggerCode == 1)
        {
            gateOpenCount = 0;
        }
    }
}
