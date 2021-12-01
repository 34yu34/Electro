using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunComponent : Component
{
    public bool IsStunned { get; private set;}

    public void Stun()
    {
        IsStunned = true;
        Character.MovementComponent.SetMovement2D(Vector2.zero);
    }

    public void UnStun()
    {
        IsStunned = false;
    }

}
