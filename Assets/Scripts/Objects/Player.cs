using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player")]
public class Player : ScriptableObject, IDamageable, IDamager
{
    public string Name;
    public bool Alive;
    public float Health { get; set; }
    public float Damage { get; set; }

    public void DoDamage(IDamageable damageable)
    {
        damageable.Health -= Damage;
    }
    
}
