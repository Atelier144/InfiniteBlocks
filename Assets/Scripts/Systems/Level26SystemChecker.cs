using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level26SystemChecker : MonoBehaviour
{
    [SerializeField] GameObject gameObjectLevel26System;

    Level26System level26System;
    // Start is called before the first frame update
    void Start()
    {
        level26System = gameObjectLevel26System.GetComponent<Level26System>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ball" || collision.gameObject.tag == "PoweredBall")
        {
            level26System.OnTriggerEnter2DFromChecker();
        }
    }
}
