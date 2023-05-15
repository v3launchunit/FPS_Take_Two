using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwnedProjectile : MonoBehaviour
{
    [SerializeField] protected int _damage = 10;

    protected Transform _owner;

    public void TransferOwnership(Transform target)
    {
        // if (target.TryGetComponent(out OwnedProjectile p))
        //     p.Owner = _owner;

        print($"{target.name} has {target.childCount} children");
        var children = target.GetComponentsInChildren<OwnedProjectile>();
        foreach(OwnedProjectile child in children)
        {
            child.Owner = _owner;
            print($"Ownership transfered to {child.transform.name}");
        }
        // for(int i = 0; i < target.childCount; i++)
        // {
        //     if (target.GetChild(i).TryGetComponent(out p))
        //         p.Owner = _owner;
        // }
    }

    public Transform Owner  { get => _owner;  set => _owner  = value; }
    public int       Damage { get => _damage; set => _damage = value; }
}
