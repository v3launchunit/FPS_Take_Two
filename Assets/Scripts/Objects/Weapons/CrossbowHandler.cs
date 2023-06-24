using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowHandler : WeaponBase
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _muzzleFlash;
    // [SerializeField] private int        _burst      =  1;
    // [SerializeField] private float      _spread     =  0;
    [SerializeField] private int        _damageBase  = 10;
    [SerializeField] private float      _chargeDelay =  1;
    [SerializeField] private int        _ammoMax     = 50;
    [SerializeField] private int        _ammo        = 15;
    [SerializeField] private int        _sharedAmmo  = -1;
    [SerializeField] private bool       _automatic   = true;

    [SerializeField] private GameObject _spinBullet;

    [SerializeField] private bool _busy = false;

    private Animator    _animator;
    private Transform   _spawner;
    private bool        _charging    = false;
    private float       _chargeTimer = 0;
    // private IEnumerator _coroutine;

    void Awake()
    {
        _animator   = gameObject.GetComponent<Animator>();
        _spawner    = transform.Find("Spawner");

        if (_bullet == null && _muzzleFlash == null)
            _ammoMax = -1;
    }

    void Update()
    {
        _animator.SetBool
        (
            "Depleted", 
            (
                (_sharedAmmo == -1 && _ammo <= 0) ||
                (_sharedAmmo != -1 && !gameObject.GetComponentInParent<WeaponSelect>()
                    .ChargeSharedAmmo(_sharedAmmo, 1, true))
            )
        );
        
        if (Input.GetButtonDown("Spin") && !_busy)
        {
            _animator.SetTrigger("Spin");
            if (_spinBullet != null)
                Instantiate(_spinBullet, _spawner.transform.position, Camera.main.transform.rotation);
        }
        
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("crossbowCharge") && !_charging)
        {
            _animator.SetTrigger("AltFire");
            _busy = false;
        }

        if 
        (
            (_bullet != null || _muzzleFlash != null) &&
            Input.GetButtonDown("Fire1")              &&
            !_busy
        )
        {
            _charging    = true;
            _chargeTimer = 0;
            _animator.SetTrigger("AltFire");
        }
        
        if 
        (
            // (_bullet != null || _muzzleFlash != null) &&
            Input.GetButton("Fire1") &&
            _charging
        )
        {
            _chargeTimer += Time.deltaTime;
            // if (_chargeTimer % _chargeDelay >= _chargeDelay);
        }

        if 
        (
            _charging &&
            (
                // (_bullet != null || _muzzleFlash != null) &&
                Input.GetButtonUp("Fire1") 
                || 
                (
                    (_sharedAmmo == -1 && _ammo <= Mathf.FloorToInt(_chargeTimer / _chargeDelay)) ||
                    (_sharedAmmo != -1 && !gameObject.GetComponentInParent<WeaponSelect>()
                        .ChargeSharedAmmo(_sharedAmmo, Mathf.FloorToInt(_chargeTimer / _chargeDelay), true))
                )
            ) 
        )
        {
            _charging = false;

            if (_chargeTimer < _chargeDelay)
            {   
                _animator.SetTrigger("AltFire");
                print("firing aborted");
                return;
            }
            // StopCoroutine(_coroutine);
            if (_sharedAmmo != -1)
            {
                if (!gameObject.GetComponentInParent<WeaponSelect>().ChargeSharedAmmo(_sharedAmmo, Mathf.FloorToInt(_chargeTimer / _chargeDelay), true))
                    _chargeTimer -= _chargeDelay;
                gameObject.GetComponentInParent<WeaponSelect>().
                    ChargeSharedAmmo(_sharedAmmo, Mathf.FloorToInt(_chargeTimer / _chargeDelay));
            }
            else
                _ammo -= Mathf.FloorToInt(_chargeTimer / _chargeDelay);

            if (_muzzleFlash != null)
                Instantiate<GameObject>
                (
                    _muzzleFlash, 
                    _spawner.transform.position, 
                    Camera.main.transform.rotation
                )
                .transform.SetParent(transform, true);

            if (_bullet != null) // for (int i = 0; i < _burst; i++)
            {
                var b = Instantiate<GameObject>
                (
                    _bullet, 
                    _spawner.transform.position, 
                    Camera.main.transform.rotation // * Quaternion.Euler
                    // (
                    //     (Random.value - 0.5f) * _spread/2, 
                    //     (Random.value - 0.5f) * _spread,
                    //     0
                    // )
                );

                if (b.TryGetComponent(out OwnedProjectile proj))
                {
                    proj.Owner = Camera.main.transform.parent.parent;
                    proj.Damage = Mathf.FloorToInt(_chargeTimer / _chargeDelay) * _damageBase;
                    print($"Charged for {_chargeTimer} seconds, firing a {proj.Damage} damage bolt.");
                }
            }

            _animator.SetTrigger("MainFire");
            GameObject.Find("Crosshairs").GetComponent<Animator>().SetTrigger("Fire");
            GetComponentInParent<Movement>().Knockback(transform.forward * (_chargeTimer / _chargeDelay) * 0.5f);
            GetComponentInParent<PlayerMovement>().CamRecoil(_chargeTimer / _chargeDelay);
            _busy = true;
        }
    }

    // private IEnumerator Charge()
    // {
    //     yield return new WaitForSeconds(_chargeDelay);
    // }

    public bool AddAmmo(int amount) { return AddAmmo(amount, false); }
    public override bool AddAmmo(int amount, bool addAltAmmo)
    {
        if (_ammo >= _ammoMax)
            return false;

        print($"Adding ammo to {gameObject.name}");
        _ammo += amount;
        if (_ammo >= _ammoMax)
            _ammo  = _ammoMax;
        return true;
    }

    public override int  MainAmmo       { get => _ammo; }
    public override int  MainAmmoMax    { get => _ammoMax; }
    public override int  SharedMainAmmo { get => _sharedAmmo; }
    public          bool Busy           { get => _busy; set => _busy=value; }
}
