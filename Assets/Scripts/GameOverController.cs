using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    GameObject[] deathObjects;
    GameObject[] playerCheck;

    // Start is called before the first frame update
    void Start()
    {
        deathObjects = GameObject.FindGameObjectsWithTag("ShowOnDeath");
        HideDeathScreen();
    }

    // Update is called once per frame
    void Update()
    {
        //maybe look for player every frame and if it cannot be found, start the game over screen
        //the game over screen will be similar to the pause screen, just remove the resume button
        playerCheck = GameObject.FindGameObjectsWithTag("Player");
        if (playerCheck.Length == 0)
        {
            ShowDeathScreen();
        }
    }
    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ShowDeathScreen()
    {
        foreach (GameObject g in deathObjects)
        {
            g.SetActive(true);
        }
    }
    public void HideDeathScreen()
    {
        foreach (GameObject g in deathObjects)
        {
            g.SetActive(false);
        }
    }
}
