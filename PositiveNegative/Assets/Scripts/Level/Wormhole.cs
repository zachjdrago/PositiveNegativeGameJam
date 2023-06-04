using System.Collections;
using UnityEngine;

public class Wormhole : MonoBehaviour
{
    [Range(3, 15)] public float shiftDuration = 3;
    [Range(1, 15)] public float shiftCooldown = 1;

    [HideInInspector] public GameManager gameManager;
    [HideInInspector] public WormholeManager wormholeManager;

    private bool active = true;
    private bool destabilising = false;
    [HideInInspector] public float stability;
    [HideInInspector] public bool fullInstability = false;
    private SpriteRenderer spriteRenderer;

    [HideInInspector] public Transform player;
    [HideInInspector] public Player playerScript;
    private Vector2 oldPosition;
    private Vector2 newPosition;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        stability = shiftDuration;
    }

    private void Update()
    {
        if (destabilising) stability -= Time.deltaTime;

        if (stability < 0)
        {
            stability = 0;
            destabilising = false;
            fullInstability = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (active && other.CompareTag("Player"))
        {
            player = other.transform;
            playerScript = other.GetComponent<Player>();

            int direction = (int)Mathf.Sign(player.position.x);
            oldPosition = player.position;
            newPosition = new Vector2(player.position.x + gameManager.dimensionOffset * 2 * -direction, player.position.y);

            WormholeActivation();
        }
    }

    private void WormholeActivation()
    {
        WormholeActive(false);
        player.position = newPosition;
        spriteRenderer.enabled = false;
        wormholeManager.AddActiveWormhole(this);

        destabilising = true;
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

        WormholeActive(true);
    }

    private void WormholeActive(bool isActive)
    {
        if (isActive)
        {
            spriteRenderer.enabled = true;
            active = isActive;
            stability = shiftDuration;
        }
    }
}