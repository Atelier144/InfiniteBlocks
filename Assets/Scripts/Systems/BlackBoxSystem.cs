using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBoxSystem : MonoBehaviour {

    MainManager mainManager;
    SignalManager signalManager;

    [SerializeField] GameObject body;
    [SerializeField] Sprite spriteHiddenBox;
    [SerializeField] Sprite spriteTransparentBox;
    [SerializeField] Sprite spriteShownBox;

    SpriteRenderer spriteRendererBody;

    bool isHidden = true;

	// Use this for initialization
	void Start () {
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
        signalManager = GameObject.Find("SignalManager").GetComponent<SignalManager>();
        spriteRendererBody = body.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isHidden)
        {
            if (signalManager.IsActiveTrapGuard())
            {
                spriteRendererBody.sprite = spriteTransparentBox;
            }
            else
            {
                spriteRendererBody.sprite = spriteHiddenBox;
            }
        }
        else
        {
            spriteRendererBody.sprite = spriteShownBox;
        }
    }

    public void Hide()
    {
        isHidden = true;
    }

    public void Show()
    {
        isHidden = false;
    }
}
