using System;
using TheProphecy.Interfaces;
using UnityEngine;

public class BasePlayer : BaseUnit, IDamageable
{
    [SerializeField] private UIController _uIController;

    protected override void Die()
    {
        base.Die();
        _uIController.ToggleDeathScreen(true);
    }

    public void Resurrect()
    {
        health = MAX_HEALTH;
        isAlive = true;
        spriteRenderer.material = originalMaterial;
        UpdateHealthBar();
    }
}
