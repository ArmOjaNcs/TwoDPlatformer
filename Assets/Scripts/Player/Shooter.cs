using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class Shooter : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform[] _shotPoints;
    [SerializeField] private float _delay;
    [SerializeField] private float _force;
    [SerializeField] private float _bulletLifeTime;

    private readonly int _firstPoint = 0;
    private readonly int _secondPoint = 1;
    private readonly int _thirdPoint = 2;
    private readonly int _fourthPoint = 3;
    private readonly int _left = -1;
    private readonly int _right = 1;

    private Transform _currentShotPoint;
    private WaitForSeconds _lifeTime;
    private AudioSource _audioSource;
    private bool _isShooting;
    private int _direction;
    private float _currentTime;

    private void Awake()
    {
        _lifeTime = new WaitForSeconds(_bulletLifeTime);
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _playerController.PerformingShot += OnPerformingShot;
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;

        if(_isShooting && _currentTime > _delay)
        {
            Bullet bullet = Instantiate(_bulletPrefab, _currentShotPoint.position, Quaternion.identity);
            bullet.Init(_lifeTime);
            bullet.AddDirectionalForce(_force, _direction);
            _audioSource.Play();
            _currentTime = 0;
        }
    }

    private void OnDisable()
    {
        _playerController.PerformingShot -= OnPerformingShot;
    }

    private void OnPerformingShot(bool status, bool direction, bool isDuck)
    {
        _isShooting = status;
        _direction = direction ? _left : _right;

        if (_direction > 0)
            _currentShotPoint = isDuck ? _shotPoints[_thirdPoint] : _shotPoints[_firstPoint];
        else if (_direction < 0)
            _currentShotPoint = isDuck ? _shotPoints[_fourthPoint] : _shotPoints[_secondPoint];
    }
}