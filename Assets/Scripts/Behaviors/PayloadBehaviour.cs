using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PayloadBehaviour : MonoBehaviour {

    public Transform PayloadSpawn;
    private Transform _target;
    public Transform PusherTarget;
    public float PusherEffort;

    private Animator _ani;
    private NavMeshAgent _route;
    
	// Use this for initialization
	void Start () {
        _target = GameObject.FindGameObjectWithTag("Tower").transform;
        _ani = GetComponent<Animator>();
        _route = GetComponent<NavMeshAgent>();
        _route.speed = 1.0f;
        _route.SetDestination(_target.position);
	}
	
	// Update is called once per frame
	void Update ()
    {
        //_target = GameObject.FindGameObjectWithTag("Tower").transform;
        //_route.SetDestination(_target.position);
        
    }

    private void FixedUpdate()
    {   
        _target = GameObject.FindGameObjectWithTag("Tower").transform;
        _route.acceleration = PusherEffort;
        //_route.destination = _target.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PayloadPusher")
        {
            PusherEffort += other.GetComponent<PayloadPusherBehaviour>().Pusher.Damage;
        }

        if (other.tag == "Tower")
        {
            SceneManager.LoadScene("4.gameover");
        }

        if(other.tag == "Player")
        {
            transform.position = PayloadSpawn.position;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PayloadPusher")
        {
            PusherEffort -= other.GetComponent<PayloadPusherBehaviour>().Pusher.Damage;
        }
    }
}
