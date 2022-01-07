using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviourPun
{
    public GameObject CatPrefab;
    public Text PlayerCountText;
    public Text Health;
    public Transform Portal1;
    public Transform Portal2;

    // Start is called before the first frame update
    public void Start()
    {
        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        this.photonView.RPC("UpdateCount", RpcTarget.All, playerCount);
        if (GameManager.Level == 0) {
            // spawn player
            Transform spawn = playerCount % 2 == 0 ? Portal2.transform : Portal1.transform;
            Vector2 position = new Vector2(spawn.position.x, spawn.position.y);
            GameManager.SpawnPoint = position;
            GameManager.Portal = playerCount % 2;
            GameManager.PlayerName = PhotonNetwork.NickName;
            GameManager.SceneBuildInitial = SceneManager.GetActiveScene().buildIndex;
            GameManager.PlayerNo = (playerCount - 1) % 8;
            PhotonNetwork.Instantiate(CatPrefab.name, position, Quaternion.identity);
        } 
        // else if (GameManager.Level < GameManager.MaxLevel) {
        //     Transform spawn = GameManager.Portal == 0 ? Portal2.transform : Portal1.transform;
        //     Vector2 position = new Vector2(spawn.position.x, spawn.position.y);
        //     GameManager.SpawnPoint = position;
        //     PhotonNetwork.Instantiate(CatPrefab.name, position, Quaternion.identity);
        // }

        // restrict to 8 players
        if (playerCount >= 8)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
        } else
        {
            PhotonNetwork.CurrentRoom.IsOpen = true;
        }
    }

    private void Update()
    {
        Health.text = "Health: " + GameManager.Health;
    }

    [PunRPC]
    private void UpdateCount(int count)
    {
        PlayerCountText.text = "Players: " + count.ToString() + "/8";
    }
}
