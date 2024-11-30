using System;
using UnityEngine;

[RequireComponent (typeof(BoxCollider2D))]
public class SurfaceChecker : MonoBehaviour
{   
    public event Action<bool> CanJump;
    public event Action<bool> CanFallThrough;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Surface _))
        {
            CanJump?.Invoke(true);
            CanFallThrough?.Invoke(true);
        }
        else if(collision.TryGetComponent(out WaterSurface _))
        {
            CanJump?.Invoke(true);
            CanFallThrough?.Invoke(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Surface _) || collision.TryGetComponent(out WaterSurface _))
        {
            CanJump?.Invoke(false);
            CanFallThrough?.Invoke(false);
        }
    }
}
