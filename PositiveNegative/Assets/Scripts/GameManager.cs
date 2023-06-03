using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using static DataManagement;
using static Player;

public class GameManager : MonoBehaviour
{
    [Range(100, 250)] public float dimensionOffset = 100;

    [Header("Shadow Management")]
    public List<GameObject> shadows;

    [Space(10)]

    [Header("Wormhole Management")]
    public Volume negativeInstabilityMask;
    public Volume positiveInstabilityMask;

    [Space()]

    [Range(0, 1)] public float instabilityVignetteIntensity;
    private Vignette instabilityVignette;

    [Space()]

    [ColorUsage(false, true)] public Color defaultScreenColour;
    [ColorUsage(false, true)] public Color instabilityScreenColour;
    private ColorAdjustments instabilityColourAdjustments;

    [Space()]

    [Range(0, 1)] public float instabilityDistortionIntensity;
    private ChromaticAberration instabilityDistortion;
    [HideInInspector] public List<Wormhole> activeWormholes;

    [Space(10)]

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
            Wormhole currentWormhole = activeWormholes[FromBack(activeWormholes, 1)];

            switch (currentWormhole.playerScript.number)
            {
                case PlayerNumber.Player1:
                    ProgressInstabilityEffect(negativeInstabilityMask, currentWormhole); break;
                case PlayerNumber.Player2:
                    ProgressInstabilityEffect(positiveInstabilityMask, currentWormhole); break;
                default: break;
            }

            if (currentWormhole.fullInstability)
            {
                currentWormhole.TeleportBack();

                switch (currentWormhole.playerScript.number)
                {
                    case PlayerNumber.Player1:
                        DisableInstabilityEffect(negativeInstabilityMask); break;
                    case PlayerNumber.Player2:
                        DisableInstabilityEffect(positiveInstabilityMask); break;
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
                if (!exitDoors[i].PlayerHere()) allDoorsActive = false;
            }
            if (allDoorsActive) SceneManager.LoadScene(sceneToLoad);
        }
        else Debug.LogWarning("No exit doors have been set. The level is not completable");
    }

    public void ProgressInstabilityEffect(Volume volume, Wormhole currentWormhole)
    {
        if (instabilityVignette == null)
            volume.profile.TryGet(out instabilityVignette);
        if (instabilityColourAdjustments == null)
            volume.profile.TryGet(out instabilityColourAdjustments);
        if (instabilityDistortion == null)
            volume.profile.TryGet(out instabilityDistortion);

        float instability = InvertRatio(currentWormhole.stability/currentWormhole.shiftDuration);

        instabilityVignette.intensity.value = instability * instabilityVignetteIntensity;
        instabilityColourAdjustments.colorFilter.value = instabilityScreenColour;
        instabilityDistortion.intensity.value = instability * instabilityDistortionIntensity;
    }

    public void DisableInstabilityEffect(Volume volume)
    {
        if (instabilityVignette == null) 
            volume.profile.TryGet(out instabilityVignette);
        if (instabilityColourAdjustments == null)
            volume.profile.TryGet(out instabilityColourAdjustments);
        if (instabilityDistortion == null)
            volume.profile.TryGet(out instabilityDistortion);

        instabilityVignette.intensity.value = 0;
        instabilityColourAdjustments.colorFilter.value = defaultScreenColour;
        instabilityDistortion.intensity.value = 0;

        instabilityVignette = null;
        instabilityColourAdjustments = null;
        instabilityDistortion = null;
    }

    public void AddActiveWormhole(Wormhole wormhole)
    {
        activeWormholes.Add(wormhole);
    }
}