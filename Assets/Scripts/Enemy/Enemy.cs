using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private DetectionZone _detectionZone;
    [SerializeField] private PatrolZoneChecker _patrolZoneChecker;
    [SerializeField] private float _speed;

    private readonly int _left = -1;
    private readonly int _right = 1;
    private readonly float _delay = 1.5f;

    private SpriteRenderer _spriteRenderer;
    private WaitForSeconds _wait;
    private Coroutine _coroutine;
    private IEnemyTarget _target;
    private int _indexOfPoint;
    private int _direction;
    private bool _isPatrolling;
    private bool _isInZone;

    public event Action<bool, IEnemyTarget> PlayerInZone;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _wait = new WaitForSeconds(_delay);
        _isPatrolling = true;
    }

    private void OnEnable()
    {
        _detectionZone.FoundTarget += OnFoundTarget;
        _detectionZone.LostTarget += OnLostTarget;
        _patrolZoneChecker.EnemyInZone += OnEnemyInZone;
    }

    private void OnDisable()
    {
        _detectionZone.FoundTarget -= OnFoundTarget;
        _detectionZone.LostTarget -= OnLostTarget;
        _patrolZoneChecker.EnemyInZone -= OnEnemyInZone;
    }

    private void Update()
    {
        if (_isPatrolling)
            Patrol();
        else
            HarassPlayer();
    }

    public void ChangePatrolPoint()
    {
        _indexOfPoint = ++_indexOfPoint % _points.Length;
    }

    private void OnFoundTarget(IEnemyTarget target)
    {
        _target = target;
        _isPatrolling = false;

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        PlayerInZone?.Invoke(true, _target);
    }

    private void OnLostTarget()
    {
        _target = null;
        _isPatrolling = true;

        if (_isInZone == false && enabled)
            _coroutine = StartCoroutine(ReturnToPatrolZone());

        PlayerInZone?.Invoke(false, _target);
    }

    private void OnEnemyInZone(bool status)
    {
        _isInZone = status;

        if (_isInZone)
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
        }
    }

    private void HarassPlayer()
    {
        if (_target != null)
        {
            _direction = _target.Position.x < transform.position.x ? _left : _right;
            _spriteRenderer.flipX = _direction == _left ? false : true;
            transform.Translate(Vector2.right * _direction * _speed * Time.deltaTime);
        }
    }

    private void Patrol()
    {
        _spriteRenderer.flipX = _points[_indexOfPoint].position.x < transform.position.x ? false : true;
        transform.position = Vector2.MoveTowards(transform.position, _points[_indexOfPoint].position, _speed * Time.deltaTime);
    }

    private IEnumerator ReturnToPatrolZone()
    {
        yield return _wait;

        transform.position = _patrolZoneChecker.PatrolZone;
    }
}