using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public float camSpeed;
    //private Transform boxingGlove;

    private PlayerController targetPlayer;

    private void Start()
    {
        SetRigParent(GameManager.instance.localPlayerObj);
    }

    private void Update()
    {
        Vector2 rightStick;
        if (Gamepad.current != null)
            rightStick = Gamepad.current.rightStick.ReadValue();
        else
        {
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");

            rightStick = new Vector2(x, y);
        }
        transform.Rotate(transform.up, rightStick.x * camSpeed);
        //transform.Rotate(transform.right, rightStick.y * camSpeed);
        //if (boxingGlove != null)
           // boxingGlove.transform.Rotate(transform.up, rightStick.x * camSpeed);
    }
    private void LateUpdate()
    {
        if (targetPlayer != null)
            transform.position = targetPlayer.transform.position;
    }

    public void SetRigParent (GameObject tr)
    {
        targetPlayer = tr.GetComponent<PlayerController>();
        //boxingGlove = targetPlayer.boxingGlove.transform;
    }
}
