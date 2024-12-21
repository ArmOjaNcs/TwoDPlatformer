using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private EnemyBulletsPool _bulletsPool;
    [SerializeField] private DetectionZone _detectionZone;
    [SerializeField] private float _delay;
    [SerializeField] private float _bulletSpeed;

    private Vector2 _direction;
    private AudioSource _audioSource;
    private bool _isShooting;
    private float _currentTime;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
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

    private void Update()
    {
        _currentTime += Time.deltaTime;

        if( _isShooting && _currentTime > _delay)
        {
            EnemyBullet enemyBullet = _bulletsPool.GetBullet();
            enemyBullet.SetStartPosition(transform);
            enemyBullet.SetSpeedAndDirection(_bulletSpeed, _direction);
            _audioSource.Play();
            _currentTime = 0;
        }
    }

    private void OnFoundTarget(Vector3 target)
    {
        _isShooting = true;

        if(target != null)
            _direction = (target - transform.position).normalized;
    }

    private void OnLostTarget()
    {
        _isShooting = false;
    }
}