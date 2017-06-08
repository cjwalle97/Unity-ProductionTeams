using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{

    public NavMeshAgent Agent;
    public Transform Target;
    public Enemy EnemyConfig;
    public GameObject Ammo;
    public GameObject ShootingPoint;
    [HideInInspector]
    public GameObject otherAmmo;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckIfAlive();
        if (EnemyConfig.Health <= 0)
        {
            EnemyConfig.Alive = false;
        }
        Agent.SetDestination(Target.position);
        //if (EnemyConfig.Attacker == true)
        //{
        //    Shoot();
        //}
    }

    public void CheckIfAlive()
    {
        if (EnemyConfig.Alive == false)
        {
            Destroy(gameObject);
        }
    }

    public void Shoot()
    {
        otherAmmo = Instantiate(Ammo);
        otherAmmo.transform.position = ShootingPoint.transform.position;
        Destroy(otherAmmo, 2f);
    }

}
