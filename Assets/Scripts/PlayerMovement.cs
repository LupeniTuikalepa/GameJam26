using LTX.ChanneledProperties.Priorities;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 5f;

    private Vector2 _movement;

    private Rigidbody2D _rb;
    private Animator _animator;

    private const string _horizontal = "Horizontal";
    private const string _vertical = "Vertical";
    private const string _isMoving = "IsMoving";

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        _movement.Set(InputManager.Movement.x, InputManager.Movement.y);

        _rb.linearVelocity = _movement * _moveSpeed;

        _animator.SetFloat(_horizontal, _movement.x);
        _animator.SetFloat(_vertical, _movement.y);
        _animator.SetBool(_isMoving, _movement.sqrMagnitude >= 0.01f);
    }
}
