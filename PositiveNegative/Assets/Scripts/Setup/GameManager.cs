using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Range(100, 250)] public float dimensionOffset = 100;

    [Space(), Header("Exit Doors")]
    public string sceneToLoad;
    public List<ExitDoor> exitDoors;
    private bool allDoorsActive;

    private void Update()
    {
        if (exitDoors.Count > 0)
        {
            allDoorsActive = true;
            for (int i = 0; i < exitDoors.Count; i++)
            {
                if (!exitDoors[i].PlayerHere()) allDoorsActive = false;
            }
            if (allDoorsActive) SceneManager.LoadScene(sceneToLoad);
        }
        else Debug.LogWarning("No exit doors have been set. The level is not completable");
    }
}