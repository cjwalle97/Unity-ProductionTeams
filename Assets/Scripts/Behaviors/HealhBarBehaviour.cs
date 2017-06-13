using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HealhBarBehaviour : MonoBehaviour
{
    #region //MEMBER VARIABLES
    private GameObject _player;
    private Slider _playerHealth;
    private Slider _towerHealth;
    private GameObject _tower;
    #endregion

    private void UpdatePlayerUI(float health)
    {
        _playerHealth.value = health;
    }

    private void UpdateTowerUI(float health)
    {
        _towerHealth.value = health;
    }

    // Use this for initialization
    void Start()
    {
        if(gameObject.tag == "PlayerHealth")
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _playerHealth = GetComponent<Slider>();
            _player.GetComponent<PlayerBehaviour>().onPlayerHealthChange.AddListener(UpdatePlayerUI);
        }
        
        if(gameObject.tag == "TowerHealth")
        {
            _tower = GameObject.FindGameObjectWithTag("Tower");
            _towerHealth = GetComponent<Slider>();
            _tower.GetComponent<TowerBehaviour>().onTowerHealthChange.AddListener(UpdateTowerUI);
        }
    }

    // Update is called once per frame
    void Update()
    {


    }
}
