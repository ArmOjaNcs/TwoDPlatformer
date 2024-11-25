using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;

    private WaitForSeconds _lifeTime;
    private Vector2 _direction;

    private void Start()
    {
        StartCoroutine(BeginLifeTime());
    }

    private void Update()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerHitZone hitZone))
        {
            hitZone.Detected(_damage);
            Destroy(gameObject);
        }
    }

    public void Init(WaitForSeconds lifeTime, Vector2 direction)
    {
        _lifeTime = lifeTime;
        _direction = direction;
    }

    private IEnumerator BeginLifeTime()
    {
        yield return _lifeTime;

        Destroy(gameObject);
    }

    private void Move()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);
    }
}