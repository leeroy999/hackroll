using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class CreateAndJoinLobby : MonoBehaviourPunCallbacks
{

    public InputField NameInput;
    public InputField CreateInput;
    public InputField JoinInput;
    public Text ErrorText;
    public Text Information;

    private void Start()
    {
        PhotonNetwork.ConnectToRegion("asia");
    }
    private void Update()
    {
        Information.text = "AppVersion: " + PhotonNetwork.AppVersion + "\n"
        + "AuthValues: " + PhotonNetwork.AuthValues.AuthGetParameters + "\n"
        + "CloudRegion: " + PhotonNetwork.CloudRegion + "\n"
        + "CountOfPlayers: " + PhotonNetwork.CountOfPlayers.ToString() + "\n"
        + "CountOfPlayersOnMaster: " + PhotonNetwork.CountOfPlayersOnMaster.ToString() + "\n"
        + "CountOfPlayersInRooms: " + PhotonNetwork.CountOfPlayersInRooms.ToString() + "\n"
        + "BestRegionSummaryInPreferences: " + PhotonNetwork.BestRegionSummaryInPreferences + "\n"
        + "CurrentLobby: " + PhotonNetwork.CurrentLobby.Name + "\n"
        + "CurrentCluster: " + PhotonNetwork.CurrentCluster + "\n"
        + "PacketLoss: " + PhotonNetwork.PacketLossByCrcCheck.ToString() + "\n"
        + "PhotonServerSettings" + PhotonNetwork.PhotonServerSettings.name + "\n"
        + "ServerAddress" + PhotonNetwork.ServerAddress + "\n"
        + "Ping: " + PhotonNetwork.GetPing().ToString() + "\n";
    }
    public void CreateRoom()
    {
        if (!string.IsNullOrWhiteSpace(NameInput.text) && !string.IsNullOrWhiteSpace(CreateInput.text))
        {
            PhotonNetwork.NickName = NameInput.text;
            PhotonNetwork.CreateRoom(CreateInput.text);
        } else
        {
            ErrorText.text = "Must have a room name and player name!";
        }
    }

    public void JoinRoom()
    {
        if (!string.IsNullOrWhiteSpace(NameInput.text) && !string.IsNullOrWhiteSpace(JoinInput.text))
        {
            PhotonNetwork.NickName = NameInput.text;
            PhotonNetwork.JoinRoom(JoinInput.text);
        } else
        {
            ErrorText.text = "Must have a room name and player name!";
        }
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        ErrorText.text = message;
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        ErrorText.text = message;
    }

        public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }

    void LoadArena()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
        }
        Debug.LogFormat("PhotonNetwork : Loading Level : {0}", PhotonNetwork.CurrentRoom.PlayerCount);
        PhotonNetwork.LoadLevel("Game");
    }

    public override void OnPlayerEnteredRoom(Player other)
    {
        Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting


        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom


            LoadArena();
        }
    }


    public override void OnPlayerLeftRoom(Player other)
    {
        Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects


        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom


            LoadArena();
        }
    }
}
