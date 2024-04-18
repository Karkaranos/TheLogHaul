using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public int FoodCollected;
    public int goal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(FoodCollected >= goal)
        {
            Debug.Log("win!");
        }  
    }
}
