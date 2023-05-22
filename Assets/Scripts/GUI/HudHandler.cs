using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class HudHandler : MonoBehaviour
{
    [SerializeField] private PlayerStatus    _playerStatus;
    [SerializeField] private WeaponSelect    _weapons;
    [SerializeField] private GameObject      _log;
    [SerializeField] private GameObject      _logEntry;
    [SerializeField] private TextMeshProUGUI _health;
    [SerializeField] private TextMeshProUGUI _ammo;

    private Sprite _weaponCrosshairs;
    // [SerializeField] private int             _logMaxLength  = 20;
    // [SerializeField] private float           _logDecayDelay = 60f;
    // private List<string> _logMessages   = new();
    // private float        _logDecayTimer = 0f;

    void Update()
    {
        string mainDisplay;
        string altDisplay;

        if (_weapons != null && _weapons.HasMainAmmo)
            mainDisplay = _weapons.CurrentMainAmmo.ToString("00");
        else
            mainDisplay = "--";
        
        if (_weapons != null && _weapons.HasAltAmmo)
            altDisplay = _weapons.CurrentAltAmmo.ToString("00");
        else
            altDisplay = "--";

        if (_playerStatus != null)
            _health.text = $"{_playerStatus.Armor:00}/{_playerStatus.Health:000}%";
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
