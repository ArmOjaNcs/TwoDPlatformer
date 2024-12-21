using System;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private PatrolPoint[] _points;
    [SerializeField] private DetectionZone _detectionZone;
    [SerializeField] private float _speed;
    [SerializeField, Range(0, 1)] private int _startIndexOfPoints;

    private readonly int _left = -1;
    private readonly int _right = 1;

    private int _direction;
    private int _indexOfPoint;
    private bool _isPatrolling;
    private Vector3 _playerTarget;

    public event Action<int> DirectionIsChange;

    private void Awake()
    {
        _isPatrolling = true;
    }

    private void OnEnable()
    {
        _detectionZone.FoundTarget += OnFoundTarget;
        _detectionZone.LostTarget += OnLostTarget;
    }

    private void OnDisable()
    {
        _detectionZone.FoundTarget -= OnFoundTarget;
        _detectionZone.LostTarget -= OnLostTarget;
    }

    private void Start()
    {
        _indexOfPoint = _startIndexOfPoints;
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

    private void OnFoundTarget(Vector3 target)
    {
        _playerTarget = target;
        _isPatrolling = false;
    }

    private void OnLostTarget()
    {
        _playerTarget = Vector3.zero;
        _isPatrolling = true;
    }

    private void HarassPlayer()
    {
        if (_playerTarget != Vector3.zero)
        {
            _direction = _playerTarget.x < transform.position.x ? _left : _right;
            DirectionIsChange?.Invoke(_direction);
            transform.Translate(Vector2.right * _direction * _speed * Time.deltaTime);
        }
    }

    private void Patrol()
    {
        _direction = _points[_indexOfPoint].Position.x < transform.position.x ? _left : _right;
        DirectionIsChange?.Invoke(_direction);
        transform.position = Vector2.MoveTowards(transform.position, _points[_indexOfPoint].Position, _speed * Time.deltaTime);
    }
}