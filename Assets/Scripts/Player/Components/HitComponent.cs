using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class HitComponent : Component
{
    [SerializeField]
    private PoolAttribute _health;

    private TargetType CharacterType => _type;
    [SerializeField] private TargetType _type;

    [FormerlySerializedAs("bodyColorRenderer")] [SerializeField]
    private Renderer _bodyColorRenderer;

    [FormerlySerializedAs("BodyColor")] [SerializeField]
    private Gradient _bodyColor;

    private bool _isImmune;
    
    public delegate void OnDeath();
    public event OnDeath OnDeathEvent;

    private void Start()
    {
        _health.Fill();
        _isImmune = false;
    }

    private void Update()
    {
        if (_bodyColorRenderer == null)
        {
            return;
        }
        var resultingColor = _bodyColor.Evaluate(1 - _health.FilledPercentage);
        _bodyColorRenderer.material.color =  resultingColor;

        Character.EnergyComponent.ChangeEnergyColor(resultingColor);

    }

    public void Immunize()
    {
        _isImmune = true;
    }

    public void Unimmunize()
    {
        _isImmune = false;
    }


    public void Hit(float damage)
    {
        if (_isImmune) return;
        
        _health.CurrentValue -= damage;

        CheckDeath();
    }

    public void Heal(float healValue)
    {
        _health.CurrentValue += healValue;
    }

    private void CheckDeath()
    {
        if (!IsDead) return;

        OnDeathEvent?.Invoke();
        
        Destroy(gameObject);
    }

    public bool IsFullHealth()
    {
        return Math.Abs(_health.FilledPercentage - 1.0f) < 0.01f;
    }

    private bool IsDead => _health.IsEmpty;

    public bool CanBeHitBy(Character character)
    {
        return (character.AttackComponent.TargetsType & CharacterType) != 0;
    }
}
