using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SyringeHandler : WeaponBase
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private int        _chargeMax  = 10;
    [SerializeField] private int        _charge     =  0;
    [SerializeField] private int        _healAmount = 10;
    [SerializeField] private bool       _automatic  = true;
    [SerializeField] private float      _recoil     =  0;
    [SerializeField] private float      _camRecoil  =  5;

    [SerializeField] private Transform  _plunger;
    [SerializeField] private Vector3    _plungerEmptyPos;
    [SerializeField] private Vector3    _plungerFullPos;
    
    [SerializeField] private GameObject _spinBullet;

    [SerializeField] private bool _busy = false;

    private Animator   _animator;
    private Transform  _spawner;
    private Vector3    _plungerVel;
    private GameObject _spawnedBullet;

    void Awake()
    {
        _animator   = gameObject.GetComponent<Animator>();
        _spawner    = transform.Find("Spawner");

        if (_bullet == null)
            _chargeMax = -1;
    }

    void Update()
    {
        if 
        (
            (_bullet != null) &&
            (
                Input.GetButtonDown("Fire1") || 
                (Input.GetButton("Fire1")    && _automatic)
            ) &&
            !_busy
        )
        {
            if (_bullet != null)
            {
                _spawnedBullet = Instantiate<GameObject>
                (
                    _bullet, 
                    _spawner.transform.position, 
                    Camera.main.transform.rotation
                );

                if (_spawnedBullet.TryGetComponent(out OwnedProjectile proj))
                    proj.Owner = Camera.main.transform.parent.parent;
            }

            _animator.SetTrigger("MainFire");
            GameObject.Find("Crosshairs").GetComponent<Animator>().SetTrigger("Fire");
            GetComponentInParent<Movement>().Knockback(transform.forward * _recoil);
            GetComponentInParent<PlayerMovement>().CamRecoil(_camRecoil);
            _busy = true;
        }

        if (Input.GetButtonDown("Fire2") && _charge > 0 && !_busy)
        {      
            _animator.SetTrigger("AltFire");
            GameObject.Find("Crosshairs").GetComponent<Animator>().SetTrigger("Fire");
            _busy = true;
        }

        if (Input.GetButtonDown("Spin") && !_busy)
        {
            _animator.SetTrigger("Spin");
            GameObject.Find("Crosshairs").GetComponent<Animator>().SetTrigger("Fire");
            if (_spinBullet != null)
                Instantiate(_spinBullet, _spawner.transform.position, Camera.main.transform.rotation);
        }

        // _animator.SetBool("Depleted", !ChargeAmmo(1, false, true));

        _plunger.localPosition = Vector3.SmoothDamp
        (
            _plunger.localPosition, 
            Vector3.Lerp(_plungerEmptyPos, _plungerFullPos, (float)_charge/(float)_chargeMax), 
            ref _plungerVel, 
            0.3f
        );
    }

    public void CheckHitOrganic()
    {
        if (_spawnedBullet.TryGetComponent(out Hitscan scan) && scan.HitOrganic && _charge < _chargeMax)
            _charge++;
        
    }

    public void HealSelf()
    {
        GetComponentInParent<PlayerStatus>().Heal(_healAmount);
        GetComponentInParent<PlayerMovement>().CamRecoil(-45);
        _charge--;
    }

    public bool AddAmmo(int amount) { return AddAmmo(amount, false); }
    public override bool AddAmmo(int amount, bool addAltAmmo)
    {
        return false;
    }

    public override int  MainAmmo       { get => _charge; }
    public override int  MainAmmoMax    { get => _chargeMax; }
    public          bool Busy           { get => _busy; set => _busy=value; }
}
