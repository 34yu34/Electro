using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndSceneScript : MonoBehaviour 
{ 
    [SerializeField] 
    private Text _enemyKilled;
    
    [SerializeField] 
    private Text _bossKilled;
    
    [SerializeField] 
    private Text _pickupTaken;
    
    [SerializeField] 
    private Text _level;

    [SerializeField] 
    private Text _score;

    [SerializeField]
    private Button _menu;
    
    [SerializeField]
    private Button _quit;
    
    private void Start()
    {
        var stats = LevelController.Instance.game_stats;

        _enemyKilled.text = $"Enemies killed : {stats.enemy_killed}";
        _bossKilled.text = $"Boss killed : {stats.boss_killed}";
        _pickupTaken.text = $"Pickup Grabbed : {stats.pickup_taken}";
        _level.text = $"Level : {stats.level}";

        _score.text = $"TOTAL SCORE : {stats.CalculateScore()}";
        
        _menu.onClick.AddListener(ToMenuClicked);
        _quit.onClick.AddListener(OnQuitClicked);
    }

    private void ToMenuClicked()
    {
        LevelController.Instance.ChangeScene("MainMenu");
    }

    private void OnQuitClicked()
    {
        Application.Quit();
    }
}
