using System.Collections;
using TMPro;
using UnityEngine;

public class VampiricAuraSmoothedText : VampiricSmoothedView
{
    [SerializeField] private TextMeshProUGUI _text;

    private readonly string _readyToStart = "Vampiric aura is ready";
    private readonly int _minusSign = -1;
    private readonly int _plusSign = 1;

    private protected override void Start()
    {
        base.Start();

        _text.text = _readyToStart;
    }

    private protected override IEnumerator UpdateView(float startValue, float endValue, float smoothDuration, int sign = 0)
    {
        float elapsedTime = 0;

        while (elapsedTime < smoothDuration)
        {
            elapsedTime += Time.deltaTime;
            float currentValue = startValue + elapsedTime * sign;
            _text.text = currentValue.ToString("#.##") + "/" + smoothDuration;

            yield return null;
        }

        _text.text = _readyToStart;
    }

    private protected override void OnAuraEnabled(bool isActive)
    {
        base.OnAuraEnabled(isActive);

        if (isActive)
            Coroutine = StartCoroutine(UpdateView(ActiveSmoothDuration, 0, ActiveSmoothDuration, _minusSign));
        else
            Coroutine = StartCoroutine(UpdateView(0, ReloadingSmoothDuration, ReloadingSmoothDuration, _plusSign));
    }
}