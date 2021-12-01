using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class EndRoomTriggerScript : MonoBehaviour
{
    private BoxCollider _boxCollider;
    private BoxCollider BoxCollider => _boxCollider ??= GetComponent<BoxCollider>();

    public delegate void OnPlayerEnter();

    public event OnPlayerEnter OnPlayerEnterTrigger;

    private void Start()
    {
        BoxCollider.isTrigger = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() == null) return;

        OnPlayerEnterTrigger?.Invoke();
        Destroy(gameObject);
    }

}
