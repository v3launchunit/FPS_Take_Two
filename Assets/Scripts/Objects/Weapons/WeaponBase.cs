using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class WeaponBase : MonoBehaviour
{
    [SerializeField] private string     _id;
    [SerializeField] private GameObject _icon;
    [SerializeField] private Sprite     _crosshairs;

    [SerializeField] private int _category;
    [SerializeField] private int _index;

    private GameObject _iconInstance;

    public void CreateIconInstance()
    {
        if (_icon != null)
        {
            if (GameObject.Find($"CategoryPanel_{_category}/{_icon.name}(Clone)"))
                _iconInstance = GameObject.Find($"CategoryPanel_{_category}/{_icon.name}(Clone)");
            else
            {
                _iconInstance = Instantiate(_icon, GameObject.Find($"CategoryPanel_{_category}").transform);
                _iconInstance.transform.SetSiblingIndex(_index);
            }
        }
    }

    void Awake()
    {
        CreateIconInstance();
    }

    void OnEnable()
    {
        if (_icon != null && _iconInstance == null)
        {
            CreateIconInstance();
        }
        if (_iconInstance != null)
        {
            _iconInstance.transform.SetSiblingIndex(_index);
            _iconInstance.GetComponent<Image>().color = new(1, 1, 1, 1);
        }
        if (_crosshairs != null)
            GameObject.Find("Crosshairs").GetComponent<Image>().sprite = _crosshairs;
    }

    void OnDisable()
    {
        // if (_icon != null && _iconInstance == null)
        // {
        //     _iconInstance = Instantiate(_icon, GameObject.Find($"CategoryPanel_{_category}").transform);
        //     _iconInstance.transform.SetSiblingIndex(_index);
        // }
        if (_iconInstance != null)
            _iconInstance.GetComponent<Image>().color = new(1, 1, 1, 0.25f);
    }

    public abstract bool AddAmmo(int amount, bool addAltAmmo);

    public      string Id             { get => _id; }
    public virtual int MainAmmo       { get => -1; }
    public virtual int MainAmmoMax    { get => -1; }
    public virtual int SharedMainAmmo { get => -1; }
    public virtual int AltAmmo        { get => -1; }
    public virtual int AltAmmoMax     { get => -1; }
    public virtual int SharedAltAmmo  { get => -1; }
    public         int Category       { get => _category; }
    public         int Index          { get => _index; }
}
