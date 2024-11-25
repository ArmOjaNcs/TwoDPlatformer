using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private EnemyHitZone _hitZone;
    [SerializeField] private float _speed;

    private readonly int _maxHitPoints = 100;

    private SpriteRenderer _spriteRenderer;
    private int _indexOfPoint;
    private int _hitPoints;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _hitPoints = _maxHitPoints;
    }

    private void OnEnable()
    {
        _hitZone.DamageDetected += OnDamageDetected;
    }

    private void Update()
    {
        MoveByPoints();
    }

    private void OnDisable()
    {
        _hitZone.DamageDetected -= OnDamageDetected;
    }

    private void OnDamageDetected(int damage)
    {
        _hitPoints -= damage;

        if (_hitPoints < 0)
            Destroy(gameObject);
    }

    private void MoveByPoints()
    {
        Transform currentPoint = _points[_indexOfPoint];
        transform.position = Vector2.MoveTowards(transform.position, currentPoint.position, _speed * Time.deltaTime);

        if (transform.position == currentPoint.position)
        {
            _indexOfPoint = ++_indexOfPoint % _points.Length;
            _spriteRenderer.flipX = !_spriteRenderer.flipX;
        }
    }
}