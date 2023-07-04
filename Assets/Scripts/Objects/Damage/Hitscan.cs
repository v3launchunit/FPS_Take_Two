using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitscan : OwnedProjectile
{
    [SerializeField] private GameObject _explosion;
    [SerializeField] private float      _maxRange              = 100f;
    [SerializeField] private float      _lifetime              =  60f;
    [SerializeField] private float      _fadeSpeed             =   5f;
    [SerializeField] private float      _fadeDelay             =   1f;
    [SerializeField] private float      _launchFactorMovement  =   0.25f;
    [SerializeField] private float      _launchFactorRigidbody = -10f;
    [SerializeField] private bool       _targetCrosshairs      = true;
    [SerializeField] private bool       _targetTarget          = false;
    [SerializeField] private LayerMask  _layerMask;
    [SerializeField] private bool       _parrier;
    [SerializeField] private bool       _piercer;

    // private Ray        _ray;
    private RaycastHit _raycastHit;
    private Vector3    _impactPoint;
    private Transform  _hit;
    private float      _moveTimer         = 0f;
    private float      _fadeTimer         = 0f;
    private int        _scanEscapeCounter = 0;
    private Vector3    _scanStartingPosition;
    private Vector3    _firedFrom;
    private bool       _hitOrganic;

    // Start is called before the first frame update
    void Start()
    {
        _scanStartingPosition = _targetCrosshairs ? Camera.main.transform.position : (_targetTarget ? _owner.position : transform.position);
        Scan();
        
        // if (_explosion != null)
        //     Instantiate
        //     (
        //         _explosion, 
        //         _impactPoint, 
        //         Quaternion.FromToRotation(Vector3.up, _raycastHit.normal)
        //     )
        //     .transform.SetParent(_hit, true);

        Destroy(gameObject, _lifetime);

        _firedFrom = transform.position;
        // print($"{_owner.name}'s hitscan hit {_hit.gameObject.name}");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = (Vector3.Lerp
        (
            _firedFrom,
            _impactPoint,
            _moveTimer / Vector3.Distance(_firedFrom, _impactPoint) * 100
        ));

        if (_fadeDelay <= 0)
        {
            LineRenderer line = gameObject.GetComponent<LineRenderer>();
            line.SetPosition(0, Vector3.Lerp
            (
                _firedFrom,
                _impactPoint,
                _fadeTimer / Vector3.Distance(_firedFrom, _impactPoint) * _fadeSpeed
            ));
            if (line.GetPosition(0) == line.GetPosition(1)) // && _moveTimer > 5
                Destroy(gameObject);

            _fadeTimer += Time.deltaTime;
        }
        else _fadeDelay -= Time.deltaTime;

        _moveTimer += Time.deltaTime;
    }

    // private void LateUpdate()
    // {
    //     transform.position = _impactPoint;
    // }

    void Scan()
    {
        LineRenderer line = gameObject.GetComponent<LineRenderer>();

        Vector3 angle = transform.rotation * Vector3.forward;
        if 
        (Physics.Raycast(_scanStartingPosition, angle, out _raycastHit, _maxRange, _layerMask, QueryTriggerInteraction.Ignore))
        {
            _impactPoint = _raycastHit.point;
            _hit         = _raycastHit.transform;

            if (_parrier && _hit.TryGetComponent(out Bullet bullet)) bullet.Parry(_owner.gameObject);

            if (_hit.TryGetComponent(out Movement targetMovement))
                targetMovement.Knockback(Vector3.Normalize(gameObject.transform.position - _hit.position) * _launchFactorMovement);

            if (_hit.TryGetComponent(out Rigidbody targetBody))
                targetBody.AddForce(Vector3.Normalize(gameObject.transform.position - _hit.position) * _launchFactorRigidbody);

            if (_hit.TryGetComponent(out Status targetDamage)) 
            {
                if (targetDamage.IsDead)
                {
                    _scanStartingPosition = _impactPoint; // + transform.forward
                    _scanEscapeCounter++;
                    if (_scanEscapeCounter < 100)
                        Scan();

                    line.SetPosition(0, transform.position);
                    line.SetPosition(1, _impactPoint);
                    return;
                }

                _hitOrganic = targetDamage.Organic;

                _damage -= targetDamage.Damage(_damage);

                if (targetDamage.BloodSpray != null)
                    Instantiate(targetDamage.BloodSpray, _impactPoint, Quaternion.FromToRotation(Vector3.up, _raycastHit.normal));

                // if (_hit.TryGetComponent(out EnemyMovement enemyMovement))
                //     enemyMovement.Target = _owner;

                if (_piercer && _damage > 0)
                {
                    SpawnExplosion();
                    Scan();
                }
            }

            SpawnExplosion();
                // .transform.SetParent(_hit, true);
        }
        else _impactPoint = Camera.main.transform.position + _maxRange * (transform.rotation * Vector3.forward);

        line.SetPosition(0, transform.position);
        line.SetPosition(1, _impactPoint);

        print(_hitOrganic);
    }

    void SpawnExplosion()
    {
        if (_explosion != null)
        {
            var e = Instantiate
            (
                _explosion, 
                _impactPoint, 
                Quaternion.FromToRotation(Vector3.up, _raycastHit.normal)
            );
            TransferOwnership(e.transform);
        }
    }

    public bool HitOrganic { get => _hitOrganic; }
}
