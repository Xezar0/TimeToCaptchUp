using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monitor : MonoBehaviour
{
    public CaptchaManager manager;
    public RectTransform screanBG;
    public AudioClip victory;
    public AudioClip loose;
    public AudioClip startSound;
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
        source.PlayOneShot(loose);
        manager.UpdateTaskNumber();
    }
    public void Reward()
    {
        manager.Reward(rewardAmount);
        source.PlayOneShot(victory);
        manager.UpdateTaskNumber();
    }

    public abstract void ToDefaultState();

    public virtual void StartCaptcha()
    {
        isShowing = true;
        manager.UpdateTaskNumber();
        source.PlayOneShot(startSound);
    }

    public abstract void HandleInput(int input);
}
