using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkBulletBehaviour : MonoBehaviour
{

    private Enemy _shooter;
    
	// Use this for initialization
	void Start ()
	{
	    //_shooter = GetComponent<EnemyBehavior>().EnemyConfig;
	}

    public void SetOwner(Enemy owner)
    {
        _shooter = owner;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_shooter == null)
            return;
        if (other.tag == "Player")
        {   
            _shooter.DoDamage(other.GetComponent<PlayerBehaviour>()._player);
            Destroy(gameObject);
        }
        if (other.tag == "Tower")
        {
            _shooter.DoDamage(other.GetComponent<TowerBehaviour>()._tower);
            Destroy(gameObject);
        }
    }
}
