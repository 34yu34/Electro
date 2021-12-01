using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Shield : Ability
{
    [SerializeField] private Renderer _renderer;
    private bool _isUsing;

    private SphereCollider _sphereCollider;

    private void Start()
    {
        _sphereCollider = GetComponent<SphereCollider>();
        _sphereCollider.isTrigger = true;

        _renderer.enabled = false;
    }

    protected override void OnCharacterUse()
    {
        User.HitComponent.Immunize();
        _renderer.enabled = true;
    }

    protected override void OnAbilityDone()
    {
        User.HitComponent.Unimmunize();
        _renderer.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsInUse || !other.TryGetComponent<Projectile>(out var proj) || proj.Launcher != User || proj.Launcher != null)
        {
            return;
        }

        Destroy(proj.gameObject);
    }
}
