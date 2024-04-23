using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelSoundBlend
{
    [Tooltip("1 is full nature sounds, 7 is full human sounds"),Range(1, 7)]
    public int deforestationLevel;
    [Tooltip("Nature sounds decrease throughout level")]
    public bool progressiveDeforestation;
}
