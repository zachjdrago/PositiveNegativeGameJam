using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteDoors : MonoBehaviour
{

    public string sceneToLoad = "NewScene"; // Name of the scene to load if both doors have the player touching them

    private int doorsWithPlayerTouching = 0;

    // Called when a collider enters the trigger zone
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            Debug.Log("Collided");
            doorsWithPlayerTouching++;

            // Check if both doors have the player touching them
            if (doorsWithPlayerTouching == 2)
            {
                Debug.Log("Both doors have the player touching them. Loading scene: " + sceneToLoad);
                // Change the scene
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }

    // Called when a collider exits the trigger zone
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            doorsWithPlayerTouching--;

            // Reset the count if the player is no longer touching both doors
            if (doorsWithPlayerTouching < 2)
            {
                doorsWithPlayerTouching = 0;
            }
        }
    }
}
