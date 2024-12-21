using System;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    private int _coinsValue;

    public event Action<int> CoinsValueChanged;

    private void Start()
    {
        _coinsValue = 0;
        CoinsValueChanged?.Invoke(_coinsValue);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Coin coin))
        {
            _coinsValue += coin.GetCollected();
            CoinsValueChanged?.Invoke(_coinsValue);
        }
    }
}