using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private DetectionZone _zone;
    [SerializeField] private EnemyBullet _enemyBulletPrefab;
    [SerializeField] private float _delay;
    [SerializeField] private float _bulletLifeTime;

    private Vector2 _direction;
    private WaitForSeconds _lifeTime;
    private AudioSource _audioSource;
    private bool _isShooting;
    private float _currentTime;

    private void Awake()
    {
        _lifeTime = new WaitForSeconds(_bulletLifeTime);
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _zone.PlayerInZone += OnPlayerInZone;
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;

        if( _isShooting && _currentTime > _delay)
        {
            EnemyBullet enemyBullet = Instantiate(_enemyBulletPrefab, transform.position + (Vector3)_direction, Quaternion.identity);
            enemyBullet.Init(_lifeTime, _direction);
            _audioSource.Play();
            _currentTime = 0;
        }
    }

    private void OnDisable()
    {
        _zone.PlayerInZone -= OnPlayerInZone;
    }

    private void OnPlayerInZone(bool isShooting, Vector3 targetPosition)
    {
        _isShooting = isShooting;
        _direction = (targetPosition - transform.position).normalized;
    }
}