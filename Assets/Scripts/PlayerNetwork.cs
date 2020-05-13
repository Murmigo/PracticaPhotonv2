using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerNetwork : MonoBehaviour
{
    public MonoBehaviour[] scripts;

    PhotonView pview;

    void Start(){

        pview = GetComponent<PhotonView>();

        if (!pview.IsMine)
        {

            foreach (var script in scripts)

            {

                script.enabled = false;

            }
        }
    }

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
        }
        else if (stream.IsReading)
            transform.position = (Vector3)stream.ReceiveNext();
    }
}
