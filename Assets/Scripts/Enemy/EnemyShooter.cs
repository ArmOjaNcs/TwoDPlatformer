using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class EnemyShooter : MonoBehaviour, IDetectionZoneListener
{
    [SerializeField] private EnemyBulletsPool _bulletsPool;
    [SerializeField] private DetectionZone _detectionZone;
    [SerializeField] private float _delay;
    [SerializeField] private float _bulletSpeed;

    private Vector2 _direction;
    private AudioSource _audioSource;
    private IEnemyTarget _playerTarget;
    private bool _isShooting;
    private float _currentTime;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
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

    private void Update()
    {
        _currentTime += Time.deltaTime;

        if( _isShooting && _currentTime > _delay)
        {
            if (_playerTarget != null)
            {
                _direction = (_playerTarget.Position - transform.position).normalized;
                EnemyBullet enemyBullet = _bulletsPool.GetBullet();
                enemyBullet.SetStartPosition(transform);
                enemyBullet.SetSpeedAndDirection(_bulletSpeed, _direction);
                _audioSource.Play();
                _currentTime = 0;
            }
        }
    }

    public void OnPlayerFounded(IEnemyTarget player)
    {
        _playerTarget = player;
    }

    public void OnTargetInZone()
    {
        _isShooting = true;
    }

    public void OnLostTarget()
    {
        _isShooting = false;
    }
}