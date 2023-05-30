using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    [HideInInspector] public bool playerHere;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) playerHere = true;
        Debug.Log(playerHere);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) playerHere = false;
    }
}