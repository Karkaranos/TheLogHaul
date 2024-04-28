using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject creditsMenu;
    [SerializeField] private GameObject controlsMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
        //FindObjectOfType<AudioManager>().PlayBirds();
    }

    public void OpenMenu(int i)
    {
        if (i <= 0)
        {
            creditsMenu.SetActive(true);
        }
        else
        {
            controlsMenu.SetActive(true);
        }
    }

    public void CloseMenus()
    {
        creditsMenu.SetActive(false);
        controlsMenu.SetActive(false);
    }
}
