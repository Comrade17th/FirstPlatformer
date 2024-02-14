using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private HealthController _healthController;
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Slider _easeHealthSlider;

    [SerializeField] private bool _isEase = true;
    [SerializeField] private bool _isDebug = false;

    private float _lerpSpeed = 3f;
    
    private void Awake()
    {
        _healthSlider.maxValue = _healthController.MaxHealth;
        _healthSlider.value = _healthController.Health;
        
        _easeHealthSlider.maxValue = _healthController.MaxHealth;
        _easeHealthSlider.value = _healthController.Health;
    }

    private void OnEnable()
    {
        _healthController.HealthChanged += ChangeHealth;
    }
    
    private void OnDisable()
    {
        _healthController.HealthChanged -= ChangeHealth;
    }

    private void Update()
    {
        if (_healthController.Health != _easeHealthSlider.value &&
            _isEase)
        {
            _easeHealthSlider.value = Mathf.Lerp(
                _easeHealthSlider.value,
                _healthController.Health,
                _lerpSpeed * Time.deltaTime);
        }
    }

    private void ChangeHealth(float delta)
    {
        _healthSlider.value = _healthController.Health;

        if (_isEase == false)
            _easeHealthSlider.value = _healthController.Health;
        
        if(_isDebug)
            Debug.Log($"{_healthController.Health}/{_healthController.MaxHealth}");
    }
    
}
