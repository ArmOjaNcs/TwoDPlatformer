using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private float _smoothing;
    [SerializeField] private HorizontalLimiter _rightLimiter;
    [SerializeField] private HorizontalLimiter _leftLimiter;
    [SerializeField] private VerticalLimiter _upLimiter;
    [SerializeField] private VerticalLimiter _downLimiter;

    private readonly float _zOffset = -10;

    private Vector3 _offset;

    private void Awake()
    {
        _offset = new Vector3(0, 0, _zOffset);
    }

    private void OnEnable()
    {
        _rightLimiter.LimitReached += OnHorizontalLimitReached;
        _leftLimiter.LimitReached += OnHorizontalLimitReached;
        _upLimiter.LimitReached += OnVerticalLimitReached;
        _downLimiter.LimitReached += OnVerticalLimitReached;
    }

    private void OnDisable()
    {
        _rightLimiter.LimitReached -= OnHorizontalLimitReached;
        _leftLimiter.LimitReached -= OnHorizontalLimitReached;
        _upLimiter.LimitReached -= OnVerticalLimitReached;
        _downLimiter.LimitReached -= OnVerticalLimitReached;
    }

    private void OnHorizontalLimitReached(float xLimit)
    {
        _offset = new Vector3(xLimit, _offset.y, _offset.z);
    }

    private void OnVerticalLimitReached(float yLimit)
    {
        _offset = new Vector3(_offset.x, yLimit, _offset.z);
    }

    private void LateUpdate()
    {
        Move();
    }

    private void Move()
    {
        if(_targetTransform != null)
        {
            Vector3 nextPosition = Vector3.Lerp(transform.position, _targetTransform.position +
           _offset, Time.deltaTime * _smoothing);

            transform.position = nextPosition;
        }
    }
}