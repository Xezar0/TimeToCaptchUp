using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CaptchaManager : MonoBehaviour
{
    [Tooltip("Depletion per second per monitor")]
    public float depletionRate = 10f;
    [Tooltip("Number of currently active tasks")]
    public int taskNumber;
    
    [SerializeField] private Slider efficiencyBarSlider;

    [SerializeField] private float efficiency = 100f;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            SceneManager.LoadScene(2);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        
        if (efficiency > 0)
        {
            efficiency -= depletionRate * taskNumber * Time.deltaTime;
            efficiencyBarSlider.value = efficiency / 100f;
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
            // --- LOOSE ---
            Debug.Log("You have Lost");
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
}
