using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public enum AmmoType
// {
//     Shells,
//     Slugs,
//     Nails,
//     Bombs,
//     Cells,
// }

public class WeaponSelect : MonoBehaviour
{
    [SerializeField] private List<GameObject> _weapons;
    [SerializeField] private List<GameObject> _weapons0;
    [SerializeField] private List<GameObject> _weapons1;
    [SerializeField] private List<GameObject> _weapons2;
    [SerializeField] private List<GameObject> _weapons3;
    [SerializeField] private List<GameObject> _weapons4;
    [SerializeField] private List<GameObject> _weapons5;
    [SerializeField] private List<GameObject> _weapons6;

    [SerializeField] private int        _selectedWeapon   = 0;
    [SerializeField] private int        _selectedCategory = 0;
    [SerializeField] private int[]      _selectedIndex    = { 0, 0, 0, 0, 0, 0, 0 };
    [SerializeField] private List<int>  _sharedAmmoMax;
    [SerializeField] private List<int>  _sharedAmmo; // = new()
    [SerializeField] private GameObject _rummageSound;

    void Start()
    {
        _weapons0.Sort(CompareWeaponIndicies);
        _weapons1.Sort(CompareWeaponIndicies);
        _weapons2.Sort(CompareWeaponIndicies);
        _weapons3.Sort(CompareWeaponIndicies);
        _weapons4.Sort(CompareWeaponIndicies);
        _weapons5.Sort(CompareWeaponIndicies);
        _weapons6.Sort(CompareWeaponIndicies);

        _weapons.AddRange(_weapons0);
        _weapons.AddRange(_weapons1);
        _weapons.AddRange(_weapons2);
        _weapons.AddRange(_weapons3);
        _weapons.AddRange(_weapons4);
        _weapons.AddRange(_weapons5);
        _weapons.AddRange(_weapons6);

        // _sharedAmmo.AddRange(_sharedAmmoMax);
        // _sharedAmmoMax.ForEach(delegate(int _) { _sharedAmmo.Add(0); });
    }

    static int CompareWeapons(GameObject x, GameObject y)
    {
        if (x.GetComponent<WeaponBase>().Category > y.GetComponent<WeaponBase>().Category)
            return 1;
            
        if (x.GetComponent<WeaponBase>().Category < y.GetComponent<WeaponBase>().Category)
            return -1;

        if (x.GetComponent<WeaponBase>().Index > y.GetComponent<WeaponBase>().Index)
            return 1;
            
        if (x.GetComponent<WeaponBase>().Index < y.GetComponent<WeaponBase>().Index)
            return -1;
        
        return 0;
    }

    static int CompareWeaponIndicies(GameObject x, GameObject y)
    {
        if (x.GetComponent<WeaponBase>().Index > y.GetComponent<WeaponBase>().Index)
            return 1;
            
        if (x.GetComponent<WeaponBase>().Index < y.GetComponent<WeaponBase>().Index)
            return -1;

        return 0;
    }

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
            SelectNext();
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
            SelectPrevious();

        if (Input.GetKeyDown(KeyCode.Alpha1))
            SelectCategory(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SelectCategory(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            SelectCategory(2);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            SelectCategory(3);
        if (Input.GetKeyDown(KeyCode.Alpha5))
            SelectCategory(4);
        if (Input.GetKeyDown(KeyCode.Alpha6))
            SelectCategory(5);
        if (Input.GetKeyDown(KeyCode.Alpha7))
            SelectCategory(6);
        // if (Input.GetKeyDown(KeyCode.Alpha8))
        //     SelectWeapon(7);
        // if (Input.GetKeyDown(KeyCode.Alpha9))
        //     SelectWeapon(8);
        // if (Input.GetKeyDown(KeyCode.Alpha0))
        //     SelectWeapon(9);
    }

    private void SelectWeapon(int index)
    {
        if (index > _weapons.Count - 1)
            return;

        _weapons[_selectedWeapon].SetActive(false);
        _selectedWeapon = index;
        _weapons[_selectedWeapon].SetActive(true);
        Instantiate(_rummageSound, transform.position, transform.rotation);
    }

    private void SelectCategory(int category)
    {
        switch (category)
        {
            default:
                if(_weapons0.Count <= 0)
                    return;
                break;
            case 1:
                if(_weapons1.Count <= 0) 
                    return;
                break;
            case 2:
                if(_weapons2.Count <= 0) 
                    return;
                break;
            case 3:
                if(_weapons3.Count <= 0) 
                    return;
                break;
            case 4:
                if(_weapons4.Count <= 0) 
                    return;
                break;
            case 5:
                if(_weapons5.Count <= 0) 
                    return;
                break;
            case 6:
                if(_weapons6.Count <= 0) 
                    return;
                break;
        }

        _weapons[_selectedWeapon].SetActive(false);
        Instantiate(_rummageSound, transform.position, transform.rotation);

        if (category == _selectedCategory)
            _selectedIndex[category]++;
        else
            _selectedCategory = category;

        switch (category)
        {
            default:
                if(_selectedIndex[0] >= _weapons0.Count) 
                    _selectedIndex[0] = 0;
                _selectedWeapon = _selectedIndex[0];
                _weapons0[_selectedIndex[0]].SetActive(true);
                break;
            case 1:
                if(_selectedIndex[1] >= _weapons1.Count) 
                    _selectedIndex[1] = 0;
                _selectedWeapon = _selectedIndex[1] + _weapons0.Count;
                _weapons1[_selectedIndex[1]].SetActive(true);
                break;
            case 2:
                if(_selectedIndex[2] >= _weapons2.Count) 
                    _selectedIndex[2] = 0;
                _selectedWeapon = _selectedIndex[2] + _weapons0.Count + _weapons1.Count;
                _weapons2[_selectedIndex[2]].SetActive(true);
                break;
            case 3:
                if(_selectedIndex[3] >= _weapons3.Count) 
                    _selectedIndex[3] = 0;
                _selectedWeapon = _selectedIndex[3] + _weapons0.Count + _weapons1.Count + _weapons2.Count;
                _weapons3[_selectedIndex[3]].SetActive(true);
                break;
            case 4:
                if(_selectedIndex[4] >= _weapons4.Count) 
                    _selectedIndex[4] = 0;
                _selectedWeapon = _selectedIndex[4] + _weapons0.Count + _weapons1.Count + _weapons2.Count + _weapons3.Count;
                _weapons4[_selectedIndex[4]].SetActive(true);
                break;
            case 5:
                if(_selectedIndex[5] >= _weapons5.Count) 
                    _selectedIndex[5] = 0;
                _selectedWeapon = _selectedIndex[5] + _weapons0.Count + _weapons1.Count + _weapons2.Count + _weapons3.Count + _weapons4.Count;
                _weapons5[_selectedIndex[5]].SetActive(true);
                break;
            case 6:
                if(_selectedIndex[6] >= _weapons6.Count) 
                    _selectedIndex[6] = 0;
                _selectedWeapon = _selectedIndex[6] + _weapons0.Count + _weapons1.Count + _weapons2.Count + _weapons3.Count + _weapons4.Count + _weapons5.Count;
                _weapons6[_selectedIndex[6]].SetActive(true);
                break;
        }
    }

    private void SelectNext()
    {
        _weapons[_selectedWeapon].SetActive(false);
        _selectedWeapon++;
        if(_selectedWeapon >= _weapons.Count) _selectedWeapon = 0;
        _weapons[_selectedWeapon].SetActive(true);
        Instantiate(_rummageSound, transform.position, transform.rotation);
    } 

    private void SelectPrevious()
    {
        _weapons[_selectedWeapon].SetActive(false);
        _selectedWeapon--;
        if(_selectedWeapon < 0) _selectedWeapon = _weapons.Count - 1;
        _weapons[_selectedWeapon].SetActive(true);
        Instantiate(_rummageSound, transform.position, transform.rotation);
    }

    public bool AddWeapon(GameObject weapon) { return AddWeapon(weapon, 0, 0); }
    public bool AddWeapon(GameObject weapon, int startingMainAmmo) { return AddWeapon(weapon, startingMainAmmo, 0); }
    public bool AddWeapon(GameObject weapon, int startingMainAmmo, int startingAltAmmo)
    {
        foreach (GameObject weap in _weapons)
        {
            var w = weap.GetComponent<WeaponBase>();
            if (w.Id == weapon.GetComponent<WeaponBase>().Id)
                return 
                (
                    (w.SharedMainAmmo == -1 && w.AddAmmo(startingMainAmmo, false))          |
                    (w.SharedMainAmmo != -1 && AddAmmo(w.SharedMainAmmo, startingMainAmmo)) |
                    (w.SharedAltAmmo  == -1 && w.AddAmmo(startingAltAmmo, true))            |
                    (w.SharedAltAmmo  != -1 && AddAmmo(w.SharedAltAmmo, startingAltAmmo))
                );
            
            // return w.AddAmmo(startingMainAmmo, false) | w.AddAmmo(startingAltAmmo, true);
        }

        var newWeapon  = Instantiate(weapon, transform);
        var newHandler = weapon.GetComponent<WeaponBase>();
        int category   = newHandler.Category;
        int index      = newHandler.Index;
        _weapons.Add(newWeapon);

        switch (category)
        {
            default:
                _weapons0.Add(newWeapon);
                _weapons0.Sort(CompareWeaponIndicies);
                break;
            
            case 1:
                _weapons1.Add(newWeapon);
                _weapons1.Sort(CompareWeaponIndicies);
                break;

            case 2:
                _weapons2.Add(newWeapon);
                _weapons2.Sort(CompareWeaponIndicies);
                break;
                
            case 3:
                _weapons3.Add(newWeapon);
                _weapons3.Sort(CompareWeaponIndicies);
                break;
                
            case 4:
                _weapons4.Add(newWeapon);
                _weapons4.Sort(CompareWeaponIndicies);
                break;
                
            case 5:
                _weapons5.Add(newWeapon);
                _weapons5.Sort(CompareWeaponIndicies);
                break;
                
            case 6:
                _weapons6.Add(newWeapon);
                _weapons6.Sort(CompareWeaponIndicies);
                break;
        }
        
        if (newHandler.SharedMainAmmo != -1)
            AddAmmo(newHandler.SharedMainAmmo, startingMainAmmo);
        else
            AddAmmo(newHandler.Id, startingMainAmmo, false);

        if (newHandler.SharedAltAmmo != -1)
            AddAmmo(newHandler.SharedAltAmmo, startingAltAmmo);
        else
            AddAmmo(newHandler.Id, startingAltAmmo, true);

        var oldHandler = _weapons[_selectedWeapon].GetComponent<WeaponBase>();
        _weapons.Sort(CompareWeapons);
        if (_weapons[_selectedWeapon].GetComponent<WeaponBase>() != oldHandler)
            _selectedWeapon++;
        newHandler.CreateIconInstance();
        // SelectCategory(category);

        return true;
    }

    public bool AddAmmo(string weapon, int amount) { return AddAmmo(weapon, amount, false); }
    public bool AddAmmo(string weapon, int amount, bool addAltAmmo)
    {
        // for (int i = 0; i <= _weapons.Count; i++ )
        foreach (GameObject weap in _weapons)
        {
            var w = weap.GetComponent<WeaponBase>();
            if (w.Id == weapon)
                return w.AddAmmo(amount, addAltAmmo);
            // if (weap.TryGetComponent(out WeaponHandler w) && w.Id == weapon)
            // {
            //     return w.AddAmmo(amount, addAltAmmo);
            // }
            // else
            // {
            //     var d = weap.GetComponent<DualWieldHandler>();
            //     return d.AddAmmo(amount, addAltAmmo);
            // }
        }

        return false;
    }
    public bool AddAmmo(int i, int amount)
    {
        if (_sharedAmmo[i] >= _sharedAmmoMax[i])
            return false;
        
        _sharedAmmo[i] += amount;
        if (_sharedAmmo[i] >= _sharedAmmoMax[i])
            _sharedAmmo[i] = _sharedAmmoMax[i];
        return true;
    }

    public bool ChargeSharedAmmo(int index, int amount) { return ChargeSharedAmmo(index, amount, false); }
    public bool ChargeSharedAmmo(int index, int amount, bool virtualCharge)
    {
        if (_sharedAmmo[index] >= amount)
        {
            if (!virtualCharge)
                _sharedAmmo[index] -= amount;
            return true;
        }
        return false;
    }

    public string SelectedWeapon { get => _weapons[_selectedWeapon].GetComponent<WeaponBase>().Id; }
    public int CurrentMainAmmo   
    {
        get // => _weapons[_selectedWeapon].GetComponent<WeaponBase>().MainAmmo;
        {
            var w = _weapons[_selectedWeapon].GetComponent<WeaponBase>();
            if (w.SharedMainAmmo == -1)
                return w.MainAmmo;
            return _sharedAmmo[w.SharedMainAmmo];
        }
            // (_weapons[_selectedWeapon].TryGetComponent(out WeaponHandler w)) ?
            //     w.MainAmmo : _weapons[_selectedWeapon].GetComponent<DualWieldHandler>().MainAmmo;
    }
    public int CurrentAltAmmo 
    { 
        get // => _weapons[_selectedWeapon].GetComponent<WeaponBase>().AltAmmo;
        {
            var w = _weapons[_selectedWeapon].GetComponent<WeaponBase>();
            if (w.SharedAltAmmo == -1)
                return w.AltAmmo;
            return _sharedAmmo[w.SharedAltAmmo];
        }
            // (_weapons[_selectedWeapon].TryGetComponent(out WeaponHandler w)) ?
            //     w.AltAmmo : _weapons[_selectedWeapon].GetComponent<DualWieldHandler>().AltAmmo;
    }
    public bool HasMainAmmo
    {
        get => _weapons[_selectedWeapon].GetComponent<WeaponBase>().MainAmmoMax != -1;
            // (_weapons[_selectedWeapon].TryGetComponent(out WeaponHandler w)) ?
            //     w.MainAmmoMax != -1 : _weapons[_selectedWeapon].GetComponent<DualWieldHandler>().MainAmmoMax != -1;
    }
    public bool HasAltAmmo
    {
        get // => _weapons[_selectedWeapon].GetComponent<WeaponBase>().AltAmmoMax != -1;
        {
            var w = _weapons[_selectedWeapon].GetComponent<WeaponBase>();
            if (w.SharedAltAmmo != -1 && w.SharedAltAmmo == w.SharedMainAmmo)
                return false;
            return w.AltAmmoMax != -1;
        }
            // (_weapons[_selectedWeapon].TryGetComponent(out WeaponHandler w)) ?
            //     w.AltAmmoMax != -1 : _weapons[_selectedWeapon].GetComponent<DualWieldHandler>().AltAmmoMax != -1;
    }
}
