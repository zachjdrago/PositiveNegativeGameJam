using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wormhole : MonoBehaviour
{
    [Range(3, 15)] public float shiftDuration = 3;
    [Range(1, 15)] public float shiftCooldown = 1;

    public GameManager gm;

    private bool active = true;
    private SpriteRenderer spriteRenderer;
    private Sprite sprite;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        sprite = spriteRenderer.sprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (active && other.CompareTag("Player"))
        {
            StartCoroutine(TeleportPlayer(other.transform));
        }
    }

    private IEnumerator TeleportPlayer(Transform player)
    {
        int direction = (int)Mathf.Sign(player.position.x);

        Vector2 oldPosition = player.position;
        Vector2 newPosition = new(player.position.x + gm.dimensionOffset * 2 * -direction, player.position.y);

        SetEnabled(false);
        player.position = newPosition;

        yield return new WaitForSeconds(shiftDuration);

        player.position = oldPosition;

        yield return new WaitForSeconds(shiftCooldown);

        SetEnabled(true);
    }

    private void SetEnabled(bool enabled)
    {
        active = enabled;

        if (enabled) spriteRenderer.sprite = sprite;
        else spriteRenderer.sprite = null;
    }
}