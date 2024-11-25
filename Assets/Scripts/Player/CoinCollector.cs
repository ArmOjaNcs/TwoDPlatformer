using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    private int _coinsValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Coin coin))
        {
            coin.GetCollected();
            _coinsValue++;
        }
    }
}
