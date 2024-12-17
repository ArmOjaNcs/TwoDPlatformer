using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _damage;

    private Rigidbody2D _rigidbody2D;
    private WaitForSeconds _lifeTime;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StartCoroutine(BeginLifeTime());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out HitZone hitZone) && hitZone.EnemyTarget == null)
        {
            hitZone.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }

    public void Init(WaitForSeconds lifeTime)
    {
        _lifeTime = lifeTime;
    }

    public void AddDirectionalForce(float force, int direction)
    {
        _rigidbody2D.linearVelocity = Vector2.right * force * direction;
    }

    private IEnumerator BeginLifeTime()
    {
        yield return _lifeTime;

        Destroy(gameObject);
    }
}