using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float rotateSpeed;
    public Rigidbody rig;
    public Transform cameraTransform;

    void Update()
    {
        Vector3 camForward = cameraTransform.right;
        Vector3 camHorizontal = cameraTransform.forward;

        float x = speed * Input.GetAxis("Horizontal");
        float z = speed * Input.GetAxis("Vertical");

        if (x != 0 || z != 0)
            rig.velocity += (camForward * x) + (camHorizontal * z);
        rig.velocity = Vector3.ClampMagnitude(rig.velocity, 20);
    }
}
