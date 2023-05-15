using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackHandler : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _muzzleFlash;
    [SerializeField] private int        _burst        =  1;
    [SerializeField] private float      _spread       =  0;
    [SerializeField] private int        _fireCooldown =  1;
    [SerializeField] private int       _sight         = 32;

    private float         _cooldown;
    private Transform     _spawner;
    private EnemyMovement _movement;
    private Transform     _target;

    void Start()
    {
        _cooldown = _fireCooldown;
        _spawner  = Utils.FindRecursive(transform, "Spawner");
        _movement = gameObject.GetComponent<EnemyMovement>();
        _target   = _movement.Target;
    }

    void Update()
    {
        //if (_target = null)
        _target = _movement.Target;

        if 
        (
            !_movement.Busy &&
            _cooldown <= 0  && 
            Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, _sight) && 
            hit.transform == _target
        )
        {
            print($"{_target} spotted");
            gameObject.GetComponent<Animator>().SetTrigger("Fire");
            _movement.Busy = true;
        }

        if (_cooldown > 0) _cooldown -= Time.deltaTime;
    }

    public void Fire()
    {
        if (_muzzleFlash != null)
        {
            GameObject flash = Instantiate(_muzzleFlash, _spawner.transform.position, _spawner.transform.rotation);
            flash.transform.SetParent(transform, true);
        }

        if (_bullet != null)
        {
            // Instantiate(_bullet, _spawner.transform.position, _spawner.transform.rotation);
            for (int i = 0; i<_burst; i++)
            {
                var b = Instantiate(_bullet, _spawner.transform.position, _spawner.transform.rotation * Quaternion.Euler(_spread * Mathf.Sin(i) * i/9, _spread * Mathf.Cos(i) * i/9, 0));
                
                if (b.TryGetComponent(out OwnedProjectile proj))
                    proj.Owner = transform;
            }
        }

        _cooldown = _fireCooldown;
    }
}
