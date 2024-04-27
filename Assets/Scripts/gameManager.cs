using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    [SerializeField] private int FoodCollected;
    [SerializeField] private TMP_Text foodHaveTxt;
    [SerializeField] private TMP_Text outOf;
    public int goal;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().PlayLevelBackgroundNoise(SceneManager.GetActiveScene().buildIndex);
        foodHaveTxt.text = FoodCollected + "";
        outOf.text = goal + "";
    }

    public void UpdateCounter()
    {
        //Cade add food picked up sfx here
        FoodCollected++;
        foodHaveTxt.text = FoodCollected + ""; 
        outOf.text = goal + "";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (FoodCollected >= goal)
        {
            if (SceneManager.GetActiveScene().buildIndex != 7)
            {
                SceneManager.LoadScene(9);
                FindObjectOfType<AudioManager>().LvlIndex = SceneManager.GetActiveScene().buildIndex + 1;
            }
            else
            {
                SceneManager.LoadScene(8);
            }
        }
    }

}
