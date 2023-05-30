using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using static CharacterController2D;

public class GameManager : MonoBehaviour
{
    [Range(100,250)] public float dimensionOffset = 100;

    [Header("Shadow Management")]
    public List<GameObject> shadows;
    [Space()]
    [Header("Wormhole Management")]
    [HideInInspector] public List<Wormhole> activeWormholes;
    public Volume negativeInstabilityMask;
    public Volume positiveInstabilityMask;
    [Space()]
    [Header("Exit Doors")]
    public string sceneToLoad;
    public List<ExitDoor> exitDoors;
    private bool allDoorsActive;

    private void Start()
    {
        for(int i = 0; i < shadows.Count; i++)
        {
            shadows[i].GetComponent<Shadow>().gm = this;
        }
    }

    private void Update()
    {
        if (activeWormholes.Count > 0)
        {
            Wormhole currentWormhole = activeWormholes[activeWormholes.Count - 1];

            switch (currentWormhole.playerScript.player)
            {
                case PlayerNumber.Player1: negativeInstabilityMask.enabled = true; break;
                case PlayerNumber.Player2: positiveInstabilityMask.enabled = true; break;
                default: break;
            }

            if (currentWormhole.fullInstability)
            {
                currentWormhole.TeleportBack();

                switch (currentWormhole.playerScript.player)
                {
                    case PlayerNumber.Player1: negativeInstabilityMask.enabled = false; break;
                    case PlayerNumber.Player2: positiveInstabilityMask.enabled = false; break;
                    default: break;
                }
                activeWormholes.Remove(currentWormhole);
            }
        }

        if (exitDoors.Count > 0)
        {
            allDoorsActive = true;
            for (int i = 0; i < exitDoors.Count; i++)
            {
                if (!exitDoors[i].playerHere) allDoorsActive = false; //Debug.Log("playerHere = " + exitDoors[i].playerHere);
            }
            if (allDoorsActive)
            {
                Debug.Log("Load Scene");
                SceneManager.LoadScene(sceneToLoad);
            }
        }
        else Debug.LogWarning("No exit doors have been set. The level is not completable");
    }

    public void AddActiveWormhole(Wormhole wormhole)
    {
        activeWormholes.Add(wormhole);
    }
}