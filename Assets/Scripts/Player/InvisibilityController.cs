using UnityEngine;
using TheProphecy.Interfaces;

public class InvisibilityController : MonoBehaviour, ISkill
{
    [Header("References")]
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [Header("Invisibility Variables")]
    [SerializeField] private float _invisibilityTime;
    [SerializeField] private float _invisibilityCooldown;

    [HideInInspector] public bool _isInvisible = false;
    private bool _isInvisibilityOnCooldown = false;
    private float _lastInvisibilityTime = 0f;
    private float _invisibilityCooldownPercentage = 1f;

    private Color _normalColor = new Color(1f, 1f, 1f, 1f);
    private Color _transparentColor = new Color(1f, 1f, 1f, 0.4f);

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        CalculateInvisibilityCooldown();
        CalculateInvisibilityCooldownPercentage();
    }

    private void ChangeCharacterVisibility()
    {
        if (_isInvisible)
        {
            _spriteRenderer.color = _transparentColor;
        }

        else
        {
            _spriteRenderer.color = _normalColor;
        }
    }

    private void CalculateInvisibilityCooldown()
    {
        _isInvisibilityOnCooldown = _lastInvisibilityTime == 0f ? false : Time.time - _lastInvisibilityTime < _invisibilityCooldown;

        if (!_isInvisible)
        {
            return;
        }

        bool isInvisibilityFinished = Time.time - _lastInvisibilityTime > _invisibilityTime;

        if (isInvisibilityFinished)
        {
            _isInvisible = false;
            ChangeCharacterVisibility();
        }
    }

    public void OnInvisibilityButtonPressed()
    {
        if (_isInvisibilityOnCooldown || _isInvisible)
        {
            return;
        }

        _lastInvisibilityTime = Time.time;
        _isInvisible = true;
        ChangeCharacterVisibility();
    }

    public float GetCooldownPercentage()
    {
        return _invisibilityCooldownPercentage;
    }


    public void CalculateInvisibilityCooldownPercentage()
    {
        if (_isInvisibilityOnCooldown)
        {
            _invisibilityCooldownPercentage = (Time.time - _lastInvisibilityTime) / _invisibilityCooldown;
        }
        else
        {
            _invisibilityCooldownPercentage = 1f;
        }
    }
}
