using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : Bullet
{
    [SerializeField] private float _homingChance = 0.5f;
    [SerializeField] private float _turnSpeed    = 1f;


    private Transform _target = null;

    void Start()
    {
        if (Random.value <= _homingChance)
        {
            // print($"Homing towards {_owner.GetComponent<EnemyMovement>().Target}");
            _target = _owner.GetComponent<EnemyMovement>().Target;
        }
        else if (_targetTarget)
            transform.LookAt(_owner.GetComponent<EnemyMovement>().Target);

        Rigidbody bulletBody = gameObject.GetComponent<Rigidbody>();

        bulletBody.AddForce(transform.forward * _bulletSpeed, ForceMode.VelocityChange);
        transform.position += transform.forward;
    }
    
    void Update()
    {
        if (!_collided)
        {
            if (_target != null)
            {
                // transform.LookAt(_target);
                var targetRotation = Quaternion.LookRotation(_target.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _turnSpeed * Time.deltaTime);

                gameObject.GetComponent<Rigidbody>().velocity = (transform.forward * _bulletSpeed);
            }

            gameObject.GetComponent<TrailRenderer>().time += Time.deltaTime;
        }
    }
}
