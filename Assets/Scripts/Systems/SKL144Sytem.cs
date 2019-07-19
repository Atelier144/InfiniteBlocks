using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SKL144Sytem : MonoBehaviour {

    MainManager mainManager;
    SignalManager signalManager;
    PrefabCreator prefabCreator;

    [SerializeField] GameObject gameObjectBody;
    [SerializeField] GameObject gameObjectFace;

    [SerializeField] int duration1;
    [SerializeField] int duration2;
    [SerializeField] int duration3;

    [SerializeField] string[] motionTrigger1;
    [SerializeField] string[] motionTrigger2;
    [SerializeField] string[] motionTrigger3;

    string[][] motionTriggers = { new string[] { }, new string[] { }, new string[] { }, new string[] { } };

    SKL144SystemBody body;
    SKL144SystemFace face;

    bool isLevelUp;
    bool isReturned;
    bool flipReturn;
    bool isIdle;

    int motionCode;
    int phaseCode = 1;

    Coroutine currentCoroutine;

    void Start()
    {
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
        signalManager = GameObject.Find("SignalManager").GetComponent<SignalManager>();
        prefabCreator = GameObject.Find("PrefabCreator").GetComponent<PrefabCreator>();

        body = gameObjectBody.GetComponent<SKL144SystemBody>();
        face = gameObjectFace.GetComponent<SKL144SystemFace>();

        motionTriggers[1] = motionTrigger1;
        motionTriggers[2] = motionTrigger2;
        motionTriggers[3] = motionTrigger3;

        isIdle = true;
    }
	
	// Update is called once per frame
	void Update () {
    }

    private void FixedUpdate()
    {
        if (mainManager.GetDialogStatus() == 0)
        {
            if (!isReturned)
            {
                isReturned = true;
                StopCoroutine(currentCoroutine);
                StartCoroutine("Return");
            }
        }
        if (mainManager.GetDialogStatus() == 2)
        {
            if (isIdle)
            {
                isIdle = false;
                isReturned = false;
                currentCoroutine = StartCoroutine(motionTriggers[phaseCode][motionCode]);
                motionCode++;
                if (motionCode >= motionTriggers[phaseCode].Length) motionCode = 0;
            }
        }
    }

    public bool IsLevelUp()
    {
        return isLevelUp;
    }

    public IEnumerator Motion1()
    {
        body.SetVelocity(-350.0f, 0.0f);
        yield return new WaitForSeconds(1.0f);
        body.SetVelocity(0.0f, -250.0f);
        yield return new WaitForSeconds(1.0f);
        body.SetVelocity(700.0f, 0.0f);
        yield return new WaitForSeconds(1.0f);
        body.SetVelocity(0.0f, 250.0f);
        yield return new WaitForSeconds(1.0f);
        body.SetVelocity(-700.0f, -250.0f);
        yield return new WaitForSeconds(1.0f);
        body.SetVelocity(0.0f, 250.0f);
        yield return new WaitForSeconds(1.0f);
        body.SetVelocity(700.0f, -250.0f);
        yield return new WaitForSeconds(1.0f);
        body.SetVelocity(0.0f, 250.0f);
        yield return new WaitForSeconds(1.0f);
        body.SetVelocity(-350.0f, -250.0f);
        yield return new WaitForSeconds(1.0f);
        body.SetVelocity(0.0f, 250.0f);
        yield return new WaitForSeconds(1.0f);
        body.SetVelocity(0.0f, 0.0f);
        body.SetBasePosition();
        yield return new WaitForSeconds(1.0f);
        isIdle = true;
    }

    public IEnumerator Flash()
    {
        face.ChangeFace(13);
        yield return new WaitForSeconds(1.0f);
        face.ChangeFace(12);
        yield return new WaitForSeconds(1.0f);
        face.ChangeFace(11);
        yield return new WaitForSeconds(1.0f);
        face.ChangeFace(0);
        mainManager.StartFlash();
        yield return new WaitForSeconds(1.0f);
        face.ChangeFace(1);
        yield return new WaitForSeconds(3.0f);
        isIdle = true;
    }


    public IEnumerator Return()
    {
        float startPositionX = gameObjectBody.transform.position.x;
        float startPositionY = gameObjectBody.transform.position.y;

        body.SetVelocity(0.0f, 0.0f);
        for (int i = 100; i > 0; i--)
        {
            float positionX = startPositionX * 0.01f * i;
            float positionY = (startPositionY - 240.0f) * 0.01f * i + 240.0f;
            gameObjectBody.transform.position = new Vector3(positionX, positionY, 0.0f);
            yield return null;
        }
        gameObjectBody.transform.position = new Vector3(0.0f, 240.0f, 0.0f);
        isIdle = true;
    }
}
