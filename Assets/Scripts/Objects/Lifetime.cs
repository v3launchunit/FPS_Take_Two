using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifetime : OwnedProjectile
{
    [SerializeField] private float      _lifetime = 5f;
    [SerializeField] private GameObject _decaySpawn;

    private void Start()
    {
        Destroy(gameObject, _lifetime);
    }

    private void OnDestroy()
    {
        if (!gameObject.scene.isLoaded) 
            return;
        
        if (_decaySpawn != null)
        {
            var e = Instantiate(_decaySpawn, transform.position, transform.rotation);
            TransferOwnership(e.transform);
            
            // if (e.TryGetComponent(out OwnedProjectile p))
        }
    }
}
