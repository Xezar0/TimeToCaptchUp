using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CaptchaManager : MonoBehaviour
{
    [Tooltip("Depletion of energy per second per monitor")]
    public float depletionRate = 10f;

    [Tooltip("Time after the game starts after which tasks start to appear (in seconds)")]
    public float startGraceTime = 10f;

    [Tooltip("Number of currently active tasks")]
    public int taskNumber;

    [SerializeField] private Slider efficiencyBarSlider;
    [SerializeField] private float efficiency = 100f;

    public List<Monitor> monitors;

    [SerializeField] private float scoreTime;
    public bool isGameActive = false;

    // time between tasks
    public float taskSpawnRate = 8f;

    // percentage of taskSpawRate that is taken off each time new task is added;
    public float taskSpawnRateSpeedUp = 0.98f;
    private float currentTimeBetweenTasks = 0f;
    private List<Monitor> monitorsAvailable = new List<Monitor>();

    private void Start()
    {
        UpdateTaskNumber();
        StartCoroutine(StartGameTimer());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            GameManager.Instance.LoadSceneByIndex(2);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (!isGameActive) return;
        
        scoreTime += Time.deltaTime;

        if (efficiency > 0)
        {
            efficiency -= depletionRate * taskNumber * Time.deltaTime;
            efficiencyBarSlider.value = efficiency / 100f;
        }

        if (taskNumber < monitors.Count)
        {
            currentTimeBetweenTasks -= Time.deltaTime;

            if (currentTimeBetweenTasks <= 0f)
            {
                taskSpawnRate *= taskSpawnRateSpeedUp;
                currentTimeBetweenTasks = taskSpawnRate;

                StartTask();
            }
        }

        if (efficiency <= 0f)
        {
            isGameActive = false;
            GameManager.Instance.GameLost(scoreTime);
        }
    }

    public void Punish(float amount)
    {
        if (efficiency - amount > 0f)
        {
            efficiency -= amount;
        }
        else
        {
            GameManager.Instance.GameLost(scoreTime);
        }
    }

    public void Reward(float amount)
    {
        if (efficiency + amount < 100f)
        {
            efficiency += amount;
        }
        else
        {
            efficiency = 100f;
        }
    }

    public void UpdateTaskNumber()
    {
        int tempTaskNumber = 0;
        monitorsAvailable.Clear();
        for (int i = 0; i < monitors.Count; i++)
        {
            if (monitors[i].isShowing)
            {   
                tempTaskNumber++;
            }
            else
            {
                monitorsAvailable.Add(monitors[i]);
            }
        }
        
        taskNumber = tempTaskNumber;
        Debug.Log("There are " + taskNumber + " currently active monitors.");
    }

    private void StartTask()
    {
        int selectedMonitor = Random.Range(0, monitorsAvailable.Count);
        monitorsAvailable[selectedMonitor].StartCaptcha();
    }

    IEnumerator StartGameTimer()
    {
        yield return new WaitForSeconds(startGraceTime);
        isGameActive = true;
    }
}
