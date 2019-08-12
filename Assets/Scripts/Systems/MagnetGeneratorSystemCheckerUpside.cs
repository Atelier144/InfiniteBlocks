using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetGeneratorSystemCheckerUpside : MonoBehaviour
{
    [SerializeField] GameObject gameObjectMagnetCheckerSystem;

    MagnetGeneratorSystem magnetGeneratorSystem;
    // Start is called before the first frame update
    void Start()
    {
        magnetGeneratorSystem = gameObjectMagnetCheckerSystem.GetComponent<MagnetGeneratorSystem>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball" || collision.gameObject.tag == "PoweredBall")
        {
            magnetGeneratorSystem.OnCollisionEnter2DFromCheckerUpside();
        }
    }
}
