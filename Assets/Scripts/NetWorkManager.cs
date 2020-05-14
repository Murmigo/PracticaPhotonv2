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
    public GameObject World;
    public Camera mainCamera;

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

    public string _roomName;
    public override void OnJoinedLobby()
    {
        RoomMenuScreen.SetActive(true);
        Debug.Log("Conectado al Lobby");
        _roomName = "room10";
    }

    public RoomOptions roomops;
    public void ConnecttoRoom()
    {

        if (RoomName.text.ToString().Length > 0)
            _roomName = RoomName.text.ToString();
        roomops = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 16 };
        PhotonNetwork.JoinOrCreateRoom(_roomName, roomops, TypedLobby.Default);
        World.SetActive(true);
        //PhotonNetwork.LoadLevel(1);

        //StartCoroutine(ActivarTrasEscena(roomops, _roomName));
    }

    public override void OnJoinedRoom()
    {
        //  RoomMenuScreen.SetActive(false);
        Debug.Log("Connected to Room " + _roomName);
        //PhotonNetwork.LoadLevel(1);
        ConnectedScreen.SetActive(false);
        RoomMenuScreen.SetActive(false);
        PhotonNetwork.Instantiate("Player", transform.position + new Vector3(Random.Range(1f, 2f), 0, Random.Range(1f, 2f)), Quaternion.identity);
        mainCamera.enabled = false;
    }


    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join the room " + message);
        if (!DisconnectedScreen.activeSelf)
        { DisconnectedScreen.SetActive(true); }
    }

    public void ActivarTrasEscena(RoomOptions rop, string rna)
    {
        
        //RoomMenuScreen.SetActive(false);
    }
}
