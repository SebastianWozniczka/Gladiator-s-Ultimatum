using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.IO;
using UnityEngine;

public class GameSetupController : MonoBehaviour
{
   public GameObject controller;
    private PhotonView PV;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {


       //if (PV.IsMine)
      // {
            CreatePlayer();
          // PhotonNetwork.ConnectUsingSettings();
     //  }
    }

    private void CreatePlayer()
    {
        Debug.Log("Creating player");

       var r = UnityEngine.Random.Range(-4, 2);
        //  var ga = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "2Handed Warrior"), new Vector3(r,-4,0), Quaternion.identity);
        var player = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs",controller.name), new Vector3(r,-4,0), Quaternion.identity);
       
       

        SpawnPlayer();

    }

    public void SpawnPlayer()
    {

        // var r = UnityEngine.Random.Range(-4, 2);
        //GameObject player = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", controller.name), new Vector3(r, -4, 0), Quaternion.identity);
        //player = Instantiate(controller);
          GameObject player = Instantiate(controller);
        PhotonView photonView = player.GetComponent<PhotonView>();

        if (PhotonNetwork.AllocateViewID(photonView))
        {
            object[] data = new object[]
            {
            player.transform.position, player.transform.rotation, photonView.ViewID
            };

            RaiseEventOptions raiseEventOptions = new RaiseEventOptions
            {
                Receivers = ReceiverGroup.Others,
                CachingOption = EventCaching.AddToRoomCache
            };

            SendOptions sendOptions = new SendOptions
            {
                Reliability = true
            };

            //PhotonNetwork.RaiseEvent(CustomManualInstantiationEventCode, data, raiseEventOptions, sendOptions);
        }
        else
        {
            Debug.LogError("Failed to allocate a ViewId.");

            Destroy(player);
        }
    }

}



