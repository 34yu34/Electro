using UnityEngine;

public class BallAttack : Attack
{
    [SerializeField] private Projectile _attackProjectile;

    protected override void OnAttackCompleted()
    {
        User.AnimationComponent.StopAttack();
    }

    protected override void OnLaunch()
    {
        User.AnimationComponent.StartAttack(AttackSpeed);

        var proj = Instantiate(_attackProjectile);

        proj.LaunchBy(this);
    }

}
