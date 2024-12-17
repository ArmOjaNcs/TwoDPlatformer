using TMPro;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    
    private int _coinsValue;

    private void Awake()
    {
        _coinsValue = 0;
        PrintText();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Coin coin))
        {
            _coinsValue += coin.GetCollected();
            _text.text = "X " + _coinsValue.ToString();
        }
    }

    private void PrintText()
    {
        _text.text = "X " + _coinsValue.ToString();
    }
}