using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);

    [SerializeField] private float _jumpForce;
    [SerializeField] private float _speed;
    [SerializeField] private float _checkDistance;
    [SerializeField] private Transform _surfaceChecker;
    [SerializeField] private PlayerHitZone _hitZone;

    private readonly int _maxHP = 100;

    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private RaycastHit2D _raycastHit2D;
    private float _direction;
    private int _hitpoints;

    public event Action Jumping;
    public event Action<bool> Ducking;
    public event Action<bool> Shooting;
    public event Action<bool> Moving;
    public event Action Hurt;
    public event Action<bool, bool, bool> PerformingShot;

    private bool IsJump => Input.GetKeyDown(KeyCode.Space);
    private bool IsDuck => Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
    private bool IsShot => Input.GetKey(KeyCode.E);

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _hitpoints = _maxHP;
    }

    private void OnEnable()
    {
        _hitZone.DamageDetected += OnDamageDetected;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        _direction = Input.GetAxisRaw(Horizontal);

        if (IsJump && CanJump())
        {
            _rigidbody2D.AddForce(Vector2.up * _jumpForce);
            Jumping?.Invoke();
        }

        if (IsDuck && _direction == 0)
            Ducking?.Invoke(true);
        else 
            Ducking?.Invoke(false);

        if (_direction < 0)
            _spriteRenderer.flipX = true;
        else if (_direction > 0)
            _spriteRenderer.flipX = false;

        if (_direction != 0 && CanJump())
            Moving?.Invoke(true);
        else if(_direction == 0)
            Moving?.Invoke(false);

        if (IsShot)
            Shooting?.Invoke(true);
        else
            Shooting?.Invoke(false);

        if ((IsShot && CanJump()) || (IsShot && IsDuck && CanJump()))
            PerformingShot?.Invoke(true, _spriteRenderer.flipX, IsDuck);
        else
            PerformingShot?.Invoke(false, _spriteRenderer.flipX, IsDuck);
    }

    private void OnDisable()
    {
        _hitZone.DamageDetected -= OnDamageDetected;
    }

    private void OnDamageDetected(int damage)
    {
        _hitpoints -= damage;
        Hurt?.Invoke();

        if (_hitpoints < 0)
            Destroy(gameObject); 
    }

    private bool CanJump()
    {
        _raycastHit2D = Physics2D.Raycast(_surfaceChecker.position, Vector2.down, _checkDistance);

        if (_raycastHit2D.collider == null)
            return false;

        if (_raycastHit2D.collider.TryGetComponent(out Surface _))
            return true;

        return false;
    }

    private void Move()
    {
        transform.Translate(Vector2.right * _direction * _speed * Time.fixedDeltaTime);
    }
}