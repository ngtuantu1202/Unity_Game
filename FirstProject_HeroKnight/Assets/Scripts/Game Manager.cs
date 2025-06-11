using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isGameActive = true;
    [SerializeField] private GameObject gameOverUI;

    private bool isGameOver = false;
    public Vector2 checkpointPos;
    public Rigidbody2D rb;

    //vi tri 
    public Transform playerTransform;

    private void Start()
    {
        Time.timeScale = 1;
        gameOverUI.SetActive(false);

        //checpoint tai vi tri dau tien
        if (rb != null)
        {
            checkpointPos = rb.transform.position;
            playerTransform = rb.transform;
        }

        //timer
        if (GameTimer.instance != null)
            GameTimer.instance.StartTimer();
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
        Time.timeScale = 1;
        SceneManager.LoadScene("Level1");
    }

    public void Continue()
    {
        isGameOver = false;
        gameOverUI.SetActive(false);
        Time.timeScale = 1;
        StartCoroutine(Respawn(0.5f));
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    //checkpoint
    public void UpdateCheckPoint(Vector2 pos)
    {
        checkpointPos = pos;
    }

    IEnumerator Respawn(float duration)
    {
        if (rb == null || playerTransform == null) yield break;

        // Lam mo nv
        rb.velocity = Vector2.zero;
        rb.simulated = false;
        playerTransform.localScale = Vector3.zero;

        yield return new WaitForSecondsRealtime(duration);

        // Respawn
        playerTransform.position = checkpointPos;
        playerTransform.localScale = Vector3.one;
        rb.simulated = true;
    }
}