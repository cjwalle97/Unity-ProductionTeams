using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPackBehaviour : MonoBehaviour {

    public float RestoreValue;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(RestoreValue <= 0.0f)
            {
                RestoreValue = 25.0f;
            }
            other.GetComponent<PlayerBehaviour>()._player.Health += RestoreValue;
            Destroy(gameObject);
        }
    }
}
