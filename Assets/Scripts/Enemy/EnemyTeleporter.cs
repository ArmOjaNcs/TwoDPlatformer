using System.Collections;
using UnityEngine;

public class EnemyTeleporter : MonoBehaviour
{
    [SerializeField] private DetectionZone _detectionZone;
    [SerializeField] private PatrolZoneChecker _patrolZoneChecker;

    private readonly float _delay = 1.5f;

    private WaitForSeconds _wait;
    private Coroutine _coroutine;
    private bool _isInZone;

    private void Awake()
    {
        _wait = new WaitForSeconds(_delay);
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

    private void OnFoundTarget(Vector3 target)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }

    private void OnLostTarget()
    {
        if (_isInZone == false && gameObject != null && _coroutine == null)
            _coroutine = StartCoroutine(ReturnToPatrolZone());
    }

    private void OnEnemyInZone(bool status)
    {
        _isInZone = status;

        if (_isInZone)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
        }
    }

    private IEnumerator ReturnToPatrolZone()
    {
        yield return _wait;

        transform.position = _patrolZoneChecker.PatrolZone;
    }
}