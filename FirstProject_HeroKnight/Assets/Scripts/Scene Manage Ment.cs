using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManageMent : MonoBehaviour
{
    public void LoadLevel()
    {
        SceneManager.LoadScene("Level2");
    }
}
