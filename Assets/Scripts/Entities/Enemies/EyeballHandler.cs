using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeballHandler : MonoBehaviour
{
    [SerializeField] private Transform  _target;
    [SerializeField] private float      _targetDesiredDistance = 10;
    [SerializeField] private float      _speed                 = 5;
    [SerializeField] private float      _maxSpeed              = 25;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _muzzleFlash;
    // [SerializeField] private int        _burst        =  1;
    // [SerializeField] private float      _spread       =  0;
    [SerializeField] private int        _fireCooldown =  5;
    // [SerializeField] private float      _sight        = 32;

    private float     _cooldown;
    private Rigidbody _body;

    void Start()
    {
        _cooldown = Random.Range(_fireCooldown - 0.5f, _fireCooldown + 0.5f);
        _body     = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 3) || (transform.position.y <= _target.position.y))
        {
            _body.AddForce(_maxSpeed * Vector3.up, ForceMode.Acceleration);
        }

        transform.LookAt(_target);

        float d = Mathf.Min(Vector3.Distance(transform.position, _target.position), _maxSpeed);

        if (d > _targetDesiredDistance)
        {
            _body.AddForce(_speed * d * transform.forward, ForceMode.Acceleration);
        }
        else if (d < _targetDesiredDistance)
        {
            _body.AddForce(-_speed * d * transform.forward, ForceMode.Acceleration);
        }

        if (_cooldown <= 0)
        {
            var b = Instantiate(_bullet, transform.position + transform.forward, transform.rotation);
            b.GetComponent<OwnedProjectile>().Owner = transform;
            _cooldown = _fireCooldown;
        }
        else
            _cooldown -= Time.deltaTime;
        
    }
}
