using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkController : MonoBehaviourPunCallbacks
{
    private int RoomSize;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
         Debug.Log("We are now connected to the " + PhotonNetwork.CloudRegion + " server!");
          PhotonNetwork.AutomaticallySyncScene = true;
         PhotonNetwork.JoinOrCreateRoom("test", null, null);
        // PhotonNetwork.JoinRandomRoom();
      //  PhotonNetwork.JoinLobby();
        Debug.Log("Quick start");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        CreateRoom();
    }

    public override void OnJoinedLobby()
    {
        //SceneManager.LoadScene(4);
    }

     void CreateRoom()
    {
        Debug.Log("Create new room");
        int randomRoomNumber = UnityEngine.Random.Range(0, 1000);
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)RoomSize };
        PhotonNetwork.CreateRoom("Room" + randomRoomNumber, roomOps);
        PhotonNetwork.JoinRoom("Room");
        Debug.Log(randomRoomNumber);
    }

 
    void QuickCancel()
    {
        PhotonNetwork.LeaveRoom();
    }
}
