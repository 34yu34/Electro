using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Serialization;

[Serializable]
public class Dash : Ability
{
    [FormerlySerializedAs("DashSpeedMultiplier")] [SerializeField] private AttributeModification _dashSpeedMultiplier;

    protected override void OnCharacterUse()
    {
        User.MovementComponent.Speed.AddModification(_dashSpeedMultiplier);
        User.StunComponent.Stun();
        User.MovementComponent.GoForward();
        User.AnimationComponent.StartDash(1 / _duration);
    }

    protected override void OnAbilityDone()
    {
        User.MovementComponent.Speed.RemoveModification(_dashSpeedMultiplier);
        User.StunComponent.UnStun();
        User.AnimationComponent.StopDash();
    }
}
