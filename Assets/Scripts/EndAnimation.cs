using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndAnimation : MonoBehaviour
{
    [SerializeField] LevelTransition[] factCycles;
    [SerializeField] GameObject hourHand;
    [SerializeField] GameObject minHand;
    [SerializeField] TMP_Text factText;
    [SerializeField] GameObject fullForest;
    Color backgroundAlpha;
    [SerializeField] private GameObject endCanvas;
    [SerializeField] GameObject clock;

    public void Start()
    {
        fullForest.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0);
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        int alpha = 0;
        for(int i=0; i<factCycles.Length; i++)
        {
            factText.text = factCycles[i].fact;
            float timeLeft = factCycles[i].transitionTime;
            while (timeLeft > 0)
            {
                hourHand.transform.eulerAngles += new Vector3(0, 0, -1 * factCycles[i].handSpeed);
                minHand.transform.eulerAngles += new Vector3(0, 0, -1 * factCycles[i].handSpeed * 30);
                timeLeft -= Time.deltaTime;
                alpha += (int)(timeLeft / factCycles[i].transitionTime) * 50;
                yield return new WaitForSeconds(Time.deltaTime);
            }

            
        }

        yield return new WaitForSeconds(.5f);

        endCanvas.SetActive(true);
        clock.SetActive(false);
        factText.gameObject.SetActive(false);
        
    }
}
