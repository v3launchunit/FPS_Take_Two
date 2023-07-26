using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    [SerializeField] protected float               _riseGrav               =  -9.81f;
    [SerializeField] protected float               _fallGrav               = -19.62f;
    [SerializeField] protected float               _cyoteTime              =   0.25f;
    [SerializeField] protected float               _knockbackFalloffAir    =   0.975f;
    [SerializeField] protected float               _knockbackFalloffGround =   0.9f;
    [SerializeField] protected float               _groundDistance         =   0.4f;
    [SerializeField] protected LayerMask           _groundMask;
    [SerializeField] protected CharacterController _controller;

    protected Transform _groundCheck;
    protected Vector3   _velocity  = Vector3.zero;
    protected Vector3   _knockback = Vector3.zero;
    protected float     _fallSpeed = 0;
    protected bool      _grounded  = true;

    void Start()
    {
        _controller  = gameObject.GetComponent<CharacterController>();
        _groundCheck = transform.Find("GroundCheck");
    }

    void Update()
    {
        HandleMovement();
        HandleGravity();

        _controller.Move(_velocity * Time.deltaTime);
    }

    void FixedUpdate()
    {
        _velocity = Vector3.zero;

        HandleKnockback();
    }

    protected virtual void HandleMovement()
    {
        return;
    }

    protected virtual void HandleGravity()
    {
        _grounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);
        if (_grounded && _fallSpeed < 0)
        {
            _fallSpeed     = -2f;
            _knockback.y   =  0;
        }

        if (_fallSpeed >= 0)
            _fallSpeed += _riseGrav * Time.deltaTime;
        else
            _fallSpeed += _fallGrav * Time.deltaTime;

        _velocity.y += _fallSpeed;
    }

    void HandleKnockback()
    {
        _velocity  += _knockback;
        _knockback *= _grounded ? _knockbackFalloffGround : _knockbackFalloffAir;

        if (_knockback.magnitude < 0.5f)
            _knockback = Vector3.zero;
    }

    public void Knockback(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            if (_fallSpeed < 0)
                _fallSpeed  = 0;
            _knockback += -direction;
        }
    }

    public bool Grounded { get => _grounded; }
}
