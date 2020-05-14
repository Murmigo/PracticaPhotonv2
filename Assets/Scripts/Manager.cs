using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviourPunCallbacks
{

    void Start()
    {

        //PhotonNetwork.Instantiate("Player", transform.position + new Vector3(Random.Range(1f, 2f), 0, Random.Range(1f, 2f)), Quaternion.identity);
        //PhotonNetwork.PlayerList;
        
        //PhotonNetwork.SendAllOutgoingCommands();
    }
}
