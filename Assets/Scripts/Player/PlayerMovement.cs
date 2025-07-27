using System.Collections;
using DG.Tweening;
using LTX.ChanneledProperties.Priorities;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private const string _horizontal = "Horizontal";
    private const string _vertical = "Vertical";
    private const string _isMoving = "IsMoving";
    private const string _isInteracting = "IsInteracting";
    private const string _triggerAttack = "Attack";
    private const string isDead = "IsDead";


    [SerializeField]
    private float maxHealth = 10f;
    private float health;

    public float Health => health;
    public float MaxHealth => maxHealth;

    [SerializeField]
    private float timeBeforeGetAttacked;
    private bool canGetAttacked;

    [SerializeField]
    private float _moveSpeed = 5f;
    [SerializeField]
    private Gradient hitGradient;

    private Vector2 _movement;
    public Vector2 FacingDirection { get; private set; }

    private Rigidbody2D _rb;
    private SpriteRenderer spriteRenderer;

    private Animator _animator;

    private void Awake()
    {
        canGetAttacked = true;
        health = maxHealth;
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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

    public void TriggerAttack()
    {
        _animator.SetTrigger(_triggerAttack);
    }

    public void Move(InputAction.CallbackContext context)
    {
        _movement = context.ReadValue<Vector2>();
        if (_movement.sqrMagnitude < 0.01) return;
        FacingDirection = _movement.normalized;
    }

    public void PlayerGetAttacked(float damages)
    {
        if (!canGetAttacked) return;
        canGetAttacked = false;
        health -= damages;
        if (health <= 0)
        {
            _animator.SetBool(isDead, true);
        }
        else
        {
            StartCoroutine(HandleCanGetAttacked());
            transform.DOPunchScale(Vector3.one * 0.6f, 0.3f);
            spriteRenderer.DOGradientColor(hitGradient, 0.3f);
        }
    }

    public void DestroyPlayer()
    {
        Destroy(gameObject);
    }
    
    private IEnumerator HandleCanGetAttacked()
    {
        yield return new WaitForSeconds(timeBeforeGetAttacked);
        canGetAttacked = true;
    }

}
