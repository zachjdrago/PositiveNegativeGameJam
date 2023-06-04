using System.Collections.Generic;
using UnityEngine;

public class ShadowManager : MonoBehaviour
{
    public GameObject shadowPrefab;
    public Transform shadowsParent;
    [Space()]
    public List<GameObject> shadowedObjects;
    public List<GameObject> shadows;
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = gameObject.GetComponent<GameManager>();

        for (int i = 0; i < shadowedObjects.Count; i++)
        {
            GameObject currentShadow = Instantiate(shadowPrefab, shadowsParent);
            shadows.Add(currentShadow);

            Shadow currentShadowScript = currentShadow.GetComponent<Shadow>();
            currentShadowScript.gameManager = gameManager;
            currentShadowScript.target = shadowedObjects[i];
        }
    }
}