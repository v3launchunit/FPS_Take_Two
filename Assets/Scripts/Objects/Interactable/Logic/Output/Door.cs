using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : LogicOutput
{
    // [SerializeField] private Vector3 _offPosition;
    // [SerializeField] private Vector3 _offRotation;
    [SerializeField] private Vector3 _onPosition;
    [SerializeField] private Vector3 _onRotation;
    [SerializeField] private float   _travelTime = 0.5f;

    private Vector3 _offPosition;
    private Vector3 _offRotation;
    private Vector3 _targetPosition;
    private Vector3 _targetRotation;
    private Vector3 _positionVel;
    private Vector3 _rotationVel;

    void Start()
    {
        // transform.localPosition    = _offPosition;
        // transform.localEulerAngles = _offRotation;
        _offPosition    = transform.localPosition;
        _offRotation    = transform.localEulerAngles;
        _targetPosition = _offPosition;
        _targetRotation = _offRotation;
    }

    void Update()
    {
        transform.localPosition    = 
            Vector3.SmoothDamp(transform.localPosition,    _targetPosition, ref _positionVel, _travelTime);
        transform.localEulerAngles = 
            Vector3.SmoothDamp(transform.eulerAngles, _targetRotation, ref _rotationVel, _travelTime);
    }

    public override void ToggleActive(bool on)
    {
        _on = on;
        _targetPosition = (on) ? _onPosition + _offPosition : _offPosition;
        _targetRotation = (on) ? _onRotation                : _offRotation;
    }
}
