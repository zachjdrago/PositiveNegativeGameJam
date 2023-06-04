using System.Collections.Generic;
using UnityEngine;

public class ShadowManager : MonoBehaviour
{
    public GameObject shadowPrefab;
    public Transform shadowsParent;
    [Space()]
    public List<GameObject> shadowedObjects;
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = gameObject.GetComponent<GameManager>();

        for (int i = 0; i < shadowedObjects.Count; i++)
        {
            Shadow currentShadow = Instantiate(shadowPrefab, shadowsParent).GetComponent<Shadow>();

            currentShadow.gameManager = gameManager;
            currentShadow.target = shadowedObjects[i];
        }
    }
}