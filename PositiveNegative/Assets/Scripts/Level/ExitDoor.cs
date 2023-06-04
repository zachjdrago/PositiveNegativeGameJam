using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    [HideInInspector] public bool PlayerHere()
    {
        return playersHere.Count > 0;
    }
    private readonly List<GameObject> playersHere = new();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) playersHere.Add(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) playersHere.Remove(other.gameObject);
    }
}