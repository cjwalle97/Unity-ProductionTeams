using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletBehaviour : MonoBehaviour {

    private Player _player;
    
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>()._player;
    }

	private void OnTriggerEnter(Collider other)
    {   
        if(other.tag == "Enemy")
        {   
            _player.DoDamage(other.GetComponent<EnemyBehavior>().EnemyConfig);
            Destroy(gameObject);
        }

        if(other.tag == "PayloadPusher")
        {
            _player.DoDamage(other.GetComponent<PayloadPusherBehaviour>().Pusher);
            Destroy(gameObject);
        }
    }
}
