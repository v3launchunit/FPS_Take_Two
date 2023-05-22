using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    [SerializeField] private   bool       _touchInteract = false;
    [SerializeField] private   bool       _shootInteract = false;
    [SerializeField] private   Sprite     _interactCrosshairs;
    [SerializeField] protected GameObject _interactSound;

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

    public Sprite InteractCrosshairs { get => _interactCrosshairs; }
}
