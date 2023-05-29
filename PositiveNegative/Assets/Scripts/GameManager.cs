using System;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Range(100,250)] public float dimensionOffset = 100;

    [Header("Shadow Management")]
    public List<GameObject> shadows;

    [HideInInspector] public List<Wormhole> activeWormholes;

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
            if (currentWormhole.fullInstability)
            {
                currentWormhole.TeleportBack();
                activeWormholes.Remove(currentWormhole);
            }
        }
    }

    public void AddActiveWormhole(Wormhole wormhole)
    {
        activeWormholes.Add(wormhole);
    }
}