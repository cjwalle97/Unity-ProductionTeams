using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraBehaviour : MonoBehaviour {

    private Transform _playertransform;
    private Camera _maincamera;
    public float camY = 12;
    public float camZ = -6;
    public float ang = 0;

	// Use this for initialization
	void Start () {
        _playertransform = GameObject.FindGameObjectWithTag("Player").transform;
        _maincamera = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {

        _playertransform = GameObject.FindGameObjectWithTag("Player").transform;        

    }


    private void FixedUpdate()
    {
        Vector3 offset = _playertransform.position;
        Vector3 calc_offset = new Vector3(0, camY, camZ);
        offset += calc_offset;
        _maincamera.transform.position = offset;
        _maincamera.transform.rotation = Quaternion.AngleAxis(ang, Vector3.right);
    }
}
