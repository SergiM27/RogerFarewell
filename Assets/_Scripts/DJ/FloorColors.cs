using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorColors : MonoBehaviour
{
    public Material[] colors;
    int maxColor;
    int previousColor;

    void Start()
    {
        maxColor = colors.Length;
        InvokeRepeating("ChangeColor", 0.431654676f, 0.431654676f);
    }

    void ChangeColor()
    {
        int r = Random.Range(0, maxColor);
        while (r == previousColor)
        {
            r = Random.Range(0, maxColor);
        }
        previousColor = r;
        GetComponent<MeshRenderer>().material = colors[r];
    }
}
