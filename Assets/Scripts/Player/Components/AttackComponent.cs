using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComponent : Component
{
    private Attack _attack;
    public bool IsAttacking { get; set; }

    [SerializeField]
    private Attribute _damage;
    public Attribute Damage => _damage;

    [SerializeField] [EnumMask]
    private TargetType _targets;
    public TargetType TargetsType => _targets;

    [SerializeField]
    private float _attackHeight;
    public Vector3 AttackStartPosition => transform.position + new Vector3(0, _attackHeight, 0);

    private void FixedUpdate()
    {
        if (!IsAttacking || Character.StunComponent.IsStunned)
        {
            return;
        }
        Attack();
    }

    public void AttachAttack(Attack attack)
    {
        RemoveAttack();
        _attack = Instantiate(attack, transform);
    }

    private void Attack()
    {
        if (!_attack) return;
        
        _attack.Use();
    }

    private void RemoveAttack()
    {
        if (!_attack) return;
        
        Destroy(_attack.gameObject);
        _attack = null;
    }
}
