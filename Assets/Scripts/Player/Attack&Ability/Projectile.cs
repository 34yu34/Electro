using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _lifetime;

    public float Lifetime => _lifetime;

    private Character _launcher;
    public Character Launcher => _launcher; 
    private Attack _attack;

    private Rigidbody _rb;
    private Rigidbody Rb => _rb ??= GetComponent<Rigidbody>();

    private Collider _collider;
    private Collider Collider => _collider ??= GetComponent<Collider>();

    private void Start()
    {
        Rb.useGravity = false;
        Rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

        Collider.isTrigger = false;

        gameObject.layer = LayerMask.NameToLayer("Projectile");
    }

    private void Deactivate()
    {
        Destroy(gameObject);
    }

    private IEnumerator DestroyAfterLifetime()
    {
        yield return new WaitForSeconds(_lifetime);
        Deactivate();
    }

    public void LaunchBy(Attack attack)
    {
        enabled = true;
        StartCoroutine(nameof(DestroyAfterLifetime));

        _launcher = attack.User;
        _attack = attack;

        var direction = _launcher.MovementComponent.Direction3D.normalized;

        Rb.WakeUp();
        Rb.position = _launcher.AttackComponent.AttackStartPosition;
        Rb.velocity = direction * _speed;
        Rb.rotation = Quaternion.LookRotation(direction);

        Physics.IgnoreCollision(Collider, _launcher.GetComponent<Collider>(), true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_launcher == null)
        {
            return;
        }

        CheckCharacterHit(collision);

        Deactivate();
    }

    private void CheckCharacterHit(Collision collision)
    {
        var characterHit = collision.collider.GetComponent<Character>();

        if (characterHit != null && characterHit.HitComponent.CanBeHitBy(_launcher))
        {
            characterHit.HitComponent.Hit(_attack.FinalDamage);
        }
    }
}
