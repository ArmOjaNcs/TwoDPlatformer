using System;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    private bool _isPlayerFounded;

    public event Action<IEnemyTarget> PlayerFounded;
    public event Action TargetInZone;
    public event Action LostTarget;

    private void Start()
    {
        _isPlayerFounded = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out IEnemyTarget player) && _isPlayerFounded == false)
        {
            _isPlayerFounded = true;
            PlayerFounded?.Invoke(player);
        }

        if (collision.TryGetComponent(out IEnemyTarget _))
            TargetInZone?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerMover _))
            LostTarget?.Invoke();
    }
}