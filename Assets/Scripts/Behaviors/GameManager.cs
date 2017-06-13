using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //AFTER 5 MINUTES NEW ROUND STARTS
    //PAYLOAD SLOWLY RETREATS IF NO PUSHERS ON IT
    //IF ALL ENEMIES ARE DEAD, ROUND ENDS
    //AT PAUSE, ITERATE THROUGH EACH ENEMY CAMERA

    #region //MEMBER VARIABLES
    public List<Transform> EnemySpawn;
    public Transform PayloadSpawn;

    private float _roundTimer;
    private List<GameObject> _enemies;
    private bool _isPaused;
    private int minuteCounter = 0;
    private int _roundCounter = 1;
    #endregion

    private GameObject _buttonpanel;
    private GameObject _gameinfopanel;

    private void PauseGame()
    {
        //player
        //enemies
        //payload
        //payload pushers
        //timer??
        Time.timeScale = 0;
        _isPaused = true;

        _buttonpanel.SetActive(true);

        var _player = GameObject.FindGameObjectWithTag("Player");
        var _enemies = GameObject.FindGameObjectsWithTag("Enemy");
        var _pushers = GameObject.FindGameObjectsWithTag("PayloadPusher");
        var _payload = GameObject.FindGameObjectWithTag("Payload");

        _player.GetComponent<Rigidbody>().isKinematic = true;
        _payload.GetComponent<Rigidbody>().isKinematic = true;

        foreach (var enemy in _enemies)
        {
            enemy.GetComponent<Rigidbody>().isKinematic = true;
        }
        foreach (var pusher in _pushers)
        {
            pusher.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    private void ResumeGame()
    {
        var _player = GameObject.FindGameObjectWithTag("Player");
        var _enemies = GameObject.FindGameObjectsWithTag("Enemy");
        var _pushers = GameObject.FindGameObjectsWithTag("PayloadPusher");
        var _payload = GameObject.FindGameObjectWithTag("Payload");

        _player.GetComponent<Rigidbody>().isKinematic = false;
        _payload.GetComponent<Rigidbody>().isKinematic = false;

        foreach (var enemy in _enemies)
        {
            enemy.GetComponent<Rigidbody>().isKinematic = false;
        }
        foreach (var pusher in _pushers)
        {
            pusher.GetComponent<Rigidbody>().isKinematic = false;
        }

        _buttonpanel.SetActive(false);

        Time.timeScale = 1.0f;
        _isPaused = false;
    }

    //NEEDS WORK
    public void SaveGame()
    {
        //NEEDS WORK
    }

    public void QuitGame()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }

    private bool GameLoop()
    {
        if (_isPaused == false) //GAME LOOP
        {
            _roundTimer += Time.deltaTime;

            if(minuteCounter == 5)
            {
                Debug.Log("GOTO NEXT ROUND");
            }

            if(_enemies.Count <= 0)
            {
                Debug.Log("GOTO NEXT ROUND");
            }

            //PAUSE THE GAME
            if (Input.GetKey(KeyCode.Q))
            {
                PauseGame();
            }
            
        }

        //CHECK IF THE GAME IS PAUSED
        if (Time.timeScale <= 0.0f && Input.GetKey(KeyCode.E)) //UNPAUSE GAME
        {
            Debug.Log("UNPAUSE");
            ResumeGame();
        }
        return false;
    }

    private void UpdateEnemies()
    {
        var _attackers = GameObject.FindGameObjectsWithTag("Enemy").ToList<GameObject>();
        var _pushers = GameObject.FindGameObjectsWithTag("PayloadPusher").ToList<GameObject>(); ;

        _attackers.ForEach(attacker =>
        {
            if (_enemies.Contains(attacker) == false)
            {
                _enemies.Add(attacker);
            }
        });

        _pushers.ForEach(pusher =>
        {
            if (_enemies.Contains(pusher) == false)
            {
                _enemies.Add(pusher);
            }
            if(pusher == null && _enemies.Contains(pusher))
            {
                _enemies.Remove(pusher);
            }
        });
    }

    //MAKE TIME FUNCTION
    private string _roundTime()
    {
        var rawTime = _roundTimer.ToString().ToList();
        string editTime = "";
        for (int i = 0; i <= 1; i++)
        {
            if (rawTime[i] == '.')
            {
                continue;
            }
            editTime += rawTime[i];
        }
        int calcTime = Int32.Parse(editTime);
        string seconds = "";
        
        if (calcTime / 60 == 1)
        {
            minuteCounter += 1;
            _roundTimer = 0.0f;
        }

        var minutes = "0" + minuteCounter;

        seconds = calcTime.ToString();
        if(calcTime <= 9)
        {
            seconds = "0" + calcTime;
        }
        string time = minutes + ":" + seconds;

        return time;
    }

    private void UpdateUI()
    {
        var _infopanel = _gameinfopanel.GetComponentsInChildren<Text>().ToList<Text>();
        var roundText = GameObject.FindGameObjectWithTag("RoundCounter");
        _infopanel.ForEach(x =>
        {
            if (x.tag == "RoundTimer")
            {
                x.text = "Time: " + _roundTime();                
            }

            if (x.tag == "EnemyCounter")
            {
                x.text = "Count: " + _enemies.Count;
            }
            
        });

        roundText.GetComponent<Text>().text = _roundCounter.ToString();
    }

    // Use this for initialization
    void Start()
    {
        _enemies = new List<GameObject>();
        _isPaused = false;
        _roundTimer = Time.time;
        _buttonpanel = GameObject.FindGameObjectWithTag("ButtonPanel");
        _gameinfopanel = GameObject.FindGameObjectWithTag("GameInfoPanel");
        _buttonpanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        GameLoop();
        //UPDATE UI WITH COUNT OF ENEMIES
        //AFTER PUSHER DIES, UI DOSENT UPDATE
        UpdateEnemies();
        UpdateUI();
        //Debug.Log(_roundTimer);
    }
}
