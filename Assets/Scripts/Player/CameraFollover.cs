using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _smoothing;

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if(_targetTransform != null)
        {
            Vector3 nextPosition = Vector3.Lerp(transform.position, _targetTransform.position +
           _offset, Time.fixedDeltaTime * _smoothing);

            transform.position = nextPosition;
        }
    }

    public void SetOffset(Vector3 offset)
    {
        _offset = offset;
    }
}