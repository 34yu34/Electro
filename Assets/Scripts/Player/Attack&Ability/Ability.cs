using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class Ability : Useable
{
    [SerializeField] protected float _duration;
    [SerializeField] protected float _energyCost;

    protected bool IsInUse { get; private set; }
    private IEnumerator UseAbility()
    {
        if (!User.EnergyComponent.TryUseEnergy(_energyCost)) yield break;
        
        IsInUse = true;
        OnCharacterUse();
        yield return new WaitForSeconds(_duration);
        IsInUse = false;
        OnAbilityDone();
    }

    public override void Use()
    {
        StartCoroutine(nameof(UseAbility));
    }

    protected abstract void OnCharacterUse();

    protected abstract void OnAbilityDone();
}
