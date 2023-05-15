using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactDamage : OwnedProjectile
{
    [SerializeField] private int _selfDamage        = 10;
    [SerializeField] private int _movementKnockback = 5;

    private void OnTriggerEnter(Collider other)
    {
        print($"Owned by {_owner.name}");
        if (other.TryGetComponent(out Status status))
            status.Damage((_owner.name == other.transform.name) ? _selfDamage : _damage);
            // status.Damage(_damage);

        if (other.TryGetComponent(out Movement movement))
            movement.Knockback
            (
                _movementKnockback * 
                Vector3.Normalize(gameObject.transform.position - other.transform.position)
            );

        // print($"{_owner.name}'s explosion hit {other.gameObject.name}");
    }
}
