using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level27SystemChecker : MonoBehaviour
{
    [SerializeField] GameObject gameObjectLevel27System;

    Level27System level27System;
    // Start is called before the first frame update
    void Start()
    {
        level27System = gameObjectLevel27System.GetComponent<Level27System>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball" || collision.gameObject.tag == "PoweredBall")
        {
            level27System.OnTriggerEnter2DFromChecker();
        }
    }
}
