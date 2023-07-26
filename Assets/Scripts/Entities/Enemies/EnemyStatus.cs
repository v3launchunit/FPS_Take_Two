using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : Status
{
    [SerializeField] private float _flinchChance;

    // private EnemyMovement _movement;

    // void Start()
    // {
    //     _movement = gameObject.GetComponent<EnemyMovement>();
    // }

    public override int Damage(int damage, float multiplier)
    {
        _health -= Mathf.RoundToInt(damage * multiplier);
        if (_health <= 0)
        {
            Kill();
            return damage + _health;
        }
        else if (Random.value <= _flinchChance)
            gameObject.GetComponent<Animator>().SetTrigger("Flinch");
        
        return damage;
    }
}
