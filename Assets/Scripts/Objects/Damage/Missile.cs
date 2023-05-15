using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Bullet
{
    private void Update()
    {
        // print(_collided);
        if (!_collided)
        {
            Vector3 angle = Camera.main.transform.rotation * Vector3.forward;
            if (Physics.Raycast(Camera.main.transform.position, angle, out _raycastHit, 1024, _mask))
                transform.LookAt(_raycastHit.point);
            else
                transform.LookAt(Camera.main.transform.position + 1024 * angle);

            gameObject.GetComponent<Rigidbody>().velocity = (transform.forward * _bulletSpeed);
        }

        if (Input.GetButtonDown("Fire2"))
            Detonate();
    }

    private void Detonate()
    {
        _collided = true;
         
        // Quaternion   rotation  = Quaternion.FromToRotation(Vector3.up, other.transform.localRotation);
        // Vector3      position  = other.transform.position;

        Instantiate(_explosion, transform.position, transform.rotation);

        Rigidbody bulletBody                        = gameObject.GetComponent<Rigidbody>();
        bulletBody.isKinematic                      = true;
        gameObject.GetComponent<Collider>().enabled = false;
        if (gameObject.TryGetComponent(out MeshRenderer mesh)) 
            mesh.enabled       = false;
        bulletBody.constraints = RigidbodyConstraints.FreezeAll;

        if (gameObject.TryGetComponent(out ParticleSystem particleSystem))
        {
            var emission     = particleSystem.emission;
            emission.enabled = false;
        }

        for (int i = 0; i < transform.childCount; i++ )
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        Destroy(gameObject, 0.25f);
    }
}
