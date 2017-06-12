using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private GameObject _buttonpanel;

    private void PauseGame()
    {
        //player
        //enemys
        //payload
        //payload pushers
        //timer??
        Time.timeScale = 0;

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
    }

    // Use this for initialization
    void Start()
    {
        _buttonpanel = GameObject.FindGameObjectWithTag("ButtonPanel");
        _buttonpanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey("joystick button 7"))
        {
            PauseGame();
        }

        //CHECK IF THE GAME IS PAUSED
        if(Time.timeScale <= 0.0f && Input.GetKey("joystick button 1"))
        {
            ResumeGame();
        }        

    }
}
