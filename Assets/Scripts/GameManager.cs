using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    public static HashSet<string> playerSet = new HashSet<string>();

    public static int Level;
    public static int MaxLevel = 1;
    public static int SceneBuildInitial;
    public static Vector2 SpawnPoint;
    public static int Portal;
    public static string PlayerName;
    public static int PlayerNo;
    public static Dictionary<string, Dictionary<string,int>> Jealousy = new Dictionary<string, Dictionary<string,int>>();
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

    public static void addPlayer(string name)
    {
        playerSet.Add(name);
    }

    public static void PlayerWin(string name)
    {
        Level += 1;
        int losers = PhotonNetwork.CurrentRoom.PlayerCount - 1;
        foreach (string playerName in playerSet)
        {
            if (playerName == name)
            {
                Jealousy[playerName] = new Dictionary<string, int>();
            } else
            {
                if (!Jealousy.ContainsKey(playerName))
                {
                    Jealousy[playerName] = new Dictionary<string, int>();
                }

                Jealousy[playerName][name] = Jealousy[playerName].ContainsKey(name)
                    ? Jealousy[playerName][name] + 1
                    : 1;
            }
        }

        // ScoreBoard[name] = Jealousy.ContainsKey(name) 
        //     ? ScoreBoard[name] + losers
        //     : losers; 
    }

    public static float JealousyCount(string name, HashSet<string> namesToCheck)
    {
        float count = 0;
        foreach (string nameCheck in namesToCheck)
        {
            if (Jealousy.ContainsKey(name))
            {
                count += Jealousy[name].ContainsKey(nameCheck)
                    ? Jealousy[name][nameCheck]
                    : 0;
            }
        }
        return count * 10 / namesToCheck.Count;
    }
}
