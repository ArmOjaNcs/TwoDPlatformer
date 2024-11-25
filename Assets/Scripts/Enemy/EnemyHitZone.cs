using System;
using UnityEngine;

public class EnemyHitZone : MonoBehaviour
{
    public event Action<int> DamageDetected;

    public void Detected(int damage)
    {
        DamageDetected?.Invoke(damage);
    }
}