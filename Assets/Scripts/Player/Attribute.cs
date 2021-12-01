using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class Attribute 
{
    private List<AttributeModification> _modifications;
    private bool _isDirty;

    [SerializeField]
    private float _baseValue;

    [SerializeField]
    private float _calculatedValue;

    public float BaseValue
    {
        get => _baseValue;
        set
        {
            _isDirty = true;
            _baseValue = value;
        }
    }

    public float CalculatedValue
    {
        get
        {
            if (_isDirty)
            {
                calculateValue();
            }

            return _calculatedValue;
        }
    }

    public void Reset()
    {
        _calculatedValue = _baseValue;
    }

    public void AddModification(AttributeModification modification)
    {
        _modifications ??= new List<AttributeModification>();

        _modifications.Add(modification);

        _isDirty = true;
    }

    public void RemoveModification(AttributeModification modification)
    {
        _modifications ??= new List<AttributeModification>();

        _modifications.Remove(modification);

        _isDirty = true;
    }

    private void calculateValue()
    {
        _calculatedValue = _baseValue;
        foreach (var modif in _modifications)
        {
            _calculatedValue += modif.calculateModif(_baseValue);
        }
        _isDirty = false;
    }

    public static implicit operator float(Attribute attrib)
    {
        return attrib.CalculatedValue;
    }
}
