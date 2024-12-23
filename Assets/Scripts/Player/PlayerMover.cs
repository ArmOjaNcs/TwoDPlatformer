using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerMover : MonoBehaviour, IEnemyTarget
{
    [SerializeField] private InputController _inputController;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _speed;

    private readonly float _colliderDisableTime = 0.3f;

    private Rigidbody2D _rigidbody2D;
    private WaitForSeconds _wait;
    private CapsuleCollider2D _capsuleCollider;
    private float _direction;
    
    public Vector3 Position => transform.position;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
        _wait = new WaitForSeconds(_colliderDisableTime);
    }

    private void OnEnable()
    {
        _inputController.Moving += OnMoving;
        _inputController.Jumping += OnJumping;
        _inputController.FallingThroughPlatform += OnFallingThrough;
    }

    private void OnDisable()
    {
        _inputController.Moving -= OnMoving;
        _inputController.Jumping -= OnJumping;
        _inputController.FallingThroughPlatform -= OnFallingThrough;
    }

    private void Update()
    {
        Move();
    }

    private void OnMoving(float direction)
    {
        _direction = direction;
    }

    private void OnJumping()
    {
        _rigidbody2D.AddRelativeForce(Vector2.up * _jumpForce);
    }

    private void OnFallingThrough()
    {
        StartCoroutine(FallingDown());
    }

    private void Move()
    {
        transform.Translate(Vector2.right * _direction * _speed * Time.deltaTime);
    }

    private IEnumerator FallingDown()
    {
        _capsuleCollider.enabled = false;
        yield return _wait;
        _capsuleCollider.enabled = true;
    }
}