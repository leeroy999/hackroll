using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    private HashSet<string> stringSet = new HashSet<string>();

    public void addName(string name)
    {
        if (!stringSet.Contains(name))
        {
            stringSet.Add(name);
        }
    }

    public void deleteName(string name)
    {
        if (stringSet.Contains(name))
        {
            stringSet.Remove(name);
        }
    }

    public void activate(string name, Rigidbody2D body, Rigidbody2D alt, float power)
    {
        // float num = GameManager.JealousyCount(name, stringSet);
        float num = 50;
        float randomnum = Random.Range(0, 100);
        if (randomnum < num)
        {
            // alternate
            alt.AddForce(new Vector2(0f, power));
        }
        else 
        {
            // normal
            body.AddForce(new Vector2(0f, power));
        }
    }

}
