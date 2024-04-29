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
    Color backgroundAlpha;
    [SerializeField] private GameObject endCanvas;
    [SerializeField] GameObject square;
    [SerializeField] GameObject clock;

    public void Start()
    {
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        FindObjectOfType<AudioManager>().PlayEndingSound();
        if(!FindObjectOfType<AudioManager>())
        {
            Debug.Log("hjkfdshgdl");
        }
        float alpha = 1;
        for(int i=0; i<factCycles.Length; i++)
        {
            factText.text = factCycles[i].fact;
            float timeLeft = factCycles[i].transitionTime;
            while (timeLeft > 0)
            {
                hourHand.transform.eulerAngles += new Vector3(0, 0, -1 * factCycles[i].handSpeed);
                minHand.transform.eulerAngles += new Vector3(0, 0, -1 * factCycles[i].handSpeed * 10);
                timeLeft -= Time.deltaTime;
                alpha -= Time.deltaTime/12;
                alpha = Mathf.Clamp(alpha, 0, 1);
                square.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, alpha);
                yield return new WaitForSeconds(Time.deltaTime);
            }

            
        }

        yield return new WaitForSeconds(.5f);

        endCanvas.SetActive(true);
        square.SetActive(false);
        factText.gameObject.SetActive(false);
        clock.SetActive(false);
        
    }
}
