using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{

 
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom("test");

    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom("test");
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("MultiGamePlayScene");
    }



    void Start()
    {
        CreateRoom();
        JoinRoom();
    }

   
    void Update()
    {
        
    }
}
