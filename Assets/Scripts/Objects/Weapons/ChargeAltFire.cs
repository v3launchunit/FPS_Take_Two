using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeAltFire : MonoBehaviour
{
    [SerializeField] private List<GameObject> _chargeBulletStages;
    [SerializeField] private List<GameObject> _chargeMuzzleFlashStages;
    // [SerializeField] private float            _stageIncrementDelay = 1f;
    [SerializeField] private bool             _overloadForceFire;
    [SerializeField] private bool             _charging;

    private WeaponHandler _weap;
    private float         _chargeTimer;
    private Animator      _animator;
    private Transform     _altSpawner;

    void Start()
    {
        _animator   = gameObject.GetComponent<Animator>();
        _altSpawner = transform.Find("AltSpawner");
        _weap       = gameObject.GetComponent<WeaponHandler>();
    }

    void Update()
    {        
        if (Input.GetButtonDown("Fire2") && !_weap.Busy)
            {
                _charging  = true;
                _weap.Busy = true;
            }

        if (Input.GetButton("Fire2") && _charging)
        {
            _chargeTimer += Time.deltaTime;
            _animator.SetFloat("ChargeTimer", _chargeTimer);
        }

        if ((Input.GetButtonUp("Fire2") || (_overloadForceFire && _chargeTimer >= _chargeBulletStages.Count - 1)) && _charging)
        {
            _charging = false;
            int i = Mathf.Min(Mathf.FloorToInt(_chargeTimer), _chargeBulletStages.Count - 1);

            if (_chargeMuzzleFlashStages[i] != null)
                Instantiate<GameObject>
                (
                    _chargeMuzzleFlashStages[i], 
                    _altSpawner.transform.position, 
                    Camera.main.transform.rotation
                )
                .transform.SetParent(transform, true);
            
            if (_chargeBulletStages[i] != null)
                Instantiate<GameObject>
                (
                    _chargeBulletStages[i], 
                    _altSpawner.transform.position, 
                    Camera.main.transform.rotation
                );

            _animator.SetTrigger("AltFire");
            // _weap.AltAmmo--;
            _weap.Busy = true;
        }
            
        // if (_weap.Busy) 
        //     _chargeTimer = 0;
    }
}
