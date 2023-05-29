using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Range(100,250)] public float dimensionOffset = 100;

    [Header("Shadow Management")]
    public List<GameObject> shadows;

    void Start()
    {
        for(int i = 0; i < shadows.Count; i++)
        {
            shadows[i].GetComponent<Shadow>().gm = this;
        }
    }

    void Update()
    {
        
    }
}