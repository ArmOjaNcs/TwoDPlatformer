using System;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    public event Action<bool, Vector3> PlayerInZone;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerMover _))
        {
            Vector3 position = collision.transform.position;
            PlayerInZone?.Invoke(true, position);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerMover _) == false)
            PlayerInZone?.Invoke(false, Vector3.zero);
    }
}