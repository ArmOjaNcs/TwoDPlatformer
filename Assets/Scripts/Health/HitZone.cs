using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class HitZone : MonoBehaviour
{
    [SerializeField] private Health _health;

    private WaitForSeconds _wait;
    private float _time = 1;
    private bool _isCanBeDamaged;
    private EnemyTarget _enemyTarget;

    public event Action<float> DamageDetected;
    public event Action<float> HealDetected;

    public EnemyTarget EnemyTarget => _enemyTarget;
    public float CurrentHealthValue => _health.CurrentValue;

    private void Awake()
    {
        TryGetComponent(out EnemyTarget component);
        _enemyTarget = component;
        _wait = new WaitForSeconds(_time);
        _isCanBeDamaged = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out MedPack medPack) && _enemyTarget != null)
        {
            HealDetected?.Invoke(medPack.GetHealing());

            medPack.PickUp();
        }
    }

    public void TakeDamage(float damage)
    {
        if(_enemyTarget != null && _isCanBeDamaged)
        {
            DamageDetected?.Invoke(damage);
            _isCanBeDamaged = false;
            StartCoroutine(WaitAfterHit());
        } 
        else if(_enemyTarget == null)
        {
            DamageDetected?.Invoke(damage);
        }
    }

    public void TakeHeal(float heal)
    {
       HealDetected?.Invoke(heal);
    }

    private IEnumerator WaitAfterHit()
    {
        yield return _wait;
        
        _isCanBeDamaged = true;
    }
}