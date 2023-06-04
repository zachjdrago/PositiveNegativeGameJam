using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatablePuzzles : MonoBehaviour
{
    private Collider2D ButtonTrigger;

    public float Speed = 3;
    public Transform EndPosition;
    public GameObject Door;

    public bool IsNegative;

    private bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        ButtonTrigger = GetComponent<Collider2D>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Negative") && IsNegative)
        {
            if (!isMoving && EndPosition != null)
            {
                float distance = Vector2.Distance(Door.transform.position, EndPosition.position);

                if (distance > 0.01f)
                {
                    StartCoroutine(MoveDoorToTarget());
                }
            }
            else
            {
                return;
            }
        }

        if (other.CompareTag("Positive") && !IsNegative)
        {
            if (!isMoving && EndPosition != null)
            {
                float distance = Vector2.Distance(Door.transform.position, EndPosition.position);

                if (distance > 0.01f)
                {
                    StartCoroutine(MoveDoorToTarget());
                }
            }
            else
            {
                return;
            }
        }
    }

    private IEnumerator MoveDoorToTarget()
    {
        isMoving = true;

        Vector2 direction = (EndPosition.position - Door.transform.position).normalized;

        while (Vector2.Distance(Door.transform.position, EndPosition.position) > 0.01f)
        {
            Door.transform.position += (Vector3)direction * Speed * Time.deltaTime;
            yield return null;
        }

        isMoving = false;
    }
}
