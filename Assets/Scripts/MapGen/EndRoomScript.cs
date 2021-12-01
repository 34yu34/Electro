using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndRoomScript : MonoBehaviour
{
    [SerializeField] private Character _boss;

    private BossSpawnSpot _bossSpawnSpot;

    public void Start()
    {
        GetComponentInChildren<EndRoomTriggerScript>().OnPlayerEnterTrigger += StartBossFight;
        _bossSpawnSpot = GetComponentInChildren<BossSpawnSpot>();
        
        Debug.Assert(_bossSpawnSpot != null);
    }

    private void StartBossFight()
    {
        Instantiate(_boss, _bossSpawnSpot.transform.position, Quaternion.identity);
    }

}
