using System;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    public event Action<Vector3> FoundTarget;
    public event Action LostTarget;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerMover player))
            FoundTarget?.Invoke(player.Position);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerMover _))
            LostTarget?.Invoke();
    }
}