using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPickup : Pickup
{
    [SerializeField] private Attack _attack; 
    
    protected override void SetupPlayerPower(Character player)
    {
        player.AttackComponent.AttachAttack(_attack);
    }
}
