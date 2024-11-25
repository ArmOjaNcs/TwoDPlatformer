using System;
using UnityEngine;

[RequireComponent (typeof(BoxCollider2D))]
public class SurfaceChecker : MonoBehaviour
{   
    public event Action<bool> CanJump;
    public event Action<bool> CanFallThrough;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Surface _))
            CanJump?.Invoke(true);

        if (collision.TryGetComponent(out Frame _))
            CanFallThrough?.Invoke(false);
        else
            CanFallThrough?.Invoke(true);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Surface _))
            CanJump?.Invoke(false);
    }
}
