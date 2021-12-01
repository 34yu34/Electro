using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(MapGenerator))]
public class LevelController : MonoBehaviour
{
    private static LevelController _instance;
    public static LevelController Instance
    {
        get
        {
            if (_instance != null) return _instance;
            
            _instance = FindObjectOfType<LevelController>();
            Debug.Assert(_instance != null);

            return _instance;
        }
    }
    
    public BossPortal BossPortal => _bossPortal;
    [SerializeField] private BossPortal _bossPortal;

    private PlayerController PlayerController => _playerController ??= FindObjectOfType<PlayerController>();
    private PlayerController _playerController;

    private int _currentRound;

    private MapGenerator _mapGenerator;
    private MapGenerator MapGenerator => _mapGenerator ??= GetComponent<MapGenerator>();

    [SerializeField] private List<EnemiesGenerationData> _enemiesData;

    public Stats game_stats;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
        ResetStats();
    }

    public void ResetStats()
    {        
        game_stats = new Stats()
        {
            boss_killed = 0,
            enemy_killed = 0,
            level = 0,
            pickup_taken = 0
        };
    }

    public void ChangeScene(string sceneName)
    {
        if (PlayerController != null)
        {
            PlayerController.Player.StunComponent.Stun();
            PlayerController.Player.MovementComponent.GoToOrigin();
        }

        SceneManager.LoadScene(sceneName);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (PlayerController != null)
        {
            PlayerController.Player.StunComponent.UnStun();
        }

        if (scene.name == "Lvl")
        {
            GenerateMap();
        }
    }
    
    private void GenerateMap()
    {
        game_stats.level += 1;
        _enemiesData[0].UpgradeDifficulty(game_stats.level);
        MapGenerator.GenerateMap(_enemiesData[0]);
    }
}
