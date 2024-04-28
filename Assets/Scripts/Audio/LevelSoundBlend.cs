/*****************************************************************************
// File Name :         LevelSoundBlend.cs
// Author :            Cade R. Naylor
// Creation Date :     April 17, 2024
//
// Brief Description :  Creates the LevelSoundBlend class which holds onto a level's
                            sound controls
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelSoundBlend
{
    [Tooltip("1 is full nature sounds, 7 is full human sounds"), Range(1, 7)]
    public int deforestationLevel;
    [Tooltip("Nature sounds decrease throughout level")]
    public bool progressiveDeforestation;
    [Tooltip("Average time to complete level(Used for sound change over level)")]
    public int secondsToFinish;

}
