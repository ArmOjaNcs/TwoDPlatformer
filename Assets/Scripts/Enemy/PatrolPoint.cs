using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PatrolPoint : MonoBehaviour
{
    public Vector3 Position => transform.position;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out EnemyMover enemy))
            enemy.ChangePatrolPoint();
    }
}