using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PatrolPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out EnemyMover enemy))
            enemy.ChangePatrolPoint();
    }
}