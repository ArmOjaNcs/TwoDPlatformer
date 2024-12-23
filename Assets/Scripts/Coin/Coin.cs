using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(SpriteRenderer))]
public class Coin : MonoBehaviour
{
    private const int Value = 1;

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

    public int GetCollected()
    {
        int coinValue = 0;

        if (_isFirstTouch)
        {
            _isFirstTouch = false;
            StartCoroutine(Collecting());
            coinValue = Value;
        }

        return coinValue;
    }

    private IEnumerator Collecting()
    {
        _audioSource.Play();
        _spriteRenderer.enabled = false;
        yield return _wait;
        Destroy(gameObject);
    }
}