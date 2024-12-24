using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class VampiricAura : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private Health _health;
    [SerializeField] private InputController _inputController;

    public readonly float ActiveTime = 6;
    public readonly float ReloadingTime = 4;

    private readonly float _periodicTime = 0.3f;

    private CircleCollider2D _auraCollider;
    private float _minDistance;
    private float _currentActiveTime;
    private float _currentReloadingTime;
    private float _currentPeriodicTime;
    private List<IPlayerTarget> _enemies;
    private IPlayerTarget _nearestEnemy;
    private bool _isActive;
    private bool _isCanActivate;

    public event Action<bool> AuraEnabled;

    private void Awake()
    {
        _auraCollider = GetComponent<CircleCollider2D>();
    }

    private void OnEnable()
    {
        _inputController.ActivateVampiricAura += OnActivateAura;
    }

    private void OnDisable()
    {
        _inputController.ActivateVampiricAura -= OnActivateAura;
    }

    private void Start()
    {
        _isCanActivate = true;
        _minDistance = _auraCollider.radius;
        _auraCollider.enabled = false;
        _enemies = new List<IPlayerTarget>();
    }

    private void Update()
    {
        StartActiveTime(_isActive);

        StartRealodingTime(_isActive);

        ApplyPeriodicDamage();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IPlayerTarget enemy))
            _enemies.Add(enemy);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IPlayerTarget enemy))
            _enemies.Remove(enemy);
    }

    private void OnActivateAura()
    {
        if (_isCanActivate)
        {
            _isActive = true;
            _auraCollider.enabled = true; 
            AuraEnabled?.Invoke(_isActive);
        }
    }

    private bool IsEnemyNearestPosition(IPlayerTarget enemy)
    {
        if (enemy != null)
            return (transform.position - enemy.Position).sqrMagnitude < _minDistance * _minDistance;

        return false;
    }

    private void StartActiveTime(bool isActive)
    {
        if (isActive)
        {
            _currentPeriodicTime += Time.deltaTime;
            _isCanActivate = false;
            _currentActiveTime += Time.deltaTime;

            if (_currentActiveTime > ActiveTime)
            {
                _isActive = false;
                _currentActiveTime = 0;
                _currentPeriodicTime = 0;
                AuraEnabled?.Invoke(_isActive);
            }
        }
    }

    private void StartRealodingTime(bool isActive)
    {
        if (isActive == false)
        {
            _minDistance = _auraCollider.radius;
            _auraCollider.enabled = false;
            _nearestEnemy = null;
            _enemies.Clear();
            _currentReloadingTime += Time.deltaTime;

            if (_currentReloadingTime > ReloadingTime)
            {
                _isCanActivate = true;
                _currentReloadingTime = 0;
            }
        }
    }

    private void ApplyPeriodicDamage()
    {
        if (_isActive && _currentPeriodicTime >= _periodicTime && _enemies != null)
        {
            foreach (IPlayerTarget enemy in _enemies)
            {
                if (IsEnemyNearestPosition(enemy))
                {
                    _minDistance = transform.position.sqrMagnitude - enemy.Position.sqrMagnitude;
                    _nearestEnemy = enemy;
                }
            }

            if (_nearestEnemy != null && _nearestEnemy.Health != null)
            {
                float oldHealthValue = _nearestEnemy.Health.CurrentValue;
                _nearestEnemy.Health.OnDamageDetected(_damage);
                float vampiredHealth = oldHealthValue - _nearestEnemy.Health.CurrentValue;
                _health.TakeHeal(vampiredHealth);
            }

            _currentPeriodicTime = 0;
            _minDistance = _auraCollider.radius;
            _nearestEnemy = null;
        }
    }
}