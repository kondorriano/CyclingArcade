using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class JoinRoom : PunBehaviour
{

    public string VersionNum;
    //public GameObject LoadCamera;
    public Transform SpawnPoint;

    GameObject player;

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
#if !SPECTATOR
        player = PhotonNetwork.Instantiate("PlayerObject", SpawnPoint.position, SpawnPoint.rotation, 0);
        player.GetComponent<PlayerSync>().SetupReal();
        //LoadCamera.SetActive(false);
#endif
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
