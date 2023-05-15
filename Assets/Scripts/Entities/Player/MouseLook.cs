using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private Transform _playerbody;
    [SerializeField] private float     _mouseSensitivity = 100f;
    [SerializeField] private float     _fovDesired       =  60f;

    private float _sensitivityMult = 1f;
    private float _pitch           = 0f;
    private float _pitchClampVel   = 0f;
    private Ray   _ray;

    void Start()
    {
        // Camera.main.fieldOfView = Globals.SETTINGS_FOV_DESIRED;
        Cursor.lockState        = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * _sensitivityMult * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * _sensitivityMult * Time.deltaTime;

        if ((_pitch - mouseY >= -90 && _pitch - mouseY <= 90) || !_playerbody.GetComponent<Movement>().Grounded)
            _pitch -= mouseY;
        if (_playerbody.GetComponent<Movement>().Grounded)
            _pitch  = Mathf.SmoothDampAngle(_pitch, Mathf.Clamp(_pitch, -90f, 90f), ref _pitchClampVel, 0.1f) % 360;

        transform.localRotation = Quaternion.Euler(_pitch, 0f, 0f);
        _playerbody.Rotate(Vector3.up * mouseX);        

        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetButtonDown("Interact") && Physics.Raycast(_ray, out RaycastHit hit, 5f, 1 << 6))
        {
            GameObject target = hit.collider.gameObject;
            if (target.TryGetComponent(out InteractableObject _)) // targetInteract
            {
                InteractableObject[] targets = target.GetComponents<InteractableObject>();

                foreach(InteractableObject t in targets)
                {
                    t.OnInteract(_playerbody.gameObject);
                }
            }
            // Debug.Log(target);
            // Debug.Log(targetInteract);
        }
    }

    public float FovDesired       { get => _fovDesired; }
    public float MouseSensitivity { get => _mouseSensitivity; }
    public float SensitivityMult  { get => _sensitivityMult; set => _sensitivityMult = value; }
}
