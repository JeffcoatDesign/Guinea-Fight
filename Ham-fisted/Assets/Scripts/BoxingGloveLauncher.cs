using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BoxingGloveLauncher : MonoBehaviour
{
    public int id;
    public bool canHit = false;
    public float force;
    private void OnTriggerStay(Collider other)
    {
        if (!canHit)
            return;
        if (other.gameObject.CompareTag("Player"))
            other.gameObject.GetComponent<PlayerController>().photonView.RPC("GetHit", RpcTarget.All, transform.position, force, id);
    }
}
