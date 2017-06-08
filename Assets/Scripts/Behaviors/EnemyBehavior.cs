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
    public Transform ShootingPoint;
    public float BulletSpeed;
    [HideInInspector]
    public GameObject otherAmmo;

    private Transform Target;

    // Use this for initialization
    void Start()
    {
        ShootingPoint = GetComponentInChildren<Transform>();
        Target = GameObject.FindGameObjectWithTag(TargetTag).transform;
        EnemyConfig = ScriptableObject.CreateInstance<Enemy>();
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
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
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
        otherAmmo = Instantiate(Ammo, ShootingPoint.position, ShootingPoint.rotation);
        otherAmmo.GetComponent<Rigidbody>().velocity += ShootingPoint.forward * BulletSpeed;
        Destroy(otherAmmo, 5f);
    }

}
