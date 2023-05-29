using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using static CharacterController2D;

public class GameManager : MonoBehaviour
{
    [Range(100,250)] public float dimensionOffset = 100;

    [Header("Shadow Management")]
    public List<GameObject> shadows;

    [HideInInspector] public List<Wormhole> activeWormholes;
    public Volume negativeInstabilityMask;
    public Volume positiveInstabilityMask;

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
    }

    public void AddActiveWormhole(Wormhole wormhole)
    {
        activeWormholes.Add(wormhole);
    }
}