using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMover : MonoBehaviour
{
    public GameObject Mover;
    public GameObject Mace;
    private float _power = 500f;
    // Upon collision with another GameObject
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.transform.childCount > 0)
        {
            GameObject player = other.gameObject.transform.GetChild(1).gameObject;
            TextMesh playerName = new TextMesh();
            if (player.TryGetComponent<TextMesh>(out playerName))
            {
                playerName = player.GetComponent<TextMesh>();
            }
        }
        
        Rigidbody2D body = Mover.GetComponent<Rigidbody2D>();
        body.AddForce(new Vector2(0f, _power));
        Debug.Log("tes");
    }
}
