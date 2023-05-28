using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.2f;

    private Rigidbody2D rb;
    private Collider2D playerCollider;
    private bool isGrounded;
    private bool isPlayer1;
    private bool isPlayer2;

    private string horizontalAxis;
    private string jumpButton;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        isPlayer1 = tag == "Player1";
        isPlayer2 = tag == "Player2";

        if (isPlayer1)
        {
            horizontalAxis = "Player1_Horizontal";
            jumpButton = "Player1_Jump";
        }
        else if (isPlayer2)
        {
            horizontalAxis = "Player2_Horizontal";
            jumpButton = "Player2_Jump";
        }
    }

    private void Update()
    {
        float moveInput = Input.GetAxis(horizontalAxis);

        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if (isGrounded && Input.GetButtonDown(jumpButton))
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




