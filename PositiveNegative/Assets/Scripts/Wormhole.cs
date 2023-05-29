using System.Collections;
using UnityEngine;

public class Wormhole : MonoBehaviour
{
    [Range(3, 15)] public float shiftDuration = 3;
    [Range(1, 15)] public float shiftCooldown = 1;

    public GameManager gm;

    private bool active = true;
    [HideInInspector] public bool fullInstability;
    private SpriteRenderer spriteRenderer;
    private Sprite sprite;

    [HideInInspector] public Transform player;
    private Vector2 oldPosition;
    private Vector2 newPosition;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        sprite = spriteRenderer.sprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (active && other.CompareTag("Player"))
        {
            player = other.transform;

            int direction = (int)Mathf.Sign(player.position.x);
            oldPosition = player.position;
            newPosition = new(player.position.x + gm.dimensionOffset * 2 * -direction, player.position.y);

            StartCoroutine(WormholeActivation());
        }
    }

    private IEnumerator WormholeActivation()
    {
        WormHoleActive(false);
        player.position = newPosition;
        gm.AddActiveWormhole(this);

        yield return new WaitForSeconds(shiftDuration);

        fullInstability = true;
    }

    public void TeleportBack()
    {
        player.position = oldPosition;
        StartCoroutine(Restabalise());
    }

    private IEnumerator Restabalise()
    {
        yield return new WaitForSeconds(shiftCooldown);

        fullInstability = false;
        player = null;
        oldPosition = new();
        newPosition = new();

        WormHoleActive(true);
    }

    private void WormHoleActive(bool isActive)
    {
        active = isActive;

        if (isActive) spriteRenderer.sprite = sprite;
        else spriteRenderer.sprite = null;
    }
}