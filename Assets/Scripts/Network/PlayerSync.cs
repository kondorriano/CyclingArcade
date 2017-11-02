using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSync : MonoBehaviour
{

    [Header("Master Objects")]
    public GameObject RealHolder;
    public Transform RealPlayer;

    [Header("Dummy Objects")]
    public GameObject DummyHolder;
    public Transform DummyPlayer;

    //Internal Lerp Values
    Vector3 PlayerPos;
    Quaternion PlayerRot;

    PhotonView view;

    bool receivedInfo = false;

    private void Awake()
    {
        view = GetComponent<PhotonView>();
    }

    public void SetupReal()
    {
        RealHolder.SetActive(true);
        DummyHolder.SetActive(false);
    }

    private void Update()
    {
        //Lerp Body Positions
        if (!view.isMine && receivedInfo)
        {
            DummyPlayer.position = Vector3.Lerp(DummyPlayer.position, PlayerPos, Time.deltaTime * 9);
            DummyPlayer.rotation = Quaternion.Lerp(DummyPlayer.rotation, PlayerRot, Time.deltaTime * 9);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //Player Positions
            stream.SendNext(RealPlayer.position);
            stream.SendNext(RealPlayer.rotation);
        }
        else
        {
            //Body Positions
            PlayerPos = (Vector3)stream.ReceiveNext();
            PlayerRot = (Quaternion)stream.ReceiveNext();

            receivedInfo = true;

        }
    }

}
