using System.Linq;
//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //AFTER 5 MINUTES NEW ROUND STARTS
    //PAYLOAD SLOWLY RETREATS IF NO PUSHERS ON IT
    //IF ALL ENEMIES ARE DEAD, GOTO THE NEXT ROUND
    //AT PAUSE, ITERATE THROUGH EACH ENEMY CAMERA

    #region //MEMBER VARIABLES
    private GameObject _buttonpanel;
    private GameObject _gameinfopanel;

    public List<Transform> EnemySpawn;
    public Transform PayloadSpawn;

    private float _roundTimer;
    private float _spawnTimer;
    private int _roundCounter = 1;
    private int minuteCounter = 0;

    private List<GameObject> _enemies;
    private int _enemyLimit;
    private int _enemySpawnCap;
    private int _enemiesSpawned;
    private int _spawnIndex;
    #endregion

    #region //GAMESTATE
    private bool _isPaused;
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
    #endregion

    #region //RANDOM NUMBERS
    public float randomEnemy;
    private float pusherChance = .5f;
    private float playerattackerChance = .5f;
    private float towerattackerChance = .2f;
    #endregion

    //DOES NOT LIMIT THE SPAWNING OF ENEMIES
    private bool Populate()
    {
        randomEnemy = Random.Range(0f, 1f);
        _spawnTimer += Time.deltaTime;

        if (_spawnTimer >= 12.0f && _enemiesSpawned != _enemySpawnCap)
        {
            //SPAWN MORE ENEMIES
            //SPAWN 1-4 IN ORDER
            if (_spawnIndex >= _enemyLimit)
            {
                _spawnIndex = 0;
            }

            if (randomEnemy <= .51f && _enemiesSpawned != _enemySpawnCap)
            {
                //var playerAttacker = Instantiate(Resources.Load("RuntimePrefabs/PlayerAttacker"), EnemySpawn[_spawnIndex].position, EnemySpawn[_spawnIndex].rotation) as GameObject;
                ////playerAttacker.GetComponent<EnemyBehavior>().Ammo.GetComponent<JunkBulletBehaviour>().SetOwner(playerAttacker.GetComponent<EnemyBehavior>().EnemyConfig);
                ////playerAttacker.GetComponent<EnemyBehavior>().EnemyConfig.Health = 100.0f;
                ////playerAttacker.GetComponent<EnemyBehavior>().EnemyConfig.Damage = 10.0f;
                ////playerAttacker.GetComponent<EnemyBehavior>().EnemyConfig.Alive = true;
                //_enemies.Add(playerAttacker);
                //_enemiesSpawned += 1;
                //_spawnIndex += 1;
                //_spawnTimer = 0f;
                //Debug.Log("playerattackerspawned");
                return true;
            }

            if(randomEnemy >= .5f && _enemiesSpawned != _enemySpawnCap)
            {
                var payloadPusher = Instantiate(Resources.Load("RuntimePrefabs/PayloadPusherPrefab"), EnemySpawn[_spawnIndex].position, EnemySpawn[_spawnIndex].rotation) as GameObject;
                //DO A NULL CHECK HERE
                //payloadPusher.GetComponent<PayloadPusherBehaviour>().Pusher.Health = 50.0f;
                //payloadPusher.GetComponent<PayloadPusherBehaviour>().Pusher.Damage = 1.0f;
                //payloadPusher.GetComponent<PayloadPusherBehaviour>().Pusher.Alive = true;
                _enemies.Add(payloadPusher);
                _enemiesSpawned += 1;
                _spawnIndex += 1;
                _spawnTimer = 0f;
                return true;
                Debug.Log("payloadpusherspawned");
            }

            if(randomEnemy <= .60f && _enemiesSpawned != _enemySpawnCap)
            {
                //var towerAttacker = Instantiate(Resources.Load("RuntimePrefabs/TowerAttacker"), EnemySpawn[_spawnIndex].position, EnemySpawn[_spawnIndex].rotation) as GameObject;
                ////towerAttacker.GetComponent<EnemyBehavior>().TowerAttacker.Health = 80.0f;
                ////towerAttacker.GetComponent<TowerAttackerBehaviour>().TowerAttacker.Damage = 8.0f;
                ////towerAttacker.GetComponent<TowerAttackerBehaviour>().TowerAttacker.Alive = true;
                //_enemies.Add(towerAttacker);
                //_enemiesSpawned += 1;
                //_spawnIndex += 1;
                //_spawnTimer = 0f;
                return true;
            }
            return false;
        }

        return false;
    }

    private bool GameLoop()
    {
        if (_isPaused == false) //GAME LOOP
        {
            _roundTimer += Time.deltaTime;

            if (minuteCounter == 5)
            {
                Debug.Log("GOTO NEXT ROUND");
            }

            if(_enemies.Count > _enemyLimit)
            {
                var difference = _enemies.Count - _enemyLimit;
                for(int i = difference; i >= 0; i--)
                {
                    _enemies.Remove(_enemies[i]);
                }
            }

            if (_enemies.Count <= 0)
            {
                Debug.Log("GOTO NEXT ROUND");
            }

            //PAUSE THE GAME
            if (Input.GetKey("joystick button 7"))
            {
                PauseGame();
            }
        }

        //CHECK IF THE GAME IS PAUSED
        if (Time.timeScale <= 0.0f && Input.GetKey("joystick button 1")) //UNPAUSE GAME
        {
            Debug.Log("UNPAUSE");
            ResumeGame();
        }
        return false;
    }
   
    private void UpdateEnemies()
    {
        _enemies.ForEach(enemy => {
           if(enemy == null && _enemies.Contains(enemy))
            {
                _enemies.Remove(enemy);
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
        int calcTime = System.Int32.Parse(editTime);
        string seconds = "";

        if (calcTime / 60 == 1)
        {
            minuteCounter += 1;
            _roundTimer = 0.0f;
        }

        var minutes = "0" + minuteCounter;

        seconds = calcTime.ToString();
        if (calcTime <= 9)
        {
            seconds = "0" + calcTime;
        }
        string time = minutes + ":" + seconds;

        return time;
    }

    private void UpdateUI()
    {
        var enemyCountText = _gameinfopanel.GetComponentInChildren<Text>();
        var roundText = GameObject.FindGameObjectWithTag("RoundCounter");
        var timerText = GameObject.FindGameObjectWithTag("RoundTimer");


        enemyCountText.text = "Count: " + _enemies.Count().ToString();
        timerText.GetComponent<Text>().text = _roundTime();
        roundText.GetComponent<Text>().text = _roundCounter.ToString();
    }

    // Use this for initialization
    void Start()
    {
        _enemies = new List<GameObject>();
        _isPaused = false;
        randomEnemy = Random.Range(0f, 1f);
        _enemyLimit = 4;
        _enemySpawnCap = 10;
        _enemiesSpawned = 0;
        _spawnIndex = 0;
        _roundTimer = Time.time;
        _spawnTimer = Time.time;
        _buttonpanel = GameObject.FindGameObjectWithTag("ButtonPanel");
        _gameinfopanel = GameObject.FindGameObjectWithTag("GameInfoPanel");
        _buttonpanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //UPDATE UI WITH COUNT OF ENEMIES
        //AFTER PUSHER DIES, UI DOSENT UPDATE
        Populate();
        UpdateEnemies();
        GameLoop();
        UpdateUI();
        //Debug.Log(_roundTimer);
        //Debug.Log(minuteCounter);
        //Debug.Log("Spawned: :" + _enemiesSpawned);
    }
}
