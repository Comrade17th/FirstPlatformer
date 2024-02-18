using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthViewSmooth : HealthView
{
    [SerializeField] private Slider _topSlider;
    [SerializeField] private Slider _downSlider;

    [SerializeField] private float _smoothDelay = 0.2f;
    [SerializeField] private float _smoothLerpSpeed = 100f;

    private WaitForSeconds _waitStep;
    private Coroutine _coroutine;

    private void Awake()
    {
        _waitStep = new WaitForSeconds(_smoothDelay);
    }

    private IEnumerator SmoothChanging(float targetValue, Slider slider)
    {
        while (slider.value != targetValue)
        {
            slider.value = Mathf.Lerp(
                slider.value,
                targetValue,
                _smoothLerpSpeed * Time.deltaTime); 
            yield return _waitStep;
        }
    }
    
    private void ChangeSlidersValues(float targetValue, Slider hardSlider, Slider smoothSlider)
    {
        hardSlider.value = targetValue;
        
        if (_coroutine != null)
            StopCoroutine(_coroutine);
            
        _coroutine = StartCoroutine(SmoothChanging(targetValue, smoothSlider)); 
    }
    
    public override void UpdateHealth(float targetValue)
    {
        targetValue /= MaxHealth;
        
        if (targetValue >= _topSlider.value)
            ChangeSlidersValues(targetValue, _downSlider, _topSlider);
        else
            ChangeSlidersValues(targetValue, _topSlider, _downSlider);
    }
}
