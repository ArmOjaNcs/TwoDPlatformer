using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerRenderer : MonoBehaviour
{
    [SerializeField] private InputController _inputController;

    private SpriteRenderer _spriteRenderer;

    public bool IsRightDirection => _spriteRenderer.flipX;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        _inputController.Moving += OnMoving;
    }

    private void OnDisable()
    {
        _inputController.Moving += OnMoving;
    }

    private void OnMoving(float direction)
    {
        if(direction != 0) 
            _spriteRenderer.flipX = direction < 0;
    }
}