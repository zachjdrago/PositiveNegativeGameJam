using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EvilObjects : MonoBehaviour
{
    public float timeToActivate;
    public float timeDeadly;

    bool containsPlayer;
    bool isActive;
    bool isDeadly;

    SpriteRenderer rend;

    private Collider2D DeathCollider;

    public void Start()
    {
        DeathCollider = GetComponent<Collider2D>();
        rend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {

        if (isDeadly && containsPlayer)
        {
            Debug.Log("dead");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (!isActive)
        {
            rend.color = Color.green;
        }
        else if (isActive && !isDeadly)
        {
            rend.color = Color.yellow;
        }
        else if (isActive && isDeadly)
        {
            rend.color = Color.red;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            containsPlayer = true;

            if (!isActive)
            {
                StartCoroutine(DeadState());
            }
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            containsPlayer = false;
        }
    }


    private IEnumerator DeadState()
    {
        isActive = true;

        yield return new WaitForSeconds(timeToActivate);

        isDeadly = true;

        yield return new WaitForSeconds(timeDeadly);

        isDeadly = false;
        isActive = false;
    }

}
