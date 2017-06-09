using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletSpawnBehaviour : MonoBehaviour {

    public Transform Spawn;

	// Use this for initialization
	void Start () {

        Spawn = GetComponent<Transform>();		
	}
	
	// Update is called once per frame
	void Update () {

        Spawn = GetComponent<Transform>();
    }
}
