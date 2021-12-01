using System;
using UnityEngine;

public class TestScene : MonoBehaviour
{
    [SerializeField] private Ability _ability;
    [SerializeField] private Attack _attack;
    private void Start()
    {
        var player = FindObjectOfType<PlayerController>().Player;
     
        player.AbilityComponent.AttachAbility(_ability);
        player.AttackComponent.AttachAttack(_attack);
        
        FindObjectOfType<LevelController>().ChangeScene("Lvl");
    }
}
