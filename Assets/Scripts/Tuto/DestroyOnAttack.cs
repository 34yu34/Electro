using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DestroyOnAttack : MonoBehaviour
{
    private Collider _collider;

    private void Start()
    {
        _collider = GetComponent<Collider>();
        _collider.isTrigger = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<Projectile>(out var projectile))
        {
            Destroy(gameObject);
        }
    }
}
