using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public Activator activator;
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.transform.childCount > 0)
        {
            GameObject player = other.gameObject.transform.GetChild(1).gameObject;
            TextMesh playerName = new TextMesh();
            if (player.TryGetComponent<TextMesh>(out playerName))
            {
                playerName = player.GetComponent<TextMesh>();
            }
            activator.addName(playerName.text);
        }
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.transform.childCount > 0)
        {
            GameObject player = other.gameObject.transform.GetChild(1).gameObject;
            TextMesh playerName = new TextMesh();
            if (player.TryGetComponent<TextMesh>(out playerName))
            {
                playerName = player.GetComponent<TextMesh>();
            }
            activator.deleteName(playerName.text);
        }
    }
}
