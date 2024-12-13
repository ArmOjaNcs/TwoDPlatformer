using UnityEngine;

public class PlayerHitPoints : MonoBehaviour
{
    [SerializeField] private PlayerHitZone _hitZone;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out MedPack medPack) && HitPoints < _maxHitPoints)
        {
            HitPoints += medPack.GetHealing();

            if(HitPoints > _maxHitPoints)
                HitPoints = _maxHitPoints;

            medPack.PickUp();
        }
    }

    private void OnDamageDetected(int damage)
    {
        HitPoints -= damage;

        if (HitPoints < 0)
            Destroy(gameObject);
    }
}