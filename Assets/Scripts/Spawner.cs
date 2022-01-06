using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Spawner : MonoBehaviour
{
    public GameObject catPrefab;

    public float minX = -7.5f;
    public float maxX = -5f;
    public float fixedY = -3.25f;

    // Start is called before the first frame update
    public void Start()
    {
        Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), fixedY);
        PhotonNetwork.Instantiate(catPrefab.name, randomPosition, Quaternion.identity);
    }
}
