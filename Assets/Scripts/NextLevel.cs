using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        TextMesh playerName = other.gameObject.GetComponentInChildren<TextMesh>();
        GameManager.PlayerWin(playerName.text);
    }
}
