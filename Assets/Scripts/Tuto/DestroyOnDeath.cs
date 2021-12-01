using System;
using UnityEngine;

public class DestroyOnDeath : MonoBehaviour
{
        [SerializeField] private Character _entity;

        public void Start()
        { 
                _entity.HitComponent.OnDeathEvent += OnDeath;
        }

        private void OnDeath()
        {
                _entity.HitComponent.OnDeathEvent -= OnDeath;
                Destroy(gameObject);
        }
}
