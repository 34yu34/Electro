using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemiesGenerationData : MonoBehaviour
{
    public List<Character> Enemies => _enemies;
    [FormerlySerializedAs("_ennemies")] [SerializeField] private List<Character> _enemies;
    
    public float InitialSpawnChance => _initialSpawnChance;
    [SerializeField] private float _initialSpawnChance = 1f;

    public float SpawnChanceDropDown => _spawnChanceDropDown;
    [SerializeField] private float _spawnChanceDropDown = 0.25f;



    public void UpgradeDifficulty(int level)
    {
        if ((level & 1) == 0)
        {
            _initialSpawnChance += _spawnChanceDropDown;
        }

        foreach (var enemy in _enemies)
        {
            enemy.AttackComponent.Damage.BaseValue += enemy.AttackComponent.Damage.BaseValue * 0.1f;
        }
        
    }
}
