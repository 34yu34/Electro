using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(CharacterController))]
public class MovementComponent : Component
{
    [SerializeField]
    private Attribute _speed;
    public Attribute Speed => _speed;

    public Vector2 Direction => new Vector2(_body.transform.forward.x, _body.transform.forward.z);
    public Vector3 Direction3D => new Vector3(_body.transform.forward.x, 0, _body.transform.forward.z);

    [FormerlySerializedAs("body")] [SerializeField] private GameObject _body;
    
    private CharacterController Controller => _controller ??= GetComponent<CharacterController>();
    private CharacterController _controller;

    private Vector3 _movement;

    private void FixedUpdate()
    {
        Controller.Move(_movement * Speed * Time.fixedDeltaTime);
    }

    public void GoToOrigin()
    {
        Controller.enabled = false;
        
        transform.position = Vector3.zero;
        
        Controller.enabled = true;
    }

    public void SetMovement2D(Vector2 direction)
    {
        _movement = new Vector3(direction.x, 0, direction.y).normalized;
    }

    public void SetLookAt(Vector2 lookAtDirection)
    {
        var lookAt = new Vector3(lookAtDirection.x, 0, lookAtDirection.y);
        _body.transform.LookAt(_body.transform.position + lookAt);
    }

    public void GoForward()
    {
        SetMovement2D(Direction);
    }
}
