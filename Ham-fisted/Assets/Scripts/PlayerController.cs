using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;

public class PlayerController : MonoBehaviourPun
{
    public float speed;
    public float rotateSpeed;
    public bool dead = false;
    public Rigidbody rig;
    public Transform cameraTransform;
    public bool isLocalPlayer = false;
    public int id;
    public Material mat;
    public Material boxingGloveMat;
    public MeshRenderer sphereBottom;
    public MeshRenderer boxingGlove;

    [HideInInspector]
    public Player photonPlayer;

    private bool isGrounded;
    private int livesLeft;

    private void Awake()
    {
        cameraTransform = Camera.main.transform.parent;
        livesLeft = GameManager.instance.playerLives;
    }

    [PunRPC]
    public void Initialize(Player player)
    {
        photonPlayer = player;
        id = photonPlayer.ActorNumber;
        GameManager.instance.players[id - 1] = this;
        SetColor(id - 1);

        if (!photonView.IsMine)
        {
            rig.isKinematic = true;
        }
        else
        {
            GameUI.instance.photonView.RPC("SpawnPlayerIcon", RpcTarget.AllBuffered, id);
            isLocalPlayer = true;
            CameraController.instance.SetRigParent(gameObject);
        }
    }

    void FixedUpdate()
    {
        if (!photonView.IsMine || dead)
            return;
        CheckGround();

        //Vector3 camForward = cameraTransform.right;
        //Vector3 camHorizontal = cameraTransform.forward;

        Vector3 controllerInput = new Vector3 (Input.GetAxis("Horizontal") * speed, 0 , Input.GetAxis("Vertical") * speed);

        Vector3 moveDirection = FlattenCameraInput(controllerInput);

        if (isGrounded && controllerInput.magnitude != 0)
            rig.AddForce(moveDirection, ForceMode.Force);
        //    rig.velocity += (camForward * x * Time.deltaTime) + (camHorizontal * z * Time.deltaTime);
        //rig.velocity = Vector3.ClampMagnitude(rig.velocity, 20f);

        if (transform.position.y < GameManager.instance.fallY)
            Respawn();
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

    [PunRPC]
    public void GetHit(Vector3 attackerPos, float force)
    {
        Debug.Log("Getting Hit");
        Vector3 launchDir = transform.position - attackerPos;
        rig.AddForce(launchDir * force, ForceMode.Impulse);
    }

    void Respawn ()
    {
        livesLeft -= 1;
        if (livesLeft <= 0)
            photonView.RPC("Die", RpcTarget.All);
        if (dead)
            return;
        rig.velocity = Vector3.zero;
        transform.position = transform.parent.position;
    }

    [PunRPC]
    public void Die ()
    {
        dead = true;
        GameManager.instance.alivePlayers -= 1;
        GameUI.instance.RemoveIcon(id);
        ChangeFocusedPlayer();

        if (PhotonNetwork.IsMasterClient)
            GameManager.instance.CheckWinCondition();
    }

    void SetColor (int index)
    {
        Color color = GameManager.instance.colors[index];
        mat = Instantiate(mat);
        boxingGloveMat = Instantiate(boxingGloveMat);
        boxingGloveMat.color = color;
        boxingGlove.material = boxingGloveMat;

        color.a = mat.color.a;
        mat.color = color;
        sphereBottom.material = mat;
    }

    void ChangeFocusedPlayer()
    {
        if (GameManager.instance.alivePlayers > 0)
            CameraController.instance.SetRigParent(GameManager.instance.players.First(x => !x.dead).gameObject);
    }
}
