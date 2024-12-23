using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxValue;
    [SerializeField] private HitZone _hitZone;

    private EnemyTarget _enemyTarget;

    public event Action HealthUpdate;

    public float MaxValue => _maxValue;
    public float CurrentValue { get; private set; }

    private void Awake()
    {
        TryGetComponent(out EnemyTarget component);
        _enemyTarget = component;
        CurrentValue = MaxValue;
    }

    private void OnEnable()
    {
        _hitZone.DamageDetected += OnDamageDetected;
    }

    private void OnDisable()
    {
        _hitZone.DamageDetected -= OnDamageDetected;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out MedPack medPack) && _enemyTarget != null)
            if (CurrentValue < MaxValue)
                TakeHeal(medPack.GetHealing());
    }

    public void TakeHeal(float heal)
    {
        CurrentValue += heal;

        if (CurrentValue > MaxValue)
            CurrentValue = MaxValue;

        HealthUpdate?.Invoke();
    }

    public void OnDamageDetected(float damage)
    {
        CurrentValue -= damage;

        if (CurrentValue < 0)
            CurrentValue = 0;

        HealthUpdate?.Invoke();

        if (CurrentValue == 0)
            Destroy(gameObject);
    }
}