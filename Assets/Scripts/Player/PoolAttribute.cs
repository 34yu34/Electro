using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PoolAttribute
{
    [SerializeField] 
    private Attribute _maxValue;

    private float _currentValue;

    public float CurrentValue
    {
        get => _currentValue;
        set => _currentValue = Mathf.Clamp(value, 0f, _maxValue.CalculatedValue);
    }

    public void Fill()
    {
        CurrentValue = _maxValue.CalculatedValue;
    }

    public float FilledPercentage => CurrentValue / _maxValue.CalculatedValue;

    public bool IsEmpty => CurrentValue == 0;

    public static explicit operator float (PoolAttribute attribute)
    {
        return attribute.CurrentValue;
    }
}
