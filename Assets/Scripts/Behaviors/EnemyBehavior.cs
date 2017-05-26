using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour {

    public NavMeshAgent NavAgent;
    public GameObject Target;
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
        if(EnemyConfig.Health <= 0)
        {
            EnemyConfig.Alive = false;
        }
        CheckIfAlive();
        gameObject.transform.localPosition = Position;
    }

    public void CheckIfAlive()
    {
        if(EnemyConfig.Alive == false)
        {
            Destroy(gameObject);
        }
    }
}
