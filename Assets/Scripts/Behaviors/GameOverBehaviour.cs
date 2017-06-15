using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverBehaviour : MonoBehaviour {

    public void TryAgain()
    {
        SceneManager.LoadScene("0.main");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
