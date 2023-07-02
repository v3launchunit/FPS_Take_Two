using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : Status
{
    [SerializeField] private int    _maxArmor = 0;
    [SerializeField] private int    _armor    = 0;
    [SerializeField] private bool[] _keys     = {false, false, false, false, false, false};

    public override int Damage(int damage, float multiplier)
    {
        FindFirstObjectByType<Flash>().DoFlash(new Color(1, 0, 0, (float)damage/(float)_maxHealth));

        // print($"1-{_armor}/{_maxArmor} = {1 - ((float)_armor/(float)_maxArmor)}");
        _health -= Mathf.RoundToInt(damage * multiplier * (1 - ((float)_armor/(float)_maxArmor)));
        _armor  -= Mathf.RoundToInt(damage * multiplier * ((float)_armor/(float)_maxArmor));
        if (_armor <= 0)
            _armor = 0;
        if (_health <= 0)
        {
            if (_armor > 0)
            {
                _armor += _health - 1;
                if (_armor <= 0)
                    _armor = 0;
                _health = 1;
            }
            else
            {
                Kill();
                return damage + _health;
            }
        }
        return damage;
    }

    public bool AddArmor(int amount)
    {
        if (_armor >= _maxArmor)
            return false;
        
        _armor += amount;
        if (_armor > _maxArmor)
            _armor = _maxArmor;
        
        return true;
    }

    public bool AddKey(int index)
    {
        if (_keys[index])
            return false;
        
        _keys[index] = true;
        
        GameObject.Find("KeyPanel").transform.GetChild(index).gameObject.GetComponent<Image>().color = new(1, 1, 1);
        return true;
    }

    // public override void Kill()
    // {
    //     print("You just got fucking murdered!");
    // }
    
    public int    MaxArmor { get => _maxArmor; }
    public int    Armor    { get => _armor; }
    public bool[] Keys     { get => _keys; }
}
