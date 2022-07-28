using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseUnit : MonoBehaviour
{
    [Header("Health Variables")]
    protected const int MAX_HEALTH = 5;
    [SerializeField] protected int health = 5;

    [Header("HealthBar")]
    [SerializeField] protected GameObject healthBarParent;
    [SerializeField] protected GameObject healthBarSlider;

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
            health = 0;
        }

        UpdateHealthBar();
    }

    protected void UpdateHealthBar()
    {
        if (healthBarParent.activeInHierarchy == false)
        {
            healthBarParent.SetActive(true);
        }

        healthBarSlider.transform.localScale = new Vector3(Mathf.Clamp01((float)health / (float)MAX_HEALTH), 1, 1);
    }

    protected virtual void Die()
    {
        gameObject.SetActive(false);
    }
}
