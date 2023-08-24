using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpectatorLook : MonoBehaviour
{
    [SerializeField] private float _mouseSensitivity = 300f;
    [SerializeField] private float _zoomSensitivity  = 300f;
    // [SerializeField] private float _moveSpeed        = 10f;

    private float _pitch = 0f;
    private float _yaw   = 0f;
    private float _timeScaleVel;

    void Update()
    {
        _pitch -= Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.unscaledDeltaTime;
        _yaw   += Input.GetAxis("Mouse X") * _mouseSensitivity * Time.unscaledDeltaTime;
        transform.localRotation = Quaternion.Euler(_pitch, _yaw, 5f);

        gameObject.GetComponent<Camera>().fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * _zoomSensitivity * Time.unscaledDeltaTime;

        // Vector3 moveX = transform.right   * Input.GetAxis("Horizontal")  * _moveSpeed * Time.unscaledDeltaTime;
        // Vector3 moveY = transform.up      * Input.GetAxis("3d Vertical") * _moveSpeed * Time.unscaledDeltaTime;
        // Vector3 moveZ = transform.forward * Input.GetAxis("Vertical")    * _moveSpeed * Time.unscaledDeltaTime;
        // transform.position += moveX + moveZ;
        
        Time.timeScale = Mathf.SmoothDamp(Time.timeScale, 0, ref _timeScaleVel, 1);

        if (Input.anyKeyDown)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(gameObject.scene.name);
        }
    }
}
