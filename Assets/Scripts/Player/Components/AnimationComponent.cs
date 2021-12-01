using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationComponent : Component
{
    private Animator _animator;

    // This will be null for some character
    private Animator Animator => _animator;

    public delegate void AnimationEvent();

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void StartAttack(float attack_speed)
    {
        if (_animator != null)
        {
            _animator.SetTrigger("Attack");
            _animator.SetFloat("AttackSpeed", attack_speed);
        }
    }

    public void StopAttack()
    {
        if (_animator != null)
        {
            _animator.ResetTrigger("Attack");
        }
    }

    public void StartDash(float dash_speed)
    {
        if (_animator != null)
        {
            _animator.SetTrigger("Dash");
            _animator.SetFloat("DashSpeed", dash_speed);
        }
    }

    public void StopDash()
    {
        if (_animator != null)
        {
            _animator.ResetTrigger("Dash");
        }
    }
}
