using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MainCameraBehaviour : MonoBehaviour {

    public bool IsCameraEnabled;
    private List<Camera> _cameras;
    private Transform _playertransform;
    private Camera _maincamera;
    public float camY = 12;
    public float camZ = -6;
    public float ang = 0;

	// Use this for initialization
	void Start () {
        _playertransform = GameObject.FindGameObjectWithTag("Player").transform;
        _maincamera = GetComponent<Camera>();
        _cameras = GameObject.FindObjectsOfType<Camera>().ToList<Camera>();
	}
	
	// Update is called once per frame
	void Update () {

        _cameras.ForEach(camera =>
        {
            if (camera != null)
            {
                camera.enabled = false;
                camera.GetComponent<AudioListener>().enabled = false;

                if (IsCameraEnabled == false)
                {
                    camera.enabled = true;
                    camera.GetComponent<AudioListener>().enabled = true;
                }

                if (camera.tag == "MainCamera")
                {
                    camera.enabled = IsCameraEnabled;
                    camera.GetComponent<AudioListener>().enabled = IsCameraEnabled;
                }
            }
        });
        _playertransform = GameObject.FindGameObjectWithTag("Player").transform;
        //_maincamera.transform.rotation = Quaternion.LookRotation(_playertransform.forward);

    }

    private void FixedUpdate()
    {
        Vector3 offset = _playertransform.position;
        Vector3 calc_offset = new Vector3(0, camY, camZ);
        offset += calc_offset;
        _maincamera.transform.position = offset;
        _maincamera.transform.rotation = Quaternion.AngleAxis(ang, Vector3.right);
        //_maincamera.transform.rotation = _playertransform.localRotation;
        //_maincamera.transform.rotation = Quaternion.LookRotation(_playertransform.forward);
    }
}
