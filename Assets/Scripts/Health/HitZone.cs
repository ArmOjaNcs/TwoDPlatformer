using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class HitZone : MonoBehaviour
{
    private WaitForSeconds _wait;
    private float _time = 1;
    private bool _isCanBeDamaged;
    private EnemyTarget _enemyTarget;

    public event Action<float> DamageDetected;

    public EnemyTarget EnemyTarget => _enemyTarget;

    private void Awake()
    {
        TryGetComponent(out EnemyTarget component);
        _enemyTarget = component;
        _wait = new WaitForSeconds(_time);
        _isCanBeDamaged = true;
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

    private IEnumerator WaitAfterHit()
    {
        yield return _wait;
        
        _isCanBeDamaged = true;
    }
}