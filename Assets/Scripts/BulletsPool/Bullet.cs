using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Bullet : MonoBehaviour
{
    [SerializeField] private protected float Damage;

    private Coroutine _lifeCoroutine;
    private Coroutine _reliaseCoroutine;

    private protected WaitForSeconds LifeTime;
    private protected bool IsPlayerTarget;

    public event Action<Bullet> TargetReached;

    private void OnEnable()
    {
        _lifeCoroutine = StartCoroutine(BeginLifeTime());
        _reliaseCoroutine = null;
    }

    private void OnDisable()
    {
        if(_reliaseCoroutine != null)
            StopCoroutine(_reliaseCoroutine);

        if(_lifeCoroutine != null)
            StopCoroutine(_lifeCoroutine);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out HitZone hitZone) && hitZone.EnemyTarget == null == IsPlayerTarget)
        {
            if (_reliaseCoroutine == null)
                _reliaseCoroutine = StartCoroutine(Release(hitZone));
        }
    }

    public void Init(WaitForSeconds lifeTime, bool isPlayerTarget)
    {
        LifeTime = lifeTime;
        IsPlayerTarget = isPlayerTarget;
    }

    public void SetStartPosition(Transform shotPoint)
    {
        transform.position = shotPoint.position;
    }

    private IEnumerator BeginLifeTime()
    {
        yield return LifeTime;

        TargetReached?.Invoke(this);
    }

    private IEnumerator Release(HitZone hitZone)
    {
        hitZone.TakeDamage(Damage);

        yield return null;

        TargetReached?.Invoke(this);
    }
}