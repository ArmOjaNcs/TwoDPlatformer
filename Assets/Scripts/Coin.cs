using System.Collections;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
[RequireComponent (typeof(SpriteRenderer))]
public class Coin : MonoBehaviour
{
    private AudioSource _audioSource;
    private SpriteRenderer _spriteRenderer;
    private bool _isFirstTouch;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _isFirstTouch = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out PlayerController _) && _isFirstTouch)
        {
            _isFirstTouch = false;
            _audioSource.Play();
            _spriteRenderer.enabled = false;
            StartCoroutine(Collecting());
        }
    }

    private IEnumerator Collecting()
    {
        yield return new WaitForSeconds(_audioSource.clip.length);
        Destroy(gameObject);
    }
}
