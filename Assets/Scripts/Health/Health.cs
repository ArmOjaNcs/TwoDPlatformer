using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxValue;
    [SerializeField] private HitZone _hitZone;

    public event Action HealthUpdate;

    public float MaxValue => _maxValue;
    public float CurrentValue { get; private set; }

    private void Awake()
    {
        CurrentValue = MaxValue;
    }

    private void OnEnable()
    {
        _hitZone.DamageDetected += OnDamageDetected;
        _hitZone.MedPackDetected += OnMedPackDetected;
    }

    private void OnDisable()
    {
        _hitZone.DamageDetected -= OnDamageDetected;
        _hitZone.MedPackDetected -= OnMedPackDetected;
    }

    private void OnDamageDetected(float damage)
    {
        CurrentValue -= damage;

        if (CurrentValue < 0)
            CurrentValue = 0;

        HealthUpdate?.Invoke();

        if (CurrentValue == 0)
            Destroy(gameObject);
    }

    private void OnMedPackDetected(float heal)
    {
        CurrentValue += heal;

        if(CurrentValue > MaxValue)
            CurrentValue = MaxValue;

        HealthUpdate?.Invoke();
    }
}