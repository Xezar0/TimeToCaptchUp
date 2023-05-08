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
    public GameObject screenHider;

    public float width;
    public float height;

    public virtual void Start()
    {
        width = screanBG.rect.width;
        height = screanBG.rect.height;
        ToDefaultState();
        ScreenUpdate();
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
        ScreenUpdate();
    }

    public abstract void HandleInput(int input);

    public void ScreenUpdate()
    {
        screenHider.SetActive(!isShowing);
    }
}
