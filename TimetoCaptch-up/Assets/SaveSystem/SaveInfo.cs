using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Temporary script for saving
// used to create SaveFile type and save/load it
// use Load func in Start function so that everything else properly loads

public class SaveInfo : MonoBehaviour
{
    [SerializeField, Tooltip("!!  Syntax:  /name  !!")]
    private string save_key = "/SAVE1";
    public float scoreTime;

    private void Start()
    {
        SaveFile save = SaveSystem.LoadData<SaveFile>(save_key);
        
        if (save != null)
        {
            scoreTime = save.scoreTime;
            GameManager.Instance.scoreTime = scoreTime;
        }
        else
        {
            Save();
        }

        Debug.Log("score time: " + scoreTime);
    }

    public void Save()
    {
        SaveFile save_file = new SaveFile(this);
        SaveSystem.SaveData(save_file, save_key);
    }
}