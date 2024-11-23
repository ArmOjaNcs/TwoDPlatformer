using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerHitZone : MonoBehaviour
{
    private WaitForSeconds _wait;
    private float _time = 1;
    private bool _isCanBeDamaged;

    public event Action<int> DamageDetected;

    private void Awake()
    {
        _wait = new WaitForSeconds(_time);
        _isCanBeDamaged = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out EnemyBullet bullet) && _isCanBeDamaged)
        {
            DamageDetected?.Invoke(bullet.Damage);
            _isCanBeDamaged = false;
            StartCoroutine(WaitAfterHit());
        }
    }

    private IEnumerator WaitAfterHit()
    {
        yield return _wait;

        _isCanBeDamaged = true;
    }
}