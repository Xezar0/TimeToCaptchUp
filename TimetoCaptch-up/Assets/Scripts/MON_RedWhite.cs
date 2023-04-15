using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MON_RedWhite : Monitor
{
    public override void Start()
    {
        base.Start();
    }

    public override void StartCaptcha()
    {
        Debug.Log("ga");
    }

    public override void HandleInput(int input)
    {
        Debug.Log("ga");
    }

    public override void ToDefaultState()
    {
        Debug.Log("ga");
    }
}
