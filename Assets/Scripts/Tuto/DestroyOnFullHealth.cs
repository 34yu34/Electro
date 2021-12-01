using System;
using UnityEngine;

public class DestroyOnFullHealth : MonoBehaviour
{
    private Character _player;

    private bool _shouldCheck;

    private void Start()
    {
        _player = FindObjectOfType<PlayerController>().Player;
        
        Debug.Assert(_player != null);

        _shouldCheck = false;
    }

    public void StartCheck()
    {
        _shouldCheck = true;
    }

    public void Update()
    {
        if (!_shouldCheck) return;

        if (_player.HitComponent.IsFullHealth() && !_player.EnergyComponent.HasEnergy(15))
        {
            Destroy(gameObject);
        }
    }
}
