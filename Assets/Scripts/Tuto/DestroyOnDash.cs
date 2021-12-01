using UnityEngine;

namespace DefaultNamespace
{
    public class DestroyOnDash : MonoBehaviour
    {
        private Character _player;
        private void Start()
        {
            _player = FindObjectOfType<PlayerController>().Player;
            _player.AbilityComponent.OnAbilityUse += OnAbilityUse;
        }

        private void OnAbilityUse()
        {
            _player.AbilityComponent.OnAbilityUse -= OnAbilityUse;
            Destroy(gameObject);
        }
    }
}