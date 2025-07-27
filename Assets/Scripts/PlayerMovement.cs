using LTX.ChanneledProperties.Priorities;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 5f;

    private Vector2 _movement;

    private Rigidbody2D _rb;
    private Animator _animator;

    private PlayerMovement playerMovement;

    private const string _horizontal = "Horizontal";
    private const string _vertical = "Vertical";
    private const string _isMoving = "IsMoving";

    private const string _isInteracting = "IsInteracting";

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    private void Update()
    {

        _rb.linearVelocity = _movement * _moveSpeed;

        _animator.SetFloat(_horizontal, _movement.y == 0 ? _movement.x : 0);
        _animator.SetFloat(_vertical, _movement.y);
        _animator.SetBool(_isMoving, _movement.sqrMagnitude >= 0.01f);
    }

    public void TriggerInteractionAnimation()
    {
            _animator.SetBool(_isInteracting, true);
    }
    
    public void StopInteractionAnimation()
    {
        _animator.SetBool(_isInteracting, false);
    }

    public void Move(InputAction.CallbackContext context)
    {
        _movement = context.ReadValue<Vector2>();
    }
}
