using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Protector : MonoBehaviour {

    MainManager mainManager;
    SystemManager systemManager;
    SignalManager signalManager;

    // Use this for initialization
    void Start()
    {
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
        systemManager = GameObject.Find("SystemManager").GetComponent<SystemManager>();
        signalManager = GameObject.Find("SignalManager").GetComponent<SignalManager>();
    }

    // Update is called once per frame
    void Update()
    {
    
    }
}
