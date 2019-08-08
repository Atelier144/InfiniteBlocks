using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSounds : MonoBehaviour
{
    [SerializeField] GameObject prefabSoundEffectGetPoint;

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
}
