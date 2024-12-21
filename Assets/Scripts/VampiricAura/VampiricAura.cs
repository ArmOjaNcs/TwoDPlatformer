using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class VampiricAura : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private HitZone _hitZone;
    [SerializeField] private InputController _inputController;

    public readonly float ActiveTime = 6;
    public readonly float ReloadingTime = 4;

    private readonly float _periodicTime = 0.5f;

    private CircleCollider2D _auraCollider;
    private float _minDistance;
    private float _currentActiveTime;
    private float _currentReloadingTime;
    private float _currentPeriodicTime;
    private List<EnemyMover> _enemies;
    private EnemyMover _nearestEnemy;
    private HitZone _enemyHitZone;
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
        _enemies = new List<EnemyMover>();
    }

    private void Update()
    {
        if (_isActive)
        {
            _isCanActivate = false;
            _currentActiveTime += Time.deltaTime;

            if (_currentActiveTime > ActiveTime)
            {
                _isActive = false;
                _currentActiveTime = 0;
                AuraEnabled?.Invoke(_isActive);
            }
        }
        else if (_isActive == false)
        {
            _minDistance = _auraCollider.radius;
            _nearestEnemy = null;
            _enemyHitZone = null;
            _enemies.Clear();
            _currentReloadingTime += Time.deltaTime;

            if (_currentReloadingTime > ReloadingTime)
            {
                _isCanActivate = true;
                _currentReloadingTime = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out EnemyMover enemy) && _isActive)
            _enemies.Add(enemy);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out EnemyMover enemy) && _isActive)
        {
            if (_enemies.Contains(enemy) == false)
                _enemies.Add(enemy);

            foreach (EnemyMover enemyMover in _enemies)
            {
                if (CheckForNearestPosition(enemyMover))
                {
                    _minDistance = transform.position.sqrMagnitude - enemyMover.transform.position.sqrMagnitude;
                    _nearestEnemy = enemyMover;
                }
            }

            _minDistance = _auraCollider.radius;

            if (_nearestEnemy != null)
                _enemyHitZone = _nearestEnemy.transform.GetComponentInChildren<HitZone>();

            if (_enemyHitZone != null)
            {
                _currentPeriodicTime += Time.deltaTime;

                if (_currentPeriodicTime >= _periodicTime)
                {
                    float oldHealthValue = _enemyHitZone.CurrentHealthValue;
                    _enemyHitZone.TakeDamage(_damage);
                    float vampiredHealth = oldHealthValue -_enemyHitZone.CurrentHealthValue;
                    _hitZone.TakeHeal(vampiredHealth);
                    _currentPeriodicTime = 0;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out EnemyMover enemy) && _isActive)
            _enemies.Remove(enemy);
    }

    private void OnActivateAura()
    {
        if (_isCanActivate)
        {
            _isActive = true;
            AuraEnabled?.Invoke(_isActive);
        }
    }

    private bool CheckForNearestPosition(EnemyMover enemy)
    {
        if(enemy != null)
            return (transform.position - enemy.transform.position).sqrMagnitude < _minDistance * _minDistance;

        return false;
    }
}