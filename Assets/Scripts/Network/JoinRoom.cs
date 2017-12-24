using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class JoinRoom : PunBehaviour
{

    public string VersionNum;
    //public GameObject LoadCamera;
    public Transform SpawnPoint;

    protected GameObject player;
    protected GameController gc;

    public void Init(GameController gameC)
    {
        gc = gameC;
    }

    private void Start()
    {
        PhotonNetwork.autoJoinLobby = true;
        PhotonNetwork.ConnectUsingSettings(VersionNum);
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    void OnPhotonRandomJoinFailed()
    {
        PhotonNetwork.CreateRoom(null);
    }

    public override void OnJoinedRoom()
    {
        player = PhotonNetwork.Instantiate("PlayerObject", SpawnPoint.position, SpawnPoint.rotation, 0);
        gc.NewPlayer(player.GetComponent<PlayerSync>().SetupReal());
    }

    void DisconnectAndClear()
    {
        PhotonNetwork.Disconnect();
    }

    void Reconnect()
    {
        if (!PhotonNetwork.connected)
        {
            PhotonNetwork.ConnectUsingSettings(VersionNum);
        }
    }

}
