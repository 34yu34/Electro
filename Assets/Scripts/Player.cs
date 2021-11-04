using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    private Vector3 _movement;

    private Rigidbody _rb;
    private Rigidbody RB => _rb ??= GetComponent<Rigidbody>();


    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        RB.position +=  _movement * Time.deltaTime * _speed;
    }

    private void Update()
    {
        _movement = new Vector3(Input.GetAxis("Horizontal"), 0 ,Input.GetAxis("Vertical"));
    }
}
