using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeballHandler : MonoBehaviour
{
    [SerializeField] private Transform  _target;
    [SerializeField] private float      _targetDesiredDistance = 10;
    [SerializeField] private float      _speed = 5;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _muzzleFlash;
    [SerializeField] private int        _burst        =  1;
    [SerializeField] private float      _spread       =  0;
    [SerializeField] private int        _fireCooldown =  5;
    [SerializeField] private float      _sight         = 32;

    private float     _cooldown;
    private Rigidbody _body;

    void Start()
    {
        _cooldown = _fireCooldown;
        _body     = GetComponent<Rigidbody>();
    }

    void Update()
    {
        transform.LookAt(_target);

        float d = Vector3.Distance(transform.position, _target.position);

        if (d > _targetDesiredDistance)
        {
            _body.AddForce(_speed * d * transform.forward, ForceMode.VelocityChange);
        }
        else if (d < _targetDesiredDistance)
        {
            _body.AddForce(-_speed * d * transform.forward, ForceMode.VelocityChange);
        }

        if (_cooldown <= 0)
        {
            Instantiate(_bullet, transform.position + transform.forward, transform.rotation);
            _cooldown = _fireCooldown;
        }
        else
            _cooldown -= Time.deltaTime;
        
    }
}
