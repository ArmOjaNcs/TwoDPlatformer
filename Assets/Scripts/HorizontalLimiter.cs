using System;
using UnityEngine;

[RequireComponent (typeof(BoxCollider2D))]
public class HorizontalLimiter : MonoBehaviour
{
    public event Action<float> LimitReached;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out IEnemyTarget player))
        {
            float xLimit = transform.position.x - player.Position.x;
            LimitReached?.Invoke(xLimit);
        }
    }
}