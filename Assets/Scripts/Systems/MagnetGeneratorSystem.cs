using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetGeneratorSystem : MonoBehaviour
{
    [SerializeField] GameObject prefabItem;

    int checkerCount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter2DFromCheckerUpside()
    {
        checkerCount++;
        if(checkerCount >= 30 && checkerCount % 8 == 0)
        {
            float[] positionsX = { -100.0f, 0.0f, 100.0f };
            float positionX = positionsX[Random.Range(0, positionsX.Length)];
            float positionY = 185.0f;
            int itemCode = 19;
            Instantiate(prefabItem, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)).GetComponent<Item>().Initialize(itemCode);
        }
    }

    public void OnCollisionEnter2DFromCheckerDownside()
    {
        checkerCount = 0;
    }
}
