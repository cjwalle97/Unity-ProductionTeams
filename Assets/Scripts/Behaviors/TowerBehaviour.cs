using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TowerBehaviour : MonoBehaviour {

    #region //MEMBER VARIABLES
    public Tower Tower;
    public float TowerHealth;

    [System.Serializable]
    public class OnTowerHealthChange : UnityEvent<float> { };
    public OnTowerHealthChange onTowerHealthChange;
    #endregion

    // Use this for initialization
    void Start () {

        Tower.Health = TowerHealth;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
