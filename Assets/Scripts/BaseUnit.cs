using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseUnit : MonoBehaviour
{
    [Header("Health Variables")]
    protected const int MAX_HEALTH = 5;
    [SerializeField] protected int health = 5;
    protected bool isAlive = true;

    [Header("HealthBar")]
    [SerializeField] protected GameObject healthBarParent;
    [SerializeField] protected GameObject healthBarSlider;

    [Header("Hit Effect")]
    [SerializeField] protected Material flashMaterial;
    [SerializeField] private float duration = 0.1f;
    protected SpriteRenderer spriteRenderer;
    protected Material originalMaterial;
    protected Coroutine flashRoutine;


    public void Start()
    {
        health = MAX_HEALTH;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
    }

    public void OnTakeDamage(int damage)
    {
        health -= damage;
        Flash();

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
        StopCoroutine(flashRoutine);
        gameObject.SetActive(false);
        isAlive = false;
    }

    public void Flash()
    {
        if (isAlive)
        {
            if (flashRoutine != null)
            {
                StopCoroutine(flashRoutine);
            }
            flashRoutine = StartCoroutine(FlashRoutine());
        }
    }

    private IEnumerator FlashRoutine()
    {
        spriteRenderer.material = flashMaterial;
        yield return new WaitForSeconds(duration);
        spriteRenderer.material = originalMaterial;
        flashRoutine = null;
    }
}
