using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MON_Word : Monitor
{
    public string wordTarget;
    public string wordCurrent;
    public int inputLenght;
    public int inputCurrent;
    public TMP_Text tmpText;

    public override void Start()
    {
        base.Start();
        //isShowing = true;
        //StartCaptcha();
    }

    public override void HandleInput(int input)
    {
        if(!isShowing) return;
        
        // decodes from int input to letters (ASCII)
        char tempSymbol = 'X';
        switch (input)
        {
            case 1:
                tempSymbol = 'C';
                break;
            case 2:
                tempSymbol = 'H';
                break;
            case 3:
                tempSymbol = 'D';
                break;
            case 4:
                tempSymbol = 'E';
                break;
            case 5:
                tempSymbol = 'I';
                break;
            case 6:
                tempSymbol = 'F';
                break;
            case 7:
                tempSymbol = 'A';
                break;
            case 8:
                tempSymbol = 'G';
                break;
            case 9:
                tempSymbol = 'B';
                break;
            default:
                Debug.LogError("Input invalid. Input: " + input);
                return;
                break;
        }

        if (tempSymbol != 'X')
        {
            wordCurrent += tempSymbol;
            inputCurrent++;
            Debug.Log("Current: " + wordCurrent);
            Debug.Log("Target: " + wordTarget);
        }

        if (inputCurrent == inputLenght)
        {
            if (wordCurrent == wordTarget)
            {
                // -- WON CAPCHA --
                Debug.Log("Won Capcha");
                ToDefaultState();
                isShowing = false;
                ScreenUpdate();
                Reward();
            }
            else
            {
                // -- LOST CAPCHA --
                Debug.Log("Lost Capcha");
                ToDefaultState();
                StartCaptcha();
                Punish();
            }
        }
    }

    public override void StartCaptcha()
    {
        base.StartCaptcha();
        wordTarget = GenerateRandomString(inputLenght, "AGBEIFCHD");
        tmpText.text = wordTarget;
        Debug.Log(wordTarget);
    }

    public override void ToDefaultState()
    {
        inputCurrent = 0;
        wordCurrent = "";
        wordTarget = "";
        tmpText.text = "ARA ARA";
    }
    
    private string GenerateRandomString(int length, string chars)
    {
        char[] result = new char[length];
        System.Random random = new System.Random();

        for (int i = 0; i < length; i++)
        {
            result[i] = chars[random.Next(chars.Length)];
        }

        return new string(result);
    }
}
