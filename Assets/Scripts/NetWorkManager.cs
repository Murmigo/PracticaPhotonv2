using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetWorkManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update


    private static NetWorkManager instance = null;

    // Game Instance Singleton
    public static NetWorkManager Instance
    {
        get
        {
            return instance;
        }
    }

    public GameObject ConnectScreen;
    public GameObject ConnectedScreen;
    public GameObject DisconnectedScreen;
    public GameObject RoomMenuScreen;

    public InputField RoomName;
    public InputField CharacterName;

    public Color PlayerColor = Color.yellow;
    public Text PlayerName;

    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;

        DontDestroyOnLoad(this.gameObject);
    }


    public void Connect()
    {

        PhotonNetwork.ConnectUsingSettings();
    }



    public override void OnConnectedToMaster()
    {
        ConnectScreen.SetActive(false);
        ConnectedScreen.SetActive(true);
        PhotonNetwork.JoinLobby();

    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        if (!DisconnectedScreen.activeSelf)
        { DisconnectedScreen.SetActive(true); }
    }

    string _roomName;
    public override void OnJoinedLobby()
    {
        RoomMenuScreen.SetActive(true);

        _roomName = "room10";



    }


    public void ConnecttoRoom()
    {

        if (RoomName.text.ToString().Length > 0)
            _roomName = RoomName.text.ToString();
        RoomOptions roomops = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 16 };
        PhotonNetwork.JoinOrCreateRoom(_roomName, roomops, TypedLobby.Default);
        RoomMenuScreen.SetActive(false);
    }

    public override void OnJoinedRoom()
    {
        //  RoomMenuScreen.SetActive(false);
        Debug.Log("Connected to Room " + _roomName);
        PhotonNetwork.LoadLevel(1);
        //PhotonNetwo rk.Instantiate("Player", transform.position, Quaternion.identity);

    }


    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join the room " + message);
        if (!DisconnectedScreen.activeSelf)
        { DisconnectedScreen.SetActive(true); }
    }
}
