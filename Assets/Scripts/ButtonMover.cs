using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMover : MonoBehaviour
{
    public GameObject Mover;
    // Upon collision with another GameObject
    private void OnCollisionEnter2D(Collision2D other)
    {
        Rigidbody2D body = Mover.GetComponent<Rigidbody2D>();
        body.AddForce(new Vector2(0f, 200f));
        TextMesh playerName = other.gameObject.GetComponentInChildren<TextMesh>();
        Debug.Log(playerName.text);
    }
}
