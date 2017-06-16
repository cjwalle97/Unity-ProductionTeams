using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PayloadPusherBehaviour : MonoBehaviour {

    [HideInInspector]
    public Enemy Pusher;
    public float Health;
    public float PushEffort;

    private Animator _pusherAni;
    private NavMeshAgent _pusher;
    private bool _pushing;
    private Transform _target;

    bool Push()
    {
        if(_pusher.remainingDistance < _pusher.stoppingDistance)
        {
            return true;
        }
        return false;
    }

    void Dead()
    {
        Pusher.Alive = false;
        Destroy(gameObject, 2);
    }

	void Start ()
    {
        _pusherAni = GetComponent<Animator>();
        _pusher = GetComponent<NavMeshAgent>();
        Pusher = ScriptableObject.CreateInstance<Enemy>();
        _target = GameObject.FindGameObjectWithTag("Payload").GetComponent<PayloadBehaviour>().PusherTarget;

        Pusher.Health = Health;
        Pusher.Damage = PushEffort;
        Pusher.Alive = true;
        _pusher.acceleration = 1.0f;
        _pusher.speed = 2.0f;
        _pusher.SetDestination(_target.position);
	}
	
	void Update ()
    {   
        _pushing = Push();
        if(Pusher.Health <= 0.0f)
        {
            Dead();
        }
	}

    private void FixedUpdate()
    {
        _target = GameObject.FindGameObjectWithTag("Payload").GetComponent<PayloadBehaviour>().PusherTarget;
        _pusher.destination = _target.position;

        _pusherAni.SetBool("Alive", Pusher.Alive);
        _pusherAni.SetBool("Pushing", _pushing);
    }
}
