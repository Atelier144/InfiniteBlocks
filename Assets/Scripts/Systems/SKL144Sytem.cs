using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SKL144Sytem : MonoBehaviour {

    MainManager mainManager;
    SignalManager signalManager;
    PrefabCreator prefabCreator;

    [SerializeField] GameObject gameObjectBody;
    [SerializeField] GameObject gameObjectFace;

    Animator animator;
    AnimatorOverrideController animatorOverrideController;

    [SerializeField] int duration1;
    [SerializeField] int duration2;
    [SerializeField] int duration3;

    [SerializeField] string[] motionTrigger1;
    [SerializeField] string[] motionTrigger2;
    [SerializeField] string[] motionTrigger3;

    string[][] motionTriggers = { new string[] { }, new string[] { }, new string[] { }, new string[] { } };

    bool isLevelUp;
    bool isReturned;
    bool flipReturn;


    AnimationCurve animationCurveX;
    AnimationCurve animationCurveY;
    AnimationClip animationClipReturn1;
    AnimationClip animationClipReturn2;

    int motionCode;
    int formCode = 1;

    void Start()
    {
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
        signalManager = GameObject.Find("SignalManager").GetComponent<SignalManager>();
        prefabCreator = GameObject.Find("PrefabCreator").GetComponent<PrefabCreator>();

        animator = GetComponent<Animator>();
        animatorOverrideController = new AnimatorOverrideController();
        animatorOverrideController.runtimeAnimatorController = animator.runtimeAnimatorController;
        animator.runtimeAnimatorController = animatorOverrideController;

        AnimationEvent animationEvent = new AnimationEvent();
        AnimationEvent animationEventFace = new AnimationEvent();

        animationEvent.time = 2.0f;
        animationEvent.functionName = "OnReturnAnimationEnd";
        animationEventFace.time = 0.0f;
        animationEventFace.intParameter = 1;
        animationEventFace.functionName = "ChangeFace";

        animationClipReturn1 = new AnimationClip();
        animationClipReturn2 = new AnimationClip();
        animationClipReturn1.AddEvent(animationEvent);
        animationClipReturn2.AddEvent(animationEvent);
        animationClipReturn1.AddEvent(animationEventFace);
        animationClipReturn2.AddEvent(animationEventFace);

        motionTriggers[1] = motionTrigger1;
        motionTriggers[2] = motionTrigger2;
        motionTriggers[3] = motionTrigger3;

        AnimatorStateInfo[] animatorStateInfos = new AnimatorStateInfo[animator.layerCount];
        for (int i = 0; i < animator.layerCount; i++) animatorStateInfos[i] = animator.GetCurrentAnimatorStateInfo(i);
        animatorOverrideController["Return"] = animationClipReturn2;
        animator.Update(0.0f);

        for (int i = 0; i < animator.layerCount; i++) animator.Play(animatorStateInfos[i].fullPathHash, i, animatorStateInfos[i].normalizedTime);
    }
	
	// Update is called once per frame
	void Update () {
        animationCurveX = AnimationCurve.Linear(0.0f, gameObjectBody.transform.position.x, 2.0f, 0.0f);
        animationCurveY = AnimationCurve.Linear(0.0f, gameObjectBody.transform.position.y, 2.0f, 240.0f);
    }

    private void FixedUpdate()
    {
        if (mainManager.GetDialogStatus() == 0)
        {
            if (!isReturned)
            {
                isReturned = true;
                SetTriggerReturn();
            }
        }
    }

    public bool IsLevelUp()
    {
        return isLevelUp;
    }

    public void OnIdleAnimationEnd()
    {
        if (mainManager.GetDialogStatus() == 2) SetTriggerMotion();
    }

    public void OnMotionAnimationEnd()
    {
        SetTriggerIdle();
    }

    public void OnReturnAnimationEnd()
    {
        isReturned = false;
        SetTriggerIdle();
    }

    public void SetTriggerIdle()
    {
        animator.SetTrigger("Idle");
    }

    public void SetTriggerMotion()
    {
        animator.SetTrigger(motionTriggers[formCode][motionCode]);
        motionCode++;
        if (motionCode >= motionTriggers[formCode].Length) motionCode = 0;
    }

    public void SetTriggerReturn()
    {

        flipReturn = !flipReturn;
        if (flipReturn)
        {
            animationClipReturn1.ClearCurves();
            animationClipReturn1.SetCurve("Body", typeof(Transform), "localPosition.x", animationCurveX);
            animationClipReturn1.SetCurve("Body", typeof(Transform), "localPosition.y", animationCurveY);
        }
        else
        {
            animationClipReturn2.ClearCurves();
            animationClipReturn2.SetCurve("Body", typeof(Transform), "localPosition.x", animationCurveX);
            animationClipReturn2.SetCurve("Body", typeof(Transform), "localPosition.y", animationCurveY);
        }

        AnimatorStateInfo[] animatorStateInfos = new AnimatorStateInfo[animator.layerCount];
        for (int i = 0; i < animator.layerCount; i++) animatorStateInfos[i] = animator.GetCurrentAnimatorStateInfo(i);
        animatorOverrideController["Return"] = flipReturn ? animationClipReturn1 : animationClipReturn2;
        animator.Update(0.0f);

        for (int i = 0; i < animator.layerCount; i++) animator.Play(animatorStateInfos[i].fullPathHash, i, animatorStateInfos[i].normalizedTime);

        animator.SetTrigger("Return");
    }

    public void Flash()
    {
        mainManager.StartFlash();
    }

    public void ChangeFace(int faceCode)
    {
        gameObjectFace.GetComponent<SKL144SystemFace>().ChangeFace(faceCode);
    }
}
