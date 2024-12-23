using System.Collections;
using UnityEngine;

public abstract class VampiricSmoothedView : MonoBehaviour
{
    [SerializeField] private protected VampiricAura VampiricAura;

    private protected Coroutine Coroutine;

    private protected float ActiveSmoothDuration;
    private protected float ReloadingSmoothDuration;

    private void OnEnable()
    {
        VampiricAura.AuraEnabled += OnAuraEnabled;
    }

    private void OnDisable()
    {
        VampiricAura.AuraEnabled -= OnAuraEnabled;
    }

    private protected virtual void Start()
    {
        ActiveSmoothDuration = VampiricAura.ActiveTime;
        ReloadingSmoothDuration = VampiricAura.ReloadingTime;
    }

    private protected abstract IEnumerator UpdateView(float startValue, float endValue, float smoothDuration, int sign = 0);

    private protected virtual void OnAuraEnabled(bool isActive)
    {
        if (Coroutine != null)
            StopCoroutine(Coroutine);
    }
}
