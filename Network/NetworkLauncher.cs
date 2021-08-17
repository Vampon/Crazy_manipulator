using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Runtime.Serialization.Formatters.Binary;
using DG.Tweening;
using UnityEngine.SceneManagement;
using LitJson;
using UnityEngine.Networking;



public class NetworkLauncher : MonoBehaviour
{

    [Header("UI")]
    public RectTransform Status;
    public RectTransform launcher;
    public Text connecting;
    public Text status;
    public Text palyerAndRoom;
    public Text info;
    public Text AllInfoView;
    public InputField PlayerName;
    public InputField RoomName;
    [Header("Pos")]
    public GameObject player1;
    public GameObject player2;
    private bool pos1, pos2;

    private string errorDialog;
    private double timeToClearDialog;
    private bool connectFailed = false;
    public string ErrorDialog
    {
        get { return this.errorDialog; }
        private set
        {
            this.errorDialog = value;
            if (!string.IsNullOrEmpty(value))
            {
                this.timeToClearDialog = Time.time + 4.0f;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings("1.0");

    }

    // Update is called once per frame
    void Update()
    {
        connecting.text = "Connecting" + GetConnectingDots();
        status.text = "Status: " + PhotonNetwork.connectionStateDetailed.ToString();
        palyerAndRoom.text = PhotonNetwork.countOfPlayers + " users are online in " + PhotonNetwork.countOfRooms + " rooms.";


        //if (!string.IsNullOrEmpty(ErrorDialog))
        //{
        //    GUILayout.Label(ErrorDialog);

        //    if (this.timeToClearDialog < Time.time)
        //    {
        //        this.timeToClearDialog = 0;
        //        ErrorDialog = "";
        //    }
        //}
    }


    public void GetRoomList()
    {
        if (PhotonNetwork.GetRoomList().Length == 0)
        {
            info.text += "Currently no games are available. ";
            info.text += "Rooms will be listed here, when they become available. ";
        }
        else
        {
            info.text += PhotonNetwork.GetRoomList().Length + " rooms available: ";
            // Room listing: simply call GetRoomList: no need to fetch/poll whatever!
            foreach (RoomInfo roomInfo in PhotonNetwork.GetRoomList())
            {
                info.text += roomInfo.Name + "   " + roomInfo.PlayerCount + "/" + roomInfo.MaxPlayers+" ";
                print("ROOM:" + roomInfo.Name + "   " + roomInfo.PlayerCount + "/" + roomInfo.MaxPlayers + " ");
            }
        }
        
    }

    public void Create_Room()
    {
        if (PlayerName.text != "")
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.IsVisible = true;
            roomOptions.maxPlayers = 2;
            PhotonNetwork.CreateRoom(RoomName.ToString(), roomOptions, TypedLobby.Default);
        }
        else
        {
            Debug.Log("用户名称为null");
        }
            
    }

  
    public void Join_Room()
    {
        if(PlayerName.text!="")
            PhotonNetwork.JoinRoom(RoomName.ToString());
    }

    void OnJoinedLobby()
    {
        ErrorDialog = "Successfully Join Lobby   ";
        AllInfoView.text += ErrorDialog;
        Debug.Log("Successfully Join Lobby");
        Invoke("GetRoomList", 2f);
        PlayerName.text = "Palyer" + PhotonNetwork.countOfPlayers.ToString();
    }

    public void Join_Random_Room()
    {
        if (PlayerName.text != "")
            PhotonNetwork.JoinRandomRoom();
        //PhotonNetwork.JoinRoom("MyTestRoom");
    }
    public void OnJoinedRoom()
    {
        Debug.Log("Welcome");
        
        launcher.DOLocalMove(new Vector3(-860, 0, 0), 1);
        Debug.Log(PhotonNetwork.GetRoomList());
        GameObject newarm;
        if (PhotonNetwork.room.PlayerCount==1)
        {
            newarm = PhotonNetwork.Instantiate("Robotic_arm_Net", player1.transform.position, Quaternion.identity, 0);
            newarm.transform.parent = GameObject.Find("ImageTarget").GetComponent<Transform>();
            newarm.transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);
        }
        else
        {
            newarm = PhotonNetwork.Instantiate("Robotic_arm_Net", player2.transform.position, Quaternion.identity, 0);
            newarm.transform.parent = GameObject.Find("ImageTarget").GetComponent<Transform>();
            newarm.transform.Rotate(0, 180, 0);
            newarm.transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);
        }  

        GameObject.Find("DOF0_1").GetComponent<LongPressButton>().MethodCallBack.AddListener(() =>
        {
            //GameObject.Find("Robotic_arm_Net").GetComponent<RoboticArm_Net>().part0.AddTorque(-50 *10 * GameObject.Find("Robotic_arm_Net").GetComponent<RoboticArm_Net>().part0.transform.forward);
        });
        GetRoomList();
    }



    public void OnConnectedToMaster() //这个地方没有被执行
    {
        ErrorDialog = "Successfully Connect to Master ";
        AllInfoView.text += ErrorDialog;
        Debug.Log("Successfully Connect to Master");
        PhotonNetwork.JoinLobby();
    }

    public void OnPhotonCreateRoomFailed()
    {
        Debug.Log("Create Failed");
    }

    public void OnPhotonRandomJoinFailed()
    {
        ErrorDialog = "Error: Can't create room (room name maybe already used). ";
        AllInfoView.text += ErrorDialog;
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsVisible = true;
        roomOptions.maxPlayers = 2;
        PhotonNetwork.CreateRoom(RoomName.ToString(), roomOptions, TypedLobby.Default);
    }

    public virtual void OnFailedToConnectToPhoton(DisconnectCause cause)
    {
        Debug.LogError("Cause: " + cause);
    }



    public void OnPhotonJoinRoomFailed(object[] cause)
    {
        ErrorDialog = "Error: Can't join room (full or unknown room name). " + cause[1];
        AllInfoView.text += ErrorDialog;
        Debug.Log("OnPhotonJoinRoomFailed got called. This can happen if the room is not existing or full or closed.");
    }


    public void OnCreatedRoom()
    {
        ErrorDialog = "OnCreatedRoom";
        AllInfoView.text += ErrorDialog;
        Debug.Log("OnCreatedRoom");
    }

    public void OnDisconnectedFromPhoton()
    {
        Debug.Log("Disconnected from Photon.");
    }

    public void OnFailedToConnectToPhoton(object parameters)
    {
        this.connectFailed = true;
        Debug.Log("OnFailedToConnectToPhoton. StatusCode: " + parameters + " ServerAddress: " + PhotonNetwork.ServerAddress);
    }

    public void backHome()
    {
        SceneManager.LoadScene(0);
    }

    string GetConnectingDots()
    {
        string str = "";
        int numberOfDots = Mathf.FloorToInt(Time.timeSinceLevelLoad * 3f % 4);

        for (int i = 0; i < numberOfDots; ++i)
        {
            str += " .";
        }

        return str;
    }

    


}
