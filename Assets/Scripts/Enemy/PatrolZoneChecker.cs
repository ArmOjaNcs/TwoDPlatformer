using System;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PatrolZoneChecker : MonoBehaviour
{
    [SerializeField] private PatrolZone _patrolZone;

    public event Action<bool> EnemyInZone;

    public Vector3 PatrolZone => _patrolZone.transform.position;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PatrolZone patrolZone))
        {
            if (_patrolZone == patrolZone)
                EnemyInZone?.Invoke(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PatrolZone patrolZone))
        {
            if (_patrolZone == patrolZone)
                EnemyInZone?.Invoke(false);
        }
    }
}