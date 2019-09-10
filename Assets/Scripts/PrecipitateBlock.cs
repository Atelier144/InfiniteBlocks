using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrecipitateBlock : MonoBehaviour {

    SignalManager signalManager;

    [SerializeField] GameObject blockEffect;

    [SerializeField] Sprite spriteEnabled;
    [SerializeField] Sprite spriteDisabled;

    SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
        signalManager = GameObject.Find("SignalManager").GetComponent<SignalManager>();

        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        spriteRenderer.sprite = signalManager.IsActiveTrapGuard() ? spriteDisabled : spriteEnabled;
	}

    public void StartEffect()
    {
        blockEffect.SetActive(false);
        blockEffect.SetActive(true);
    }
}
