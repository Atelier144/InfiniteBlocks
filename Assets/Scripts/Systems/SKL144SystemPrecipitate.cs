using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SKL144SystemPrecipitate : MonoBehaviour
{
    [SerializeField] GameObject gameObjectEffectLeft;
    [SerializeField] GameObject gameObjectEffectRight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
