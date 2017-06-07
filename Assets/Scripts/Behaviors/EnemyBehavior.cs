using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour {

    public NavMeshAgent Agent;
    public Transform Target;
    public Enemy EnemyConfig;
    [HideInInspector]
    public Enemy _other;
    
    private Vector3 Position;

	// Use this for initialization
	void Start () {
        Position = gameObject.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
        CheckIfAlive();
        if (EnemyConfig.Health <= 0)
        {
            EnemyConfig.Alive = false;
        }
        Agent.SetDestination(Target.position);
    }

    public void CheckIfAlive()
    {
        if(EnemyConfig.Alive == false)
        {
            Destroy(gameObject);
        }
    }
}
