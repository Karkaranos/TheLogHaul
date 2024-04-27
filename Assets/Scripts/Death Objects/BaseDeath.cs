using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseDeath : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            KillPlayer();
        }
    }
    public void KillPlayer()
    {
        FindObjectOfType<AudioManager>().Play("PlayerDeath");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
