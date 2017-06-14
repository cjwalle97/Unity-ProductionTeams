using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public float Health = 100;
    public float Damage = 10;
    public string TargetTag;
    public NavMeshAgent Agent;
    public Enemy EnemyConfig;
    public GameObject Ammo;
    public float BulletSpeed;
    public Animator EnemyAnimator;
    public Transform _location;
    [HideInInspector]
    public GameObject otherAmmo;

    private Transform Target;


    // Use this for initialization
    void Start()
    {
        Target = GameObject.FindGameObjectWithTag(TargetTag).transform;
        EnemyConfig.Health = Health;
        EnemyConfig.Damage = Damage;
        EnemyConfig.Alive = true;
        
    }

    // Update is called once per frame
    void Update()
    {

        Target = GameObject.FindGameObjectWithTag(TargetTag).transform;
        if (EnemyConfig.Health <= 0)
        {
            EnemyConfig.Alive = false;
        }
        CheckIfAlive();
        Agent.SetDestination(Target.position);
        if (Agent.remainingDistance > Agent.stoppingDistance)
        {
            EnemyAnimator.SetBool("Target in Range", false);
        }
        else
        {
            Shoot();
        }

    }

    public void CheckIfAlive()
    {
        if (EnemyConfig.Alive == false)
        {
            EnemyAnimator.SetBool("Alive", EnemyConfig.Alive);
            Destroy(gameObject, 5f);
        }
    }

    public void Shoot()
    {
        EnemyAnimator.SetBool("Target in Range", true);
        otherAmmo = Instantiate(Ammo, _location.position, gameObject.transform.localRotation);
        otherAmmo.GetComponent<Rigidbody>().velocity += gameObject.transform.forward * BulletSpeed;
        Destroy(otherAmmo, 5f);
    }

}
