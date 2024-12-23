using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class VerticalLimiter : MonoBehaviour
{
    public event Action<float> LimitReached;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IEnemyTarget player))
        {
            float yLimit = transform.position.y - player.Position.y;
            LimitReached?.Invoke(yLimit);
        }
    }
}