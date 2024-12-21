using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);

    [SerializeField] private SurfaceChecker _surfaceChecker;
    [SerializeField] private PlayerRenderer _playerRenderer;

    private float _direction;
    private bool _isCanJump;
    private bool _isCanFallThrough;
    private bool _isDuck;

    public event Action Jumping;
    public event Action<bool> Ducking;
    public event Action<bool> Shooting;
    public event Action<float> Moving;
    public event Action FallingThroughPlatform;
    public event Action<ShotData> PerformingShot;
    public event Action ActivateVampiricAura;

    private bool IsJump => Input.GetKeyDown(KeyCode.Space);
    private bool IsDuck => Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
    private bool IsShot => Input.GetKey(KeyCode.E);
    private bool IsRightDirection => _playerRenderer.IsRightDirection;
    private bool IsActivateVampiricAura => Input.GetKeyDown(KeyCode.Q);

    private void OnEnable()
    {
        _surfaceChecker.CanJump += OnCanJump;
        _surfaceChecker.CanFallThrough += OnCanFallThrough;
    }

    private void Update()
    {
        _direction = Input.GetAxisRaw(Horizontal);

        Moving?.Invoke(_direction);

        if (IsJump && _isCanJump && IsDuck == false)
            Jumping?.Invoke();

        if (_direction != 0)
            _isDuck = false;
        else
            _isDuck = IsDuck;

        Ducking?.Invoke(IsDuck && _direction == 0);

        if(_isCanFallThrough && IsDuck && IsJump && _isCanJump)
            FallingThroughPlatform?.Invoke();

        Shooting?.Invoke(IsShot);

        if (IsShot)
            PerformingShot?.Invoke(new ShotData(true, IsRightDirection, _isDuck));
        else
            PerformingShot?.Invoke(new ShotData(false, IsRightDirection, _isDuck));

        if(IsActivateVampiricAura)
            ActivateVampiricAura?.Invoke();
    }

    private void OnDisable()
    {
        _surfaceChecker.CanJump -= OnCanJump;
        _surfaceChecker.CanFallThrough -= OnCanFallThrough;
    }

    private void OnCanJump(bool isCanJump)
    {
        _isCanJump = isCanJump;
    }

    private void OnCanFallThrough(bool isCanFallThrough)
    {
        _isCanFallThrough = isCanFallThrough;
    }
}