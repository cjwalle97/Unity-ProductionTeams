using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamager
{
    void DoDamage(IDamageable damageable);
    float Damage { get; set; }
}

public interface IDamageable
{
    void TakeDamage(float damage);
    float Health { get; set; }
}
