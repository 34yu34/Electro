using UnityEngine;


[RequireComponent(typeof(Character))]
public class BossDeath : MonoBehaviour
{
    private Character _body;
    private Character Body => _body ??= GetComponent<Character>();


    // Start is called before the first frame update
    void Start()
    {
        Body.HitComponent.OnDeathEvent += OnDeath;
    }

    private void OnDeath()
    {
        LevelController.Instance.game_stats.boss_killed += 1;

        Instantiate(LevelController.Instance.BossPortal, transform.position + (Vector3.up * 0.01f), Quaternion.identity);
    }

}
