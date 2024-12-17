using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class MedPack : MonoBehaviour
{
    private readonly float _healingPower = 20;

    private AudioSource _audioSource;
    private SpriteRenderer _spriteRenderer;
    private WaitForSeconds _wait;
    private bool _isFirstTouch;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _wait = new WaitForSeconds(_audioSource.clip.length);
        _isFirstTouch = true;
    }

    public float GetHealing()
    {
        return _healingPower;
    }

    public void PickUp()
    {
        StartCoroutine(PickingUp());
    }

    private IEnumerator PickingUp()
    {
        if (_isFirstTouch)
        {
            _isFirstTouch = false;
            _audioSource.Play();
            _spriteRenderer.enabled = false;
            yield return _wait;
            Destroy(gameObject);
        }
    }
}