using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class HudHandler : MonoBehaviour
{
    [SerializeField] private PlayerStatus    _playerStatus;
    [SerializeField] private WeaponSelect    _weapons;
    [SerializeField] private GameObject      _log;
    [SerializeField] private GameObject      _logEntry;
    [SerializeField] private TextMeshProUGUI _health;
    [SerializeField] private TextMeshProUGUI _ammo;
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

        // if (_logMessages.Count > 0)
        // {
        //     _logDecayTimer += Time.deltaTime;
        //     if (_logDecayTimer >= _logDecayDelay)
        //     {
        //         _logMessages.RemoveAt(0);
        //         _log.text = string.Join("\n", _logMessages);
        //         _logDecayTimer = 0;
        //     }
        // }
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
}
