using System;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    public event Action<IEnemyTarget> FoundTarget;
    public event Action LostTarget;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IEnemyTarget player))
            FoundTarget?.Invoke(player);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IEnemyTarget _))
            LostTarget?.Invoke();
    }
}