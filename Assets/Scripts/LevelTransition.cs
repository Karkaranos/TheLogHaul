/*****************************************************************************
// File Name :         LevelTransition.cs
// Author :            Cade R. Naylor
// Creation Date :     April 24, 2024
//
// Brief Description :  Creates a custom class that holds onto information regarding
                        level transitions
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelTransition
{
    public float transitionTime;
    public float handSpeed = .2f;
    public string fact;
}
