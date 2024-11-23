using System;
using UnityEngine;

public class EnemyHitZone : MonoBehaviour
{
    public event Action<int> DamageDetected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Bullet bullet))
            DamageDetected?.Invoke(bullet.Damage);
    }
}