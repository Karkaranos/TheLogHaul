using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    [SerializeField] private int FoodCollected;
    [SerializeField] private TMP_Text score;
    public int goal;

    // Start is called before the first frame update
    void Start()
    {

        score.text = FoodCollected + " / " + goal;
    }

    public void UpdateCounter()
    {
        FoodCollected++;
        score.text = FoodCollected + " / " + goal;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (FoodCollected >= goal)
        {
            if(SceneManager.GetActiveScene().buildIndex == 1)
            {
                FindObjectOfType<AudioManager>().PlayCars();
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

}
