using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateBar : MonoBehaviour {

    [SerializeField] int directionCode;
    [SerializeField] int distance;

    float movingSpeed = 0.0f;
    int movingCount = 0;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (movingCount > 0)
        {
            movingCount--;
            this.transform.position += new Vector3(movingSpeed, 0.0f, 0.0f);
        }
    }

    public void Open()
    {
        movingCount = distance - movingCount;
        if (directionCode == 0) movingSpeed = -15.0f;
        if (directionCode == 1) movingSpeed = 15.0f;
    }

    public void Shut()
    {
        movingCount = distance - movingCount;
        if (directionCode == 0) movingSpeed = 15.0f;
        if (directionCode == 1) movingSpeed = -15.0f;
    }
}
