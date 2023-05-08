using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MON_RedWhite : Monitor
{
    [Header("-- RED WHITE MONITOR --")]
    public PerlinNoise perlinNoise;
    
    [Header("Canvas references")]
    public RectTransform red;
    public RectTransform white;
    
    [Header("Feel")]
    public float speed;
    public float minimumStartingDistance = 7.5f;

    private float minx, miny, maxx, maxy;
    private Vector2 direction;
    public override void Start()
    {
        maxx = screanBG.rect.width / 2 - red.rect.width / 2;
        minx = -screanBG.rect.width / 2 + red.rect.width / 2;
        maxy = screanBG.rect.height / 2 - red.rect.height / 2;
        miny = -screanBG.rect.height / 2 + red.rect.height / 2;
        base.Start();
        //isShowing = true;
        //StartCaptcha();
    }
    private void FixedUpdate()
    {
        // monitor must be turned on
        if (!isShowing) return;

        red.anchoredPosition += direction * (speed * Time.fixedDeltaTime);
        red.anchoredPosition = new Vector2(
            Mathf.Clamp(red.anchoredPosition.x, minx, maxx),
            Mathf.Clamp(red.anchoredPosition.y, miny, maxy));
        
        // WON CAPTCHA
        if(Vector2.Distance(red.anchoredPosition, white.anchoredPosition) < white.rect.width/2f)
        {
            ToDefaultState();
            isShowing = false;
            ScreenUpdate();
            Reward();
        }
    }
    public override void StartCaptcha()
    {
        base.StartCaptcha();
        perlinNoise.RandomizeOffset();
        perlinNoise.TextureUpdate();
        
        direction = Vector2.zero;
        
        // get the starting position of red and white
        while (true)
        {
            Vector2 redrand, whiterand;
            
            redrand = new Vector2(Random.Range(minx, maxx), Random.Range(miny, maxy));
            whiterand = new Vector2(Random.Range(minx, maxx), Random.Range(miny, maxy));
            
            // if the distance is sufficient than brake
            if (Vector2.Distance(redrand, whiterand) > white.rect.width + minimumStartingDistance)
            {
                break;
            }
        }

        red.anchoredPosition = new Vector2(Random.Range(minx, maxx), Random.Range(miny, maxy));
        white.anchoredPosition = new Vector2(Random.Range(minx, maxx), Random.Range(miny, maxy));
    }

    public override void HandleInput(int input)
    {
        // switch case remaps int input to direction of red
        switch (input)
        {
            case 1:
                direction = new Vector2(0, -1);
                break;
            case 2:
                direction = new Vector2(1, 0);
                break;
            case 3:
                direction = new Vector2(0, 1);
                break;
            case 4:
                direction = new Vector2(-1, 0);
                break;
            default:
                Debug.LogError("INPUT IN REDWHITE DOESN'T MATCH UP");
                break;
        }
    }

    public override void ToDefaultState()
    {
        perlinNoise.TextureUpdate();
        direction = Vector2.zero;
        red.anchoredPosition = new Vector2(minx, miny);
        white.anchoredPosition = new Vector2(maxx, miny);
    }
}
