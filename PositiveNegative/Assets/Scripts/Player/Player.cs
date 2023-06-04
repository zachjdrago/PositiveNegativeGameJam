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

    private void Awake()
    {
        if (movement == null) movement = GetComponent<PlayerMovement>();
        movement.player = this;
    }
}