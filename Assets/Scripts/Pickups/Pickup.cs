using System;
using UnityEngine;
using UnityEngine.Assertions;


[RequireComponent(typeof(BoxCollider))]
public abstract class Pickup : MonoBehaviour
{
    [SerializeField] private GameObject _body;
    private BoxCollider _boxCollider;

    private Vector3 _initialPosition;
    private Vector3 _currentPosition;

    private const float ROTATION_PER_SECONDS = 0.25f;
    private const float MOVEMENT_HEIGHT = 0.25f;
    
    public delegate void PickupEvent();

    public event PickupEvent OnPickup;
    
    private void Start()
    {
        Debug.Assert(_body != null);
        
        _boxCollider = GetComponent<BoxCollider>();
        _boxCollider.isTrigger = true;

        _initialPosition = _body.transform.localPosition;
        _currentPosition = _initialPosition;
    }

    private void Update()
    {
        Animate();
    }

    private void Animate()
    {
        GenerateRotation();
        GenerateVerticalMovement();
    }

    private void GenerateRotation()
    {
        _body.transform.rotation = Quaternion.AngleAxis(CurrentAngle(), Vector3.up);
    }

    private static float CurrentAngle()
    {
        return ROTATION_PER_SECONDS * 360 * (Time.time % (1 / ROTATION_PER_SECONDS));
    }

    private void GenerateVerticalMovement()
    {
        _currentPosition.y = _initialPosition.y + Mathf.Sin(Time.time) * MOVEMENT_HEIGHT;
        _body.transform.localPosition = _currentPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<PlayerController>(out var playerController))
        {
            return;
        }
        
        SetupPlayerPower(playerController.Player);

        if (OnPickup != null)
        {
            OnPickup.Invoke();
        }

        LevelController.Instance.game_stats.pickup_taken += 1;
        
        Destroy(gameObject);
    }

    protected abstract void SetupPlayerPower(Character player);
}
