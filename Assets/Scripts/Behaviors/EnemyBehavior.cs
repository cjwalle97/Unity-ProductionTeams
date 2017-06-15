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
    private float ShotTime;


    // Use this for initialization
    void Start()
    {
        Target = GameObject.FindGameObjectWithTag(TargetTag).transform;
        EnemyConfig.Health = Health;
        EnemyConfig.Damage = Damage;
        EnemyConfig.Alive = true;
        
    }

    void FixedUpdate()
    {
        Agent.SetDestination(Target.position);
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
        
        if (Agent.remainingDistance > Agent.stoppingDistance)
        {
            ShotTime = 0f;
            EnemyAnimator.SetBool("Target in Range", false);
        }
        else
        {
            EnemyAnimator.SetBool("Target in Range", true);
            ShotTime += 1f;
            Aim();
            Shoot();
        }
        
    }

    private void Aim()
    {
        Vector3 direction = Target.position - _location.position;
        Quaternion lookhere = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(_location.rotation, lookhere, Time.deltaTime * 2);
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
        if (ShotTime % 30 == 0 && EnemyConfig.Alive == true)
        {
            otherAmmo = Instantiate(Ammo, _location.position, gameObject.transform.localRotation);
            otherAmmo.GetComponent<Rigidbody>().velocity += gameObject.transform.forward * BulletSpeed;
            Destroy(otherAmmo, 3f);
        }
    }

}
