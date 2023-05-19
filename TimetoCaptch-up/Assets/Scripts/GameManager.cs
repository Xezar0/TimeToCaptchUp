using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(SaveInfo))]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private SaveInfo saveInfo;
    public float scoreTime;
    
    // ensures Singleton behaviour 
    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this;
            saveInfo = GetComponent<SaveInfo>();
        }
    }

    //private void Start()
    //{
    //    Application.targetFrameRate = 60;
    //}

    public void LoadSceneByName(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void LoadSceneByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void ExitGame()
    {
        Application.Quit();
        print("izišo");
    }

    public void GameLost(float score)
    {
        Debug.Log("You have lost");
        LoadSceneByIndex(2);
        if (score > scoreTime) scoreTime = score;
        saveInfo.scoreTime = scoreTime;
        saveInfo.Save();
    }
}
