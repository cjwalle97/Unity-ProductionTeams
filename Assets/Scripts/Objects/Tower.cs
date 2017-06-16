using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerObject", menuName = "Towers/Tower")]
public class Tower : ScriptableObject, IDamageable
{
    public float _health;
    public float _MaxHealth;
    public float Health
    {
        get { return _health; }
        set { _health = value; }
    }
    public float MaxHealth
    {
        get { return _MaxHealth; }
        set { _MaxHealth = value; }
    }
}
