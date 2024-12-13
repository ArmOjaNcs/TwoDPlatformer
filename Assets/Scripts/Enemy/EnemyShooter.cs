using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private EnemyBullet _enemyBulletPrefab;
    [SerializeField] private Enemy _enemy;
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
        _enemy.PlayerInZone += OnPlayerInZone;
    }

    private void OnDisable()
    {
        _enemy.PlayerInZone -= OnPlayerInZone;
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;

        if( _isShooting && _currentTime > _delay)
        {
            EnemyBullet enemyBullet = Instantiate(_enemyBulletPrefab, transform.position, Quaternion.identity);
            enemyBullet.Init(_lifeTime, _direction);
            _audioSource.Play();
            _currentTime = 0;
        }
    }

    private void OnPlayerInZone(bool isShooting, IEnemyTarget target)
    {
        _isShooting = isShooting;

        if(target != null)
            _direction = (target.Position - transform.position).normalized;
    }
}