using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class WeaponHandler : WeaponBase
{
    [SerializeField] private GameObject _mainBullet;
    [SerializeField] private GameObject _mainMuzzleFlash;
    [SerializeField] private GameObject _mainFireSound;
    [SerializeField] private int        _mainBurst      =  1;
    [SerializeField] private float      _mainSpread     =  0;
    [SerializeField] private int        _mainAmmoMax    = 50;
    [SerializeField] private int        _mainAmmo       =  0;
    [SerializeField] private int        _sharedMainAmmo = -1;
    [SerializeField] private bool       _mainAutomatic  = true;
    [SerializeField] private float      _mainRecoil     = 0;
    [SerializeField] private float      _mainCamRecoil  = 5;

    [SerializeField] private GameObject _altBullet;
    [SerializeField] private GameObject _altMuzzleFlash;
    [SerializeField] private GameObject _altFireSound;
    [SerializeField] private int        _altBurst      =  1;
    [SerializeField] private float      _altSpread     =  0;
    [SerializeField] private int        _altAmmoMax    =  5;
    [SerializeField] private int        _altAmmo       =  0;
    [SerializeField] private int        _sharedAltAmmo = -1;
    [SerializeField] private bool       _altAutomatic  = false;
    [SerializeField] private float      _altRecoil     = 0;
    [SerializeField] private float      _altCamRecoil  = 5;
    
    [SerializeField] private GameObject _spinBullet;

    [SerializeField] private bool _busy = false;

    private Animator  _animator;
    private Transform _spawner;
    private Transform _altSpawner;

    void Awake()
    {
        _animator   = gameObject.GetComponent<Animator>();
        _spawner    = transform.Find("Spawner");
        _altSpawner = transform.Find("AltSpawner");

        if (_mainBullet == null && _mainMuzzleFlash == null)
            _mainAmmoMax = -1;
        
        if (_altBullet == null && _altMuzzleFlash == null && !gameObject.TryGetComponent(out ChargeAltFire _))
            _altAmmoMax = -1;

        // _mainAmmo = 0; // _mainAmmoMax;
        // _altAmmo  = 0; // _altAmmoMax;
    }

    void Update()
    {
        if 
        (
            (_mainBullet != null || _mainMuzzleFlash != null) &&
            (
                Input.GetButtonDown("Fire1") || 
                (Input.GetButton("Fire1")    && _mainAutomatic)
            )      &&
            !_busy && 
            ChargeAmmo(1, false) 
        )
        {
            if (_mainMuzzleFlash != null)
                Instantiate<GameObject>
                (
                    _mainMuzzleFlash, 
                    _spawner.transform.position, 
                    Camera.main.transform.rotation * Quaternion.Euler
                    (
                        0,
                        0,
                        (Random.value - 0.5f) * 45f
                    )
                )
                .transform.SetParent(transform, true);

            if (_mainBullet != null)
                for (int i = 0; i < _mainBurst; i++)
                {
                    var b = Instantiate<GameObject>
                    (
                        _mainBullet, 
                        _spawner.transform.position, 
                        Camera.main.transform.rotation * Quaternion.Euler
                        (
                            (Random.value - 0.5f) * _mainSpread/2, 
                            (Random.value - 0.5f) * _mainSpread,
                            0
                        )
                    );

                    if (b.TryGetComponent(out OwnedProjectile proj))
                        proj.Owner = Camera.main.transform.parent.parent;
                }

            if (_mainFireSound != null)
                Instantiate<GameObject>(_mainFireSound);

            _animator.SetTrigger("MainFire");
            GameObject.Find("Crosshairs").GetComponent<Animator>().SetTrigger("Fire");
            GetComponentInParent<Movement>().Knockback(transform.forward * _mainRecoil);
            GetComponentInParent<PlayerMovement>().CamRecoil(_mainCamRecoil);
            _busy = true;
        }

        if 
        (
            (_altBullet != null || _altMuzzleFlash != null) && 
            (
                Input.GetButtonDown("Fire2") || 
                (Input.GetButton("Fire2")    && _altAutomatic)
            )      &&
            !_busy && 
            ChargeAmmo(1, true)
        )
        {
            if (_altMuzzleFlash != null)
                Instantiate<GameObject>
                (
                    _altMuzzleFlash, 
                    _altSpawner.transform.position, 
                    Camera.main.transform.rotation
                )
                .transform.SetParent(transform, true);

            // Instantiate<GameObject>
            // (
            //     _altBullet, 
            //     _altSpawner.transform.position, 
            //     Camera.main.transform.rotation
            // );
            
            if (_altBullet != null)
                for (int i = 0; i < _altBurst; i++)
                {
                    var b = Instantiate<GameObject>
                    (
                        _altBullet, 
                        _altSpawner.transform.position, 
                        Camera.main.transform.rotation * Quaternion.Euler
                        (
                            (Random.value - 0.5f) * _altSpread/2, 
                            (Random.value - 0.5f) * _altSpread,
                            0
                        )
                    );

                    if (b.TryGetComponent(out OwnedProjectile proj))
                        proj.Owner = Camera.main.transform.parent.parent;
                }
            
            if (_altFireSound != null)
                Instantiate<GameObject>(_altFireSound);
            
            _animator.SetTrigger("AltFire");
            GameObject.Find("Crosshairs").GetComponent<Animator>().SetTrigger("Fire");
            GetComponentInParent<Movement>().Knockback(transform.forward * _altRecoil);
            GetComponentInParent<PlayerMovement>().CamRecoil(_altCamRecoil);
            _busy = true;
        }

        if (Input.GetButtonDown("Spin") && !_busy)
        {
            _animator.SetTrigger("Spin");
            GameObject.Find("Crosshairs").GetComponent<Animator>().SetTrigger("Fire");
            if (_spinBullet != null)
                Instantiate(_spinBullet, _spawner.transform.position, Camera.main.transform.rotation);
        }

        _animator.SetBool("Depleted", !ChargeAmmo(1, false, true));
    }

    public bool AddAmmo(int amount) { return AddAmmo(amount, false); }
    public override bool AddAmmo(int amount, bool addAltAmmo)
    {
        if (!addAltAmmo && _mainAmmo >= _mainAmmoMax)
            return false;
            
        if (addAltAmmo && _altAmmo >= _altAmmoMax)
            return false;

        if (!addAltAmmo)
        {
            print($"Adding main ammo to {gameObject.name}");
            _mainAmmo += amount;
            if (_mainAmmo >= _mainAmmoMax)
                _mainAmmo  = _mainAmmoMax;
            return true;
        }
        else
        {
            print($"Adding alt ammo to {gameObject.name}");
            _altAmmo += amount;
            if (_altAmmo >= _altAmmoMax)
                _altAmmo  = _altAmmoMax;
            return true;
        }
    }

    public bool ChargeAmmo(int amount) { return ChargeAmmo(amount, false); }
    public bool ChargeAmmo(int amount, bool chargeAltAmmo) { return ChargeAmmo(amount, chargeAltAmmo, false); }
    public bool ChargeAmmo(int amount, bool chargeAltAmmo, bool virtualCharge)
    {
        if (!chargeAltAmmo)
        {
            if (_mainAmmoMax == -1 || (_sharedMainAmmo < 0 && _mainAmmo > 0))
            {
                if (!virtualCharge)
                    _mainAmmo--;
                return true;
            }
            else if (_sharedMainAmmo != -1)
                return gameObject.GetComponentInParent<WeaponSelect>()
                    .ChargeSharedAmmo(_sharedMainAmmo, _mainAmmoMax, virtualCharge);
        }
        else
        {
            if (_altAmmoMax == -1 || (_sharedAltAmmo < 0 && _altAmmo > 0))
            {
                if (!virtualCharge)
                    _altAmmo--;
                return true;
            }
            else if (_sharedAltAmmo != -1)
                return gameObject.GetComponentInParent<WeaponSelect>()
                    .ChargeSharedAmmo(_sharedAltAmmo, _altAmmoMax, virtualCharge);
        }
        return false;
    }

    public override int  MainAmmo       { get => _mainAmmo; }
    public override int  MainAmmoMax    { get => _mainAmmoMax; }
    public override int  SharedMainAmmo { get => _sharedMainAmmo; }
    public override int  AltAmmo        { get => _altAmmo; } // set => _altAmmo=value;
    public override int  AltAmmoMax     { get => _altAmmoMax; }
    public override int  SharedAltAmmo  { get => _sharedAltAmmo; }
    public          bool Busy           { get => _busy; set => _busy=value; }
}
