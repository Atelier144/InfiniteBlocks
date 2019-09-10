using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SKL144SystemBody : MonoBehaviour {


    PrefabCreator prefabCreator;

    Rigidbody2D rbody2D;
    AudioSource audioSource;

    void Start()
    {
        prefabCreator = GameObject.Find("PrefabCreator").GetComponent<PrefabCreator>();

        rbody2D = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetVelocity(float x, float y)
    {
        rbody2D.velocity = new Vector2(x, y);
    }

    public void SetPosition(float x, float y)
    {
        rbody2D.MovePosition(new Vector2(x, y));
    }

    public void SetBasePosition()
    {
        rbody2D.MovePosition(new Vector2(0.0f, 240.0f));
    }

    public void CreateNormalBlocks()
    {
        float positionX1 = transform.position.x - 50.0f;
        float positionX2 = transform.position.x + 50.0f;
        float positionY = transform.position.y + 130.0f;
        prefabCreator.CreateNormalBlock(positionX1, positionY, Random.Range(0, 7));
        prefabCreator.CreateNormalBlock(positionX2, positionY, Random.Range(0, 7));
        audioSource.time = 0.0f;
        audioSource.Play();
    }

    public void CreateHardBlocks()
    {
        float positionX1 = transform.position.x - 50.0f;
        float positionX2 = transform.position.x + 50.0f;
        float positionY = transform.position.y + 130.0f;
        prefabCreator.CreateHardBlock(positionX1, positionY);
        prefabCreator.CreateHardBlock(positionX2, positionY);
        audioSource.time = 0.0f;
        audioSource.Play();
    }
}
