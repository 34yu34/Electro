using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPickup : Pickup
{
    [SerializeField] private Ability _ability; 
    
    protected override void SetupPlayerPower(Character player)
    {
        player.AbilityComponent.AttachAbility(_ability);
    }
}
