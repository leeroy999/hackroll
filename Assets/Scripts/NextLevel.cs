using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        TextMesh playerName = other.gameObject.GetComponentInChildren<TextMesh>();
        
    }
}
