using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    public static int Level;
    public static int MaxLevel = 0;
    public static int SceneBuildInitial;
    public static int Health = 100;
    public static Vector2 SpawnPoint;
    public static int Portal;
    public static string PlayerName;
    public static int PlayerNo;
    public static Dictionary<string, int> Jealousy = new Dictionary<string, int>();
    public static Dictionary<string, int> ScoreBoard = new Dictionary<string, int>();
    

    public static Color[] PlayerColors = 
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

    [PunRPC]
    public static void Injured()
    {
        Health -= 10;
    }

    [PunRPC]
    public static void PlayerWin(string name)
    {
        Level += 1;
        int losers = PhotonNetwork.CurrentRoom.PlayerCount - 1;
        if (PlayerName == name)
        {
            Jealousy.Clear();
        } else
        {
            Jealousy[name] = Jealousy.ContainsKey(name) 
                ? Jealousy[name] + 1
                : 1; 
        }
        ScoreBoard[name] = Jealousy.ContainsKey(name) 
            ? ScoreBoard[name] + losers
            : losers; 
        int lvl = SceneBuildInitial + Level;
        Debug.Log("Win" + lvl.ToString());
        SceneManager.LoadScene(lvl);
    }
}
