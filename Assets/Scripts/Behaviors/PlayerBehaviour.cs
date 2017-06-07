using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

    private float _speed = 20;
    private float _lookspeed = 10;
    //NEEDS WORK
    public Player Player;

	// Use this for initialization
	void Start () {
        
	}


    Vector3 LookAround()
    {
        var _hori = Input.GetAxis("HorizontalArrow");
        var _vert = Input.GetAxis("VerticalArrow");

        Vector3 _direction = new Vector3(_hori, 0, _vert);        

        return _direction * _lookspeed * Time.deltaTime;
    }


	
	// Update is called once per frame
	void Update () {

        //SETUP MOVEMENT
        //SETUP CAMERA FOR PLAYER
        
    }

    void FixedUpdate()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        transform.localPosition += new Vector3(h, 0, v) * _speed * Time.deltaTime;

        transform.localRotation = Quaternion.LookRotation(LookAround());
    }

}
