using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    public SOLevelAssets Floor;
    public SOLevelAssets[] Layers;
    void Start()
    {
        foreach(SOLevelAssets layer in Layers)
        {
            BuildFloor(Floor);
            LevelGenrater(layer);
        }
    }

    private void BuildFloor(SOLevelAssets floor)
    {
        
    }

    private void LevelGenrater(SOLevelAssets layer)
    {
        for(int i = 0; i < layer.Map.width; i++)
        {
            for (int h = 0; h < layer.Map.width; h++)
            {
                Color pixel = layer.Map.GetPixel(i,h);
                if (pixel.a == 0)
                {
                    continue;
                }
                foreach(PixelToColor n in layer.assets)
                {
                    if (n.color.Equals(pixel))
                    {
                        Vector3 postion = new Vector3(i,0,h);
                        GameObject.Instantiate(n.Prefeb,postion,Quaternion.identity,transform);
                    }
                }
            }
        }
    }

    
}
