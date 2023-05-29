using UnityEngine;

public class Shadow : MonoBehaviour
{
    public GameObject target;
    private Transform targetTransform;
    private int direction;

    [HideInInspector] public GameManager gm;
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer targetSpriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        targetSpriteRenderer = target.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = targetSpriteRenderer.sprite;

        targetTransform = target.transform;
        direction = (int)Mathf.Sign(targetTransform.position.x);
    }

    void Update()
    {
        transform.position = new(targetTransform.position.x + gm.dimensionOffset * 2 * -direction, targetTransform.position.y);
        transform.localScale = targetTransform.localScale;
        transform.rotation = targetTransform.rotation;
    }
}