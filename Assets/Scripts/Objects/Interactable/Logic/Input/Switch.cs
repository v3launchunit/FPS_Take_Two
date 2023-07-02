using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : InteractableObject
{
    // [SerializeField] private Vector3           _offPosition;
    // [SerializeField] private Vector3           _offRotation;
    [SerializeField] private Vector3           _onPosition;
    [SerializeField] private Vector3           _onRotation;
    [SerializeField] private float             _travelTime  =  0.5f;
    [SerializeField] private float             _resetDelay  = -1;
    [SerializeField] private int               _requiredKey = -1;
    [SerializeField] private string            _lockedText;
    [SerializeField] private bool              _oneWay;
    [SerializeField] private List<LogicOutput> _targets;

    private Vector3 _offPosition;
    private Vector3 _offRotation;
    private Vector3 _targetPosition;
    private Vector3 _targetRotation;
    private Vector3 _positionVel;
    // private Vector3 _rotationVel;
    private float   _pitchVel;
    private float   _yawVel;
    private float   _rollVel;
    private float   _resetTimer = 0;
    private bool    _on = false;

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
        // transform.localEulerAngles = 
        //     Vector3.SmoothDamp(transform.localEulerAngles, _targetRotation, ref _rotationVel, _travelTime);
        transform.localEulerAngles = new
        (
            Mathf.SmoothDampAngle(transform.localEulerAngles.x, _targetRotation.x, ref _pitchVel, _travelTime),
            Mathf.SmoothDampAngle(transform.localEulerAngles.y, _targetRotation.y, ref _yawVel,   _travelTime),
            Mathf.SmoothDampAngle(transform.localEulerAngles.z, _targetRotation.z, ref _rollVel,  _travelTime)
        );
        
        if (_on && _resetDelay != -1)
        {
            _resetTimer += Time.deltaTime;
            if (_resetTimer >= _resetDelay)
            {
                ToggleActive(false);
                _resetTimer = 0;
            }
        }
    }

    public override void OnInteract(GameObject other)
    {
        if (_requiredKey != -1 && !other.GetComponentInParent<PlayerStatus>().Keys[_requiredKey])
        {
            if (_lockedText != "")
                FindFirstObjectByType<HudHandler>().Log(_lockedText);
            return;
        }

        if (!_on)
        {
            print("switch pressed!");
            ToggleActive(true);
            _resetTimer = 0;
        }
        else if (!_oneWay && _on)
        {
            ToggleActive(false);
        }
    }

    public void ToggleActive(bool on)
    {
        if (_interactSound != null)
            Instantiate(_interactSound, transform.position, transform.rotation);
            
        _on = on;
        _targetPosition = (on) ? _onPosition + _offPosition : _offPosition;
        _targetRotation = (on) ? _onRotation                : _offRotation;

        foreach (var t in _targets)
        {
            t.ToggleActive(on);
        }
    }

    public bool On { get => _on; }
}
