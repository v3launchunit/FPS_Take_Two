using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    [SerializeField] private bool _touchInteract = false;

    void OnCollisionEnter(Collision collision)
    {
        if (_touchInteract && collision.gameObject.name == "Player")
            OnInteract(collision.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (_touchInteract && other.gameObject.name == "Player")
            OnInteract(other.gameObject);
    }

    public abstract void OnInteract(GameObject other);
}
