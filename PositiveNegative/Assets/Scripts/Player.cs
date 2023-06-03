using UnityEngine;

public class Player : MonoBehaviour
{
    public enum PlayerNumber
    {
        Select,
        Player1,
        Player2
    }
    public PlayerNumber number;

    [Header("Player Script Components")]
    public PlayerMovement movement;

    [Header("Other Components")]
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
    public Rigidbody2D rigidbody;
    public Collider2D collider;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword

    private void Awake()
    {
        if (movement == null) movement = GetComponent<PlayerMovement>();
        movement.player = this;

        if (rigidbody == null) rigidbody = GetComponent<Rigidbody2D>();
        if (collider == null) collider = GetComponent<Collider2D>();
    }
}