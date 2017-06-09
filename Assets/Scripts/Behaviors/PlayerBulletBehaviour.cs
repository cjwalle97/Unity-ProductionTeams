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
            other.GetComponent<EnemyBehavior>().EnemyConfig.TakeDamage(_player.Damage);
            Destroy(gameObject);
        }

        if(other.tag == "PayloadPusher")
        {
            other.GetComponent<EnemyBehavior>().EnemyConfig.TakeDamage(_player.Damage);
            Destroy(gameObject);
        }
    }
}
