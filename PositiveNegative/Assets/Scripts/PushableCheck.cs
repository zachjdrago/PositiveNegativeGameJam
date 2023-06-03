using System.Collections;
using System.Collections.Generic;
using static Player;
using UnityEngine;
public class PushableCheck: MonoBehaviour
{
    [SerializeField] private GameObject allowedPrefab;
    private Rigidbody2D rb;
    private bool isBeingPushed;
    public PlayerNumber targetPlayer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static; // Set initial body type to static
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the colliding object is the allowed prefab
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<Player>().number == targetPlayer)
            {
                isBeingPushed = true;
                rb.bodyType = RigidbodyType2D.Dynamic; // Change body type to dynamic when pushed
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Check if the colliding object is the allowed prefab
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<Player>().number == targetPlayer)
            {
                isBeingPushed = false;
            }

            // Check if no allowed prefab is pushing the box
            if (!isBeingPushed)
            {
                rb.bodyType = RigidbodyType2D.Static; // Change body type back to static
            }
        }
    }
}

