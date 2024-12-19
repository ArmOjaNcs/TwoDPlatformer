using UnityEngine;

[RequireComponent (typeof(BoxCollider2D))]
public class PatrolPoint : MonoBehaviour
{
    private bool _isFirstContact;

    private void Awake()
    {
        _isFirstContact = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isFirstContact)
        {
            if(collision.TryGetComponent(out Enemy enemy))
            {
                enemy.ChangePatrolPoint();
                _isFirstContact = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy _))
            _isFirstContact = true;
    }
}