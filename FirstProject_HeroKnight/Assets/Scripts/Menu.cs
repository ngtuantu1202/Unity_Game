using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void LoadGame()
    {
        if (SaveSystem.HasSaveFile())
        {
            SceneManager.LoadScene("Level1");
        }
        else
        {
            //Debug.Log("No save");
        }
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
