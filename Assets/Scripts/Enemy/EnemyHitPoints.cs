using UnityEngine;

public class EnemyHitPoints : MonoBehaviour
{
    [SerializeField] private EnemyHitZone _hitZone;

    private readonly int _maxHitPoints = 100;

    public int HitPoints { get; private set; }

    private void Awake()
    {
        HitPoints = _maxHitPoints;
    }

    private void OnEnable()
    {
        _hitZone.DamageDetected += OnDamageDetected;
    }

    private void OnDisable()
    {
        _hitZone.DamageDetected -= OnDamageDetected;
    }

    private void OnDamageDetected(int damage)
    {
        HitPoints -= damage;

        if (HitPoints < 0)
            Destroy(gameObject);
    }
}