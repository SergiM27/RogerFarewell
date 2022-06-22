using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PubLight : MonoBehaviour
{
    Light lt;
    public Color32[] colors;
    int maxColor;
    int previousColor;

    void Start()
    {
        maxColor = colors.Length;
        lt = GetComponent<Light>();
        InvokeRepeating("ChangeColor", 0.5f,0.5f);
    }

    // Darken the light completely over a period of 2 seconds.
    void Update()
    {
  
    }


    void ChangeColor()
    {
        int r = Random.Range(0, maxColor);
        while (r == previousColor)
        {
            r = Random.Range(0, maxColor);
        }
        lt.color = colors[r];
        previousColor = r;
    }
}
