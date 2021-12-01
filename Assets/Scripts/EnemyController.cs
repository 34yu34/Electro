using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private BallAttack _attack;  
    
    private Character _enemy;
    private Character _player;

    private void Start()
    {
        Debug.Assert(_attack != null);
        
        _player = FindObjectOfType<PlayerController>().Player;
        _enemy = GetComponent<Character>();
        _enemy.AttackComponent.AttachAttack(_attack);

        _enemy.HitComponent.OnDeathEvent += OnDeath;
    }

    private void FixedUpdate()
    {
        _enemy.AttackComponent.IsAttacking = true;
        _enemy.MovementComponent.SetLookAt(PlayerDirection);
    }

    private Vector2 PlayerDirection
    {
        get 
        {
            var difference = _player.transform.position - transform.position;
            return new Vector2(difference.x, difference.z);
        }
    }

    private static void OnDeath()
    {
        LevelController.Instance.game_stats.enemy_killed += 1;
    }
}
