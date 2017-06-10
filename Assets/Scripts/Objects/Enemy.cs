using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy")]
public class Enemy : ScriptableObject, IDamageable, IDamager {

    public float Health { get; set; }
    public float Damage { get; set; }
    public bool Alive;
    public bool Attacker;

    public void DoDamage(IDamageable damageable)
    {
        damageable.Health -= Damage;
    }
}
