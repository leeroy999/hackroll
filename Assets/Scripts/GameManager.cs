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
    public static Dictionary<string, int> Jealousy = new Dictionary<string, int>();
    public static Dictionary<string, int> ScoreBoard = new Dictionary<string, int>();
    

    public static void PlayerWin(string name)
    {
        Level += 1;
        if (PlayerName == name)
        {
            Jealousy.Clear();
        } else
        {
            Jealousy[name] = Jealousy.ContainsKey(name) 
                ? Jealousy[name] + 1
                : 1; 
        }
        int lvl = SceneBuildInitial + Level;
        Debug.Log("Win" + lvl.ToString());
        SceneManager.LoadScene(lvl);
    }
}
