using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PayloadBehaviour : MonoBehaviour {

    public Transform Target;
    public Transform PusherTarget;
    public float PusherEffort;

    private Animator _ani;
    private NavMeshAgent _route;



	// Use this for initialization
	void Start () {

        _ani = GetComponent<Animator>();
        _route = GetComponent<NavMeshAgent>();
        _route.SetDestination(Target.position);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        _route.speed = PusherEffort;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PayloadPusher")
        {
            PusherEffort += other.GetComponent<PayloadPusherBehaviour>().Pusher.Damage;
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
