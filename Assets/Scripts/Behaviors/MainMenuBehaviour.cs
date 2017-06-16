using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.AI;

public class MainMenuBehaviour : MonoBehaviour
{
    public Text ControlsText;
    public Text ArtistsText;
    public Text ProgrammersText;

    private Transform _spawn;
    private GameObject _enemy;
    private float _timer = 0.0f;
    private GameObject _overlay;

    private void Start()
    {
        ControlsText.text = "";
        ArtistsText.text = "";
        ProgrammersText.text = "";
        _spawn = GameObject.FindGameObjectWithTag("MainMenuSpawn").transform;
        _enemy = (GameObject)Instantiate(Resources.Load("RuntimePrefabs/TowerAttacker"), _spawn.position, _spawn.rotation);
        _overlay = GameObject.FindGameObjectWithTag("OverlayPanel");
        _overlay.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("0.main");
        DestroyImmediate(gameObject);
    }

    public void DisplayControls()
    {
        ControlsText.text = "A ~> Select\n" + "B ~> Back\n" + "Right Trigger ~> Fire Weapon\n" + "Start ~> Pause Game\n" + "Left Joystick ~> Character Movement\n" + "Right Joystick ~> Character Aiming\n";
    }

    public void DisplayCredits()
    {
        ArtistsText.text = "Artists ~> Blasie Badon, Joshua Cambre, Bruce Mayo";
        ProgrammersText.text = "Programmers ~> Christopher Walle, Trent Butler";
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if(_overlay.activeSelf == true)
        {
            GameObject.FindGameObjectWithTag("Title").GetComponent<Text>().enabled = false;
        }

        if (_overlay.activeSelf == false)
        {
            GameObject.FindGameObjectWithTag("Title").GetComponent<Text>().enabled = true;
        }

        if (_timer < 30.0f)//wait
        {
            _overlay.SetActive(true);
            _enemy.GetComponent<NavMeshAgent>().isStopped = true;
        }

        if(_timer >= 30.0f)//move
        {
            _overlay.SetActive(false);
            _enemy.GetComponent<NavMeshAgent>().isStopped = false;            
        }

        if (_timer >= 80.0f)//reset
        {
            _enemy.GetComponent<EnemyBehavior>().EnemyConfig.Alive = false;
            _enemy = (GameObject)Instantiate(Resources.Load("RuntimePrefabs/TowerAttacker"), _spawn.position, _spawn.rotation);
            _timer = 0.0f;
            return;
        }

        Debug.Log(_timer);
    }
}
