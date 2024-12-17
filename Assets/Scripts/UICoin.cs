using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UICoin : MonoBehaviour
{
    [SerializeField] private Image _coin;

    private Tween _rotateCoin;
    
    private void Awake()
    {
        _rotateCoin = _coin.transform.DORotate(new Vector3(0, 0, 180), 1.5f, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
    }

    private void Start()
    {
        _rotateCoin.Play();
    }
}