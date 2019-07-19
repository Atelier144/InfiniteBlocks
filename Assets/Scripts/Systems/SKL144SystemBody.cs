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

    public void SetVelocity(float x, float y)
    {
        rbody2D.velocity = new Vector2(x, y);
    }

    public void SetBasePosition()
    {
        rbody2D.MovePosition(new Vector2(0.0f, 240.0f));
    }
}
