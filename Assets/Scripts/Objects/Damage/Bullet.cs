using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : OwnedProjectile
{
    [SerializeField] protected GameObject _explosion;
    [SerializeField] protected bool       _stickyExplosion  = false;
    [SerializeField] protected float      _bulletSpeed      = 500f;
    [SerializeField] private float        _lifetime         =  60f;
    [SerializeField] private bool         _destroyOnImpact  = true;
    [SerializeField] private bool         _explodeOnDecay   = false;
    // [SerializeField] private float        _linger           =   0.25f;
    [SerializeField] private float        _launchFactor     =   1;
    [SerializeField] protected bool       _targetCrosshairs = false;
    [SerializeField] protected bool       _targetTarget     = false;
    [SerializeField] protected LayerMask  _mask;
    [SerializeField] private   bool       _piercer;
    [SerializeField] private   bool       _bouncy;

    protected bool       _collided = false;
    protected RaycastHit _raycastHit;

    private void Start()
    {
        Vector3 angle = transform.rotation * Vector3.forward;
        if (_targetCrosshairs && Physics.Raycast(Camera.main.transform.position, angle, out _raycastHit, 1024, _mask))
            transform.LookAt(_raycastHit.point);
        else if (_targetCrosshairs)
            transform.LookAt(Camera.main.transform.position + 1024 * (transform.rotation * Vector3.forward));

        // else if (_targetTarget && Physics.Raycast(_owner.position, angle, out _raycastHit, 1024, _mask))
        //     transform.LookAt(_raycastHit.point);
        else if (_targetTarget)
            transform.LookAt(_owner.GetComponent<EnemyMovement>().Target);

        Rigidbody bulletBody = gameObject.GetComponent<Rigidbody>();

        bulletBody.AddForce(transform.forward * _bulletSpeed, ForceMode.VelocityChange);
        Destroy(gameObject, _lifetime);
    }

    public void Parry(GameObject parrier)
    {
        gameObject.layer = parrier.layer;
        _owner           = parrier.GetComponent<OwnedProjectile>().Owner;

        Vector3 angle = Camera.main.transform.rotation * Vector3.forward;
        if (Physics.Raycast(Camera.main.transform.position, angle, out _raycastHit, 1024, _mask))
            transform.LookAt(_raycastHit.point);
        else
            transform.LookAt(Camera.main.transform.position + 1024 * (transform.rotation * Vector3.forward));

        Rigidbody bulletBody = gameObject.GetComponent<Rigidbody>();

        bulletBody.AddForce(transform.forward * _bulletSpeed);
    }

    // private void Update()
    // {
    //     Rigidbody bulletBody = gameObject.GetComponent<Rigidbody>();
    // 
    //     bulletBody.AddForce(transform.forward * _rocketSpeed);
    // }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_collided)
        {
            _collided = true;

            ContactPoint contact   = collision.contacts[0];
            Quaternion   rotation  = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3      position  = contact.point;

            
            if (collision.gameObject.TryGetComponent(out Movement targetMovement))
                targetMovement.Knockback(Vector3.Normalize(gameObject.transform.position - collision.transform.position) * _launchFactor);

            if (collision.gameObject.TryGetComponent(out Status targetStatus)) 
            {
                targetStatus.Damage(_damage);

                // if (collision.gameObject.TryGetComponent(out EnemyMovement enemyMovement))
                //     enemyMovement.Target = _owner;
            }
            else if (_bouncy)
            {
                _collided = false;
                return;
            }

            var e = Instantiate(_explosion, position, rotation);
            TransferOwnership(e.transform);
            
            if (_stickyExplosion)
                e.transform.SetParent(collision.transform, true);

            Detonate();

            // print($"{_owner.name}'s bullet hit {collision.gameObject.name}");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_piercer) return;
        _collided = true;
         
        // Quaternion   rotation  = Quaternion.FromToRotation(Vector3.up, other.transform.localRotation);
        // Vector3      position  = other.transform.position;

        // Instantiate(_explosion, transform.position, transform.rotation).transform.SetParent(other.transform, true); // GameObject bulletSpawn = 
        var e = Instantiate(_explosion, transform.position, transform.rotation);
        TransferOwnership(e.transform);

        if (other.gameObject.TryGetComponent(out Movement targetMovement))
            targetMovement.Knockback(Vector3.Normalize(transform.position - other.transform.position) * _launchFactor);

        if (other.gameObject.TryGetComponent(out Status targetStatus)) 
        {
            _damage -= targetStatus.Damage(_damage);

            // if (other.gameObject.TryGetComponent(out EnemyMovement enemyMovement))
            //     enemyMovement.Target = _owner;

            if (_damage > 0)
            {
                _collided = false;
                return;
            }
        }

        Detonate();

        // print($"{_owner.name}'s bullet hit {other.gameObject.name}");
    }

    private void OnDestroy()
    {
        if (_explodeOnDecay && !_collided)
        {
            var e = Instantiate(_explosion, transform.position, transform.rotation);
            TransferOwnership(e.transform);
            
            // if (e.TryGetComponent(out OwnedProjectile p))
        }
    }

    public  void Detonate()
    {
        _collided = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.GetComponent<Collider>().enabled      = false;
        if (gameObject.TryGetComponent(out MeshRenderer mesh)) 
            mesh.enabled = false;
        // if (gameObject.TryGetComponent(out LineRenderer line)) 
        //     line.enabled = false;
        foreach (var line in gameObject.GetComponentsInChildren<LineRenderer>())
            line.enabled = false;
        foreach (var flare in gameObject.GetComponentsInChildren<LensFlareComponentSRP>())
            flare.enabled = false;

        Rigidbody bulletBody   = gameObject.GetComponent<Rigidbody>();
        bulletBody.constraints = RigidbodyConstraints.FreezeAll;

        if (gameObject.TryGetComponent(out ParticleSystem particleSystem))
        {
            var emission     = particleSystem.emission;
            emission.enabled = false;
        }

        if (_destroyOnImpact)
            Destroy(gameObject, 0.25f);
    }
    
    public float      Speed     { get => _bulletSpeed; set => _bulletSpeed = value; }
    public bool       Collided  { get => _collided; }
    public GameObject Explosion { get => _explosion; }
}
