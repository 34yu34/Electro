using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Character))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    private Character _player;
    public Character Player => _player ??= GetComponent<Character>();

    private PlayerInput _playerInputs;
    private PlayerInput PlayerInput => _playerInputs ??= GetComponent<PlayerInput>();

    private Vector2 _lastMovement;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        Player.HitComponent.OnDeathEvent += OnPlayerDeath;
    }

    private void OnAbility()
    {
        if (Player.StunComponent.IsStunned) return;
        Player.AbilityComponent.UseAbility();
    }

    private void OnFire(InputValue input)
    {
        Player.AttackComponent.IsAttacking = input.isPressed;
    }

    public void OnMove(InputValue value)
    {
        if (Player.StunComponent.IsStunned)
        {
            StartCoroutine(nameof(DeferredMove), value.Get<Vector2>());
            return;
        }

        Player.MovementComponent.SetMovement2D(value.Get<Vector2>());
    }
    
    public void OnLook(InputValue value)
    {
        if (Player.StunComponent.IsStunned) return;

        Player.MovementComponent.SetLookAt(value.Get<Vector2>());
    }

    public void OnMouse(InputValue value)
    {
        if (Player.StunComponent.IsStunned) return;

        var pos = value.Get<Vector2>();
        
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Deg2Rad;
        
        Player.MovementComponent.SetLookAt(dir);
    }

    private IEnumerator DeferredMove(Vector2 move)
    {
        _lastMovement = move;
        yield return new WaitWhile(() => Player.StunComponent.IsStunned);
        Player.MovementComponent.SetMovement2D(_lastMovement);
    }

    private void OnPlayerDeath()
    {
        Destroy(gameObject);
        
        FindObjectOfType<LevelController>().ChangeScene("EndScene");
    }
}
