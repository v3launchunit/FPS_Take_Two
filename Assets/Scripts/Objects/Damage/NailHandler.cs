using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NailHandler : OwnedProjectile
{
    [SerializeField] private GameObject _explosion;
    [SerializeField] private GameObject _decaySpawn;

    [SerializeField] private bool _detonated = false;

    void Update()
    {
        if (Input.GetButtonDown("Detonate") && !_detonated)
        {
            _detonated = true;
            if (_explosion != null)
            {
                var spawn = Instantiate(_explosion, transform.position, transform.rotation);
                TransferOwnership(spawn.transform);
            }
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        if (!this.gameObject.scene.isLoaded) 
            return;
            
        if (!_detonated)
        {
            if (TryGetComponent(out Collider c))
                c.enabled = false;
            var spawn = Instantiate(_decaySpawn, transform.position, Quaternion.LookRotation(Vector3.down));
            if (spawn.TryGetComponent(out Bullet b))
                b.Speed = 0;
            TransferOwnership(spawn.transform);
        }
    }
}
