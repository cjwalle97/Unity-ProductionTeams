using System.Linq;
//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //AFTER 5 MINUTES NEW ROUND STARTS
    //PAYLOAD SLOWLY RETREATS IF NO PUSHERS ON IT
    //IF ALL ENEMIES ARE DEAD, GOTO THE NEXT ROUND
    //AT PAUSE, ITERATE THROUGH EACH ENEMY CAMERA

    #region //MEMBER VARIABLES
    private GameObject _buttonpanel;
    private GameObject _gameinfopanel;
    public GameObject HealthPack;

    public List<Transform> EnemySpawn;
    public Transform PayloadSpawn;
    public Transform PlayerSpawn;
    public Transform HealthPackSpawn;

    private float _roundTimer;
    private float _spawnTimer;
    private int _roundCounter = 1;
    private int minuteCounter = 0;

    private List<GameObject> _enemies;
    private int _enemyLimit;
    private int _enemySpawnCap;
    private int _enemiesSpawned;
    private int _spawnIndex;
    private int _enemiesLeft;
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
        var _payload = GameObject.FindGameObjectWithTag("Payload");
        var _enemybullets = GameObject.FindGameObjectsWithTag("JunkBullet");
        var _playerbullets = GameObject.FindGameObjectsWithTag("PlayerBullet");

        var enemies = Gather();        
        var enemyBullets = _enemybullets.ToList<GameObject>();
        var playerBullets = _playerbullets.ToList<GameObject>();

        enemies.ForEach(enemy => { enemy.GetComponent<Rigidbody>().isKinematic = true; });
        enemyBullets.ForEach(bullet => { bullet.GetComponent<Rigidbody>().isKinematic = true; });
        playerBullets.ForEach(bullet => { bullet.GetComponent<Rigidbody>().isKinematic = true; });

        _player.GetComponent<Rigidbody>().isKinematic = true;
        _payload.GetComponent<Rigidbody>().isKinematic = true;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("3.mainmenu");
    }

    private void ResumeGame()
    {
        var _player = GameObject.FindGameObjectWithTag("Player");
        var _payload = GameObject.FindGameObjectWithTag("Payload");
        var _enemybullets = GameObject.FindGameObjectsWithTag("JunkBullet");
        var _playerbullets = GameObject.FindGameObjectsWithTag("PlayerBullet");

        var enemies = Gather();
        var enemyBullets = _enemybullets.ToList<GameObject>();
        var playerBullets = _playerbullets.ToList<GameObject>();

        enemies.ForEach(enemy => { enemy.GetComponent<Rigidbody>().isKinematic = false; });
        enemyBullets.ForEach(bullet => { bullet.GetComponent<Rigidbody>().isKinematic = false; });
        playerBullets.ForEach(bullet => { bullet.GetComponent<Rigidbody>().isKinematic = false; });

        _player.GetComponent<Rigidbody>().isKinematic = false;
        _payload.GetComponent<Rigidbody>().isKinematic = false;

        Time.timeScale = 1.0f;
        _buttonpanel.SetActive(false);
        _isPaused = false;
    }

    public void QuitGame()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }
    #endregion
    
    private void NextRound()
    {
        Time.timeScale = 0.0f;
        Debug.Log("GOING TO NEXT ROUND");
        //IF ANY ENEMIES LEFT, GATHER AND DELETE
        //INCREMENT ROUND NUMBER
        //RESET PLAYER POSITION TO 'PLAYERSPAWN'

        _roundCounter += 1;
        _spawnTimer = 0.0f;
        _roundTimer = 0.0f;
        minuteCounter = 0;
        _enemyLimit += 1;
        _enemySpawnCap += 10;
        _enemiesLeft = _enemySpawnCap;
        _spawnIndex = 0;

        for(int i = 0; i < _enemyLimit; i++)
        {
            var enemies = Gather();

            for (int enemy = 0; enemy < enemies.Count; enemy++)
            {
                Destroy(enemies[enemy]);
            }

            var payload = GameObject.FindGameObjectWithTag("Payload");
            payload.transform.position = PayloadSpawn.position;
            payload.transform.rotation = PayloadSpawn.rotation;

            var player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = PlayerSpawn.position;
            player.transform.rotation = PlayerSpawn.rotation;
        }

        var player_maxheal = GameObject.FindGameObjectWithTag("Player");
        var tower_maxheal = GameObject.FindGameObjectWithTag("Tower");
        player_maxheal.GetComponent<PlayerBehaviour>()._player.MaxHealth += 20;
        tower_maxheal.GetComponent<TowerBehaviour>()._tower.MaxHealth += 25;

        var healthpack = Instantiate(HealthPack, HealthPackSpawn.position, HealthPackSpawn.rotation);

        Time.timeScale = 1.0f;
    }

    private void Populate()
    {
        _spawnTimer += Time.deltaTime;

        if (_spawnTimer >= 12.0f && _enemiesSpawned != _enemySpawnCap)
        {
            //SPAWN MORE ENEMIES
            //SPAWN 1-4 IN ORDER
            if (_spawnIndex >= _enemyLimit)
            {
                _spawnIndex = 0;
            }

            if (_enemiesSpawned != _enemySpawnCap)
            {
                var playerAttacker = Instantiate(Resources.Load("RuntimePrefabs/PlayerAttacker"), EnemySpawn[_spawnIndex].position, EnemySpawn[_spawnIndex].rotation) as GameObject;
                //playerAttacker.GetComponent<EnemyBehavior>().Ammo.GetComponent<JunkBulletBehaviour>().SetOwner(playerAttacker.GetComponent<EnemyBehavior>().EnemyConfig);
                //playerAttacker.GetComponent<EnemyBehavior>().EnemyConfig.Health = 100.0f;
                //playerAttacker.GetComponent<EnemyBehavior>().EnemyConfig.Damage = 10.0f;
                //playerAttacker.GetComponent<EnemyBehavior>().EnemyConfig.Alive = true;
                _enemies.Add(playerAttacker);
                _enemiesSpawned += 1;
                _enemiesLeft -= 1;
                _spawnIndex += 1;
                _spawnTimer = 0f;
                Debug.Log("playerattackerspawned");
                //return true;
            }

            if (_enemiesSpawned != _enemySpawnCap)
            {
                var payloadPusher = Instantiate(Resources.Load("RuntimePrefabs/PayloadPusher"), EnemySpawn[_spawnIndex].position, EnemySpawn[_spawnIndex].rotation) as GameObject;
                //DO A NULL CHECK HERE
                //payloadPusher.GetComponent<PayloadPusherBehaviour>().Pusher.Health = 50.0f;
                //payloadPusher.GetComponent<PayloadPusherBehaviour>().Pusher.Damage = 1.0f;
                //payloadPusher.GetComponent<PayloadPusherBehaviour>().Pusher.Alive = true;
                _enemies.Add(payloadPusher);
                _enemiesSpawned += 1;
                _enemiesLeft -= 1;
                _spawnIndex += 1;
                _spawnTimer = 0f;
                //return true;
                Debug.Log("payloadpusherspawned");
            }

            if(_enemiesSpawned != _enemySpawnCap && _roundCounter >= 5)
            {
                var towerAttacker = Instantiate(Resources.Load("RuntimePrefabs/TowerAttacker"), EnemySpawn[4].position, EnemySpawn[4].rotation) as GameObject;
                //towerAttacker.GetComponent<EnemyBehavior>().TowerAttacker.Health = 80.0f;
                //towerAttacker.GetComponent<TowerAttackerBehaviour>().TowerAttacker.Damage = 8.0f;
                //towerAttacker.GetComponent<TowerAttackerBehaviour>().TowerAttacker.Alive = true;
                _enemies.Add(towerAttacker);
                _enemiesSpawned += 1;
                _enemiesLeft -= 1;
                _spawnIndex += 1;
                _spawnTimer = 0f;
                //return true;
            }
            //return false;
        }

        //return false;
    }

    private List<GameObject> Gather()
    {
        List<GameObject> gathered = new List<GameObject>();

        //PUSHERS
        //ATTACKERS
        //TOWERATTACKERS
        var attackers = GameObject.FindGameObjectsWithTag("Enemy");
        var pushers = GameObject.FindGameObjectsWithTag("PayloadPusher");

        var enemylist = attackers.ToList<GameObject>();
        var pusherlist = pushers.ToList<GameObject>();

        enemylist.ForEach(enemy => { gathered.Add(enemy); });
        pusherlist.ForEach(pusher => { gathered.Add(pusher); });

        return gathered;
    }

    private bool GameLoop()
    {
        if (_isPaused == false) //GAME LOOP
        {
            _roundTimer += Time.deltaTime;

            if (minuteCounter == 3)
            {
                Debug.Log("GOTO NEXT ROUND");
                NextRound();
                return true;
            }

            if(_enemies.Count > _enemyLimit)
            {
                var difference = _enemies.Count - _enemyLimit;
                for(int i = difference; i >= 0; i--)
                {
                    _enemies.Remove(_enemies[i]);
                }
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


        enemyCountText.text = "Count: " + _enemiesLeft.ToString();
        timerText.GetComponent<Text>().text = _roundTime();
        roundText.GetComponent<Text>().text = _roundCounter.ToString();
    }

    // Use this for initialization
    void Start()
    {
        _enemies = new List<GameObject>();
        _isPaused = false;
        _enemyLimit = 4;
        _enemySpawnCap = 10;
        _enemiesSpawned = 0;
        _enemiesLeft = _enemySpawnCap;
        _spawnIndex = 0;
        _roundTimer = 0.0f;
        _spawnTimer = 0.0f;
        _buttonpanel = GameObject.FindGameObjectWithTag("ButtonPanel");
        _gameinfopanel = GameObject.FindGameObjectWithTag("GameInfoPanel");
        var healthpack = Instantiate(HealthPack, HealthPackSpawn.position, HealthPackSpawn.rotation);
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
