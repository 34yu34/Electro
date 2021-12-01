using System;
using UnityEngine;

namespace Pickups
{
    public class DestroyOnPickup : MonoBehaviour
    {
        [SerializeField] private Pickup _pickup;

        private void Start()
        {
            _pickup.OnPickup += OnPickup;
        }

        private void OnPickup()
        {
            _pickup.OnPickup -= OnPickup;
            Destroy(gameObject);
        }
    }
}