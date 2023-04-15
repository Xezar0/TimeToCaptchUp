using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MON_Pair : Monitor
{
    [Tooltip("MUST HAVE LENGHT 12")]
    [SerializeField] private List<Sprite> symbols;
    [Tooltip("MUST HAVE LENGHT 4")]
    [SerializeField] private List<Image> pairs;
    
    private int select1 = 999;
    private int select2 = 998;
    private int show1 = 999;
    private int show2 = 998;
    private int[] holderArray = {0, 0, 1, 2};
    private int[] symbolArray = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11};

    private float time;
    
    public override void Start()
    {
        base.Start();
        //isShowing = true;
        //StartCaptcha();
    }

    public override void HandleInput(int input)
    {
        if (!isShowing)
        {
            return;
        }
        
        // VV THIS IS FOR BUTTON REMAPING VV
        int show = 0;
        int temp = 0;
        switch (input)
        {
            case 1:
                show = 2;
                temp = holderArray[2];
                break;
            case 2:
                show = 3;
                temp = holderArray[3];
                break;
            case 3:
                show = 1;
                temp = holderArray[1];
                break;
            case 4:
                show = 0;
                temp = holderArray[0];
                break;
            default:
                Debug.Log("Yaaa,  something is wrong");
                break;
        }

        if (select1 > 100 || select2 > 100)
        {
            if (select1 > 100)
            {
                select1 = temp;
                show1 = show;
                pairs[show].color = Color.gray;
            }
            else
            {
                select2 = temp;
                show2 = show;
                pairs[show].color = Color.gray;
            }
        }

        if (select1 < 100 && select2 < 100)
        {
            if (select1 == select2)
            {
                Debug.Log("Won PAIRS");
                pairs[show1].color = Color.blue;
                pairs[show2].color = Color.blue;
                StartCoroutine(WinDelay());
                Reward();
            }
            else
            {
                Debug.Log("Lost PAIRS");
                pairs[show1].color = Color.red;
                pairs[show2].color = Color.red;
                StartCoroutine(RestartDelay());
                Punish();
            }
        }
    }
    public override void ToDefaultState()
    {
        select1 = 999;
        select2 = 998;
        show1 = 999;
        show2 = 998;
        
        holderArray = new []{0,0,1,2};
        symbolArray = new []{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11};
        for (int i = 0; i < 4; i++)
        {
            pairs[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
            pairs[i].transform.GetChild(0).GetComponent<RectTransform>().rotation = 
                Quaternion.Euler(0,0 ,0);
            
            pairs[i].transform.GetChild(1).GetComponent<Image>().sprite = null;
            pairs[i].transform.GetChild(1).GetComponent<RectTransform>().rotation = 
                Quaternion.Euler(0,0 ,0);
            
            pairs[i].color = Color.white;
        }
    }
    public override void StartCaptcha()
    {
        for (int i = 0; i < 4; i++)
        {
            int rand = Random.Range(0, 4);
            (holderArray[rand], holderArray[i]) = (holderArray[i], holderArray[rand]);
        }
        
        for (int i = 0; i < 12; i++)
        {
            int rand = Random.Range(0, 12);
            (symbolArray[rand], symbolArray[i]) = (symbolArray[i], symbolArray[rand]);
        }

        for (int i = 0; i < 4; i++)
        {
            pairs[i].transform.GetChild(0).GetComponent<Image>().sprite = symbols[symbolArray[holderArray[i]]];
            pairs[i].transform.GetChild(0).GetComponent<RectTransform>().rotation = 
                Quaternion.Euler(0,0 ,Random.Range(0f, 360f));
            
            pairs[i].transform.GetChild(1).GetComponent<Image>().sprite = symbols[symbolArray[holderArray[i] + 6]];
            pairs[i].transform.GetChild(1).GetComponent<RectTransform>().rotation = 
                Quaternion.Euler(0,0 ,Random.Range(0f, 360f));
        }
    }

    IEnumerator RestartDelay()
    {
        yield return new WaitForSeconds(2f);
        ToDefaultState();
        StartCaptcha();
    }

    IEnumerator WinDelay()
    {
        yield return new WaitForSeconds(2f);
        ToDefaultState();
        isShowing = false;
    }
}
