using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VampiricSmoothedBar : VampiricSmoothedView
{
    [SerializeField] private Slider _slider;

    private protected override void Start()
    {
        base.Start();

        _slider.value = ActiveSmoothDuration / ActiveSmoothDuration;
    }

    private protected override IEnumerator UpdateView(float startValue, float endValue, float smoothDuration, int sign = 0)
    {
        float elapsedTime = 0;
        
        while (elapsedTime < smoothDuration)
        {
            elapsedTime += Time.deltaTime;
            float normalizedPosition = elapsedTime / smoothDuration;
            _slider.value = Mathf.MoveTowards(startValue, endValue, normalizedPosition);

            yield return null;
        }

        _slider.value = endValue;
    }

    private protected override void OnAuraEnabled(bool isActive)
    {
        base.OnAuraEnabled(isActive);

        if (isActive)
            Coroutine = StartCoroutine(UpdateView(_slider.maxValue, _slider.minValue, ActiveSmoothDuration));
        else
            Coroutine = StartCoroutine(UpdateView(_slider.minValue, _slider.maxValue, ReloadingSmoothDuration));
    }
}