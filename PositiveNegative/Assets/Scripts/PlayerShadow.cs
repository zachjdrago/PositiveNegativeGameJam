using UnityEngine;
using static CharacterController2D;

public class PlayerShadow : MonoBehaviour
{
    public PlayerNumber targetPlayer;
    private Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.Find(targetPlayer.ToString()).transform;
    }

    void Update()
    {
        transform.position = new(-playerTransform.position.x, playerTransform.position.y);
    }
}