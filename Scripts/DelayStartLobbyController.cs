using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine;

public class DelayStartLobbyController : MonoBehaviourPunCallbacks
{


    [SerializeField]
    private GameObject delayStartButton;
    [SerializeField]
    private GameObject delayCancelButton;
    [SerializeField]
    private int roomSize;


    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        delayStartButton.SetActive(true);
    }


    public void DelayStart()
    {
        delayStartButton.SetActive(false);
        delayCancelButton.SetActive(false);
        PhotonNetwork.JoinRandomRoom();
        Debug.Log("Delay Start");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        CreateRoom();
    }

    private void CreateRoom()
    {
        Debug.Log("Creating room now");
        int randomNumber = UnityEngine.Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)roomSize};
        PhotonNetwork.CreateRoom("Room" + randomNumber, roomOps);
        Debug.Log(randomNumber);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create room... trying again");
        CreateRoom();
    }

    public void DelayCancel()
    {
        delayCancelButton.SetActive(false);
        delayStartButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }

    void Start()
    {
       
    }

    void Update()
    {
        
    }
}
