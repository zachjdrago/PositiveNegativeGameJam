using UnityEngine;
using static Player;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector] public Player player;

    [Space()]

    [Header("Movement")]
    [Range(1,10)] public float speed = 5f;
    [Range(1,10)] public float jumpForce = 5f;
    private float MoveInput()
    {
        return Input.GetAxis(player.number.ToString() + "_Horizontal") * speed;
    }
    private bool JumpInput()
    {
        switch (player.number)
        {
            case PlayerNumber.Player1: return Input.GetKeyDown(KeyCode.Joystick1Button0);
            case PlayerNumber.Player2: return Input.GetKeyDown(KeyCode.Joystick2Button0);
            default: Debug.LogError(gameObject.name + " must have a player number assigned."); return false;
        }
    }

    [Header("Collision")]
    #pragma warning disable CS0108
    public Rigidbody2D rigidbody;
    public Collider2D collider;
#pragma warning restore CS0108

    [Space()]
    public Transform groundCheckOrigin;
    [Range(0,1)] public float groundCheckRadius = 0.1f;
    private bool Grounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheckOrigin.position, Vector2.down, groundCheckRadius);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Ground")) return true;
            else if (hit.collider.CompareTag("Positive")) return true;
            else if (hit.collider.CompareTag("Negative")) return true;
            else return false;
        }
        else return false;
    }

    private void LateUpdate()
    {
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(MoveInput(), player.GetComponent<Rigidbody>().velocity.y);

        if (Grounded() && JumpInput())
        {
            player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }
}