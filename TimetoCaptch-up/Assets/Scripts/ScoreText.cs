using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    private void Start()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        text.text = GameManager.Instance.scoreTime.ToString();
    }
}
