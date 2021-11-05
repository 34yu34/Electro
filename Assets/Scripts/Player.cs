using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    [SerializeField] InputAction move;

    private Vector3 _movement;

    private Rigidbody _rb;
    private Rigidbody RB => _rb ??= GetComponent<Rigidbody>();


    private void Start()
    {
        RB.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        move.Enable();
    }

    private void FixedUpdate()
    {
        RB.position +=  _movement * Time.deltaTime * _speed;
    }

    private void Update()
    {
        var movement2d = move.ReadValue<Vector2>();

        _movement = new Vector3(movement2d.x, 0 ,movement2d.y);
    }
}
