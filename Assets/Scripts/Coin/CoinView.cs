using TMPro;
using UnityEngine;

public class CoinView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private CoinCollector _coinCollector;

    private void OnEnable()
    {
        _coinCollector.CoinsValueChanged += OnCoinsValueChanged;
    }

    private void OnDisable()
    {
        _coinCollector.CoinsValueChanged -= OnCoinsValueChanged;
    }

    private void OnCoinsValueChanged(int newValue)
    {
        _text.text = "X " + newValue.ToString();
    }
}