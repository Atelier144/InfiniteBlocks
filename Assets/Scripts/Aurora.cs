using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aurora : MonoBehaviour {

    [SerializeField] GameObject auroraSprite;
    [SerializeField] GameObject auroraSprite2;

    [SerializeField] Sprite spriteAuroraReplayMode;
    [SerializeField] Sprite spriteVerticalLooperAurora;
    [SerializeField] Sprite spriteVerticalLooperAurora2;
    [SerializeField] Sprite spriteLevelUpAurora;

    bool isActiveProtector;
    bool isActiveReplayModeAurora;
    bool isActiveVerticalLooperAurora;
    bool isActiveLevelUpAurora;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetActiveProtector(bool s)
    {
        isActiveProtector = s;
        DrawAurora();
    }
    public void SetActiveReplayModeAurora(bool s)
    {
        isActiveReplayModeAurora = s;
        DrawAurora();
    }

    public void SetActiveVerticalLooperAurora(bool s)
    {
        isActiveVerticalLooperAurora = s;
        DrawAurora();
    }

    public void SetActiveLevelUpAurora(bool s)
    {
        isActiveLevelUpAurora = s;
        DrawAurora();
    }

    void DrawAurora()
    {
        if (isActiveProtector)
        {
            auroraSprite.GetComponent<SpriteRenderer>().sprite = null;
        }
        else if (isActiveReplayModeAurora)
        {
            auroraSprite.GetComponent<SpriteRenderer>().sprite = spriteAuroraReplayMode;
        }
        else if (isActiveVerticalLooperAurora)
        {
            auroraSprite.GetComponent<SpriteRenderer>().sprite = spriteVerticalLooperAurora;
        }
        else if (isActiveLevelUpAurora)
        {
            auroraSprite.GetComponent<SpriteRenderer>().sprite = spriteLevelUpAurora;
        }
        else
        {
            auroraSprite.GetComponent<SpriteRenderer>().sprite = null;
        }

        if (isActiveVerticalLooperAurora)
        {
            auroraSprite2.GetComponent<SpriteRenderer>().sprite = spriteVerticalLooperAurora2;
        }
        else
        {
            auroraSprite2.GetComponent<SpriteRenderer>().sprite = null;
        }
    }
}
