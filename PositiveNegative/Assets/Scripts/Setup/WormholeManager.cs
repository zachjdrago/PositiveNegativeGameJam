using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using static DataManagement;
using static Player;

public class WormholeManager : MonoBehaviour
{
    [Space()]
    public List<Wormhole> wormholes;
    [HideInInspector] public List<Wormhole> activeWormholes;
    [HideInInspector] public GameManager gameManager;

    [Space(), Header("Masks")]
    public Volume negativeInstabilityMask;
    public Volume positiveInstabilityMask;

    [Space(), Header("Vignette")]
    [Range(0, 1)] public float instabilityVignetteIntensity;
    private Vignette instabilityVignette;

    [Space(), Header("Colour")]
    [ColorUsage(false, true)] public Color defaultScreenColour;
    [ColorUsage(false, true)] public Color instabilityScreenColour;
    private ColorAdjustments instabilityColourAdjustments;

    [Space(), Header("Distortion")]
    [Range(0, 1)] public float instabilityDistortionIntensity;
    private ChromaticAberration instabilityDistortion;

    private void Awake()
    {
        gameManager = gameObject.GetComponent<GameManager>();

        for (int i = 0; i < wormholes.Count; i++)
        {
            Wormhole currentWormhole = wormholes[i].GetComponent<Wormhole>();
            currentWormhole.gameManager = gameManager;
            currentWormhole.wormholeManager = this;
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
    }

    public void ProgressInstabilityEffect(Volume volume, Wormhole currentWormhole)
    {
        if (instabilityVignette == null)
            volume.profile.TryGet(out instabilityVignette);
        if (instabilityColourAdjustments == null)
            volume.profile.TryGet(out instabilityColourAdjustments);
        if (instabilityDistortion == null)
            volume.profile.TryGet(out instabilityDistortion);

        float instability = InvertRatio(currentWormhole.stability / currentWormhole.shiftDuration);

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