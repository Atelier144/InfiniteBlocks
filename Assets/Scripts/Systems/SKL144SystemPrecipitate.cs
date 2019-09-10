using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SKL144SystemPrecipitate : MonoBehaviour
{
    SignalManager signalManager;

    [SerializeField] GameObject gameObjectEffectLeft;
    [SerializeField] GameObject gameObjectEffectRight;

    [SerializeField] Sprite spriteEnabled;
    [SerializeField] Sprite spriteDisabled;

    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        signalManager = GameObject.Find("SignalManager").GetComponent<SignalManager>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        gameObjectEffectLeft.SetActive(false);
        gameObjectEffectRight.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.sprite = signalManager.IsActiveTrapGuard() ? spriteDisabled : spriteEnabled;
    }

    public void StartEffectLeft()
    {
        gameObjectEffectLeft.SetActive(false);
        gameObjectEffectLeft.SetActive(true);
    }

    public void StartEffectRight()
    {
        gameObjectEffectRight.SetActive(false);
        gameObjectEffectRight.SetActive(true);
    }
}
