using JetBrains.Annotations;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DelayStartRoomController : MonoBehaviourPunCallbacks
{
    private PhotonView myPhotonView;

    [SerializeField]
    private int multiplayerSceneIndex;

    [SerializeField]
    private int menuSceneIndex;

    private int playerCount;
    private int roomSize;

    [SerializeField] 
    private int minPlayersToStart;

    [SerializeField]
    private Text roomCountDisplay;


    [SerializeField]
    private Text timerToStartDisplay;

    private bool readyToContDown;
    private bool readyToStart;
    private bool startingGame;

    private float timeToStartGame;
    private float notFullGameTimer;
    private float fullGameTimer;

    [SerializeField]
    private float maxWaitTime;
    [SerializeField]
    private float maxFullGametime;


    void Start()
    {
        myPhotonView = GetComponent<PhotonView>();
        fullGameTimer = maxFullGametime;
        notFullGameTimer = maxWaitTime;
        timeToStartGame = maxWaitTime;

        PlayerCountUpdate();

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsVisible = false;
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom("test", roomOptions, TypedLobby.Default);

    }

    private void PlayerCountUpdate()
    {
        playerCount = PhotonNetwork.PlayerList.Length;
        roomSize = PhotonNetwork.CurrentRoom.MaxPlayers;
        roomCountDisplay.text = playerCount + ":" + roomSize;

        if (playerCount == roomSize)
        {
            readyToStart = true;
        }

        else if (playerCount >= minPlayersToStart)
        {
            readyToContDown = true;
        }
        else
        {
            readyToContDown = false;
            readyToStart = false;
        }

    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        PlayerCountUpdate();

        if (PhotonNetwork.IsMasterClient)
        {
            myPhotonView.RPC("RPC_SendTimer", RpcTarget.Others, timeToStartGame);
        }
    }


    [PunRPC]
    public void RPC_SendTimer(float timeIn)
    {
        timeToStartGame = timeIn;
        notFullGameTimer = timeIn;
        if(timeIn < fullGameTimer)
        {
            fullGameTimer = timeIn;
        }

    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        PlayerCountUpdate();
    }



    void Update()
    {
        fullGameTimer -= Time.deltaTime;
        timeToStartGame -= Time.deltaTime;

        string tempTimer = string.Format("{0:00}", timeToStartGame);
        timerToStartDisplay.text = tempTimer;

        if (photonView.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                float r = UnityEngine.Random.Range(0f, 1f);
                float g = UnityEngine.Random.Range(0f, 1f);
                float b = UnityEngine.Random.Range(0f, 1f);

                Debug.Log(r + " " + g + " " + b);
                photonView.RPC("changeColour", RpcTarget.AllBuffered, r, g, b);
            }
        }


        //WaitingForMorePlayers();
    }

    [PunRPC]
    void changeColour(float r, float g, float b)
    {
        GetComponent<Renderer>().material.color = new Color(r, g, b, 1f);
    }

    private void WaitingForMorePlayers()
    {
       if(playerCount <= 1)
        {
            ResetTimer();
        }

        if (readyToStart)
        {
            fullGameTimer -= Time.deltaTime;
            timeToStartGame = fullGameTimer;
        }else if (readyToContDown)
        {
            notFullGameTimer -= Time.deltaTime;
            timeToStartGame = notFullGameTimer;
        }

        string tempTimer = string.Format("{0:00}", timeToStartGame);
        timerToStartDisplay.text = tempTimer;

        if(timeToStartGame <= 0)
        {
            if (startingGame)
                return;
            StartGame();
        }
    }

    private void StartGame()
    {
       startingGame = true;
        if (PhotonNetwork.IsMasterClient)
            return;
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.LoadLevel(multiplayerSceneIndex);
    }

    private void ResetTimer()
    {
        timeToStartGame = maxWaitTime;
        notFullGameTimer = maxWaitTime;
        fullGameTimer = maxFullGametime;
    }

    public void DelayCancel()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(menuSceneIndex);
    }
}
