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
}
