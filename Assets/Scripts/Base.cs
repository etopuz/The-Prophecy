using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour
{
    [Header("Health Variables")]
    [SerializeField] protected const int MAX_HEALTH = 5;
    [SerializeField] protected int health = 5;

    [Header("HealthBar")]
    [SerializeField] protected Canvas healthBar;
    [SerializeField] protected Image healthBarSlider;

    public void Start()
    {
        health = MAX_HEALTH;
    }

    public void OnTakeDamage(int damage)
    {
        health -= damage;

        if (health < 1)
        {
            Die();
        }

        UpdateHealthBar();
    }

    protected void UpdateHealthBar()
    {
        if (healthBar.enabled == false)
        {
            healthBar.enabled = true;
        }

        healthBarSlider.fillAmount = Mathf.Clamp01((float)health / (float)MAX_HEALTH);
    }

    protected virtual void Die()
    {
        gameObject.SetActive(false);
    }
}
