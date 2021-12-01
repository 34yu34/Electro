using UnityEngine;
using System;

[Serializable]
public class AttributeModification
{
    [SerializeField]
    private float _multiplier;

    public float calculateModif(float baseValue)
    {
        return _multiplier * baseValue;
    }
}
