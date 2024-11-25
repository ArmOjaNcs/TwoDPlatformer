using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private const string IsMoving = nameof(IsMoving);
    private const string IsDucking = nameof(IsDucking);
    private const string Jump = nameof(Jump);
    private const string Grounded = nameof(Grounded);
    private const string Hurt = nameof(Hurt);
    private const string IsShooting = nameof(IsShooting);

    [SerializeField] private InputController _inputController;
    [SerializeField] private PlayerHitZone _playerHitZone;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _inputController.Moving += OnMoving;
        _inputController.Ducking += OnDucking;
        _inputController.Jumping += OnJumping;
        _inputController.Shooting += OnShooting;
        _playerHitZone.DamageDetected += OnHurt;
    }

    private void OnDisable()
    {
        _inputController.Moving -= OnMoving;
        _inputController.Ducking -= OnDucking;
        _inputController.Jumping -= OnJumping;
        _inputController.Shooting -= OnShooting;
        _playerHitZone.DamageDetected -= OnHurt;
    }

    private void OnHurt(int damage = 0)
    {
        _animator.SetTrigger(Hurt);        
    }

    private void OnMoving(float direction)
    {
        bool isMoving = direction != 0 ? true : false; 
        _animator.SetBool(IsMoving, isMoving);
    }

    private void OnDucking(bool status)
    {
        _animator.SetBool(IsDucking, status);
    }

    private void OnJumping()
    {
        _animator.SetTrigger(Jump);
    }

    private void OnShooting(bool status)
    {
        _animator.SetBool(IsShooting, status);
    }
}