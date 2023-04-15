using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard : Interactable
{
    private Outline outline;
    [SerializeField] private int input;
    [SerializeField] private Monitor monitor;
    [SerializeField] private AudioClip keypressSounds;
    [SerializeField] private AudioSource source;
    [SerializeField] private Animator animator;

    public override void Awake()
    {
        base.Awake();
        outline = GetComponent<Outline>();
        outline.enabled = true;
        StartCoroutine(OutlineDelay());
    }
    
    public override void OnFocus()
    {
        outline.enabled = true;
    }

    public override void OnInteract()
    {
        animator.Rebind();
        source.PlayOneShot(keypressSounds);
        animator.Play("button" + input);
        Debug.Log("button" + input);
        monitor.HandleInput(input);
    }

    public override void OnLoseFocus()
    {
        outline.enabled = false;
    }

    IEnumerator OutlineDelay()
    {
        yield return new WaitForSeconds(0.1f);
        outline.enabled = false;
    }
}
