using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HealthRoomTrigger : MonoBehaviour
{
    [SerializeField] private DestroyOnFullHealth _destroyScript;
    
    private Collider _collider;

    private void Start()
    {
        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<PlayerController>(out var component)) return;
        
        component.Player.HitComponent.Hit(40);
        
        _destroyScript.StartCheck();
        Destroy(gameObject);
    }
}
