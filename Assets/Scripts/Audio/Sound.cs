/*****************************************************************************
// File Name :         Sound.cs
// Author :            Cade R. Naylor
// Creation Date :     January 28, 2024
//
//Based on:             Brackeys Audio Manager
//Tutorial video:       https://youtu.be/6OT43pvUyfY

// Brief Description :  Creates the settings for audio sources in each sound in 
AudioManager
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    #region variables

    public string name;
    public AudioClip audClip;

    [Range(0, 1)]
    public float clipVolume;
    [Range(0.1f, 3)]
    public float clipPitch;
    public bool canLoop;

    [HideInInspector]
    public AudioSource source;
    [HideInInspector]
    public int weightedRangeLow;
    [HideInInspector]
    public int weightedRangeHigh;


    public bool isBackground;
    [Tooltip("What produces the sound")]
    public SoundFlavor soundSource;
    [Tooltip("How often the sound appears when weighted. 1 is rare, 10 is common"), Range(1,10)]
    public int soundWeight = 5;

    public enum SoundFlavor
    {
        NATURE_ENVIRONMENT, HUMAN_ENVIRONMENT, PLAYER
    }

    #endregion
}

