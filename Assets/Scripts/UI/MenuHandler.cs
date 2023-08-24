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
                transform.Find("Canvas/PauseMenu").gameObject.SetActive(true);
            }
            else
            {
                _menuLayer--;
                if (_menuLayer == 0)
                {
                    Unpause();
                }
            }
        }
    }

    void Unpause()
    {
        _menuLayer = 0;
        transform.Find("Canvas/PauseMenu").gameObject.SetActive(false);
        Time.timeScale   = _timeScale;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public int MenuLayer { get => _menuLayer; }
}
