using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviourPun
{
    void Start()
    {
        PhotonNetwork.Instantiate("Player", transform.position + new Vector3(Random.Range(1f, 2f), 0, Random.Range(1f, 2f)), Quaternion.identity);
    }
}
