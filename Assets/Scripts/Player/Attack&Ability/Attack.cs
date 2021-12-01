using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : Useable
{
    [SerializeField]
    private float _cooldown;

    protected float AttackSpeed => 1 / _cooldown;

    [SerializeField]
    private float _damageMultiplier;

    [SerializeField]
    private float _energyCost;

    public float EnergyCost => _energyCost;

    private bool _canAttack;
    public bool CanAttack => _canAttack;
    public float FinalDamage =>  User.AttackComponent.Damage * _damageMultiplier;

    protected virtual void Start()
    {
        _canAttack = true;
    }

    public override void Use()
    {
        if (!_canAttack || !User.EnergyComponent.TryUseEnergy(_energyCost))
        {
            return;
        }

        OnLaunch();

        StartCoroutine(nameof(CalculateCooldown));
    }

    private IEnumerator CalculateCooldown()
    {
        _canAttack = false;
        yield return new WaitForSeconds(_cooldown);
        _canAttack = true;
        OnAttackCompleted();
    }

    protected abstract void OnAttackCompleted();


    protected abstract void OnLaunch();
}
