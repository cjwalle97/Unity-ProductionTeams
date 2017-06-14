using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TowerBehaviour : MonoBehaviour {

    #region //MEMBER VARIABLES
    public Tower _tower;
    public float TowerMaxHealth;
    public float TowerHealth;

    [System.Serializable, HideInInspector]
    public class OnTowerHealthChange : UnityEvent<float> { };
    public OnTowerHealthChange onTowerHealthChange;
    #endregion

    // Use this for initialization
    void Start () {
        _tower = ScriptableObject.CreateInstance<Tower>();
        _tower.MaxHealth = TowerMaxHealth;
        _tower.Health = TowerHealth;
    }
	
	// Update is called once per frame
	void Update () {

        onTowerHealthChange.Invoke(_tower.Health);
	}
}
