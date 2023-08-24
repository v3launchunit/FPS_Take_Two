using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    [SerializeField] protected int        _maxHealth    = 100;
    [SerializeField] protected int        _health       = 100;
    [SerializeField] protected GameObject _damageSound;
    [SerializeField] protected GameObject _bloodSpray;
    [SerializeField] private GameObject   _deathExplosion;
    [SerializeField] private int          _gibThreshold = 10;
    [SerializeField] private GameObject   _gibExplosion;
    [SerializeField] private Transform    _explosionPos;
    [SerializeField] private GameObject[] _loot;
    [SerializeField] private bool         _organic = false;

    private bool _isDead = false;

    void Start()
    {
        if (_explosionPos == null)
            _explosionPos = transform;
    }

    void Update()
    {

        if (_health <= 0)
        {
            Kill();
        }
    }

    public int Damage(int damage) { return Damage(damage, 1); }
    public virtual int Damage(int damage, float multiplier) //
    {
        if (_damageSound != null)
            Instantiate(_damageSound, transform);
        _health -= Mathf.RoundToInt(damage * multiplier);
        if (_health <= 0)
        {
            Kill();
            return damage + _health;
        }
        return damage;
    }

    
    public bool Heal(int amount) { return Heal(amount, false); }

    public bool Heal(int amount, bool canOverheal)
    {
        if (!canOverheal && _health >= _maxHealth)
            return false;
        
        _health += amount;
        if (!canOverheal && _health > _maxHealth)
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
                Instantiate(_deathExplosion, _explosionPos.position, _explosionPos.rotation);
            if (_deathExplosion != null)
                Instantiate(_deathExplosion, _explosionPos.position, _explosionPos.rotation);
            
            if (_loot.Length != 0)
            {
                Instantiate(_loot[Random.Range(0, _loot.Length)], _explosionPos.position, _explosionPos.rotation);
            }
            
            Destroy(gameObject);
        }
    }
    
    public virtual bool Organic  { get => _organic; }

    public int        MaxHealth  { get => _maxHealth; }
    public int        Health     { get => _health; }
    public bool       IsDead     { get => _isDead; }
    public GameObject BloodSpray { get => _bloodSpray; }
}
