using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monitor : MonoBehaviour
{
    public CaptchaManager manager;
    public RectTransform screanBG;
    public AudioClip victory;
    public AudioClip loose;
    public AudioSource source;
    public float rewardAmount;
    public float looseAmount;
    public bool isShowing = false;

    public float width;
    public float height;

    public virtual void Start()
    {
        width = screanBG.rect.width;
        height = screanBG.rect.height;
        ToDefaultState();
    }

    public void Punish()
    {
        manager.Punish(looseAmount);
    }
    public void Reward()
    {
        manager.Reward(rewardAmount);
    }

    public abstract void ToDefaultState();
    public abstract void StartCaptcha();
    public abstract void HandleInput(int input);
}
