using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(StunComponent))]
[RequireComponent(typeof(AttackComponent))]
[RequireComponent(typeof(MovementComponent))]
[RequireComponent(typeof(AbilityComponent))]
[RequireComponent(typeof(HitComponent))]
[RequireComponent(typeof(EnergyComponent))]
[RequireComponent(typeof(AnimationComponent))]
public class Character : MonoBehaviour
{
    public AttackComponent AttackComponent => _attackComponent ??= GetComponent<AttackComponent>();
    private AttackComponent _attackComponent;

    public StunComponent StunComponent => _stunComponent ??= GetComponent<StunComponent>();
    private StunComponent _stunComponent;

    public MovementComponent MovementComponent => _movementComponent ??= GetComponent<MovementComponent>();
    private MovementComponent _movementComponent;

    public AbilityComponent AbilityComponent => _abilityComponent ??= GetComponent<AbilityComponent>();
    private AbilityComponent _abilityComponent;

    public HitComponent HitComponent => _hitComponent ??= GetComponent<HitComponent>();
    private HitComponent _hitComponent;

    public EnergyComponent EnergyComponent => _energyComponent ??= GetComponent<EnergyComponent>();
    private EnergyComponent _energyComponent;

    public AnimationComponent AnimationComponent => _animationComponent ??= GetComponent<AnimationComponent>();
    private AnimationComponent _animationComponent;
}
