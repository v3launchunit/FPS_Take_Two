using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Movement
{
    [SerializeField] protected float _walkSpeed    =  9f;
    [SerializeField] protected float _jumpHeight   =  3f;
    [SerializeField] protected float _crouchHeight =  1f;
    [SerializeField] protected float _slideSpeed   = 18f;
    [SerializeField] protected float _tiltFactor   =  3f;
    [SerializeField] private   float _weaponDrag   =  3f;

    [SerializeField] protected Transform _playerCam;

    [SerializeField] protected Transform _playerHands;
    [SerializeField] protected float     _bobSpeed     = 3.14f;
    [SerializeField] protected float     _bobIntensity = 0.25f;

    protected float _cyoteTimeLeft;
    private   float _crouchVel       = 0;
    private   float _handSmoothFall  = 0;
    private   float _handSmoothPitch = 0;
    private   float _handSmoothYaw   = 0;
    private   float _fallPitch       = 0;
    private   float _movePitch       = 0;
    private   float _camRecoil       = 0;
    private   float _camRecoilVel    = 0;

    protected override void HandleMovement()
    {
        // Retrieve inputs
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Handle actual movement
        Vector3 move = transform.right * x + transform.forward * z;
        _controller.Move(_walkSpeed * Time.deltaTime * move);
        if (Input.GetButtonDown("Crouch") && _grounded)
            Knockback(-_slideSpeed * move);

        // Handle camera bobbing
        Vector3 bob = new
        (
            Mathf.Min(move.magnitude, 1) * Mathf.Sin(Time.time * _bobSpeed)     * _bobIntensity,
            Mathf.Min(move.magnitude, 1) * Mathf.Sin(Time.time * _bobSpeed * 2) * _bobIntensity/2
        );
        _playerCam.localPosition = bob;

        // Handle camera tilting
        _camRecoil = Mathf.SmoothDamp(_camRecoil, 0, ref _camRecoilVel, 0.5f);
        Vector3 tilt = new(_camRecoil, 0, -x * _tiltFactor);
        _playerCam.localEulerAngles = tilt;

        // Handle gun bobbing
        _playerHands.localPosition = new
        (
            -x * 0.1f,
            Mathf.Min(move.magnitude, 1) * Mathf.Sin(Time.time * _bobSpeed * 2) * _bobIntensity/4,
            0
        );
    }
    
    protected override void HandleGravity()
    {
        _grounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);
        if (_grounded && _fallSpeed < 0)
        {
            _fallSpeed     = -2f;
            _knockback.y   =  0;
            _cyoteTimeLeft = _cyoteTime;
        }
        else if (_cyoteTimeLeft > 0)
            _cyoteTimeLeft -= Time.deltaTime;

        if (Input.GetButtonDown("Jump") && (_grounded || _cyoteTimeLeft > 0))
        {
            _fallSpeed = Mathf.Sqrt(_jumpHeight * -2f * _riseGrav);
            _cyoteTimeLeft = 0;
        }

        // fallSpeed += fallGrav * Time.deltaTime;
        if (Input.GetButton("Jump") && _fallSpeed >= 0)
            _fallSpeed += _riseGrav * Time.deltaTime;
        else
            _fallSpeed += _fallGrav * Time.deltaTime;

        _velocity.y += _fallSpeed;

        _fallPitch = Mathf.SmoothDampAngle(_fallPitch, _fallSpeed,                              ref _handSmoothFall,  0.05f);
        _movePitch = Mathf.SmoothDampAngle(_movePitch, -Input.GetAxis("Mouse Y") * _weaponDrag, ref _handSmoothPitch, 0.3f);
        _playerHands.localEulerAngles = new
        (
            _movePitch + _fallPitch - _camRecoil,
            Mathf.SmoothDampAngle(_playerHands.localEulerAngles.y, Input.GetAxis("Mouse X") * _weaponDrag, ref _handSmoothYaw, 0.3f),
            0
        );

        SetCrouch();
    }

    public void CamRecoil(float strength)
    {
        _camRecoil   -= strength;
        _camRecoilVel = 0;
    }

    private void SetCrouch()
    {
        var    collider = GetComponent<CapsuleCollider>();

        // float crouchPos = Mathf.SmoothDamp
        // (
        //     collider.height, 
        //     Input.GetButton("Crouch") ? _crouchHeight : 2, 
        //     ref _crouchVel, 
        //     0.1f
        // );
        // collider.center            = new(0, crouchPos/2, 0);
        // collider.height            = crouchPos;
        // _controller.center         = new(0, crouchPos/2, 0);
        // _controller.height         = crouchPos;
        // _groundCheck.localPosition = new(0, 1 - crouchPos, 0);

        if (Input.GetButtonDown("Crouch"))
        {
            collider.center            = new(0, _crouchHeight/2, 0);
            collider.height            = _crouchHeight;
            _controller.center         = new(0, _crouchHeight/2, 0);
            _controller.height         = _crouchHeight;
            _groundCheck.localPosition = Vector3.zero;
            // Knockback()
            // if (_grounded)
            //     _controller.Move(Vector3.down);
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            collider.center            = Vector3.zero;
            collider.height            = 2;
            _controller.center         = Vector3.zero;
            _controller.height         = 2;
            _groundCheck.localPosition = Vector3.down;
            // if (_grounded)
            //     _controller.Move(Vector3.up);
        }
    }
}
