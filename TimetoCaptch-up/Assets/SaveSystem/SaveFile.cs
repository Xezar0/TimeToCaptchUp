using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//SCRIPT ACTUALLY USED FOR SAVING THE CURRENT STATE

[System.Serializable]
public class SaveFile
{
    public float scoreTime;

    public SaveFile(SaveInfo save)
    {
        scoreTime = save.scoreTime;
    }
}