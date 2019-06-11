using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour {

    public GameObject gameOverMenu;
    public PlayerModel player;

    void Update()
    {
        if (player.health>=0)
        {
            GameOver();
        }
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void GameOver()
    {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f;
        
    }
}