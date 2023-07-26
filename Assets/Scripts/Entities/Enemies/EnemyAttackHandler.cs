using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackHandler : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _muzzleFlash;
    [SerializeField] private int        _burst        =  1;
    [SerializeField] private float      _spread       =  0;
    [SerializeField] private float      _fireCooldown =  1;
    [SerializeField] private float      _sight         = 32;
    [SerializeField] private GameObject _meleeBullet;
    [SerializeField] private float      _meleeRange    =  3;
    [SerializeField] private GameObject _foleySound;
    [SerializeField] private LayerMask  _sightMask;

    private float         _cooldown;
    private Transform     _spawner;
    private EnemyMovement _movement;
    [SerializeField] private Transform     _target;

    void Start()
    {
        _cooldown = Random.Range(_fireCooldown - 0.5f, _fireCooldown + 0.5f);
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
            _target != null &&
            !_movement.Busy &&
            (_cooldown <= 0 || (_meleeBullet != null && Vector3.Distance(transform.position, _target.position) <= _meleeRange))
            && 
            !Physics.Linecast(transform.position, _target.position, _sightMask)
        )
        {
            // print($"{_target} spotted");
            if (_meleeBullet != null && Vector3.Distance(transform.position, _target.position) <= _meleeRange)
                gameObject.GetComponent<Animator>().SetTrigger("Melee");
            else if (_bullet != null)
                gameObject.GetComponent<Animator>().SetTrigger("Fire");
            else return;
            transform.LookAt(_target);
            _movement.Busy = true;
        }

        if (_cooldown > 0) _cooldown -= Time.deltaTime;
    }

    public void Fire()
    {
        // transform.LookAt(_target);

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
    public void Melee()
    {
        // transform.LookAt(_target);

        if (_meleeBullet != null)
        {
            // Instantiate(_bullet, _spawner.transform.position, _spawner.transform.rotation);
            var b = Instantiate(_meleeBullet, _spawner.transform.position, transform.rotation);

            if (b.TryGetComponent(out OwnedProjectile proj))
                proj.Owner = transform;
        }

        _cooldown = _fireCooldown;
    }

    public void Foley()
    {
        Instantiate(_foleySound);
    }
}
