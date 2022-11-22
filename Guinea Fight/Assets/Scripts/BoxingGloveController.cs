using System;
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
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 controllerInput = new Vector3(0, FindAngle(x, z), 0);

        if (x != 0 || z != 0)
            RotateTo(FlattenInput(controllerInput));

        transform.position = ballTransform.position;
    }

    Quaternion FlattenInput(Vector3 input)
    {
        Quaternion flatten = Quaternion.LookRotation(cameraRig.forward, Vector3.up) * Quaternion.Euler(input);
        return flatten;
    }

    float FindAngle (float x, float y)
    {
        float value = (float)(Mathf.Atan2(x, y) / Math.PI) * 180f;
        if (value < 0)
            value += 360;
        return value;
    }

    void RotateTo (Quaternion target)
    {
        Quaternion total = transform.rotation;
        float cr = transform.rotation.eulerAngles.y;
        float tr = target.eulerAngles.y;
        float angleDifference = Modulo(tr - cr, 360f);
        if (cr > tr + 5 || cr < tr - 5)
        {
            if (angleDifference > 180)
            {
                transform.Rotate(transform.rotation.x, 360 - (speed * Time.deltaTime), transform.rotation.z);
            }
            else
            {
                transform.Rotate(transform.rotation.x, 0 + (speed * Time.deltaTime), transform.rotation.z);
            }
        }
    }

    float Modulo (float x, float m)
    {
        return (x % m + m) % m;
    }
}
