using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterController2D : MonoBehaviour
{
    public enum PlayerNumber
    {
        Select,
        Player1,
        Player2
    }
    
    [Space()]

    public PlayerNumber player;

    [Header("Movement")]
    [Range(1,10)] public float speed = 5f;
    [Range(1,10)] public float jumpForce = 5f;
    private float MoveInput()
    {
        return Input.GetAxis(player.ToString() + "_Horizontal") * speed;
    }
    private bool JumpInput()
    {
        switch (player)
        {
            case PlayerNumber.Player1: return Input.GetKeyDown(KeyCode.Joystick1Button0);
            case PlayerNumber.Player2: return Input.GetKeyDown(KeyCode.Joystick2Button0);
            default: Debug.LogError(gameObject.name + " must have a player number assigned."); return false;
        }
    }

    [Header("Collision")]
    public LayerMask groundLayer;
    public Transform groundCheck;
    [Range(0,1)] public float groundCheckRadius = 0.2f;
    private bool Grounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckRadius);
        if (hit.collider != null) return hit.collider.CompareTag("Ground");
        else return false;
    }

    [Header("Components")]
    public Rigidbody2D rb;
    public Collider2D playerCollider;

    private void Awake()
    {
        if(rb == null) rb = GetComponent<Rigidbody2D>();
        if(playerCollider == null) playerCollider = GetComponent<Collider2D>();
    }

    private void LateUpdate()
    {
        rb.velocity = new Vector2(MoveInput(), rb.velocity.y);

        if (Grounded() && JumpInput())
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }
}