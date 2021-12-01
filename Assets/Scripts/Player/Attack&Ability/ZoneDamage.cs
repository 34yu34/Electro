using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class ZoneDamage : Attack
{
    private SphereCollider _collider;

    private List<Character> _targets;

    protected override void Start()
    {
        base.Start();
        _collider = GetComponent<SphereCollider>();
        _collider.isTrigger = true;
    }

    protected override void OnAttackCompleted()
    {
        //do nothing
    }
    
    protected override void OnLaunch()
    {
        foreach (var target in _targets)  
        {
            target.HitComponent.Hit(FinalDamage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<Character>(out var target)) return;
        
        if (target == User) return;
        
        _targets.Add(target);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<Character>(out var target)) return;
        
        _targets.Remove(target);
    }
}
