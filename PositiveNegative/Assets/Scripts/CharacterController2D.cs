using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    public enum PlayerNumbers
    {
        Player1,
        Player2
    }
    public PlayerNumbers player;

    public float speed = 5f;
    public float jumpForce = 5f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.2f;

    private Rigidbody2D rb;
    private Collider2D playerCollider;
    private bool isGrounded;

    private string horizontalAxis;
    private KeyCode jumpButton;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();

        horizontalAxis = player.ToString() + "_Horizontal";

        if (player == PlayerNumbers.Player1)
        {
            jumpButton = KeyCode.Joystick1Button0;
        }
        else if (player == PlayerNumbers.Player2)
        {
            jumpButton = KeyCode.Joystick2Button0;
        }
    }

    private void Update()
    {
        float moveInput = Input.GetAxis(horizontalAxis);

        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if (isGrounded && Input.GetKeyDown(jumpButton))
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }

        isGrounded = CheckGrounded();
    }

    private bool CheckGrounded()
    {
        float extraHeight = 0.1f;
        RaycastHit2D hit = Physics2D.Raycast(playerCollider.bounds.center, Vector2.down, playerCollider.bounds.extents.y + extraHeight, groundLayer);
        return hit.collider != null;
    }
}




