using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheProphecy.Interfaces;
using System;
using UnityEngine.UI;

public class BasePlayer : Base, IDamageable
{
    protected override void Die()
    {
        base.Die();
        Debug.Log("Git Gud");
    }
}
