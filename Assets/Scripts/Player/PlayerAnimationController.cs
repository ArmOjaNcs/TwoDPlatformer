using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : MonoBehaviour
{
    private const string IsMoving = nameof(IsMoving);
    private const string IsDucking = nameof(IsDucking);
    private const string Jump = nameof(Jump);
    private const string Grounded = nameof(Grounded);
    private const string Hurt = nameof(Hurt);
    private const string IsShooting = nameof(IsShooting);

    [SerializeField] private PlayerController _player;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _player.Moving += OnMoving;
        _player.Ducking += OnDucking;
        _player.Jumping += OnJumping;
        _player.Shooting += OnShooting;
        _player.Hurt += OnHurt;
    }

    private void OnDisable()
    {
        _player.Moving -= OnMoving;
        _player.Ducking -= OnDucking;
        _player.Jumping -= OnJumping;
        _player.Shooting -= OnShooting;
        _player.Hurt -= OnHurt;
    }

    private void OnHurt()
    {
        _animator.SetTrigger(Hurt);        
    }

    private void OnMoving(bool status)
    {
        _animator.SetBool(IsMoving, status);
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