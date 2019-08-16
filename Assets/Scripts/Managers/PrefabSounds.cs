using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSounds : MonoBehaviour
{
    [SerializeField] GameObject prefabSoundEffectGetPoint;
    [SerializeField] GameObject prefabSoundEffectGetExtraBall;
    [SerializeField] GameObject prefabSoundEffectGetCounterfeit;
    [SerializeField] GameObject prefabSoundEffectGetNothing;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetPoint()
    {
        Instantiate(prefabSoundEffectGetPoint);
    }

    public void GetExtraBall()
    {
        Instantiate(prefabSoundEffectGetExtraBall);
    }

    public void GetCounterfeit()
    {
        Instantiate(prefabSoundEffectGetCounterfeit);
    }

    public void GetNothing()
    {
        Instantiate(prefabSoundEffectGetNothing);
    }
}
