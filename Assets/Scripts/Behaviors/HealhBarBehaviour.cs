using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HealhBarBehaviour : MonoBehaviour
{
    #region //MEMBER VARIABLES
    public GameObject _player;
    [SerializeField]
    private Slider _playerHealth;
    [SerializeField]
    private Slider _towerHealth;
    public GameObject _tower;
    #endregion

    public void UpdatePlayerUI(float health)
    {
        _playerHealth.value = health;
    }

    public void UpdateTowerUI(float health)
    {
        _towerHealth.value = health;
    }
    
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _tower = GameObject.FindGameObjectWithTag("Tower");
        
        _playerHealth.maxValue = _player.GetComponent<PlayerBehaviour>()._player.MaxHealth;
        _playerHealth.value = _player.GetComponent<PlayerBehaviour>()._player.Health;
        _player.GetComponent<PlayerBehaviour>().onPlayerHealthChange.AddListener(UpdatePlayerUI);

        _towerHealth.maxValue = _tower.GetComponent<TowerBehaviour>()._tower.MaxHealth;
        _towerHealth.value = _tower.GetComponent<TowerBehaviour>()._tower.Health;
        _tower.GetComponent<TowerBehaviour>().onTowerHealthChange.AddListener(UpdateTowerUI);
    }

    void Update()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _tower = GameObject.FindGameObjectWithTag("Tower");

        _playerHealth.maxValue = _player.GetComponent<PlayerBehaviour>()._player.MaxHealth;
        _playerHealth.value = _player.GetComponent<PlayerBehaviour>()._player.Health;
        _player.GetComponent<PlayerBehaviour>().onPlayerHealthChange.AddListener(UpdatePlayerUI);

        _towerHealth.maxValue = _tower.GetComponent<TowerBehaviour>()._tower.MaxHealth;
        _towerHealth.value = _tower.GetComponent<TowerBehaviour>()._tower.Health;
        _tower.GetComponent<TowerBehaviour>().onTowerHealthChange.AddListener(UpdateTowerUI);
    }
}
