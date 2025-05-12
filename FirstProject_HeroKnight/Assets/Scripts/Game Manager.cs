using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isGameActive = true;
    [SerializeField] private GameObject gameOverUI;
    private bool isGameOver = false;

    private void Start()
    {
        Time.timeScale = 1;
        gameOverUI.SetActive(false);    
    }

    public void GameOver()
    {

        isGameOver = true;

        Time.timeScale = 0;
        gameOverUI.SetActive(true);
    }

    public void Restart()
    {
        isGameOver = false;
        SceneManager.LoadScene("Level1");
        Time.timeScale = 1;
    } 

    public bool IsGameOver()
    {
        return isGameOver;
    }
}

