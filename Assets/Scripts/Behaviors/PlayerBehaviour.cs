using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

    private float _speed = 20;
    //NEEDS WORK
    public Player Player;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

        //SETUP MOVEMENT
        //SETUP CAMERA FOR PLAYER
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        transform.localPosition += new Vector3(h, 0, v) * _speed * Time.deltaTime;
    }
}
