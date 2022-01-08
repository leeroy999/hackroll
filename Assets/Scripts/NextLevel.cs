using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PhotonView))]
public class NextLevel : MonoBehaviourPun
{
    private PhotonView _view;
    private void Start()
    {
        _view = GetComponent<PhotonView>();
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);
        TextMesh playerName = other.gameObject.GetComponentInChildren<TextMesh>();
        Debug.Log("trigg");
        this.photonView.RPC("Win", RpcTarget.All, playerName.text);
        Destroy(this);
    }

    [PunRPC]
    private void Win(string name)
    {
        Debug.Log("win");
        GameManager.PlayerWin(name);
        int lvl = GameManager.SceneBuildInitial + GameManager.Level;
        Debug.Log("Win" + lvl.ToString() + name);
        SceneManager.LoadScene(lvl);
    }
}
