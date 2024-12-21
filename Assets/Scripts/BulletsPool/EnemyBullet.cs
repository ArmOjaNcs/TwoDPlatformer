using UnityEngine;

public class EnemyBullet : Bullet
{
    private float _speed;
    private Vector2 _direction;

    private void Update()
    {
        Move();
    }
    
    public void SetSpeedAndDirection(float speed, Vector2 direction)
    {
        _speed = speed;
        _direction = direction;
    }

    private void Move()
    {
        transform.Translate(_direction.normalized * _speed * Time.deltaTime);
    }
}