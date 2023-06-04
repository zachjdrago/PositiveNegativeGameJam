using UnityEngine;
using static Player;

public class CameraController : MonoBehaviour
{
    public PlayerNumber targetPlayer;
    private Transform playerTransform;

    public float panSpeed;
    private float PanSpeed(float distance)
    {
        return panSpeed * distance * Time.deltaTime;
    }

    private void Start()
    {
        playerTransform = GameObject.Find(targetPlayer.ToString()).transform;
        transform.position = new(playerTransform.position.x, playerTransform.position.y + 3, transform.position.z);
    }

    private void FixedUpdate()
    {
        CameraMove();
    }

    private void CameraMove()
    {
        Vector3 offsetPosition = new(playerTransform.position.x, playerTransform.position.y + 3, transform.position.z);
        float distance = (transform.position - offsetPosition).magnitude;

        if (distance > 2)
        {
            float newX = Mathf.Lerp(transform.position.x, offsetPosition.x, PanSpeed(distance));
            float newY = Mathf.Lerp(transform.position.y, offsetPosition.y, PanSpeed(distance));

            transform.position = new Vector3(newX, newY, offsetPosition.z);
        }
    }
}