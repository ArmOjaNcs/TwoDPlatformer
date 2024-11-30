using System;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PatrolZoneChecker : MonoBehaviour
{
    [SerializeField] private PatrulZone _patrulZone;

    public event Action<bool> EnemyInZone;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PatrulZone patrulZone))
        {
            if (_patrulZone == patrulZone)
                EnemyInZone?.Invoke(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PatrulZone patrulZone))
        {
            if (_patrulZone == patrulZone)
                EnemyInZone?.Invoke(false);
        }
    }
}