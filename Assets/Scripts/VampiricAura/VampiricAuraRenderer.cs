using System.Collections;
using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
public class VampiricAuraRenderer : MonoBehaviour
{
    [SerializeField] private VampiricAura _vampiricAura;

    private readonly float _activateTime = 0.3f;

    private SpriteRenderer _spriteRenderer;
    private Color _defaultColor;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultColor = _spriteRenderer.color;
    }

    private void OnEnable()
    {
        _vampiricAura.AuraEnabled += OnAuraEnabled;
    }

    private void OnDisable()
    {
        _vampiricAura.AuraEnabled -= OnAuraEnabled;
    }

    private void Start()
    {
        _spriteRenderer.color = Color.clear;
    }

    private void OnAuraEnabled(bool isActive)
    {
        if (isActive)
            StartCoroutine(UpdateColor(_spriteRenderer.color, _defaultColor));
        else
            StartCoroutine(UpdateColor(_defaultColor, Color.clear));
    }

    private IEnumerator UpdateColor(Color startColor, Color targetColor)
    {
        float elapsedTime = 0;

        while (elapsedTime < _activateTime)
        {
            elapsedTime += Time.deltaTime;
            float normalizedPosition = elapsedTime / _activateTime;
            _spriteRenderer.color = Color.Lerp(startColor, targetColor, normalizedPosition);

            yield return null;
        }

        _spriteRenderer.color = targetColor;
    }
}