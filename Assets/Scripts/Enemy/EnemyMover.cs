using System;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMover : MonoBehaviour, IDetectionZoneListener, IPlayerTarget
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
    private IEnemyTarget _playerTarget;

    public event Action<int> DirectionIsChange;

    public Health Health { get; set; }
    public Vector3 Position => transform.position;

    private void Awake()
    {
        Health = GetComponent<Health>();
        _isPatrolling = true;
    }

    private void OnEnable()
    {
        _detectionZone.PlayerFounded += OnPlayerFounded;
        _detectionZone.TargetInZone += OnTargetInZone;
        _detectionZone.LostTarget += OnLostTarget;
    }

    private void OnDisable()
    {
        _detectionZone.PlayerFounded -= OnPlayerFounded;
        _detectionZone.TargetInZone -= OnTargetInZone;
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

    public void OnPlayerFounded(IEnemyTarget player)
    {
        _playerTarget = player;
    }

    public void OnTargetInZone()
    {
        _isPatrolling = false;
    }

    public void OnLostTarget()
    {
        _isPatrolling = true;
    }

    private void HarassPlayer()
    {
        if (_playerTarget != null)
        {
            _direction = _playerTarget.Position.x < transform.position.x ? _left : _right;
            DirectionIsChange?.Invoke(_direction);
            transform.Translate(Vector2.right * _direction * _speed * Time.deltaTime);
        }
    }

    private void Patrol()
    {
        _direction = _points[_indexOfPoint].transform.position.x < transform.position.x ? _left : _right;
        DirectionIsChange?.Invoke(_direction);
        transform.position = Vector2.MoveTowards(transform.position, _points[_indexOfPoint].transform.position, _speed * Time.deltaTime);
    }
}