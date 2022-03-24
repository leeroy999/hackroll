using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

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
            PhotonNetwork.CreateRoom(CreateInput.text.ToUpper());
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
            PhotonNetwork.JoinRoom(JoinInput.text.ToUpper());
        } else
        {
            ErrorText.text = "Must have a room name and player name!";
        }
    }

    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.CurrentRoom.Name);
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
}
