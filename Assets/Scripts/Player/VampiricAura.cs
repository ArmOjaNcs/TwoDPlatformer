using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class VampiricAura : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private HitZone _hitZone;

    private readonly float _damageTime = 1.0f;

    private float _currentTime;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            _currentTime += Time.deltaTime;

            if (_currentTime >= _damageTime)
            {
                _currentTime = 0;
                HitZone hitZone = enemy.transform.GetComponentInChildren<HitZone>();

                if (hitZone != null)
                {
                    hitZone.TakeDamage(_damage);
                    _hitZone.TakeHeal(_damage);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy _))
            _currentTime = 0;
    }
}