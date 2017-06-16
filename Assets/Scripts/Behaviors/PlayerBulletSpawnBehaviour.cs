using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletSpawnBehaviour : MonoBehaviour {

    public Transform Spawn;
    
	void Start ()
    {
        Spawn = transform;		
	}

	void FixedUpdate ()
    {
        Spawn = transform;
    }
}
