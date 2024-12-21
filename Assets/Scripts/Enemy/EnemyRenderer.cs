using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EnemyRenderer : MonoBehaviour
{
    [SerializeField] private EnemyMover _enemyMover;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        _enemyMover.DirectionIsChange += OnDirectionChange;
    }

    private void OnDisable()
    {
        _enemyMover.DirectionIsChange -= OnDirectionChange;
    }

    private void OnDirectionChange(int direction)
    {
        _spriteRenderer.flipX = direction < 0 ? false : true;
    }
}