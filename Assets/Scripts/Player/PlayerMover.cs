using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private PlayerHitZone _hitZone;
    [SerializeField] private InputController _inputController;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _speed;

    private readonly int _maxHitPoints = 100;
    private readonly float _colliderDisableTime = 0.4f;

    private Rigidbody2D _rigidbody2D;
    private WaitForSeconds _wait;
    private CapsuleCollider2D _capsuleCollider;
    private float _direction;
    private int _hitPoints;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
        _hitPoints = _maxHitPoints;
        _wait = new WaitForSeconds(_colliderDisableTime);
    }

    private void OnEnable()
    {
        _hitZone.DamageDetected += OnDamageDetected;
        _inputController.Moving += OnMoving;
        _inputController.Jumping += OnJumping;
        _inputController.FallingThroughPlatform += OnFallingThrough;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnDisable()
    {
        _hitZone.DamageDetected -= OnDamageDetected;
        _inputController.Moving -= OnMoving;
        _inputController.Jumping -= OnJumping;
        _inputController.FallingThroughPlatform -= OnFallingThrough;
    }

    private void OnMoving(float direction)
    {
        _direction = direction;
    }

    private void OnJumping()
    {
        _rigidbody2D.AddRelativeForce(Vector2.up * _jumpForce);
    }

    private void OnDamageDetected(int damage)
    {
        _hitPoints -= damage;

        if (_hitPoints < 0)
            Destroy(gameObject);
    }

    private void OnFallingThrough()
    {
        StartCoroutine(FallingDown());
    }

    private void Move()
    {
        transform.Translate(Vector2.right * _direction * _speed * Time.fixedDeltaTime);
    }

    private IEnumerator FallingDown()
    {
        _capsuleCollider.enabled = false;
        yield return _wait;
        _capsuleCollider.enabled = true;
    }
}