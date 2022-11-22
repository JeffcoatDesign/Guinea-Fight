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

    private bool isGrounded;

    void Update()
    {
        CheckGround();

        //Vector3 camForward = cameraTransform.right;
        //Vector3 camHorizontal = cameraTransform.forward;

        Vector3 controllerInput = new Vector3 (Input.GetAxis("Horizontal") * speed, 0 , Input.GetAxis("Vertical") * speed);

        Vector3 moveDirection = FlattenCameraInput(controllerInput);

        if (isGrounded && controllerInput.magnitude != 0)
            rig.AddForce(moveDirection);
        //    rig.velocity += (camForward * x * Time.deltaTime) + (camHorizontal * z * Time.deltaTime);
        rig.velocity = Vector3.ClampMagnitude(rig.velocity, 20f);
    }

    void CheckGround ()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, 1.1f))
            isGrounded = true;
        else
            isGrounded = false;
    }

    Vector3 FlattenCameraInput (Vector3 input)
    {
        Quaternion flatten = Quaternion.LookRotation(-Vector3.up, cameraTransform.forward) * Quaternion.Euler(-90f, 0, 0);
        return flatten * input;
    }
}
