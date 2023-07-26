using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    private int   _menuLayer = 0;
    private float _timeScale;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_menuLayer == 0)
            {
                _timeScale = Time.timeScale;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
                _menuLayer++;
            }
            else
            {
                _menuLayer--;
                if (_menuLayer == 0)
                {
                    Time.timeScale = _timeScale;
                Cursor.lockState = CursorLockMode.Locked;
                }
            }
        }
    }
}
