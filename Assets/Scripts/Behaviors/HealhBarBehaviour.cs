using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealhBarBehaviour : MonoBehaviour {

    private GameObject _player;
    private Slider _playerHealth;
    
    //private Slider _towerHealth;
    //private GameObject _tower;



    private void UpdateUI()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        if (_player != null && _playerHealth != null)
        {
            //_playerHealth.maxValue = _player.GetComponent<PlayerBehaviour>()._player.Health;
            _playerHealth.maxValue = 100;
            _playerHealth.value = _player.GetComponent<PlayerBehaviour>()._player.Health;
        }
    }


	// Use this for initialization
	void Start () {

        _player = GameObject.FindGameObjectWithTag("Player");
        _playerHealth = GetComponent<Slider>();       

	}
	
	// Update is called once per frame
	void Update () {

        UpdateUI();

	}
}
