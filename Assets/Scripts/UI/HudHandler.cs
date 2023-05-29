using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HudHandler : MonoBehaviour
{
    [SerializeField] private PlayerStatus    _playerStatus;
    [SerializeField] private WeaponSelect    _weapons;
    [SerializeField] private GameObject      _log;
    [SerializeField] private GameObject      _logEntry;
    [SerializeField] private TextMeshProUGUI _health;
    [SerializeField] private TextMeshProUGUI _ammo;
    [SerializeField] private float           _smoothTime = 0.5f;

    private Sprite _weaponCrosshairs;
    private float  _healthDisp   = 0;
    private float  _healthVel    = 0;
    private float  _armorDisp    = 0;
    private float  _armorVel     = 0;
    private float  _mainAmmoDisp = 0;
    private float  _mainAmmoVel  = 0;
    private float  _altAmmoDisp  = 0;
    private float  _altAmmoVel   = 0;

    void Update()
    {
        string mainDisplay;
        string altDisplay;

        if (_weapons != null && _weapons.HasMainAmmo)
        {
            _mainAmmoDisp = Mathf.SmoothDamp
            (
                _mainAmmoDisp, 
                _weapons.CurrentMainAmmo, 
                ref _mainAmmoVel, 
                _smoothTime
            );
            mainDisplay = Mathf.RoundToInt(_mainAmmoDisp).ToString("00");
        }
        else
            mainDisplay = "--";
        
        if (_weapons != null && _weapons.HasAltAmmo)
        {
            _mainAmmoDisp = Mathf.SmoothDamp
            (
                _altAmmoDisp, 
                _weapons.CurrentAltAmmo, 
                ref _altAmmoVel, 
                _smoothTime
            );
            altDisplay = Mathf.RoundToInt(_altAmmoDisp).ToString("00");
        }
        else
            altDisplay = "--";

        if (_playerStatus != null)
        {
            _armorDisp = Mathf.SmoothDamp
            (
                _armorDisp, 
                _playerStatus.Armor, 
                ref _armorVel, 
                _smoothTime
            );
            _healthDisp = Mathf.SmoothDamp
            (
                _healthDisp, 
                _playerStatus.Health, 
                ref _healthVel, 
                _smoothTime
            );
            _health.text = $"{Mathf.RoundToInt(_armorDisp):00}/{Mathf.RoundToInt(_healthDisp):000}%";
        }
        _ammo.text   = $"{mainDisplay}/{altDisplay}";

        if 
        (
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 3) &&
            hit.collider.gameObject.TryGetComponent(out InteractableObject target)                    &&
            target.InteractCrosshairs != null
        )
            GameObject.Find("Crosshairs").GetComponent<Image>().sprite = target.InteractCrosshairs;
        else
            GameObject.Find("Crosshairs").GetComponent<Image>().sprite = _weaponCrosshairs;

        GameObject.Find("BloodVignette").GetComponent<Image>().color = new
        (
            0.5f, 
            0, 
            0, 
            2 * (0.5f - ((float)_playerStatus.Health/(float)_playerStatus.MaxHealth))
        );
    }

    public void Log(string text)
    {
        // _log.text = text;
        // _logMessages.Add(text);
        // if (_logMessages.Count > _logMaxLength)
        //     _logMessages.RemoveAt(0);
        // _log.text = string.Join("\n", _logMessages);

        Instantiate(_logEntry, _log.transform).GetComponent<TextMeshProUGUI>().text = text;
    }

    public Sprite WeaponCrosshairs { get => _weaponCrosshairs; set => _weaponCrosshairs=value; }
}
