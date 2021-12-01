using UnityEngine;

public class HealthPickup : Pickup
{
    [SerializeField] private float _healthValue;
    
    protected override void SetupPlayerPower(Character player)
    {
        player.HitComponent.Heal(_healthValue);
    }
}
