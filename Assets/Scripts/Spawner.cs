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
    public Transform Portal1;
    public Transform Portal2;
    private Color[] colors = 
    {
        Color.white,
        Color.cyan, 
        Color.yellow, 
        new Color(250, 180, 245, 255), // pink
        Color.gray, 
        new Color(0x6a, 0x75, 0x95, 0xFF), // light blue-ish
        new Color(233, 157, 173, 255), //light red
        new Color(157, 164, 233, 255), //light blue
    };

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
            CatPrefab.GetComponent<SpriteRenderer>().color = colors[playerCount - 1];
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

    [PunRPC]
    private void UpdateCount(int count)
    {
        PlayerCountText.text = "Players: " + count.ToString() + "/8";
    }
}
