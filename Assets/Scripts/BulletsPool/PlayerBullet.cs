using UnityEngine;

public class PlayerBullet : Bullet
{
    private Rigidbody2D _rigidbody2D;
    
    public void InitializeRigidbody()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void AddDirectionalForce(float force, int targetDirection)
    {
        _rigidbody2D.linearVelocity = Vector2.right * force * targetDirection;
    }
}