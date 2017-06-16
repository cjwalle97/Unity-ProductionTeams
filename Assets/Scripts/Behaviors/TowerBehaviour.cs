using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TowerBehaviour : MonoBehaviour {

    #region //MEMBER VARIABLES
    public Tower _tower;
    public float TowerMaxHealth;
    public float TowerHealth;

    [System.Serializable]
    public class OnTowerHealthChange : UnityEvent<float> { };
    public OnTowerHealthChange onTowerHealthChange;
    #endregion
    void Awake()
    {
        _tower = ScriptableObject.CreateInstance<Tower>();
    }
    
    void Start ()
    {   
        _tower.MaxHealth = TowerMaxHealth;
        _tower.Health = TowerHealth;
    }
	
	void Update ()
    {
        onTowerHealthChange.Invoke(_tower.Health);
	}
}
