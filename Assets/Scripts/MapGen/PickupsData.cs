using System.Collections.Generic;
using UnityEngine;

public class PickupsData : MonoBehaviour
{
    [SerializeField] private List<Pickup> _pickups;

    public Pickup GetRandom()
    {
        return _pickups[Random.Range(0, _pickups.Count)];
    }
}
