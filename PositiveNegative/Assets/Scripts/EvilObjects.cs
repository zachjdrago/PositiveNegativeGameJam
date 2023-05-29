using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EvilObjects : MonoBehaviour
{
    private Collider2D DeathCollider;

    public void Start()
    {
        DeathCollider = GetComponent<Collider2D>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        { 
            StartCoroutine(DeadState());
        }
    }
    private IEnumerator DeadState()
    {
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
