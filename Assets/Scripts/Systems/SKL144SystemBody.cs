using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SKL144SystemBody : MonoBehaviour {

    Rigidbody2D rbody2D;

    void Start()
    {
        rbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BeginReturning()
    {
        float positionX = transform.position.x;
        float positionY = transform.position.y;


    }

    public void FinishReturning()
    {

    }
}
