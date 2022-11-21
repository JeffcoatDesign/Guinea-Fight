using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoxingGloveController : MonoBehaviour
{
    public Transform ballTransform;
    public float speed;

    private Transform cameraRig;

    private void Start()
    {
        cameraRig = Camera.main.transform.parent;
    }

    public void Update()
    {
        Quaternion angle = transform.rotation;
        transform.position = ballTransform.position;

        Vector2 leftStick;
        if (Gamepad.current != null)
            leftStick = Gamepad.current.leftStick.ReadValue();
        else
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            leftStick = new Vector2(x, y);
        }
        if (leftStick != null)
        {
            angle.eulerAngles = new Vector3 (transform.rotation.x, Mathf.Atan2(leftStick.y, leftStick.x) * Mathf.Rad2Deg, transform.rotation.z);
            angle.Set(angle.x, angle.y + cameraRig.rotation.y, angle.z, angle.w);
            transform.SetPositionAndRotation(transform.position, angle);
        }
    }
}
