/*****************************************************************************
// File Name :         ClockController.cs
// Author :            Cade R. Naylor
// Creation Date :     April 24, 2024
//
// Brief Description :  Makes the clock hands spin on transition levels
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClockController : MonoBehaviour
{
    [SerializeField] LevelTransition[] transitionInfo;
    [SerializeField] GameObject hourHand;
    [SerializeField] GameObject minHand;
    [SerializeField] TMP_Text factText;

    public void Start()
    {
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        int level = FindObjectOfType<AudioManager>().LvlIndex - 2;
        factText.text = transitionInfo[level].fact;
        float timeLeft = transitionInfo[level].transitionTime;
        while(timeLeft > 0)
        {
            hourHand.transform.eulerAngles += new Vector3(0, 0, -1 * transitionInfo[level].handSpeed);
            minHand.transform.eulerAngles += new Vector3(0, 0, -1 * transitionInfo[level].handSpeed * 30);
            timeLeft -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene(level + 2);
    }


}
