using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : ScriptableObject, IDamageable
{
    public float Health { get; set; }
    public float MaxHealth { get; set; }
}
