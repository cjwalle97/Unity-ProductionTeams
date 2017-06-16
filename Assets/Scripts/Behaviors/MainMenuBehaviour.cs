using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuBehaviour : MonoBehaviour
{
    public Text ControlsText;
    public Text ArtistsText;
    public Text ProgrammersText;

    void Start()
    {
        ControlsText.text = "";
        ArtistsText.text = "";
        ProgrammersText.text = "";
    }

    public void StartGame()
    {
        SceneManager.LoadScene("0.main");
        DestroyImmediate(gameObject);
    }

    public void DisplayControls()
    {
        ControlsText.text = "Designed for use with an Xbox One Controller \n"
            + "Left Analog Stick = Movement \n"
            + "Right Analog Stick = Aim \n"
            + "Right Trigger = Shoot"
            + "A = Select (In Menus) \n"
            + "B = Back (In Menus)";
    }

    public void DisplayCredits()
    {
        ArtistsText.text = "ARTISTS: " + "Bruce Mayo, " + "Joshau Cambre, " + "Blaise Badon";
        ProgrammersText.text = "PROGRAMMERS: " + "Trent Butler, " + "Christopher Walle";
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
