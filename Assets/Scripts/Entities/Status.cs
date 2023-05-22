using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    [SerializeField] protected int      _maxHealth    = 100;
    [SerializeField] protected int      _health       = 100;
    [SerializeField] private GameObject _deathExplosion;
    [SerializeField] private int        _gibThreshold = 10;
    [SerializeField] private GameObject _gibExplosion;
    [SerializeField] private bool       _organic = false;

    private bool _isDead = false;

    public int Damage(int damage) { return Damage(damage, 1); }
    public virtual int Damage(int damage, float multiplier) //
    {
        _health -= Mathf.RoundToInt(damage * multiplier);
        if (_health <= 0)
        {
            Kill();
            return damage + _health;
        }
        return damage;
    }

    public bool Heal(int amount)
    {
        if (_health >= _maxHealth)
            return false;
        
        _health += amount;
        if (_health > _maxHealth)
            _health = _maxHealth;
        
        return true;
    }

    public virtual void Kill()
    {
        if (!_isDead)
        {
            _isDead = true;
            print(gameObject.name + " fucking died!");
            if (_gibExplosion != null && _health <= 0 - _gibThreshold)
                Instantiate(_deathExplosion, transform.position, transform.rotation);
            if (_deathExplosion != null)
                Instantiate(_deathExplosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
    
    public virtual bool Organic   { get => _organic; }

    public int  MaxHealth { get => _maxHealth; }
    public int  Health    { get => _health; }
    public bool IsDead    { get => _isDead; }
}
