using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    public SOLevelAssets[] Layers;
    void Start()
    {
        foreach(SOLevelAssets layer in Layers)
        {
            LevelGenrater(layer);
        }
       
    }

    private void LevelGenrater(SOLevelAssets layer)
    {
        
    }
}
